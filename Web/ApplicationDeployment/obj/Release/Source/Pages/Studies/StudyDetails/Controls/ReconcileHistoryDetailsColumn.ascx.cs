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
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Core.Reconcile;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Web.Common.Utilities;
using Resources;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
    public partial class ReconcileHistoryDetailsColumn : System.Web.UI.UserControl
    {
        private StudyHistory _historyRecord;
        private StudyReconcileDescriptor _description;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public StudyHistory HistoryRecord
        {
            set { _historyRecord = value; }
        }

        public StudyReconcileDescriptor ReconcileHistory
        {
            get
            {
                if (_description == null && _historyRecord!=null)
                {
                    StudyReconcileDescriptorParser parser = new StudyReconcileDescriptorParser();
                    _description =parser.Parse(_historyRecord.ChangeDescription);
                }
                return _description;
            }
        }

        
        protected string ChangeSummaryText
        {
            get{
                return ActionTranslator.Translate(ReconcileHistory.Action);
            }
        }

        protected string PerformedBy
        {
            get
            {
                return String.Format(SR.StudyDetails_History_Reconcile_PerformedBy, ReconcileHistory.UserName ?? SR.Unknown);
            }
        }
    }


    public static class ActionTranslator
    {
        public static string Translate(StudyReconcileAction action)
        {
            switch (action)
            {
                case StudyReconcileAction.CreateNewStudy:
                    return HtmlUtility.Encode(SR.StudyDetails_Reconcile_NewStudy_Description);
                case StudyReconcileAction.Discard:
                    return HtmlUtility.Encode(SR.StudyDetails_Reconcile_Discard_Description);
                case StudyReconcileAction.Merge:
                    return HtmlUtility.Encode(SR.StudyDetails_Reconcile_Merge_Description);
                case StudyReconcileAction.ProcessAsIs:
                    return HtmlUtility.Encode(SR.StudyDetails_Reconcile_ProcessAsIs_Description);
            }

            return HtmlUtility.Encode(HtmlUtility.GetEnumInfo(action).LongDescription);
        }
    }
}