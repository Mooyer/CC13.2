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
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Services.WorkQueue.WebDeleteStudy
{
    internal class DeleteSeriesFromDBCommand : ServerDatabaseCommand
    {
        private readonly StudyStorageLocation _location;
        private readonly Series _series;

        public DeleteSeriesFromDBCommand(StudyStorageLocation location, Series series)
            : base(String.Format("Delete Series In DB {0}", series.SeriesInstanceUid))
        {
            _location = location;
            _series = series;
        }

        public Series Series
        {
            get { return _series; }
        }


        protected override void OnExecute(CommandProcessor theProcessor, MatrixPACS.Enterprise.Core.IUpdateContext updateContext)
        {
            IDeleteSeries broker = updateContext.GetBroker<IDeleteSeries>();
            DeleteSeriesParameters criteria = new DeleteSeriesParameters();
            criteria.StudyStorageKey = _location.Key;
            criteria.SeriesInstanceUid = _series.SeriesInstanceUid;
            if (!broker.Execute(criteria))
                throw new ApplicationException("Error occurred when calling DeleteSeries");
        }
    }

    internal class DeleteSeriesFromDBCommandEventArgs:EventArgs
    {
    }
}