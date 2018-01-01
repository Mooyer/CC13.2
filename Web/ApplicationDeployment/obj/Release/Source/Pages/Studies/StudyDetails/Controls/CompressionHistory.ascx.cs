#region License

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

using MatrixPACS.Dicom;
using MatrixPACS.ImageServer.Common.StudyHistory;
using SR = Resources.SR;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
	public partial class CompressionHistory : System.Web.UI.UserControl
	{
		public string OriginalTransferSyntax
		{
			get { return TransferSyntax.GetTransferSyntax(CompressHistory.OriginalTransferSyntaxUid).Name; }
		}

		public string FinalTransferSyntax
		{
			get { return TransferSyntax.GetTransferSyntax(CompressHistory.FinalTransferSyntaxUid).Name; }
		}

		public CompressionStudyHistory CompressHistory
		{
			get;set;
		}

		public string SummaryText
		{
			get { return string.Format(SR.StudyDetails_History_Compress, FinalTransferSyntax); }
		}
	}
}