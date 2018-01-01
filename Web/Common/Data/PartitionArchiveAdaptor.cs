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
using System.Web;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Core;
using MatrixPACS.ImageServer.Core.Helpers;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Web.Common.Data
{
	public class PartitionArchiveAdaptor : BaseAdaptor<PartitionArchive, IPartitionArchiveEntityBroker, PartitionArchiveSelectCriteria, PartitionArchiveUpdateColumns>
	{
		#region Private Members

		private readonly IPersistentStore _store = PersistentStoreRegistry.GetDefaultStore();

		#endregion Private Members

		public bool RestoreStudy(Study theStudy)
		{
			RestoreQueue restore = ServerHelper.InsertRestoreRequest(theStudy.LoadStudyStorage(HttpContext.Current.GetSharedPersistentContext()));
            if (restore==null)
                throw new ApplicationException("Unable to restore the study. See the log file for details.");

			return true;
		}
	}
}
