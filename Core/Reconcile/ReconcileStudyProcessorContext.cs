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
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Core.Reconcile
{
	/// <summary>
	/// Encapsulated the context of the image reconciliation operation.
	/// </summary>
	public class ReconcileStudyProcessorContext
	{
		#region Public Properties

		/// <summary>
		/// The 'ReconcileStudy' <see cref="WorkQueue"/> item.
		/// </summary>
		public WorkQueue WorkQueueItem { get; set; }

		/// <summary>
		/// The server partition associated with <see cref="WorkQueueItem"/>
		/// </summary>
		public ServerPartition Partition { get; set; }

		/// <summary>
		/// The "decoded" queue data associated with <see cref="WorkQueueItem"/>
		/// </summary>
		public ReconcileStudyWorkQueueData ReconcileWorkQueueData { get; set; }

		/// <summary>
		/// The <see cref="StudyHistory"/> associated with the <see cref="WorkQueueItem"/>
		/// </summary>
		public StudyHistory History { get; set; }

		/// <summary>
		/// The <see cref="StudyStorageLocation"/> of the resultant study which the images will be reconciled to.
		/// </summary>
		//public StudyStorageLocation DestStorageLocation { get; set; }

		public IList<WorkQueueUid> WorkQueueUidList { get; set; }

		public StudyStorageLocation WorkQueueItemStudyStorage { get; set; }

		#endregion
	}
}