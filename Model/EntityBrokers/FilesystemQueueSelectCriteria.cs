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

using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model.EntityBrokers
{
    public partial class  FilesystemQueueSelectCriteria
    {
        /// <summary>
        /// Used for EXISTS or NOT EXISTS subselects against the FilesystemQueue table.
        /// </summary>
        /// <remarks>
        /// A <see cref="WorkQueueSelectCriteria"/> instance is created with the subselect parameters, 
        /// and assigned to this Sub-Criteria.  Note that the link between the <see cref="FilesystemQueue"/>
        /// and <see cref="WorkQueue"/> tables is automatically added into the <see cref="WorkQueueSelectCriteria"/>
        /// instance by the broker.
        /// </remarks>
		public IRelatedEntityCondition<EntitySelectCriteria> WorkQueue
        {
            get
            {
                if (!SubCriteria.ContainsKey("WorkQueueCondition"))
                {
                    SubCriteria["WorkQueueCondition"] = new RelatedEntityCondition<EntitySelectCriteria>("WorkQueueCondition", "StudyStorageKey", "StudyStorageKey");
                }
				return (IRelatedEntityCondition<EntitySelectCriteria>)SubCriteria["WorkQueueCondition"];
            }
        }

		/// <summary>
		/// Used for EXISTS or NOT EXISTS subselects against the FilesystemQueue table
		/// </summary>
		public IRelatedEntityCondition<EntitySelectCriteria> StudyStorage
		{
			get
			{
				if (!SubCriteria.ContainsKey("StudyStorageCondition"))
				{
					SubCriteria["StudyStorageCondition"] = new RelatedEntityCondition<EntitySelectCriteria>("WorkQueueCondition", "StudyStorageKey", "StudyStorageKey");
				}
				return (IRelatedEntityCondition<EntitySelectCriteria>)SubCriteria["StudyStorageCondition"];
			}
		}

    }
}
