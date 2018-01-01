#region License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.matriximage.ca
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
using System.Xml;
using System.Xml.Schema;
using MatrixPACS.Common;
using MatrixPACS.Common.Actions;
using MatrixPACS.Common.Specifications;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Rules;

namespace MatrixPACS.ImageServer.Rules.Jpeg2000Codec.Jpeg2000LosslessAction
{
	[ExtensionOf(typeof(XmlActionCompilerOperatorExtensionPoint<ServerActionContext, ServerRuleTypeEnum>))]
	public class Jpeg2000LosslessActionOperator : ActionOperatorCompilerBase, IXmlActionCompilerOperator<ServerActionContext, ServerRuleTypeEnum>
	{
		public Jpeg2000LosslessActionOperator()
			: base("jpeg-2000-lossless")
		{
		}

		public IActionItem<ServerActionContext> Compile(XmlElement xmlNode)
		{
			if (xmlNode.Attributes["time"] == null)
				throw new XmlActionCompilerException(
					"Unexpected missing time attribute for jpeg-2000-lossless scheduling action");
			if (xmlNode.Attributes["unit"] == null)
				throw new XmlActionCompilerException(
					"Unexpected missing unit attribute for jpeg-2000-lossless scheduling action");

			int time;
			if (false == int.TryParse(xmlNode.Attributes["time"].Value, out time))
				throw new XmlActionCompilerException("Unable to parse time value for jpeg-2000-lossless scheduling rule");

			string xmlUnit = xmlNode.Attributes["unit"].Value;

			// this will throw exception if the unit is not defined
			TimeUnit unit = (TimeUnit)Enum.Parse(typeof(TimeUnit), xmlUnit, true);

			string refValue = xmlNode.Attributes["refValue"] != null ? xmlNode.Attributes["refValue"].Value : null;


			if (!String.IsNullOrEmpty(refValue))
			{
				if (xmlNode["expressionLanguage"] != null)
				{
					string language = xmlNode["expressionLanguage"].Value;
					Expression scheduledTime = CreateExpression(refValue, language);
					return new Jpeg2000LosslessActionItem(time, unit, scheduledTime);
				}
				else
				{
					Expression scheduledTime = CreateExpression(refValue);
					return new Jpeg2000LosslessActionItem(time, unit, scheduledTime);
				}
			}
			else
			{
				return new Jpeg2000LosslessActionItem(time, unit);
			}
		}
		public XmlSchemaElement GetSchema(ServerRuleTypeEnum ruleType)
		{
			if (!ruleType.Equals(ServerRuleTypeEnum.StudyCompress))
				return null;

			XmlSchemaElement element = GetTimeSchema(OperatorTag);

			return element;
		}
	}
}