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

using MatrixPACS.Common.Serialization;

namespace MatrixPACS.ImageServer.Common.WorkQueue
{
	/// <summary>
	/// Enumerated values for different handling of Duplicate SOP Instances.
	/// </summary>
	public enum DuplicateProcessingEnum
	{
		/// <summary>
		/// Compares duplicate against the existing instance
		/// </summary>
		Compare,
		/// <summary>
		/// Overwrite existing instance in the filesystem and process it.
		/// </summary>
		OverwriteSop,

		/// <summary>
		/// Overwrite existing instance in the filesystem and process it.
		/// </summary>
		OverwriteSopAndUpdateDatabase,

		/// <summary>
		/// Overwrite existing report in the filesystem
		/// </summary>
		OverwriteReport,

		/// <summary>
		/// Rejects duplicate
		/// </summary>
		Reject
	}

    [WorkQueueDataType("947CA259-E3A7-409F-B51D-12D358D01B13")]
    public class WorkQueueUidData : DataContractBase
    {
        public string OperationToken { get; set; }

        public string GroupId { get; set; }

        public string RelativePath { get; set; }

        public string Extension { get; set; }

		public DuplicateProcessingEnum? DuplicateProcessing { get; set; }
    }
}
