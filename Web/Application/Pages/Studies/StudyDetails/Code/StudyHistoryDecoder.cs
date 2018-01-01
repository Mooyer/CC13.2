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
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core.Edit;
using MatrixPACS.ImageServer.Core.Reconcile;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Code
{
    /// <summary>
    /// Provides methods to decode the information in a <see cref="StudyHistory"/> record.
    /// </summary>
    static class StudyHistoryRecordDecoder
    {
        public static ReconcileHistoryRecord ReadReconcileRecord(StudyHistory historyRecord)
        {
            Platform.CheckTrue(historyRecord.StudyHistoryTypeEnum == StudyHistoryTypeEnum.StudyReconciled,
                               "History record has invalid history record type");

            ReconcileHistoryRecord record = new ReconcileHistoryRecord();
            record.InsertTime = historyRecord.InsertTime;
            record.StudyStorageLocation = StudyStorageLocation.FindStorageLocations(StudyStorage.Load(historyRecord.StudyStorageKey))[0];
            StudyReconcileDescriptorParser parser = new StudyReconcileDescriptorParser();
            record.UpdateDescription = parser.Parse(historyRecord.ChangeDescription);
            return record;
        }

        public static WebEditStudyHistoryRecord ReadEditRecord(StudyHistory historyRecord)
        {
            Platform.CheckTrue(historyRecord.StudyHistoryTypeEnum == StudyHistoryTypeEnum.WebEdited
                               || historyRecord.StudyHistoryTypeEnum == StudyHistoryTypeEnum.ExternalEdit,
                               "History record has invalid history record type");

            WebEditStudyHistoryRecord record = new WebEditStudyHistoryRecord
                                                   {
                                                       InsertTime = historyRecord.InsertTime,
                                                       StudyStorageLocation =
                                                           StudyStorageLocation.FindStorageLocations(
                                                               StudyStorage.Load(historyRecord.StudyStorageKey))[0],
                                                       UpdateDescription =
                                                           XmlUtils.Deserialize<WebEditStudyHistoryChangeDescription>(
                                                               historyRecord.ChangeDescription)
                                                   };
            return record;
        }
    }
    
    internal class StudyHistoryRecordBase
    {
        public DateTime InsertTime;
        public StudyStorageLocation StudyStorageLocation;
    }
}
