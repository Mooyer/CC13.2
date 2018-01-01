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
using System.Security.Permissions;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Web.Application.Pages.Common;
using AuthorityTokens=MatrixPACS.ImageServer.Common.Authentication.AuthorityTokens;
using Resources;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Queues.ArchiveQueue
{
	public partial class Default : BasePage
	{

		[PrincipalPermission(SecurityAction.Demand, Role = AuthorityTokens.ArchiveQueue.Search)]
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			SearchPanel.EnclosingPage = this;

			ServerPartitionSelector.UseNonResearchPartitions = true;
			ServerPartitionSelector.PartitionChanged += delegate(ServerPartition partition)
				{
					SearchPanel.ServerPartition = partition;
					SearchPanel.Reset();
				};

			ServerPartitionSelector.SetUpdatePanel(PageContent);

			SetPageTitle(Titles.ArchiveQueuePageTitle);
		}

		public void ResetArchiveQueueItem(IList<Model.ArchiveQueue> list)
		{
			if (list != null)
			{
				ResetArchiveQueueDialog.ArchiveQueueItemList = list;
				ResetArchiveQueueDialog.Show();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (ServerPartitionSelector.IsEmpty)
			{
				SearchPanel.Visible = false;
			}
			else
			{
				SearchPanel.ServerPartition = ServerPartitionSelector.SelectedPartition;
			}
		}
	}
}
