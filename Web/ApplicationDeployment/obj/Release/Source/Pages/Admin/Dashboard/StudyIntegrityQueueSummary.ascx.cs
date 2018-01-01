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
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Common.Data;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Dashboard
{
    public partial class StudyIntegrityQueueSummary : System.Web.UI.UserControl
    {
        private int _duplicateCount;
        private int _inconsistentDataCount;
        
        public int Duplicates
        {
            get { return _duplicateCount; }
            set { _duplicateCount = value; }
        }

        public int InconsistentData
        {
            get { return _inconsistentDataCount; }
            set { _inconsistentDataCount = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StudyIntegrityQueueController controller = new StudyIntegrityQueueController();
            StudyIntegrityQueueSelectCriteria criteria = new StudyIntegrityQueueSelectCriteria();

            criteria.StudyIntegrityReasonEnum.EqualTo(StudyIntegrityReasonEnum.Duplicate);
            Duplicates = controller.GetReconcileQueueItemsCount(criteria);
            DuplicateLink.PostBackUrl = ImageServerConstants.PageURLs.StudyIntegrityQueuePage + "?Databind=true&Reason=" + StudyIntegrityReasonEnum.Duplicate.Lookup;

            criteria.StudyIntegrityReasonEnum.EqualTo(StudyIntegrityReasonEnum.InconsistentData);
            InconsistentData = controller.GetReconcileQueueItemsCount(criteria);
            InconsistentDataLink.PostBackUrl = ImageServerConstants.PageURLs.StudyIntegrityQueuePage + "?Databind=true&Reason=" + StudyIntegrityReasonEnum.InconsistentData.Lookup ;

            TotalLinkButton.PostBackUrl = ImageServerConstants.PageURLs.StudyIntegrityQueuePage + "?Databind=true";


        }
    }
}