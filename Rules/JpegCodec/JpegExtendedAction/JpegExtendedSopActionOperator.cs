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

namespace MatrixPACS.ImageServer.Rules.JpegCodec.JpegExtendedAction
{
	/// <summary>
	/// Jpeg Extended SOP Compress Action Operator, for use with <see cref="IXmlActionCompilerOperator{ServerActionContext}"/>
	/// </summary>
	[ExtensionOf(typeof(ServerRuleActionCompilerOperatorExtensionPoint))]
	[ActionApplicability(ApplicableRuleType.SopCompress)]
	public class JpegExtendedSopActionOperator : ActionOperatorCompilerBase, IXmlActionCompilerOperator<ServerActionContext>
	{
		public JpegExtendedSopActionOperator()
			: base("jpeg-extended-sop")
		{
		}

		public IActionItem<ServerActionContext> Compile(XmlElement xmlNode)
		{
			int quality;
			if (false == int.TryParse(xmlNode.Attributes["quality"].Value, out quality))
				throw new XmlActionCompilerException("Unable to parse quality value for jpeg-extended-sop scheduling rule");

			bool convertFromPalette = false;
			if (xmlNode.Attributes["convertFromPalette"] != null)
			{
				if (false == bool.TryParse(xmlNode.Attributes["convertFromPalette"].Value, out convertFromPalette))
					throw new XmlActionCompilerException("Unable to parse convertFromPalette value for jpeg-extended-sop scheduling rule");
			}

			return new JpegExtendedSopActionItem(quality, convertFromPalette);
		}

		public XmlSchemaElement GetSchema()
		{
			XmlSchemaElement element = GetBaseSchema(OperatorTag);

			XmlSchemaAttribute attrib = new XmlSchemaAttribute
			                            	{
			                            		Name = "quality",
			                            		Use = XmlSchemaUse.Required,
			                            		SchemaTypeName =
			                            			new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema")
			                            	};
			(element.SchemaType as XmlSchemaComplexType).Attributes.Add(attrib);

			attrib = new XmlSchemaAttribute
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
