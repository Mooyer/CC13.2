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
    using MatrixPACS.Enterprise.Core;
    using MatrixPACS.ImageServer.Enterprise;

    public partial class ServerPartitionAlternateAeTitleSelectCriteria : EntitySelectCriteria
    {
        public ServerPartitionAlternateAeTitleSelectCriteria()
        : base("ServerPartitionAlternateAeTitle")
        {}
        public ServerPartitionAlternateAeTitleSelectCriteria(ServerPartitionAlternateAeTitleSelectCriteria other)
        : base(other)
        {}
        public override object Clone()
        {
            return new ServerPartitionAlternateAeTitleSelectCriteria(this);
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="ServerPartitionGUID")]
        public ISearchCondition<ServerEntityKey> ServerPartitionKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("ServerPartitionKey"))
              {
                 SubCriteria["ServerPartitionKey"] = new SearchCondition<ServerEntityKey>("ServerPartitionKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["ServerPartitionKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="AeTitle")]
        public ISearchCondition<String> AeTitle
        {
            get
            {
              if (!SubCriteria.ContainsKey("AeTitle"))
              {
                 SubCriteria["AeTitle"] = new SearchCondition<String>("AeTitle");
              }
              return (ISearchCondition<String>)SubCriteria["AeTitle"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="Port")]
        public ISearchCondition<Int32> Port
        {
            get
            {
              if (!SubCriteria.ContainsKey("Port"))
              {
                 SubCriteria["Port"] = new SearchCondition<Int32>("Port");
              }
              return (ISearchCondition<Int32>)SubCriteria["Port"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="Enabled")]
        public ISearchCondition<Boolean> Enabled
        {
            get
            {
              if (!SubCriteria.ContainsKey("Enabled"))
              {
                 SubCriteria["Enabled"] = new SearchCondition<Boolean>("Enabled");
              }
              return (ISearchCondition<Boolean>)SubCriteria["Enabled"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="AllowStorage")]
        public ISearchCondition<Boolean> AllowStorage
        {
            get
            {
              if (!SubCriteria.ContainsKey("AllowStorage"))
              {
                 SubCriteria["AllowStorage"] = new SearchCondition<Boolean>("AllowStorage");
              }
              return (ISearchCondition<Boolean>)SubCriteria["AllowStorage"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="AllowKOPR")]
        public ISearchCondition<Boolean> AllowKOPR
        {
            get
            {
              if (!SubCriteria.ContainsKey("AllowKOPR"))
              {
                 SubCriteria["AllowKOPR"] = new SearchCondition<Boolean>("AllowKOPR");
              }
              return (ISearchCondition<Boolean>)SubCriteria["AllowKOPR"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="AllowRetrieve")]
        public ISearchCondition<Boolean> AllowRetrieve
        {
            get
            {
              if (!SubCriteria.ContainsKey("AllowRetrieve"))
              {
                 SubCriteria["AllowRetrieve"] = new SearchCondition<Boolean>("AllowRetrieve");
              }
              return (ISearchCondition<Boolean>)SubCriteria["AllowRetrieve"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerPartitionAlternateAeTitle", ColumnName="AllowQuery")]
        public ISearchCondition<Boolean> AllowQuery
        {
            get
            {
              if (!SubCriteria.ContainsKey("AllowQuery"))
              {
                 SubCriteria["AllowQuery"] = new SearchCondition<Boolean>("AllowQuery");
              }
              return (ISearchCondition<Boolean>)SubCriteria["AllowQuery"];
            } 
        }
    }
}
