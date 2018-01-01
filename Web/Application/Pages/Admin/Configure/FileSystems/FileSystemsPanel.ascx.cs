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
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using MatrixPACS.ImageServer.Core.Query;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Application.Helpers;
using MatrixPACS.ImageServer.Web.Common.Data;
using MatrixPACS.ImageServer.Web.Common.WebControls.UI;
using Resources;

[assembly: WebResource("MatrixPACS.ImageServer.Web.Application.Pages.Admin.Configure.FileSystems.FileSystemsPanel.js", "application/x-javascript")]

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Configure.FileSystems
{
    [ClientScriptResource(ComponentType = "MatrixPACS.ImageServer.Web.Application.Pages.Admin.Configure.FileSystems.FileSystemsPanel", ResourcePath = "MatrixPACS.ImageServer.Web.Application.Pages.Admin.Configure.FileSystems.FileSystemsPanel.js")]
    /// <summary>
    /// Panel to display list of FileSystems for a particular server partition.
    /// </summary>
    public partial class FileSystemsPanel : AJAXScriptControl
    {
        #region Private members

        // the controller used for interaction with the database.
        private FileSystemsConfigurationController _theController;
        // the filesystems whose information will be displayed in this panel
        private IList<Filesystem> _filesystems;
        // list of filesystem tiers users can filter on
        private IList<FilesystemTierEnum> _tiers;

        #endregion Private members

        #region Public Properties

        [ExtenderControlProperty]
        [ClientPropertyName("EditButtonClientID")]
        public string EditButtonClientID
        {
            get { return EditFileSystemButton.ClientID; }
        }

        [ExtenderControlProperty]
        [ClientPropertyName("FileSystemListClientID")]
        public string FileSystemListClientID
        {
            get { return FileSystemsGridView1.TheGrid.ClientID; }
        }

        /// <summary>
        /// Sets/Gets the filesystems whose information are displayed in this panel.
        /// </summary>
        public IList<Filesystem> FileSystems
        {
            get { return _filesystems; }
            set { _filesystems = value; }
        }

        /// <summary>
        /// Sets or gets the list of filesystems users can filter.
        /// </summary>
        public IList<FilesystemTierEnum> Tiers
        {
            get { return _tiers; }
            set { _tiers = value; }
        }

        private Default _enclosingPage;

        public Default EnclosingPage
        {
            get { return _enclosingPage; }
            set { _enclosingPage = value; }
        }

        #endregion

        #region protected methods

        protected override void OnPreRender(EventArgs e)
        {
            UpdateUI();
            base.OnPreRender(e);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // initialize the controller
            _theController = new FileSystemsConfigurationController();

            // setup child controls
            GridPagerTop.InitializeGridPager(SR.GridPagerFileSystemSingleItem, SR.GridPagerFileSystemMultipleItems, FileSystemsGridView1.TheGrid, delegate { return FileSystemsGridView1.FileSystems.Count; }, ImageServerConstants.GridViewPagerPosition.Top);
            FileSystemsGridView1.Pager = GridPagerTop;
            GridPagerTop.Reset();
                
            Tiers = _theController.GetFileSystemTiers();

            int prevSelectIndex = TiersDropDownList.SelectedIndex;
            if (TiersDropDownList.Items.Count == 0)
            {
                TiersDropDownList.Items.Add(new ListItem(SR.All));
                foreach (FilesystemTierEnum tier in Tiers)
                {
                    TiersDropDownList.Items.Add(new ListItem(ServerEnumDescription.GetLocalizedDescription(tier), tier.Lookup));
                }
            }
            TiersDropDownList.SelectedIndex = prevSelectIndex;
        }

        #endregion Protected methods

        /// <summary>
        /// Load the FileSystems for the partition based on the filters specified in the filter panel.
        /// </summary>
        /// <remarks>
        /// This method only reloads and binds the list bind to the internal grid. <seealso cref="UpdateUI()"/> should be called
        /// to explicit update the list in the grid. 
        /// <para>
        /// This is intentionally so that the list can be reloaded so that it is available to other controls during postback.  In
        /// some cases we may not want to refresh the list if there's no change. Calling <seealso cref="UpdateUI()"/> will
        /// give performance hit as the data will be transfered back to the browser.
        ///  
        /// </para>
        /// </remarks>
        public void LoadFileSystems()
        {
            FilesystemSelectCriteria criteria = new FilesystemSelectCriteria();


            if (String.IsNullOrEmpty(DescriptionFilter.TrimText) == false)
            {
                QueryHelper.SetGuiStringCondition(criteria.Description,
                                      SearchHelper.TrailingWildCard(DescriptionFilter.TrimText));
            }

            if (TiersDropDownList.SelectedIndex >= 1) /* 0 = "All" */
                criteria.FilesystemTierEnum.EqualTo(Tiers[TiersDropDownList.SelectedIndex - 1]);

            FileSystemsGridView1.FileSystems = _theController.GetFileSystems(criteria);
            FileSystemsGridView1.RefreshCurrentPage();
        }

        /// <summary>
        /// Updates the FileSystem list window in the panel.
        /// </summary>
        /// <remarks>
        /// This method should only be called when necessary as the information in the list window needs to be transmitted back to the client.
        /// If the list is not changed, call <seealso cref="LoadFileSystems()"/> instead.
        /// </remarks>
        public void UpdateUI()
        {
            LoadFileSystems();

            Filesystem dev = FileSystemsGridView1.SelectedFileSystem;
            if (dev == null)
            {
                // no FileSystem being selected
                EditFileSystemButton.Enabled = false;
            }
            else
            {
                EditFileSystemButton.Enabled = true;
            }

            SearchUpdatePanel.Update();
        }

        protected void SearchButton_Click(object sender, ImageClickEventArgs e)
        {
            //UpdateUI();
        }


        protected void AddFileSystemButton_Click(object sender, ImageClickEventArgs e)
        {
            EnclosingPage.OnAddFileSystem();
        }

        protected void EditFileSystemButton_Click(object sender, ImageClickEventArgs e)
        {
            // Call the edit filesystem delegate 
            LoadFileSystems();
            Filesystem fs = FileSystemsGridView1.SelectedFileSystem;
            if (fs != null)
            {
                EnclosingPage.OnEditFileSystem(_theController, fs);
            }
        }

        protected void RefreshButton_Click(object sender, ImageClickEventArgs e)
        {
            //UpdateUI();
        }
    }
}