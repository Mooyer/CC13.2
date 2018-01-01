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
using System.Diagnostics;
using System.IO;
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core.Command;
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Core.Validation;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Services.WorkQueue.CleanupReconcile
{
    /// <summary>
    /// For processing 'CleanupReconcile' WorkQueue items.
    /// </summary>
    [StudyIntegrityValidation(ValidationTypes = StudyIntegrityValidationModes.None)]
    class CleanupReconcileItemProcessor : BaseItemProcessor
    {
        private ReconcileStudyWorkQueueData _reconcileQueueData;

        protected override bool CanStart()
        {
            return true;
        }

		protected override void ProcessItem(Model.WorkQueue item)
		{
			Platform.CheckForNullReference(item, "item");
			Platform.CheckForNullReference(item.Data, "item.Data");

			_reconcileQueueData = XmlUtils.Deserialize<ReconcileStudyWorkQueueData>(WorkQueueItem.Data);

			LoadUids(item);


			if (WorkQueueUidList.Count == 0)
			{
				DirectoryUtility.DeleteIfEmpty(_reconcileQueueData.StoragePath);

				Platform.Log(LogLevel.Info, "Reconcile Cleanup is completed. GUID={0}.", WorkQueueItem.GetKey());
				PostProcessing(WorkQueueItem,
					WorkQueueProcessorStatus.Complete,
					WorkQueueProcessorDatabaseUpdate.ResetQueueState);
			}
			else
			{
				Platform.Log(LogLevel.Info,
				             "Starting Cleanup of Reconcile Queue item for study {0} for Patient {1} (PatientId:{2} A#:{3}) on Partition {4}, {5} objects",
				             Study.StudyInstanceUid, Study.PatientsName, Study.PatientId,
				             Study.AccessionNumber, ServerPartition.Description,
				             WorkQueueUidList.Count);

				ProcessUidList();

				Platform.Log(LogLevel.Info, "Successfully complete Reconcile Cleanup. GUID={0}. {0} uids processed.", WorkQueueItem.GetKey(), WorkQueueUidList.Count);
				PostProcessing(WorkQueueItem,
					WorkQueueProcessorStatus.Pending,
					WorkQueueProcessorDatabaseUpdate.None);
			}
		}

        private void ProcessUidList()
        {
            Platform.CheckForNullReference(WorkQueueUidList, "WorkQueueUidList");

            foreach(WorkQueueUid uid in WorkQueueUidList)
            {
                ProcessUid(uid);
            }
        }

        private void ProcessUid(WorkQueueUid uid)
        {
            Platform.CheckForNullReference(uid, "uid");

            string imagePath = GetUidPath(uid);
            
            using (ServerCommandProcessor processor = new ServerCommandProcessor(String.Format("Deleting {0}", uid.SopInstanceUid)))
            {
                
                // If the file for some reason doesn't exist, we just ignore it
                if (File.Exists(imagePath))
                {
					Platform.Log(ServerPlatform.InstanceLogLevel, "Deleting {0}", imagePath);
                    FileDeleteCommand deleteFile = new FileDeleteCommand(imagePath, true);
                    processor.AddCommand(deleteFile);
                }
                else
                {
                    Platform.Log(LogLevel.Warn, "WARNING {0} is missing.", imagePath);
                }

                DeleteWorkQueueUidCommand deleteUid = new DeleteWorkQueueUidCommand(uid);
                processor.AddCommand(deleteUid);
                if (!processor.Execute())
                {
                    throw new Exception(String.Format("Unable to delete image {0}", uid.SopInstanceUid));
                }
            }

        }

        private string GetUidPath(WorkQueueUid sop)
        {
            string imagePath = Path.Combine(_reconcileQueueData.StoragePath, sop.SopInstanceUid + ServerPlatform.DicomFileExtension);
            Debug.Assert(String.IsNullOrEmpty(imagePath)==false);

            return imagePath;
        }
    }
}