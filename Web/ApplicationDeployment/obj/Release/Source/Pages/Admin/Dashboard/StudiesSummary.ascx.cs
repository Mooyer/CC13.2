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
using System.Web.UI.WebControls;
using MatrixPACS.ImageServer.Common.Authentication;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Common.Data;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Dashboard
{
    public partial class StudiesSummary : System.Web.UI.UserControl
    {
        private IList<ServerPartition> _partitions;

        protected void Page_Load(object sender, EventArgs e)
        {
            var criteria = new ServerPartitionSelectCriteria();
            criteria.AeTitle.SortAsc(0);
            if (!Thread.CurrentPrincipal.IsInRole(AuthorityTokens.Vfs.ViewPartitions))
                criteria.ServerPartitionTypeEnum.EqualTo(ServerPartitionTypeEnum.Standard);

            var partitionController = new ServerPartitionConfigController();
            IList<ServerPartition> partitions = partitionController.GetPartitions(criteria);

            long totalStudies = 0;
            foreach(ServerPartition partition in partitions)
            {
                totalStudies += partition.StudyCount;
            }
            TotalStudiesLabel.Text = totalStudies.ToString("N0");

            _partitions = partitions;
            StudiesDataList.DataSource = _partitions;
            DataBind();
        }

        protected void Item_DataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ServerPartition partition = e.Item.DataItem as ServerPartition;
                LinkButton button = e.Item.FindControl("PartitionLink") as LinkButton;
                button.PostBackUrl = ImageServerConstants.PageURLs.StudiesPage + "?AETitle=" + partition.AeTitle;
            }
        }
    }
}