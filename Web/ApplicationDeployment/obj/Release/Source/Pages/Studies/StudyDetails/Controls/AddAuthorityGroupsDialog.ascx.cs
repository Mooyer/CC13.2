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
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Enterprise.Common.Admin.AuthorityGroupAdmin;
using MatrixPACS.ImageServer.Common.Authentication;
using MatrixPACS.ImageServer.Web.Common.Data;
using MatrixPACS.ImageServer.Web.Common.Data.DataSource;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
    public partial class AddAuthorityGroupsDialog : UserControl
    {
        public IList<StudySummary> AuthorityGroupStudies
        {
            get
            {
                return ViewState["AuthorityGroupStudies"] as IList<StudySummary>;
            }
            set { ViewState["AuthorityGroupStudies"] = value; }
        }
        
        public override void DataBind()
        {
            StudyListing.DataSource = AuthorityGroupStudies;

            if (Thread.CurrentPrincipal.IsInRole(AuthorityTokens.Study.EditDataAccess))
            {
                if (AuthorityGroupStudies != null)
                {
                    AuthorityGroupCheckBoxList.Items.Clear();

                    var study = CollectionUtils.FirstElement(AuthorityGroupStudies);
                    var adapter = new ServerPartitionDataAdapter();
                    IList<AuthorityGroupDetail> accessAllStudiesList;
                    IList<AuthorityGroupDetail> groups = adapter.GetAuthorityGroupsForPartition(study.ThePartition.Key, true, out accessAllStudiesList);


                    IList<ListItem> items = CollectionUtils.Map(
                        accessAllStudiesList,
                        delegate(AuthorityGroupDetail group)
                            {

                                var item = new ListItem(@group.Name,
                                                        @group.AuthorityGroupRef.ToString(false, false))
                                               {
                                                   Enabled = false,
                                                   Selected = true
                                               };
                                item.Attributes["title"] = @group.Description;
                                return item;
                            });

                    foreach (var group in groups)
                    {
                        var item = new ListItem(@group.Name,
                                                @group.AuthorityGroupRef.ToString(false, false))
                                       {
                                           Selected = false
                                       };
                        item.Attributes["title"] = @group.Description;
                        items.Add(item);
                    }

                    AuthorityGroupCheckBoxList.Items.AddRange(CollectionUtils.ToArray(items));
                }
            }

            base.DataBind();
        }       

        protected void AddButton_Clicked(object sender, ImageClickEventArgs e)
        {

            if (Page.IsValid)
            {
                try
                {
                    var assignedGroups = new List<string>();
                    foreach (ListItem item in AuthorityGroupCheckBoxList.Items)
                    {
                        if (item.Selected && item.Enabled)
                            assignedGroups.Add(item.Value);
                        item.Selected = false;
                    }

                    foreach (StudySummary study in AuthorityGroupStudies)
                    {
                        try
                        {
                            StudyDataAccessController controller = new StudyDataAccessController();                            
                            controller.AddStudyAuthorityGroups(study.StudyInstanceUid, study.AccessionNumber, study.TheStudyStorage.Key, assignedGroups);
                        }
                        catch (Exception ex)
                        {
                            Platform.Log(LogLevel.Error, ex, "AddClicked failed: Unable to add authority groups to studies");
                            throw;
                        }
                    }           
                }
                finally
                {
                    Close();
                }
            }
            else
            {
                EnsureDialogVisible();
            }
        }

        protected void CancelButton_Clicked(object sender, ImageClickEventArgs e)
        {
            Close();
        }

        internal void EnsureDialogVisible()
        {
            ModalDialog.Show();
        }

        public void Close()
        {
            ModalDialog.Hide();
        }

        public void Initialize(IList<StudySummary> list)
        {
            AuthorityGroupStudies = list;
        }

        internal void Show()
        {
            DataBind();
            EnsureDialogVisible();
        }
    }
}