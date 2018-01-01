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

using MatrixPACS.Common;
using MatrixPACS.ImageServer.Common.ServiceModel;
using MatrixPACS.ImageServer.Common.Utilities;

namespace MatrixPACS.ImageServer.Web.Common.Utilities
{
    public static class ServerUtility
    {
        /// <summary>
        /// Retrieves the <see cref="FilesystemInfo"/> for a specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FilesystemInfo GetFilesystemInfo(string path)
        {
            FilesystemInfo fsInfo = null;
            Platform.GetService(delegate(IFilesystemService service)
            {
                fsInfo = service.GetFilesystemInfo(path);
            });

            return fsInfo;
        }
    }
}
