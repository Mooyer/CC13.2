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

    public partial class PartitionTransferSyntaxSelectCriteria : EntitySelectCriteria
    {
        public PartitionTransferSyntaxSelectCriteria()
        : base("PartitionTransferSyntax")
        {}
        public PartitionTransferSyntaxSelectCriteria(PartitionTransferSyntaxSelectCriteria other)
        : base(other)
        {}
        public override object Clone()
        {
            return new PartitionTransferSyntaxSelectCriteria(this);
        }
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionTransferSyntax", ColumnName="ServerPartitionGUID")]
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
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionTransferSyntax", ColumnName="ServerTransferSyntaxGUID")]
        public ISearchCondition<ServerEntityKey> ServerTransferSyntaxKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("ServerTransferSyntaxKey"))
              {
                 SubCriteria["ServerTransferSyntaxKey"] = new SearchCondition<ServerEntityKey>("ServerTransferSyntaxKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["ServerTransferSyntaxKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="PartitionTransferSyntax", ColumnName="Enabled")]
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
