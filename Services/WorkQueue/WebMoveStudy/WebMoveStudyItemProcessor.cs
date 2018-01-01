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
using System.IO;
using System.Linq;
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Dicom.Network.Scu;
using MatrixPACS.Dicom.Utilities.Xml;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core.Edit;
using MatrixPACS.ImageServer.Core.Helpers;
using MatrixPACS.ImageServer.Core.Validation;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Services.WorkQueue.AutoRoute;

namespace MatrixPACS.ImageServer.Services.WorkQueue.WebMoveStudy
{
	[StudyIntegrityValidation(ValidationTypes = StudyIntegrityValidationModes.None)]
	public class WebMoveStudyItemProcessor : AutoRouteItemProcessor
	{

		/// <summary>
		/// Gets the list of instances to be sent from the study xml
		/// </summary>
		/// <returns></returns>
		protected override IEnumerable<StorageInstance> GetStorageInstanceList()
		{
			IList<WorkQueueUid> seriesList = WorkQueueUidList;

			Platform.CheckForNullReference(StorageLocation, "StorageLocation");

			var list = new List<StorageInstance>();

			// We alread moved the series
			if (WorkQueueItem.Data != null && seriesList.Count == 0)
				return list;

			string studyPath = StorageLocation.GetStudyPath();
			StudyXml studyXml = LoadStudyXml(StorageLocation);
			foreach (SeriesXml seriesXml in studyXml)
			{
				var matchingSops = new List<string>();
				// FOR SERIES LEVEL Move,
				// Check if the series is in the WorkQueueUid list. If it is not in the list then don't include it.
				if (seriesList.Count > 0)
				{
					bool found = false;
					foreach (WorkQueueUid uid in seriesList)
					{
						if (!string.IsNullOrEmpty(uid.SeriesInstanceUid))
							if (uid.SeriesInstanceUid.Equals(seriesXml.SeriesInstanceUid))
							{
								found = true;
								if (!string.IsNullOrEmpty(uid.SopInstanceUid))
									matchingSops.Add(uid.SopInstanceUid);
								else
								{
									matchingSops.Clear();
									break;
								}
							}
					}
					if (!found) continue; // don't send this series
				}

				foreach (InstanceXml instanceXml in seriesXml)
				{
					if (matchingSops.Count > 0)
					{
						bool found = matchingSops.Any(uid => uid.Equals(instanceXml.SopInstanceUid));
						if (!found) continue; // don't send this sop
					}

					string seriesPath = Path.Combine(studyPath, seriesXml.SeriesInstanceUid);
					string instancePath = Path.Combine(seriesPath, instanceXml.SopInstanceUid + ServerPlatform.DicomFileExtension);
					var instance = new StorageInstance(instancePath)
						{
							SopClass = instanceXml.SopClass,
							TransferSyntax = instanceXml.TransferSyntax,
							SopInstanceUid = instanceXml.SopInstanceUid,
							StudyInstanceUid = studyXml.StudyInstanceUid,
							SeriesInstanceUid = seriesXml.SeriesInstanceUid,
							PatientId = studyXml.PatientId,
							PatientsName = studyXml.PatientsName
						};

					list.Add(instance);
				}
			}

			return list;
		}

		protected override void OnComplete()
		{
			if (WorkQueueItem.Data == null)
			{
				AddWorkQueueData();
			}

			// Force the entry to idle and stay for a while
			// Note: the assumption is the code will set ScheduledTime = ExpirationTime = some future time
			// so that the item will be removed when it is processed again.
			PostProcessing(WorkQueueItem,
			               WorkQueueProcessorStatus.CompleteDelayDelete,
			               WorkQueueProcessorDatabaseUpdate.None);

		}

		private void AddWorkQueueData()
		{
			var data = new WebMoveWorkQueueEntryData
				{
					Timestamp = DateTime.Now,
					UserId = ServerHelper.CurrentUserName
				};
			using (
				IUpdateContext update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
			{
				var broker = update.GetBroker<IWorkQueueEntityBroker>();
				var cols = new WorkQueueUpdateColumns
					{
						Data = XmlUtils.SerializeAsXmlDoc(data)
					};
				broker.Update(WorkQueueItem.Key, cols);
				update.Commit();
			}
		}

		protected override bool CanStart()
		{
			IList<Model.WorkQueue> relatedItems = FindRelatedWorkQueueItems(WorkQueueItem,
			                                                                new[]
				                                                                {
					                                                                WorkQueueTypeEnum.StudyProcess,
					                                                                WorkQueueTypeEnum.ReconcileStudy
				                                                                },
			                                                                new[]
				                                                                {
					                                                                WorkQueueStatusEnum.Idle,
					                                                                WorkQueueStatusEnum.InProgress,
					                                                                WorkQueueStatusEnum.Pending
				                                                                });

			if (relatedItems != null && relatedItems.Count > 0)
			{
				// can't do it now. Reschedule it for future
				List<Model.WorkQueue> list = CollectionUtils.Sort(relatedItems,
				                                                  (item1, item2) =>
				                                                  item1.ScheduledTime.CompareTo(item2.ScheduledTime));

				DateTime newScheduledTime = list[0].ScheduledTime.AddSeconds(WorkQueueProperties.PostponeDelaySeconds);
				if (newScheduledTime < Platform.Time.AddMinutes(1))
					newScheduledTime = Platform.Time.AddMinutes(1);

				PostponeItem(newScheduledTime, newScheduledTime.AddDays(1), "Study is being processed or reconciled.");
				Platform.Log(LogLevel.Info, "{0} postponed to {1}. Study UID={2}", WorkQueueItem.WorkQueueTypeEnum, newScheduledTime,
				             StorageLocation.StudyInstanceUid);
				return false;
			}

			return base.CanStart();
		}
	}
}
