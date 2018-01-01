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
using System.Collections.Generic;
using System.Web;
using MatrixPACS.Common;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core;
using MatrixPACS.ImageServer.Core.Helpers;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Web.Common.Data
{
    /// <summary>
    /// Controllers for updating <see cref="StudyIntegrityQueue"/> record that has <see cref="StudyIntegrityReasonEnum"/> equal to <see cref="StudyIntegrityReasonEnum.Duplicate"/>
    /// </summary>
    public class DuplicateSopEntryController
    {
        /// <summary>
        /// Inserts work queue entry to process the duplicates.
        /// </summary>
        /// <param name="entryKey"><see cref="ServerEntityKey"/> of the <see cref="StudyIntegrityQueue"/> entry  that has <see cref="StudyIntegrityReasonEnum"/> equal to <see cref="StudyIntegrityReasonEnum.Duplicate"/> </param>
        /// <param name="action"></param>
        public void Process(ServerEntityKey entryKey, ProcessDuplicateAction action)
        {

			DuplicateSopReceivedQueue entry = DuplicateSopReceivedQueue.Load(HttpContext.Current.GetSharedPersistentContext(), entryKey);
            Platform.CheckTrue(entry.StudyIntegrityReasonEnum == StudyIntegrityReasonEnum.Duplicate, "Invalid type of entry");

            IList<StudyIntegrityQueueUid> uids = LoadDuplicateSopUid(entry);

            using(IUpdateContext context = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                ProcessDuplicateQueueEntryQueueData data = new ProcessDuplicateQueueEntryQueueData
                {
                    Action = action,
                    DuplicateSopFolder = entry.GetFolderPath(context),
                    UserName = ServerHelper.CurrentUserName,                                                              		
                }; 
                
                LockStudyParameters lockParms = new LockStudyParameters
                {
                    QueueStudyStateEnum = QueueStudyStateEnum.ReconcileScheduled,
                    StudyStorageKey = entry.StudyStorageKey
                };

                ILockStudy lockBbroker = context.GetBroker<ILockStudy>();
                lockBbroker.Execute(lockParms);
                if (!lockParms.Successful)
                {
                    throw new ApplicationException(lockParms.FailureReason);
                }

            	IWorkQueueProcessDuplicateSopBroker broker = context.GetBroker<IWorkQueueProcessDuplicateSopBroker>();
                WorkQueueProcessDuplicateSopUpdateColumns columns = new WorkQueueProcessDuplicateSopUpdateColumns
                                                                    	{
                                                                    		Data = XmlUtils.SerializeAsXmlDoc(data),
                                                                    		GroupID = entry.GroupID,
                                                                    		ScheduledTime = Platform.Time,
                                                                    		ExpirationTime =
                                                                    			Platform.Time.Add(TimeSpan.FromMinutes(15)),
                                                                    		ServerPartitionKey = entry.ServerPartitionKey,
                                                                    		WorkQueuePriorityEnum =
                                                                    			WorkQueuePriorityEnum.Medium,
                                                                    		StudyStorageKey = entry.StudyStorageKey,
                                                                    		WorkQueueStatusEnum = WorkQueueStatusEnum.Pending
                                                                    	};

            	WorkQueueProcessDuplicateSop processDuplicateWorkQueueEntry = broker.Insert(columns);

                IWorkQueueUidEntityBroker workQueueUidBroker = context.GetBroker<IWorkQueueUidEntityBroker>();
                IStudyIntegrityQueueUidEntityBroker duplicateUidBroke = context.GetBroker<IStudyIntegrityQueueUidEntityBroker>();
                foreach (StudyIntegrityQueueUid uid in uids)
                {
                    WorkQueueUidUpdateColumns uidColumns = new WorkQueueUidUpdateColumns
                                                           	{
                                                           		Duplicate = true,
                                                           		Extension = ServerPlatform.DuplicateFileExtension,
                                                           		SeriesInstanceUid = uid.SeriesInstanceUid,
                                                           		SopInstanceUid = uid.SopInstanceUid,
                                                           		RelativePath = uid.RelativePath,
                                                           		WorkQueueKey = processDuplicateWorkQueueEntry.GetKey()
                                                           	};

                	workQueueUidBroker.Insert(uidColumns);

                    duplicateUidBroke.Delete(uid.GetKey());
                }

                IDuplicateSopEntryEntityBroker duplicateEntryBroker =
                    context.GetBroker<IDuplicateSopEntryEntityBroker>();
                duplicateEntryBroker.Delete(entry.GetKey());


                context.Commit();
            }
        }

        private static IList<StudyIntegrityQueueUid> LoadDuplicateSopUid(DuplicateSopReceivedQueue entry)
        {
            IStudyIntegrityQueueUidEntityBroker broker =
				HttpContext.Current.GetSharedPersistentContext().GetBroker<IStudyIntegrityQueueUidEntityBroker>();

            StudyIntegrityQueueUidSelectCriteria criteria = new StudyIntegrityQueueUidSelectCriteria();
            criteria.StudyIntegrityQueueKey.EqualTo(entry.GetKey());
            return broker.Find(criteria);
        }
    }
}
