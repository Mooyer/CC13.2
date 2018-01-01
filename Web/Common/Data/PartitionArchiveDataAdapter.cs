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

using System;
using System.Collections.Generic;
using MatrixPACS.Common;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Web.Common.Data
{
    /// <summary>
    /// Used to create/update/delete server partition entries in the database.
    /// </summary>
    public class PartitionArchiveDataAdapter :
        BaseAdaptor
            <PartitionArchive, IPartitionArchiveEntityBroker, PartitionArchiveSelectCriteria, PartitionArchiveUpdateColumns>
    {
        #region Public methods

        /// <summary>
        /// Gets a list of all server partitions.
        /// </summary>
        /// <returns></returns>
        public IList<PartitionArchive> GetPartitionArchives()
        {
            return Get();
        }

        public IList<PartitionArchive> GetPartitionArchives(PartitionArchiveSelectCriteria criteria)
        {
            return Get(criteria);
        }

        /// <summary>
        /// Creats a new server parition.
        /// </summary>
        /// <param name="partition"></param>
        public bool AddPartitionArchive(PartitionArchive partition)
        {
            PartitionArchiveAdaptor adaptor = new PartitionArchiveAdaptor();
            PartitionArchiveUpdateColumns columns = new PartitionArchiveUpdateColumns();
                
            columns.Description = partition.Description;
            columns.ArchiveDelayHours = partition.ArchiveDelayHours;
            columns.ArchiveTypeEnum = partition.ArchiveTypeEnum;
            columns.ConfigurationXml = partition.ConfigurationXml;
            columns.Enabled = partition.Enabled;
            columns.ReadOnly = partition.ReadOnly;
            columns.ServerPartitionKey = partition.ServerPartitionKey;

            adaptor.Add(columns);

            return true;
        }

        public bool Update(PartitionArchive partition)
        {
            PartitionArchiveUpdateColumns parms = new PartitionArchiveUpdateColumns();
            parms.Description = partition.Description;
            parms.ServerPartitionKey = partition.ServerPartitionKey;
            parms.ArchiveTypeEnum = partition.ArchiveTypeEnum;
            parms.ConfigurationXml = partition.ConfigurationXml;
            parms.Enabled = partition.Enabled;
            parms.ReadOnly = partition.ReadOnly;
            parms.ArchiveDelayHours = partition.ArchiveDelayHours;
            
            
            return Update(partition.Key, parms);
        }

        #endregion Public methods
    }
}

