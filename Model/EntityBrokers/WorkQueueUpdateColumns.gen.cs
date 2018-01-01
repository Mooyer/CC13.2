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

   public partial class WorkQueueUpdateColumns : EntityUpdateColumns
   {
       public WorkQueueUpdateColumns()
       : base("WorkQueue")
       {}
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="ServerPartitionGUID")]
        public ServerEntityKey ServerPartitionKey
        {
            set { SubParameters["ServerPartitionKey"] = new EntityUpdateColumn<ServerEntityKey>("ServerPartitionKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="StudyStorageGUID")]
        public ServerEntityKey StudyStorageKey
        {
            set { SubParameters["StudyStorageKey"] = new EntityUpdateColumn<ServerEntityKey>("StudyStorageKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="WorkQueueTypeEnum")]
        public WorkQueueTypeEnum WorkQueueTypeEnum
        {
            set { SubParameters["WorkQueueTypeEnum"] = new EntityUpdateColumn<WorkQueueTypeEnum>("WorkQueueTypeEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="WorkQueueStatusEnum")]
        public WorkQueueStatusEnum WorkQueueStatusEnum
        {
            set { SubParameters["WorkQueueStatusEnum"] = new EntityUpdateColumn<WorkQueueStatusEnum>("WorkQueueStatusEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="WorkQueuePriorityEnum")]
        public WorkQueuePriorityEnum WorkQueuePriorityEnum
        {
            set { SubParameters["WorkQueuePriorityEnum"] = new EntityUpdateColumn<WorkQueuePriorityEnum>("WorkQueuePriorityEnum", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="FailureCount")]
        public Int32 FailureCount
        {
            set { SubParameters["FailureCount"] = new EntityUpdateColumn<Int32>("FailureCount", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="ScheduledTime")]
        public DateTime ScheduledTime
        {
            set { SubParameters["ScheduledTime"] = new EntityUpdateColumn<DateTime>("ScheduledTime", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="InsertTime")]
        public DateTime InsertTime
        {
            set { SubParameters["InsertTime"] = new EntityUpdateColumn<DateTime>("InsertTime", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="LastUpdatedTime")]
        public DateTime? LastUpdatedTime
        {
            set { SubParameters["LastUpdatedTime"] = new EntityUpdateColumn<DateTime?>("LastUpdatedTime", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="FailureDescription")]
        public String FailureDescription
        {
            set { SubParameters["FailureDescription"] = new EntityUpdateColumn<String>("FailureDescription", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="Data")]
        public XmlDocument Data
        {
            set { SubParameters["Data"] = new EntityUpdateColumn<XmlDocument>("Data", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="ExternalRequestQueueGUID")]
        public ServerEntityKey ExternalRequestQueueKey
        {
            set { SubParameters["ExternalRequestQueueKey"] = new EntityUpdateColumn<ServerEntityKey>("ExternalRequestQueueKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="ProcessorID")]
        public String ProcessorID
        {
            set { SubParameters["ProcessorID"] = new EntityUpdateColumn<String>("ProcessorID", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="GroupID")]
        public String GroupID
        {
            set { SubParameters["GroupID"] = new EntityUpdateColumn<String>("GroupID", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="ExpirationTime")]
        public DateTime? ExpirationTime
        {
            set { SubParameters["ExpirationTime"] = new EntityUpdateColumn<DateTime?>("ExpirationTime", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="DeviceGUID")]
        public ServerEntityKey DeviceKey
        {
            set { SubParameters["DeviceKey"] = new EntityUpdateColumn<ServerEntityKey>("DeviceKey", value); }
        }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueue", ColumnName="StudyHistoryGUID")]
        public ServerEntityKey StudyHistoryKey
        {
            set { SubParameters["StudyHistoryKey"] = new EntityUpdateColumn<ServerEntityKey>("StudyHistoryKey", value); }
        }
    }
}
