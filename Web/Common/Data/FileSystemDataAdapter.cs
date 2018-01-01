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

using System.Collections.Generic;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;
using MatrixPACS.ImageServer.Model.EntityBrokers;

namespace MatrixPACS.ImageServer.Web.Common.Data
{
    /// <summary>
    /// Used to create/update/delete file system entries in the database.
    /// </summary>
    ///
    public class FileSystemDataAdapter : BaseAdaptor<Filesystem, IFilesystemEntityBroker, FilesystemSelectCriteria,FilesystemUpdateColumns>
    {
        #region Public methods

        /// <summary>
        /// Gets a list of all file systems.
        /// </summary>
        /// <returns></returns>
        public IList<Filesystem> GetAllFileSystems()
        {
            return Get();
        }

        public IList<Filesystem> GetFileSystems(FilesystemSelectCriteria criteria)
        {
            return Get(criteria);
        }

        /// <summary>
        /// Creats a new file system.
        /// </summary>
        /// <param name="filesystem"></param>
        public bool AddFileSystem(Filesystem filesystem)
        {
            bool ok;

            // This filesystem update must be used, because the stored procedure does some 
            // additional work on insert.
            using (IUpdateContext ctx = PersistentStore.OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                IInsertFilesystem insert = ctx.GetBroker<IInsertFilesystem>();
                FilesystemInsertParameters parms = new FilesystemInsertParameters();
                parms.Description = filesystem.Description;
                parms.Enabled = filesystem.Enabled;
                parms.FilesystemPath = filesystem.FilesystemPath;
                parms.ReadOnly = filesystem.ReadOnly;
                parms.TypeEnum = filesystem.FilesystemTierEnum;
                parms.WriteOnly = filesystem.WriteOnly;
                parms.HighWatermark = filesystem.HighWatermark;
                parms.LowWatermark = filesystem.LowWatermark;

                Filesystem newFilesystem = insert.FindOne(parms);

				ok = newFilesystem != null;

                if (ok)
                    ctx.Commit();
            }

            return ok;
        }


        public bool Update(Filesystem filesystem)
        {

            FilesystemUpdateColumns parms = new FilesystemUpdateColumns();
            parms.Description = filesystem.Description;
            parms.Enabled = filesystem.Enabled;
            parms.FilesystemPath = filesystem.FilesystemPath;
            parms.ReadOnly = filesystem.ReadOnly;
            parms.FilesystemTierEnum = filesystem.FilesystemTierEnum;
            parms.WriteOnly = filesystem.WriteOnly;
            parms.HighWatermark = filesystem.HighWatermark;
            parms.LowWatermark = filesystem.LowWatermark;


            return Update(filesystem.Key, parms);
        }

        public IList<FilesystemTierEnum> GetFileSystemTiers()
        {
            return FilesystemTierEnum.GetAll();
        }

        #endregion Public methods
    }
}
