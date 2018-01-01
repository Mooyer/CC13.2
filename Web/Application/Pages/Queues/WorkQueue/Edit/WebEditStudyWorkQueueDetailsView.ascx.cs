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
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Queues.WorkQueue.Edit
{
    /// <summary>
    /// The defails view control for the <see cref="WorkQueue"/> inside the <see cref="WorkQueueItemDetailsPanel"/>
    /// </summary>
    public partial class WebEditStudyWorkQueueDetailsView : WorkQueueDetailsViewBase
    {
        #region Private members

        #endregion Private members

        #region Public Properties

        /// <summary>
        /// Sets or gets the width of work queue details view panel
        /// </summary>
        public override Unit Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                WebEditStudyDetailsView.Width = value;
            }
        }

        #endregion Public Properties

        #region Protected Methods

        #endregion Protected Methods

        #region Public Methods

        public override void DataBind()
        {
            if (WorkQueue != null)
            {
                var detailsList = new List<WorkQueueDetails>();
                detailsList.Add(WorkQueueDetailsAssembler.CreateWorkQueueDetail(WorkQueue));
                WebEditStudyDetailsView.DataSource = detailsList;
            }
            else
                WebEditStudyDetailsView.DataSource = null;


            base.DataBind();
        }


        protected void WebEditStudyDetailsView_DataBound(object sender, EventArgs e)
        {
            var item = WebEditStudyDetailsView.DataItem as WorkQueueDetails;
            if (item != null)
            {
            }
        }

        #endregion Public Methods
    }
}