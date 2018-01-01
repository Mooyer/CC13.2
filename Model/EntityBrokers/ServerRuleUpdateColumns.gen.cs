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

// This file is auto-generated by the MatrixPACS.Model.SqlServer.CodeGenerator project.

namespace MatrixPACS.ImageServer.Model.EntityBrokers
{
    using System;
    using System.Xml;
    using MatrixPACS.ImageServer.Enterprise;

   public partial class ServerRuleUpdateColumns : EntityUpdateColumns
   {
       public ServerRuleUpdateColumns()
       : base("ServerRule")
       {}
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="RuleName")]
        public String RuleName
        {
            set { SubParameters["RuleName"] = new EntityUpdateColumn<String>("RuleName", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerPartitionGUID")]
        public ServerEntityKey ServerPartitionKey
        {
            set { SubParameters["ServerPartitionKey"] = new EntityUpdateColumn<ServerEntityKey>("ServerPartitionKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerRuleTypeEnum")]
        public ServerRuleTypeEnum ServerRuleTypeEnum
        {
            set { SubParameters["ServerRuleTypeEnum"] = new EntityUpdateColumn<ServerRuleTypeEnum>("ServerRuleTypeEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerRuleApplyTimeEnum")]
        public ServerRuleApplyTimeEnum ServerRuleApplyTimeEnum
        {
            set { SubParameters["ServerRuleApplyTimeEnum"] = new EntityUpdateColumn<ServerRuleApplyTimeEnum>("ServerRuleApplyTimeEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="Enabled")]
        public Boolean Enabled
        {
            set { SubParameters["Enabled"] = new EntityUpdateColumn<Boolean>("Enabled", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="DefaultRule")]
        public Boolean DefaultRule
        {
            set { SubParameters["DefaultRule"] = new EntityUpdateColumn<Boolean>("DefaultRule", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ExemptRule")]
        public Boolean ExemptRule
        {
            set { SubParameters["ExemptRule"] = new EntityUpdateColumn<Boolean>("ExemptRule", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="RuleXml")]
        public XmlDocument RuleXml
        {
            set { SubParameters["RuleXml"] = new EntityUpdateColumn<XmlDocument>("RuleXml", value); }
        }
    }
}
