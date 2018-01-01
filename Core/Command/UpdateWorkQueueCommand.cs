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
using MatrixPACS.Common;
using MatrixPACS.Dicom;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.WorkQueue;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Core.Command
{
    public class UpdateWorkQueueCommand : ServerDatabaseCommand
    {
        #region Private Members
        private readonly DicomMessageBase _message;
        private readonly StudyStorageLocation _storageLocation;
        private WorkQueue _insertedWorkQueue;
        private readonly bool _duplicate;
        private readonly WorkQueueData _data;
        private readonly WorkQueueUidData _uidData;
        private readonly ExternalRequestQueue _request;
	    private readonly WorkQueuePriorityEnum _priority;
        #endregion

        public UpdateWorkQueueCommand(DicomMessageBase message, StudyStorageLocation location, bool duplicate, WorkQueueData data=null, WorkQueueUidData uidData=null, ExternalRequestQueue request=null, WorkQueuePriorityEnum priority=null)
            : base("Update/Insert a WorkQueue Entry")
        {
            Platform.CheckForNullReference(message, "Dicom Message object");
            Platform.CheckForNullReference(location, "Study Storage Location");
            
            _message = message;
            _storageLocation = location;
            _duplicate = duplicate;
            _data = data;
            _request = request;
            _uidData = uidData;
	        _priority = priority;
        }

        public WorkQueue InsertedWorkQueue
        {
            get { return _insertedWorkQueue; }
        }

        protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
        {
            var insert = updateContext.GetBroker<IInsertWorkQueue>();
            var parms = new InsertWorkQueueParameters
                            {
                                WorkQueueTypeEnum = WorkQueueTypeEnum.StudyProcess,
                                StudyStorageKey = _storageLocation.GetKey(),
                                ServerPartitionKey = _storageLocation.ServerPartitionKey,
                                SeriesInstanceUid = _message.DataSet[DicomTags.SeriesInstanceUid].GetString(0, String.Empty),
                                SopInstanceUid = _message.DataSet[DicomTags.SopInstanceUid].GetString(0, String.Empty),
                                ScheduledTime = Platform.Time,
                                
                            };

	        if (_priority != null)
		        parms.WorkQueuePriorityEnum = _priority;

            if (_data != null)
            {
                parms.WorkQueueData = ImageServerSerializer.SerializeWorkQueueDataToXmlDocument(_data);
            }
            if (_request != null)
            {
                parms.ExternalRequestQueueKey = _request.Key;
            }
            if (_uidData != null)
            {
                parms.WorkQueueUidData = _uidData;
                parms.Extension = _uidData.Extension;
                parms.UidGroupID = _uidData.GroupId;
                parms.WorkQueueGroupID = _uidData.GroupId;
            }

            if (_duplicate)
            {
                parms.Duplicate = _duplicate;
            }

            _insertedWorkQueue = insert.FindOne(parms);

            if (_insertedWorkQueue == null)
                throw new ApplicationException("UpdateWorkQueueCommand failed");
        }
    }
}
