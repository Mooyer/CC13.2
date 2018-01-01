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
using System.Web.UI;
using MatrixPACS.ImageServer.Model;
namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.SeriesDetails
{
    /// <summary>
    /// study summary information panel within the <see cref="SeriesDetailsPanel"/> 
    /// </summary>
    public partial class StudySummaryPanel : UserControl
    {
        #region private members
        private Study _study;
        #endregion private members


        #region Public Properties
        /// <summary>
        /// Gets or sets the <see cref="PatientSummary"/> object used by the panel.
        /// </summary>
        public Study Study
        {
            get { return _study; }
            set { _study = value; }
        }

        #endregion Public Properties


        #region Protected methods


        public override void DataBind()
        {
            if (_study != null)
            {
                AccessionNumber.Text = _study.AccessionNumber;
                StudyDescription.Text = _study.StudyDescription;

                StudyDate.Value = _study.StudyDate;
                
                ReferringPhysician.PersonName = _study.ReferringPhysiciansName;
            }


            base.DataBind();
        }

        #endregion Protected methods
    }
}