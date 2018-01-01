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
    using MatrixPACS.Dicom;
    using MatrixPACS.Enterprise.Core;
    using MatrixPACS.ImageServer.Enterprise;
    using MatrixPACS.ImageServer.Enterprise.Command;
    using MatrixPACS.ImageServer.Model.EntityBrokers;

    [Serializable]
    public partial class WorkQueueUid: ServerEntity
    {
        #region Constructors
        public WorkQueueUid():base("WorkQueueUid")
        {}
        public WorkQueueUid(
             ServerEntityKey _workQueueKey_
            ,Int16 _failureCount_
            ,Boolean _failed_
            ,Boolean _duplicate_
            ,String _extension_
            ,String _groupID_
            ,String _relativePath_
            ,XmlDocument _workQueueUidData_
            ,String _seriesInstanceUid_
            ,String _sopInstanceUid_
            ):base("WorkQueueUid")
        {
            WorkQueueKey = _workQueueKey_;
            FailureCount = _failureCount_;
            Failed = _failed_;
            Duplicate = _duplicate_;
            Extension = _extension_;
            GroupID = _groupID_;
            RelativePath = _relativePath_;
            WorkQueueUidData = _workQueueUidData_;
            SeriesInstanceUid = _seriesInstanceUid_;
            SopInstanceUid = _sopInstanceUid_;
        }
        #endregion

        #region Public Properties
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="WorkQueueGUID")]
        public ServerEntityKey WorkQueueKey
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="FailureCount")]
        public Int16 FailureCount
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="Failed")]
        public Boolean Failed
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="Duplicate")]
        public Boolean Duplicate
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="Extension")]
        public String Extension
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="GroupID")]
        public String GroupID
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="RelativePath")]
        public String RelativePath
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="WorkQueueUidData")]
        public XmlDocument WorkQueueUidData
        { get { return _WorkQueueUidData; } set { _WorkQueueUidData = value; } }
        [NonSerialized]
        private XmlDocument _WorkQueueUidData;
        [DicomField(DicomTags.SeriesInstanceUid, DefaultValue = DicomFieldDefault.Null)]
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="SeriesInstanceUid")]
        public String SeriesInstanceUid
        { get; set; }
        [DicomField(DicomTags.SopInstanceUid, DefaultValue = DicomFieldDefault.Null)]
        [EntityFieldDatabaseMappingAttribute(TableName="WorkQueueUid", ColumnName="SopInstanceUid")]
        public String SopInstanceUid
        { get; set; }
        #endregion

        #region Static Methods
        static public WorkQueueUid Load(ServerEntityKey key)
        {
            using (var context = new ServerExecutionContext())
            {
                return Load(context.ReadContext, key);
            }
        }
        static public WorkQueueUid Load(IPersistenceContext read, ServerEntityKey key)
        {
            var broker = read.GetBroker<IWorkQueueUidEntityBroker>();
            WorkQueueUid theObject = broker.Load(key);
            return theObject;
        }
        static public WorkQueueUid Insert(WorkQueueUid entity)
        {
            using (var update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                WorkQueueUid newEntity = Insert(update, entity);
                update.Commit();
                return newEntity;
            }
        }
        static public WorkQueueUid Insert(IUpdateContext update, WorkQueueUid entity)
        {
            var broker = update.GetBroker<IWorkQueueUidEntityBroker>();
            var updateColumns = new WorkQueueUidUpdateColumns();
            updateColumns.WorkQueueKey = entity.WorkQueueKey;
            updateColumns.FailureCount = entity.FailureCount;
            updateColumns.Failed = entity.Failed;
            updateColumns.Duplicate = entity.Duplicate;
            updateColumns.Extension = entity.Extension;
            updateColumns.GroupID = entity.GroupID;
            updateColumns.RelativePath = entity.RelativePath;
            updateColumns.WorkQueueUidData = entity.WorkQueueUidData;
            updateColumns.SeriesInstanceUid = entity.SeriesInstanceUid;
            updateColumns.SopInstanceUid = entity.SopInstanceUid;
            WorkQueueUid newEntity = broker.Insert(updateColumns);
            return newEntity;
        }
        #endregion
    }
}
