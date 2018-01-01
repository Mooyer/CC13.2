#region License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.MatrixPACS.ca
//
// This file is part of the MatrixPACS RIS/PACS open source project.
//
// The MatrixPACS RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The MatrixPACS RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the MatrixPACS RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Core.Command
{
	/// <summary>
	/// <see cref="ServerDatabaseCommand"/> for Locking or Unlocking a Study.
	/// </summary>
	public class LockStudyCommand : ServerDatabaseCommand
	{
		private readonly QueueStudyStateEnum _queueStudyState;
		private readonly bool? _writeLock;
		private readonly bool? _readLock;
		private readonly ServerEntityKey _studyStorageKey;

		public LockStudyCommand(ServerEntityKey studyStorageKey, QueueStudyStateEnum studyState) : base("LockStudy")
		{
			_studyStorageKey = studyStorageKey;
			_queueStudyState = studyState;
		}

		public LockStudyCommand(ServerEntityKey studyStorageKey, bool writeLock, bool readLock)
			: base("LockStudy")
		{
			_studyStorageKey = studyStorageKey;
			_readLock = readLock;
			_writeLock = writeLock;
		}

		protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
		{
			ILockStudy lockStudyBroker = updateContext.GetBroker<ILockStudy>();
			LockStudyParameters lockParms = new LockStudyParameters();
			lockParms.StudyStorageKey = _studyStorageKey;
			if (_queueStudyState != null)
				lockParms.QueueStudyStateEnum = _queueStudyState;
			if (_writeLock.HasValue)
				lockParms.WriteLock = _writeLock.Value;
			if (_readLock.HasValue)
				lockParms.ReadLock = _readLock.Value;
			bool retVal = lockStudyBroker.Execute(lockParms);

			if (!retVal || !lockParms.Successful)
			{
				throw new ApplicationException(String.Format("Unable to lock the study: {0}", lockParms.FailureReason));
			}
		}
	}
}
