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

namespace MatrixPACS.ImageServer.Model
{
    using System;
    using System.Xml;
    using MatrixPACS.Enterprise.Core;
    using MatrixPACS.ImageServer.Enterprise;
    using MatrixPACS.ImageServer.Enterprise.Command;
    using MatrixPACS.ImageServer.Model.EntityBrokers;

    [Serializable]
    public partial class ServerRule: ServerEntity
    {
        #region Constructors
        public ServerRule():base("ServerRule")
        {}
        public ServerRule(
             String _ruleName_
            ,ServerEntityKey _serverPartitionKey_
            ,ServerRuleTypeEnum _serverRuleTypeEnum_
            ,ServerRuleApplyTimeEnum _serverRuleApplyTimeEnum_
            ,Boolean _enabled_
            ,Boolean _defaultRule_
            ,Boolean _exemptRule_
            ,XmlDocument _ruleXml_
            ):base("ServerRule")
        {
            RuleName = _ruleName_;
            ServerPartitionKey = _serverPartitionKey_;
            ServerRuleTypeEnum = _serverRuleTypeEnum_;
            ServerRuleApplyTimeEnum = _serverRuleApplyTimeEnum_;
            Enabled = _enabled_;
            DefaultRule = _defaultRule_;
            ExemptRule = _exemptRule_;
            RuleXml = _ruleXml_;
        }
        #endregion

        #region Public Properties
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="RuleName")]
        public String RuleName
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerPartitionGUID")]
        public ServerEntityKey ServerPartitionKey
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerRuleTypeEnum")]
        public ServerRuleTypeEnum ServerRuleTypeEnum
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ServerRuleApplyTimeEnum")]
        public ServerRuleApplyTimeEnum ServerRuleApplyTimeEnum
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="Enabled")]
        public Boolean Enabled
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="DefaultRule")]
        public Boolean DefaultRule
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="ExemptRule")]
        public Boolean ExemptRule
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerRule", ColumnName="RuleXml")]
        public XmlDocument RuleXml
        { get { return _RuleXml; } set { _RuleXml = value; } }
        [NonSerialized]
        private XmlDocument _RuleXml;
        #endregion

        #region Static Methods
        static public ServerRule Load(ServerEntityKey key)
        {
            using (var context = new ServerExecutionContext())
            {
                return Load(context.ReadContext, key);
            }
        }
        static public ServerRule Load(IPersistenceContext read, ServerEntityKey key)
        {
            var broker = read.GetBroker<IServerRuleEntityBroker>();
            ServerRule theObject = broker.Load(key);
            return theObject;
        }
        static public ServerRule Insert(ServerRule entity)
        {
            using (var update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                ServerRule newEntity = Insert(update, entity);
                update.Commit();
                return newEntity;
            }
        }
        static public ServerRule Insert(IUpdateContext update, ServerRule entity)
        {
            var broker = update.GetBroker<IServerRuleEntityBroker>();
            var updateColumns = new ServerRuleUpdateColumns();
            updateColumns.RuleName = entity.RuleName;
            updateColumns.ServerPartitionKey = entity.ServerPartitionKey;
            updateColumns.ServerRuleTypeEnum = entity.ServerRuleTypeEnum;
            updateColumns.ServerRuleApplyTimeEnum = entity.ServerRuleApplyTimeEnum;
            updateColumns.Enabled = entity.Enabled;
            updateColumns.DefaultRule = entity.DefaultRule;
            updateColumns.ExemptRule = entity.ExemptRule;
            updateColumns.RuleXml = entity.RuleXml;
            ServerRule newEntity = broker.Insert(updateColumns);
            return newEntity;
        }
        #endregion
    }
}
