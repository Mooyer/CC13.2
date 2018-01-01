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
    public partial class ServerTransferSyntax: ServerEntity
    {
        #region Constructors
        public ServerTransferSyntax():base("ServerTransferSyntax")
        {}
        public ServerTransferSyntax(
             String _uid_
            ,String _description_
            ,Boolean _lossless_
            ):base("ServerTransferSyntax")
        {
            Uid = _uid_;
            Description = _description_;
            Lossless = _lossless_;
        }
        #endregion

        #region Public Properties
        [DicomField(DicomTags.Uid, DefaultValue = DicomFieldDefault.Null)]
        [EntityFieldDatabaseMappingAttribute(TableName="ServerTransferSyntax", ColumnName="Uid")]
        public String Uid
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerTransferSyntax", ColumnName="Description")]
        public String Description
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="ServerTransferSyntax", ColumnName="Lossless")]
        public Boolean Lossless
        { get; set; }
        #endregion

        #region Static Methods
        static public ServerTransferSyntax Load(ServerEntityKey key)
        {
            using (var context = new ServerExecutionContext())
            {
                return Load(context.ReadContext, key);
            }
        }
        static public ServerTransferSyntax Load(IPersistenceContext read, ServerEntityKey key)
        {
            var broker = read.GetBroker<IServerTransferSyntaxEntityBroker>();
            ServerTransferSyntax theObject = broker.Load(key);
            return theObject;
        }
        static public ServerTransferSyntax Insert(ServerTransferSyntax entity)
        {
            using (var update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                ServerTransferSyntax newEntity = Insert(update, entity);
                update.Commit();
                return newEntity;
            }
        }
        static public ServerTransferSyntax Insert(IUpdateContext update, ServerTransferSyntax entity)
        {
            var broker = update.GetBroker<IServerTransferSyntaxEntityBroker>();
            var updateColumns = new ServerTransferSyntaxUpdateColumns();
            updateColumns.Uid = entity.Uid;
            updateColumns.Description = entity.Description;
            updateColumns.Lossless = entity.Lossless;
            ServerTransferSyntax newEntity = broker.Insert(updateColumns);
            return newEntity;
        }
        #endregion
    }
}
