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
using System.Xml;
using MatrixPACS.Common;
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
	/// Command for inserting a FilesystemQueue record into the persistent store.
	/// </summary>
    public class InsertFilesystemQueueCommand : ServerDatabaseCommand
    {
        private readonly ServerEntityKey _filesystemKey;
        private readonly FilesystemQueueTypeEnum _queueType;
        private readonly DateTime _scheduledTime;
        private readonly ServerEntityKey _studyStorageKey;
    	private readonly XmlDocument _queueXml;

        public InsertFilesystemQueueCommand(FilesystemQueueTypeEnum queueType, ServerEntityKey filesystemKey,
                                            ServerEntityKey studyStorageKey, DateTime scheduledTime, XmlDocument queueXml)
            : base("Insert FilesystemQueue Record of type " + queueType)
        {
            _queueType = queueType;
            _filesystemKey = filesystemKey;
            _studyStorageKey = studyStorageKey;
            _scheduledTime = scheduledTime;
        	_queueXml = queueXml;
        }

        protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
        {
            FilesystemQueueInsertParameters parms = new FilesystemQueueInsertParameters();

            parms.FilesystemQueueTypeEnum = _queueType;
            parms.ScheduledTime = _scheduledTime;
            parms.StudyStorageKey = _studyStorageKey;
            parms.FilesystemKey = _filesystemKey;

			if (_queueXml != null)
				parms.QueueXml = _queueXml;

            IInsertFilesystemQueue insertQueue = updateContext.GetBroker<IInsertFilesystemQueue>();

            if (false == insertQueue.Execute(parms))
            {
                Platform.Log(LogLevel.Error, "Unexpected failure inserting FilesystemQueue entry");
                throw new PersistenceException("Unexpected failure inserting FilesystemQueue entry", null);
            }
        }
    }
}