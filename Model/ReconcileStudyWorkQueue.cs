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
using System.IO;
using System.Xml;
using MatrixPACS.Common;
using MatrixPACS.ImageServer.Enterprise.Command;

namespace MatrixPACS.ImageServer.Model
{
    public class ReconcileStudyWorkQueue : WorkQueue
    {
        private StudyStorageLocation _location;

        public ReconcileStudyWorkQueue(Model.WorkQueue workqueue)
        {
            Platform.CheckTrue(workqueue.WorkQueueTypeEnum.Equals(WorkQueueTypeEnum.ReconcileStudy),
                               String.Format("Cannot copy data from Work Queue record of type {0}",
                                             workqueue.WorkQueueTypeEnum));

            this.SetKey(workqueue.GetKey());
            this.Data= workqueue.Data;
            this.InsertTime = workqueue.InsertTime;
            this.DeviceKey = workqueue.DeviceKey;
            this.ExpirationTime = workqueue.ExpirationTime;
            this.FailureCount = workqueue.FailureCount;
            this.FailureDescription = workqueue.FailureDescription;
            this.GroupID = workqueue.GroupID;
            this.InsertTime = workqueue.InsertTime;
            this.ProcessorID = workqueue.ProcessorID;
            this.ScheduledTime = workqueue.ScheduledTime;
            this.ServerPartitionKey = workqueue.ServerPartitionKey;
            this.StudyHistoryKey = workqueue.StudyHistoryKey;
            this.StudyStorageKey = workqueue.StudyStorageKey;
            this.WorkQueuePriorityEnum = workqueue.WorkQueuePriorityEnum;
            this.WorkQueueStatusEnum = workqueue.WorkQueueStatusEnum;
            this.WorkQueueTypeEnum = this.WorkQueueTypeEnum;
        }

        public string GetFolderPath()
        {
            if (_location == null)
            {
                if (_studyStorage == null)
                {
                    using (var context = new ServerExecutionContext())
                    {
                        _studyStorage = StudyStorage.Load(context.ReadContext, this.StudyStorageKey);
                    }
                }

                _location = StudyStorageLocation.FindStorageLocations(_studyStorage)[0];

            }
            
            XmlNode nodeStoragePath = Data.SelectSingleNode("//StoragePath");
            String path = Path.Combine(_location.FilesystemPath, _location.PartitionFolder);
            path = Path.Combine(path, "Reconcile");
            path = Path.Combine(path, nodeStoragePath.InnerText);
            return path;
        }

        public string GetSopPath(string seriesUid, string instanceUid)
        {
            string path = Path.Combine(GetFolderPath(), instanceUid);
            path += "." + "dcm";
            return path;
        }
    }
}
