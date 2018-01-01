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
using System.Runtime.Serialization;
using MatrixPACS.Common.Serialization;

namespace MatrixPACS.ImageServer.Common.StudyHistory
{
	/// <summary>
	/// Base class for Study History records.
	/// </summary>
	[DataContract(Namespace = ImageServerStudyHistoryNamespace.Value)]
	[ImageServerStudyHistoryType("FA1B30A1-B9C8-4C90-BF79-EEEB6BB6872E")]
	public abstract class ImageServerStudyHistory : DataContractBase
	{
	}

	[DataContract(Namespace = ImageServerStudyHistoryNamespace.Value)]
	[ImageServerStudyHistoryType("98ED279E-4D76-400C-99E1-9AEFFC81AD8E")]
	public class CompressionStudyHistory : ImageServerStudyHistory
	{
		public DateTime TimeStamp { get; set; }

		public string OriginalTransferSyntaxUid { get; set; }

		public string FinalTransferSyntaxUid { get; set; }

		public string RuleName { get; set; }
	}
}
