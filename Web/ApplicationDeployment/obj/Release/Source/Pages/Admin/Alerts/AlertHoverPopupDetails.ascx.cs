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

using System.Web.UI;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Services.WorkQueue;
using MatrixPACS.ImageServer.Web.Common.Data.DataSource;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Alerts
{
    public partial class AlertHoverPopupDetails : System.Web.UI.UserControl
    {
        #region Private Members

        #endregion
        
        #region Public Properties

        public AlertSummary Alert { get; set; }

        #endregion

        public override void DataBind()
        {
            if (Alert!=null && Alert.ContextData!=null)
            {
                IAlertPopupView popupView = null;
                if (Alert.ContextData is WorkQueueAlertContextData)
                {
                    popupView = Page.LoadControl("WorkQueueAlertContextDataView.ascx") as IAlertPopupView;
                    
                }

                if (Alert.ContextData is StudyAlertContextInfo)
                {
                    popupView = Page.LoadControl("StudyAlertContextInfoView.ascx") as IAlertPopupView;
                }

                if (popupView!=null)
                {
                    popupView.SetAlert(Alert);
                    DetailsPlaceHolder.Controls.Add(popupView as UserControl);
                }                
            }
            base.DataBind();
        }
    }
}