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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixPACS.Common;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Rules.StudyAutoRouteAction
{

	[ExtensionOf(typeof (SampleRuleExtensionPoint))]
	public class StudyAutoRouteDelaySample : SampleRuleBase
	{
		public StudyAutoRouteDelaySample()
			: base("StudyAutoRouteDelay",
			       "Delay Study AutoRoute",
			       ServerRuleTypeEnum.StudyAutoRoute,
			       "Sample_StudyAutoRouteDelay.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
		}
	}

	[ExtensionOf(typeof(SampleRuleExtensionPoint))]
	public class StudyAutoRouteQcStatus : SampleRuleBase
	{
		public StudyAutoRouteQcStatus()
			: base("StudyAutoRouteQcStatus",
				   "Study AutoRoute on QC Status",
				   ServerRuleTypeEnum.StudyAutoRoute,
				   "Sample_StudyAutoRouteQcStatus.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
		}
	}

	[ExtensionOf(typeof(SampleRuleExtensionPoint))]
	public class StudyAutoRouteSchedule : SampleRuleBase
	{
		public StudyAutoRouteSchedule()
			: base("StudyAutoRouteSchedule",
				   "Study AutoRoute on Schedule",
				   ServerRuleTypeEnum.StudyAutoRoute,
				   "Sample_StudyAutoRouteSchedule.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
		}
	}

	[ExtensionOf(typeof(SampleRuleExtensionPoint))]
	public class StudyAutoRouteSimple : SampleRuleBase
	{
		public StudyAutoRouteSimple()
			: base("StudyAutoRouteSimple",
				   "Simple Study AutoRoute",
				   ServerRuleTypeEnum.StudyAutoRoute,
				   "Sample_StudyAutoRouteSimple.xml")
		{
			ApplyTimeList.Add(ServerRuleApplyTimeEnum.StudyProcessed);
		}
	}
}
