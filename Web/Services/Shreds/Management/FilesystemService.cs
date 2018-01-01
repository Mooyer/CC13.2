#region License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.matriximage.ca
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
using System.ServiceModel;
using MatrixPACS.Common;
using MatrixPACS.Common.Shreds;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.Server.ShredHost;

namespace MatrixPACS.ImageServer.Web.Services.Shreds.Management
{
    [ServiceContract]
    public interface IFilesystemService
    {
        [OperationContract]
        FilesystemInfo GetFilesystemInfo(string path);
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ExtensionOf(typeof(ShredExtensionPoint))]
    public class FilesystemService : WcfShred, IFilesystemService
    {

        #region IFilesystemService Members

        public FilesystemInfo GetFilesystemInfo(string path)
        {
            return FilesystemUtils.GetDirectoryInfo(path);
        }

        #endregion

        #region WcfShred override
        public override void Start()
        {
            try
            {
                ServiceEndpointDescription sed = StartHttpHost<FilesystemService, IFilesystemService>("FilesystemService", SR.FilesystemServiceDisplayDescription);

            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, "Failed to start {0} : {1}", SR.FilesystemServiceDisplayName, e.StackTrace);
                ServerPlatform.Alert(AlertCategory.Application, AlertLevel.Error, SR.FilesystemServiceDisplayName,
                                     AlertTypeCodes.UnableToStart, null, TimeSpan.Zero, SR.AlertFilesystemUnableToStart, e.Message);
            }
        }

        public override void Stop()
        {
            StopHost("FilesystemService");
        }

        public override string GetDisplayName()
        {
            return SR.FilesystemServiceDisplayName;
        }

        public override string GetDescription()
        {
            return SR.FilesystemServiceDisplayDescription;
        }

        #endregion WcfShred override
    }

}

