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
using System.Linq;
using MatrixPACS.Common;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Web.Common.Data;

namespace MatrixPACS.ImageServer.Web.Common
{

    public interface IServerPartitionTabsExtension
    {
        IEnumerable<ServerPartition> LoadServerPartitions();
		IEnumerable<ServerPartition> LoadNonResearchServerPartitions();
    }
    
    
    [ExtensionPoint]
    public class ServerPartitionTabsExtensionPoint : ExtensionPoint<IServerPartitionTabsExtension> { }


    public sealed class DefaultServerPartitionTabsExtension : IServerPartitionTabsExtension
    {
        private readonly ServerPartitionConfigController _controller = new ServerPartitionConfigController();

        public IEnumerable<ServerPartition> LoadServerPartitions()
        {
            return _controller.GetAllPartitions();
        }

		public IEnumerable<ServerPartition> LoadNonResearchServerPartitions()
		{
			var list = LoadServerPartitions();

			return list.Where(p => !p.ServerPartitionTypeEnum.Equals(ServerPartitionTypeEnum.VFS)).ToList();
		}
    }
}