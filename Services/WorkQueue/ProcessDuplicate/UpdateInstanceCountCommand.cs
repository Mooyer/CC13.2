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
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Services.WorkQueue.ProcessDuplicate
{
    internal class UpdateInstanceCountCommand : ServerDatabaseCommand
    {

        #region Private Members

        private readonly StudyStorageLocation _studyLocation;
        private readonly string _seriesInstanceUid;
        private readonly string _sopInstanceUid;

        #endregion

        #region Constructors

        public UpdateInstanceCountCommand(StudyStorageLocation studyLocation, string seriesInstanceUid, string sopInstanceUid)
            :base("Update Study Count")
        {
            _studyLocation = studyLocation;
            _seriesInstanceUid = seriesInstanceUid;
            _sopInstanceUid = sopInstanceUid;
        }

        #endregion

        #region Overridden Protected Methods

        protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
        {
            var deleteInstanceBroker = updateContext.GetBroker<IDeleteInstance>();
            var parameters = new DeleteInstanceParameters
                                                      {
                                                          StudyStorageKey = _studyLocation.GetKey(),
                                                          SeriesInstanceUid = _seriesInstanceUid,
                                                          SOPInstanceUid = _sopInstanceUid
                                                      };
            if (!deleteInstanceBroker.Execute(parameters))
            {
                throw new ApplicationException("Unable to update instance count in db");
            }

        }

        #endregion

    }
}