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

namespace MatrixPACS.ImageServer.Rules
{
	/// <summary>
	/// <see cref="ArchiveQueue"/> for inserting into the <see cref="CommandBase"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Note that This stored procedure checks to see if a study delete record has been 
	/// inserted into the database, so it should be called after the rules engine has 
	/// been run & appropriate records inserted into the database.
	/// </para>
	/// <para>
	/// Note also that it can be called when we reprocess a study.
	/// </para>
	/// </remarks>
	public class InsertArchiveQueueCommand : ServerDatabaseCommand
	{
		private readonly ServerEntityKey _serverPartitionKey;
		private readonly ServerEntityKey _studyStorageKey;
		

		public InsertArchiveQueueCommand(ServerEntityKey serverPartitionKey, ServerEntityKey studyStorageKey)
			: base("Insert ArchiveQueue record")
		{
			_serverPartitionKey = serverPartitionKey;

			_studyStorageKey = studyStorageKey;

		}

		protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
		{
			// Setup the insert parameters
		    var parms = new InsertArchiveQueueParameters
		                    {
                                ServerPartitionKey = _serverPartitionKey, 
                                StudyStorageKey = _studyStorageKey
                            };

		    // Get the Insert ArchiveQueue broker and do the insert
			var insert = updateContext.GetBroker<IInsertArchiveQueue>();

			// Do the insert
            if (!insert.Execute(parms))
                throw new ApplicationException("InsertArchiveQueueCommand failed");
		}
	}
}