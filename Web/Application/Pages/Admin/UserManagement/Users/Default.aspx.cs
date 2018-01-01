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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security.Permissions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MatrixPACS.Enterprise.Common.Admin.UserAdmin;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Web.Application.Controls;
using MatrixPACS.ImageServer.Web.Application.Pages.Common;
using MatrixPACS.ImageServer.Web.Common.Data;
using MatrixPACS.ImageServer.Web.Common.Data.DataSource;
using Resources;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.UserManagement.Users
{
    [PrincipalPermission(SecurityAction.Demand, Role = MatrixPACS.Enterprise.Common.AuthorityTokens.Admin.Security.User)]
    public partial class Default : BasePage
    {
        UserManagementController _controller = new UserManagementController();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupEventHandlers();

            SetPageTitle(Titles.UserManagementPageTitle);
        }


        /// <summary>
        /// Set up the event handlers for child controls.
        /// </summary>
        protected void SetupEventHandlers() {

            AddEditUserDialog.OKClicked += delegate(UserRowData user)
                                                   {
                                                       if (AddEditUserDialog.EditMode)
                                                       {
                                                           // Commit the change into database
                                                           if (_controller.UpdateUser(user))
                                                           {
                                                               UserPanel.UpdateUI();
                                                               return true;
                                                           }
                                                           return false;
                                                       }
                                                       else
                                                       {
                                                           try
                                                           {
                                                               if (_controller.AddUser(user))
                                                               {
                                                                   UserPanel.UpdateUI();
                                                                   return true;
                                                               }
                                                               return false;
                                                           }
														   catch(Exception)
                                                           {
                                                               return false;
                                                           }
                                                       }
                                                   };


            DeleteConfirmation.Confirmed += delegate(object data)
                                            {
                                                // delete the device and reload the affected partition.

                                                UserRowData user = data as UserRowData;
                                                _controller.DeleteUser(user);
                                                UserPanel.UpdateUI();
                                            };

 
        }

        public void OnAddUser()
        {
            AddEditUserDialog.EditMode = false;
            AddEditUserDialog.Show(true);
        }

        public void OnEditUser(UserRowData userRowData)
        {
            AddEditUserDialog.EditMode = true;
            AddEditUserDialog.User = userRowData;
            AddEditUserDialog.Show(true);
        }

        public void OnDeleteUser(UserRowData userRowData)
        {
            DeleteConfirmation.Message = string.Format(SR.AdminUser_DeleteDialog_AreYouSure, userRowData.DisplayName, userRowData.UserName);
            DeleteConfirmation.MessageType = MessageBox.MessageTypeEnum.YESNO;
            DeleteConfirmation.Data = userRowData;
            DeleteConfirmation.Show();
        }

        public void OnResetPassword(UserRowData userRowData)
        {
            if (_controller.ResetPassword(userRowData))
            {
                PasswordResetConfirmation.Message = string.Format(SR.AdminUser_PasswordReset, userRowData.UserName);
            } else {
                PasswordResetConfirmation.Message = ErrorMessages.PasswordResetFailed;
                }
            PasswordResetConfirmation.Title = Titles.AdminUser_PasswordResetDialogTitle;
            PasswordResetConfirmation.MessageType = MessageBox.MessageTypeEnum.INFORMATION;
            PasswordResetConfirmation.Show();
        }
    }
}
