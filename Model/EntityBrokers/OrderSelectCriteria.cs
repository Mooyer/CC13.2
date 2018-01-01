﻿#region License

// Copyright (c) 2014, MatrixPACS Inc.
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

using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model.EntityBrokers
{
	public partial class OrderSelectCriteria
	{
		/// <summary>
		/// Used for EXISTS or NOT EXISTS subselects against the Study table.
		/// </summary>
		/// <remarks>
		/// A <see cref="StudySelectCriteria"/> instance is created with the subselect parameters, 
		/// and assigned to this Sub-Criteria.  Note that the link between the <see cref="Order"/>
		/// and <see cref="Study"/> tables is automatically added into the <see cref="OrderSelectCriteria"/>
		/// instance by the broker.
		/// </remarks>
		public IRelatedEntityCondition<EntitySelectCriteria> StudyRelatedEntityCondition
		{
			get
			{
				if (!SubCriteria.ContainsKey("StudyRelatedEntityCondition"))
				{
					SubCriteria["StudyRelatedEntityCondition"] = new RelatedEntityCondition<EntitySelectCriteria>("StudyRelatedEntityCondition", "Key", "OrderKey");
				}
				return (IRelatedEntityCondition<EntitySelectCriteria>)SubCriteria["StudyRelatedEntityCondition"];
			}
		}

		/// <summary>
		/// Used for EXISTS or NOT EXISTS subselects against the Staff table from the ReferringStaffKey column.
		/// </summary>
		/// <remarks>
		/// A <see cref="StudySelectCriteria"/> instance is created with the subselect parameters, 
		/// and assigned to this Sub-Criteria.  Note that the link between the <see cref="Order"/>
		/// and <see cref="Staff"/> tables is automatically added into the <see cref="StaffSelectCriteria"/>
		/// instance by the broker.
		/// </remarks>
		public IRelatedEntityCondition<EntitySelectCriteria> ReferringStaffRelatedEntityCondition
		{
			get
			{
				if (!SubCriteria.ContainsKey("ReferringStaffRelatedEntityCondition"))
				{
					SubCriteria["ReferringStaffRelatedEntityCondition"] = new RelatedEntityCondition<EntitySelectCriteria>("ReferringStaffRelatedEntityCondition", "ReferringStaffKey", "Key");
				}
				return (IRelatedEntityCondition<EntitySelectCriteria>)SubCriteria["ReferringStaffRelatedEntityCondition"];
			}
		}

		/// <summary>
		/// Used for EXISTS or NOT EXISTS subselects against the Staff table from the EnteredByStaffKey column.
		/// </summary>
		/// <remarks>
		/// A <see cref="StudySelectCriteria"/> instance is created with the subselect parameters, 
		/// and assigned to this Sub-Criteria.  Note that the link between the <see cref="Order"/>
		/// and <see cref="Staff"/> tables is automatically added into the <see cref="StaffSelectCriteria"/>
		/// instance by the broker.
		/// </remarks>
		public IRelatedEntityCondition<EntitySelectCriteria> EnteredByStaffRelatedEntityCondition
		{
			get
			{
				if (!SubCriteria.ContainsKey("EnteredByStaffRelatedEntityCondition"))
				{
					SubCriteria["EnteredByStaffRelatedEntityCondition"] = new RelatedEntityCondition<EntitySelectCriteria>("EnteredByStaffRelatedEntityCondition", "EnteredByStaffKey", "Key");
				}
				return (IRelatedEntityCondition<EntitySelectCriteria>)SubCriteria["EnteredByStaffRelatedEntityCondition"];
			}
		}
	}
}
