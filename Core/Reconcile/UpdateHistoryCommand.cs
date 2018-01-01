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

using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Core.Reconcile.CreateStudy;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;

namespace MatrixPACS.ImageServer.Core.Reconcile
{
    
	/// <summary>
	/// Command to update the study history record
	/// </summary>
	public class UpdateHistorySeriesMappingCommand : ServerDatabaseCommand
    {
        private readonly UidMapper _map;
        private readonly StudyHistory _studyHistory;
	    private readonly StudyStorageLocation _destStudy;

	    public UpdateHistorySeriesMappingCommand(StudyHistory studyHistory, StudyStorageLocation destStudy, UidMapper map) 
            : base("Update Study History Series Mapping")
        {
            _map = map;
            _studyHistory = studyHistory;
            _destStudy = destStudy;
        }

        protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
        {
            IStudyHistoryEntityBroker historyUpdateBroker = updateContext.GetBroker<IStudyHistoryEntityBroker>();
        	StudyHistoryUpdateColumns parms = new StudyHistoryUpdateColumns {DestStudyStorageKey = _destStudy.Key};

        	if (_map != null)
            {
                // replace the mapping in the history
                StudyReconcileDescriptor changeLog = XmlUtils.Deserialize<StudyReconcileDescriptor>(_studyHistory.ChangeDescription);
                changeLog.SeriesMappings = new System.Collections.Generic.List<SeriesMapping>(_map.GetSeriesMappings());
                parms.ChangeDescription = XmlUtils.SerializeAsXmlDoc(changeLog);
            }

            historyUpdateBroker.Update(_studyHistory.Key, parms);
        }
    }
}