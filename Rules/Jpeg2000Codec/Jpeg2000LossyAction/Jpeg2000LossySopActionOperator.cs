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

using System.Xml;
using System.Xml.Schema;
using MatrixPACS.Common;
using MatrixPACS.Common.Actions;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Rules.Jpeg2000Codec.Jpeg2000LossyAction
{
	[ExtensionOf(typeof(XmlActionCompilerOperatorExtensionPoint<ServerActionContext, ServerRuleTypeEnum>))]
	public class Jpeg2000LossySopActionOperator : ActionOperatorCompilerBase, IXmlActionCompilerOperator<ServerActionContext, ServerRuleTypeEnum>
	{
		public Jpeg2000LossySopActionOperator()
			: base("jpeg-2000-lossy-sop")
		{
		}

		public IActionItem<ServerActionContext> Compile(XmlElement xmlNode)
		{
			float ratio;
			if (false == float.TryParse(xmlNode.Attributes["ratio"].Value, out ratio))
				throw new XmlActionCompilerException("Unable to parse ratio value for jpeg-2000-lossy-sop compression ratio");

			return new Jpeg2000LossySopActionItem(ratio);
		}

		public XmlSchemaElement GetSchema(ServerRuleTypeEnum ruleType)
		{
			if (!ruleType.Equals(ServerRuleTypeEnum.SopCompress))
				return null;

			XmlSchemaElement element = GetBaseSchema(OperatorTag);

			XmlSchemaAttribute attrib = new XmlSchemaAttribute();
			attrib.Name = "ratio";
			attrib.Use = XmlSchemaUse.Required;
			attrib.SchemaTypeName = new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");
			(element.SchemaType as XmlSchemaComplexType).Attributes.Add(attrib);

			return element;
		}
	}
}
