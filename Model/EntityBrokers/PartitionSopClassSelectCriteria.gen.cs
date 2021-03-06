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

    public partial class PartitionSopClassSelectCriteria : EntitySelectCriteria
    {
        public PartitionSopClassSelectCriteria()
        : base("PartitionSopClass")
        {}
        public PartitionSopClassSelectCriteria(PartitionSopClassSelectCriteria other)
        : base(other)
        {}
        public override object Clone()
        {
            return new PartitionSopClassSelectCriteria(this);
        }
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionSopClass", ColumnName="ServerPartitionGUID")]
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
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionSopClass", ColumnName="ServerSopClassGUID")]
        public ISearchCondition<ServerEntityKey> ServerSopClassKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("ServerSopClassKey"))
              {
                 SubCriteria["ServerSopClassKey"] = new SearchCondition<ServerEntityKey>("ServerSopClassKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["ServerSopClassKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionSopClass", ColumnName="Enabled")]
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
    }
}
