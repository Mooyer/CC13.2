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

using System.Xml;
using System.Xml.Schema;
using MatrixPACS.Common;
using MatrixPACS.Common.Actions;
using MatrixPACS.Dicom.Utilities.Rules;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Rules.JpegCodec.JpegLosslessAction
{
	/// <summary>
	/// Jpeg Losless SOP Compress Action Operator, for use with <see cref="IXmlActionCompilerOperator{ServerActionContext}"/>
	/// </summary>
	[ExtensionOf(typeof(ServerRuleActionCompilerOperatorExtensionPoint))]
	[ActionApplicability(ApplicableRuleType.SopCompress)]
	public class JpegLosslessSopActionOperator : ActionOperatorCompilerBase, IXmlActionCompilerOperator<ServerActionContext>
	{
		public JpegLosslessSopActionOperator()
			: base("jpeg-lossless-sop")
		{
		}

		public IActionItem<ServerActionContext> Compile(XmlElement xmlNode)
		{
			bool convertFromPalette = false;
			if (xmlNode.Attributes["convertFromPalette"] != null)
			{
				if (false == bool.TryParse(xmlNode.Attributes["convertFromPalette"].Value, out convertFromPalette))
					throw new XmlActionCompilerException("Unable to parse convertFromPalette value for jpeg-lossless-sop scheduling rule");
			}

			return new JpegLosslessSopActionItem(convertFromPalette);
		}

		public XmlSchemaElement GetSchema()
		{
			XmlSchemaElement element = GetBaseSchema(OperatorTag);

			XmlSchemaAttribute attrib = new XmlSchemaAttribute
			{
				Name = "convertFromPalette",
				Use = XmlSchemaUse.Optional,
				SchemaTypeName = new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema")
			};
			(element.SchemaType as XmlSchemaComplexType).Attributes.Add(attrib);

			return element;
		}
	}
}
