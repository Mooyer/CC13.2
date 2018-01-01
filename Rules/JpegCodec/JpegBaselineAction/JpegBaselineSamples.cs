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

using MatrixPACS.Common;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Rules;

namespace MatrixPACS.ImageServer.Rules.JpegCodec.JpegBaselineAction
{
	[ExtensionOf(typeof (SampleRuleExtensionPoint))]
	public class JpegBaselineSamples : SampleRuleBase
	{
		public JpegBaselineSamples()
			: base("JpegBaseline",
			       "JPEG Baseline Time Rule",
			       ServerRuleTypeEnum.StudyCompress,
			       "SampleJpegBaseline.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyArchived);
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyRestored);
		}
	}

	[ExtensionOf(typeof(SampleRuleExtensionPoint))]
	public class JpegBaselineSampleStudyDate : SampleRuleBase
	{
		public JpegBaselineSampleStudyDate()
			: base("JpegBaselineStudyDate",
			       "JPEG Baseline, Study Date Time",
			       ServerRuleTypeEnum.StudyCompress,
			       "SampleJpegBaselineStudyDate.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyArchived);
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyRestored);
		}
	}

	[ExtensionOf(typeof(SampleRuleExtensionPoint))]
	public class JpegBaselineSopSample : SampleRuleBase
	{
		public JpegBaselineSopSample()
			: base("JpegBaselineSopRfXaUs",
				   "JPEG Baseline SOP Compression, RF XA US Compress",
				   ServerRuleTypeEnum.SopCompress,
				   "SampleJpegBaselineSop.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.SopProcessed);
		}
	}
}