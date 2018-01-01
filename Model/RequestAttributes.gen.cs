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
    public partial class RequestAttributes: ServerEntity
    {
        #region Constructors
        public RequestAttributes():base("RequestAttributes")
        {}
        public RequestAttributes(
             ServerEntityKey _seriesKey_
            ,String _requestedProcedureId_
            ,String _scheduledProcedureStepId_
            ):base("RequestAttributes")
        {
            SeriesKey = _seriesKey_;
            RequestedProcedureId = _requestedProcedureId_;
            ScheduledProcedureStepId = _scheduledProcedureStepId_;
        }
        #endregion

        #region Public Properties
        [EntityFieldDatabaseMappingAttribute(TableName="RequestAttributes", ColumnName="SeriesGUID")]
        public ServerEntityKey SeriesKey
        { get; set; }
        [DicomField(DicomTags.RequestedProcedureId, DefaultValue = DicomFieldDefault.Null)]
        [EntityFieldDatabaseMappingAttribute(TableName="RequestAttributes", ColumnName="RequestedProcedureId")]
        public String RequestedProcedureId
        { get; set; }
        [DicomField(DicomTags.ScheduledProcedureStepId, DefaultValue = DicomFieldDefault.Null)]
        [EntityFieldDatabaseMappingAttribute(TableName="RequestAttributes", ColumnName="ScheduledProcedureStepId")]
        public String ScheduledProcedureStepId
        { get; set; }
        #endregion

        #region Static Methods
        static public RequestAttributes Load(ServerEntityKey key)
        {
            using (var context = new ServerExecutionContext())
            {
                return Load(context.ReadContext, key);
            }
        }
        static public RequestAttributes Load(IPersistenceContext read, ServerEntityKey key)
        {
            var broker = read.GetBroker<IRequestAttributesEntityBroker>();
            RequestAttributes theObject = broker.Load(key);
            return theObject;
        }
        static public RequestAttributes Insert(RequestAttributes entity)
        {
            using (var update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                RequestAttributes newEntity = Insert(update, entity);
                update.Commit();
                return newEntity;
            }
        }
        static public RequestAttributes Insert(IUpdateContext update, RequestAttributes entity)
        {
            var broker = update.GetBroker<IRequestAttributesEntityBroker>();
            var updateColumns = new RequestAttributesUpdateColumns();
            updateColumns.SeriesKey = entity.SeriesKey;
            updateColumns.RequestedProcedureId = entity.RequestedProcedureId;
            updateColumns.ScheduledProcedureStepId = entity.ScheduledProcedureStepId;
            RequestAttributes newEntity = broker.Insert(updateColumns);
            return newEntity;
        }
        #endregion
    }
}
