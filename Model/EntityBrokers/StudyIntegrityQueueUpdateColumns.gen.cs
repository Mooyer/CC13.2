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

   public partial class StudyIntegrityQueueUpdateColumns : EntityUpdateColumns
   {
       public StudyIntegrityQueueUpdateColumns()
       : base("StudyIntegrityQueue")
       {}
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="ServerPartitionGUID")]
        public ServerEntityKey ServerPartitionKey
        {
            set { SubParameters["ServerPartitionKey"] = new EntityUpdateColumn<ServerEntityKey>("ServerPartitionKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="StudyStorageGUID")]
        public ServerEntityKey StudyStorageKey
        {
            set { SubParameters["StudyStorageKey"] = new EntityUpdateColumn<ServerEntityKey>("StudyStorageKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="InsertTime")]
        public DateTime InsertTime
        {
            set { SubParameters["InsertTime"] = new EntityUpdateColumn<DateTime>("InsertTime", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="StudyData")]
        public XmlDocument StudyData
        {
            set { SubParameters["StudyData"] = new EntityUpdateColumn<XmlDocument>("StudyData", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="StudyIntegrityReasonEnum")]
        public StudyIntegrityReasonEnum StudyIntegrityReasonEnum
        {
            set { SubParameters["StudyIntegrityReasonEnum"] = new EntityUpdateColumn<StudyIntegrityReasonEnum>("StudyIntegrityReasonEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="GroupID")]
        public String GroupID
        {
            set { SubParameters["GroupID"] = new EntityUpdateColumn<String>("GroupID", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="Details")]
        public XmlDocument Details
        {
            set { SubParameters["Details"] = new EntityUpdateColumn<XmlDocument>("Details", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueue", ColumnName="Description")]
        public String Description
        {
            set { SubParameters["Description"] = new EntityUpdateColumn<String>("Description", value); }
        }
    }
}