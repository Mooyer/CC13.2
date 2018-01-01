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
using MatrixPACS.Common.Utilities;
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Code;
using MatrixPACS.ImageServer.Web.Common.Data;
using MatrixPACS.ImageServer.Web.Common.Data.DataSource;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
    
    public partial class HistoryPanel : System.Web.UI.UserControl
    {
        private IList<StudyHistory> _historyList; 
        public StudySummary TheStudySummary;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            // load history
            LoadHistory();

            StudyHistoryGridView.DataSource = _historyList;
            base.DataBind();
        }

        private void LoadHistory()
        {
            StudyHistoryeAdaptor adaptor = new StudyHistoryeAdaptor();
            StudyHistorySelectCriteria criteria = new StudyHistorySelectCriteria();
            criteria.DestStudyStorageKey.EqualTo(TheStudySummary.TheStudyStorage.GetKey());
            criteria.InsertTime.SortDesc(0);
            _historyList = CollectionUtils.Select(adaptor.Get(criteria),
                        delegate(StudyHistory history)
                            {
                                // only include reconciliation records that result in updating the current study
                                if (history.StudyHistoryTypeEnum==StudyHistoryTypeEnum.StudyReconciled)
                                {
                                    ReconcileHistoryRecord desc = StudyHistoryRecordDecoder.ReadReconcileRecord(history);
                                    switch(desc.UpdateDescription.Action)
                                    {
                                        case StudyReconcileAction.CreateNewStudy:
                                        case StudyReconcileAction.Merge:
                                        case StudyReconcileAction.ProcessAsIs:
                                            return true;
                                    }
                                    return false;
                                }
                                return true;
                            });

        }

        protected void StudyHistoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }

        protected void StudyHistoryGridView_PageIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        protected void StudyHistoryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StudyHistory item = e.Row.DataItem as StudyHistory;
                
                StudyHistoryChangeDescPanel panel = e.Row.FindControl("StudyHistoryChangeDescPanel") as StudyHistoryChangeDescPanel;
                panel.HistoryRecord = item;
                panel.DataBind();
            }
        }
    }
}