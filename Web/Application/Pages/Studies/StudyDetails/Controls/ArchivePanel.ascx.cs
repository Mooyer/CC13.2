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
using System.Web.UI.WebControls;
using MatrixPACS.Dicom;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Common.Data;
using MatrixPACS.ImageServer.Web.Common.Utilities;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
    /// <summary>
    /// Study level detailed information panel within the <see cref="StudyDetailsPanel"/>
    /// </summary>
    public partial class ArchivePanel : System.Web.UI.UserControl
    {
        #region Private members

        private Unit _width;
        private Study _study;
        private IList<ArchiveStudyStorage> _storage;

        #endregion Private members
        
        #region Public Properties

        /// <summary>
        /// Sets or gets the Study
        /// </summary>
        public Study Study
        {
            get { return _study; }
            set { _study = value; }
        }

        public Unit Width
        {
            get { return _width; }
            set { _width = value; }
        }

        #endregion Public Properties

        public override void DataBind()
        {
            StudyController studyController = new StudyController();
            ArchiveQueueGridView.DataSource = studyController.GetArchiveQueueItems(_study);
            _storage = studyController.GetArchiveStudyStorage(_study);
            ArchiveStudyStorageGridView.DataSource = _storage;
            base.DataBind();
        }

        protected void ArchiveQueueGridView_PageIndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void ArchiveQueueGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ArchiveQueueGridView.PageIndex = e.NewPageIndex;
            DataBind();
        }

        protected void ArchiveStudyStorageGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   int index = ArchiveStudyStorageGridView.PageIndex * ArchiveStudyStorageGridView.PageSize + e.Row.RowIndex;
                    ArchiveStudyStorage storage = _storage[index];

                    Label xmlLabel = e.Row.FindControl("XmlText") as Label;
                    if (xmlLabel != null && storage.ArchiveXml != null)
                    {
                        xmlLabel.Text = XmlUtils.GetXmlDocumentAsString(storage.ArchiveXml, true);    
                    }

                    Label stsLabel = e.Row.FindControl("ServerTranseferSyntax") as Label;
                    if (stsLabel != null && storage.ServerTransferSyntaxKey != null)
                    {
                        ServerTransferSyntaxAdaptor adaptor = new ServerTransferSyntaxAdaptor();
                        ServerTransferSyntax sts = adaptor.Get(storage.ServerTransferSyntaxKey);

                        if (sts != null)
                        {
                            stsLabel.Text = sts.Description;
                        }
                    }
                }
            }
        }       
}