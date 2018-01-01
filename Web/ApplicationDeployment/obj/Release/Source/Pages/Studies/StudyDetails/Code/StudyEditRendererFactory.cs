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

using System.Text;
using System.Web.UI;
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Core.Edit;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Services.WorkQueue.WebEditStudy;
using MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Code
{
    /// <summary>
    /// Helper class used in rendering the information encoded of a "WebEdit"
    /// StudyHistory record.
    /// </summary>
    internal class StudyEditRendererFactory : IStudyHistoryColumnControlFactory
    {
        public Control GetChangeDescColumnControl(Control parent, StudyHistory historyRecord)
        {
            EditHistoryDetailsColumn control = parent.Page.LoadControl("~/Pages/Studies/StudyDetails/Controls/EditHistoryDetailsColumn.ascx") as EditHistoryDetailsColumn;
            control.HistoryRecord = historyRecord;
            return control;
        }
    }

    /// <summary>
    /// Helper class to decode the information of a "WebEdit" study history record.
    /// </summary>
    class WebEditStudyHistoryRecord : StudyHistoryRecordBase
    {
        #region Private Fields
        private StudyInformation _originalStudy;
        private WebEditStudyHistoryChangeDescription _updateDescription;

        #endregion

        #region Public Properties

        public StudyInformation OriginalStudyData
        {
            get { return _originalStudy; }
            set { _originalStudy = value; }
        }

        public WebEditStudyHistoryChangeDescription UpdateDescription
        {
            get { return _updateDescription; }
            set { _updateDescription = value; }
        }
        #endregion

        #region Constructors

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Trigger: {0}", UpdateDescription.EditType == EditType.WebEdit ? "Manual (Web UI)" : "External Edit");
            sb.AppendLine();
            sb.AppendFormat("Updates:");
            sb.AppendLine();
            foreach (BaseImageLevelUpdateCommand cmd in UpdateDescription.UpdateCommands)
            {
                sb.AppendFormat("{0}", cmd);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        #region Public Static Methods

        /// <summary>
        /// Creates an instance of <see cref="WebEditStudyHistoryRecord"/>
        /// from a <see cref="StudyHistory"/>
        /// </summary>
        /// <param name="historyRecord"></param>
        /// <returns></returns>
        #endregion
    }

}