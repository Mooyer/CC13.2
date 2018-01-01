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
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Dicom;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Core;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Rules;

namespace MatrixPACS.ImageServer.Services.ServiceLock.PartitionReapplyRules
{
	class PartitionReapplyRulesItemProcessor : BaseReapplyRulesServiceLockItemProcessor, IServiceLockItemProcessor, ICancelable
	{
		protected override void OnProcess(Model.ServiceLock item)
		{
			var partition = ServerPartition.Load(item.ServerPartitionKey);
			ReprocessPartition(partition);

			//Platform.Log(LogLevel.Info, "Completed reprocess of filesystem: {0}", info.Filesystem.Description);

			//_stats.StudyRate.SetData(_stats.NumStudies);
			//_stats.StudyRate.End();

			//StatisticsLogger.Log(LogLevel.Info, _stats);

			item.ScheduledTime = item.ScheduledTime.AddDays(1);

			if (CancelPending)
			{
				Platform.Log(LogLevel.Info,
							 "Partition Reprocess of {0} has been canceled, rescheduling.  Entire partition will be reprocessed.",
							 partition.Name);
				UnlockServiceLock(item, true, Platform.Time.AddMinutes(1));
			}
			else
				UnlockServiceLock(item, false, Platform.Time.AddDays(1));
		}


		/// <summary>
		/// Reprocess a file systems
		/// </summary>
		/// <param name="partition"></param>
		private void ReprocessPartition(ServerPartition partition)
		{
			//TODO CR (Jan 2014) - Cached engine not being used now, due to #11673
			var engine = new ServerRulesEngine(ServerRuleApplyTimeEnum.StudyProcessed, partition.Key);
			engine.Load();

			var postArchivalEngine = new ServerRulesEngine(ServerRuleApplyTimeEnum.StudyArchived, partition.Key);
			postArchivalEngine.Load();

			var dataAccessEngine = new ServerRulesEngine(ServerRuleApplyTimeEnum.StudyProcessed, partition.Key);
			dataAccessEngine.AddIncludeType(ServerRuleTypeEnum.DataAccess);
			dataAccessEngine.Load();

			var filesystems = FilesystemMonitor.Instance.GetFilesystems();

			foreach (var f in filesystems)
			{
				var partitionDir = Path.Combine(f.Filesystem.FilesystemPath, partition.PartitionFolder);
				var filesystemDir = new DirectoryInfo(partitionDir);
				foreach (DirectoryInfo dateDir in filesystemDir.GetDirectories())
				{
					if (dateDir.FullName.EndsWith("Deleted")
						|| dateDir.FullName.EndsWith(ServerPlatform.ReconcileStorageFolder))
						continue;

					foreach (DirectoryInfo studyDir in dateDir.GetDirectories())
					{
						String studyInstanceUid = studyDir.Name;
						try
						{
							StudyStorageLocation location = LoadReadableStorageLocation(partition.GetKey(), studyInstanceUid);
							if (location == null)
							{
								foreach (DirectoryInfo seriesDir in studyDir.GetDirectories())
								{
									FileInfo[] sopInstanceFiles = seriesDir.GetFiles("*.dcm");

									DicomFile file = null;
									foreach (FileInfo sopFile in sopInstanceFiles)
									{
										if (!sopFile.FullName.EndsWith(ServerPlatform.DicomFileExtension))
											continue;

										try
										{
											file = new DicomFile(sopFile.FullName);
											file.Load(DicomTags.StudyId, DicomReadOptions.DoNotStorePixelDataInDataSet | DicomReadOptions.Default);
											break;
										}
										catch (Exception e)
										{
											Platform.Log(LogLevel.Warn, e, "Unexpected failure loading file: {0}.  Continuing to next file.",
														 sopFile.FullName);
											file = null;
										}
									}
									if (file != null)
									{
										studyInstanceUid = file.DataSet[DicomTags.StudyInstanceUid].ToString();
										break;
									}
								}

								location = LoadReadableStorageLocation(partition.GetKey(), studyInstanceUid);
								if (location == null)
									continue;
							}

							ProcessStudy(partition, location, engine, postArchivalEngine, dataAccessEngine);
							//_stats.NumStudies++;

							if (CancelPending) return;
						}
						catch (Exception e)
						{
							Platform.Log(LogLevel.Error, e,
										 "Unexpected error while processing study: {0} on partition {1}.", studyInstanceUid,
										 partition.Description);
						}
					}

					// Cleanup the directory, if its empty.
					DirectoryUtility.DeleteIfEmpty(dateDir.FullName);
				}
			}
		}
	}
}
