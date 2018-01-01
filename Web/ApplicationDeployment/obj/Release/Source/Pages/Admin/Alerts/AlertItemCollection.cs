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

using System.Collections.Generic;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Web.Common;
using MatrixPACS.ImageServer.Web.Common.Data.DataSource;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Alerts
{
	/// <summary>
	/// Encapsulates a collection of <see cref="Alert"/> which can be accessed based on the <see cref="ServerEntityKey"/>
	/// </summary>
	public class AlertItemCollection : KeyedCollectionBase<AlertSummary, ServerEntityKey>
	{

		public AlertItemCollection(IList<AlertSummary> list)
			: base(list)
		{
		}

		protected override ServerEntityKey GetKey(AlertSummary item)
		{
			return item.Key;
		}
	}
}
