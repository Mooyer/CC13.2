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
using MatrixPACS.Common;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Web.Common.Data
{
	public class ArchiveQueueController
	{
        private readonly ArchiveQueueAdaptor _adaptor = new ArchiveQueueAdaptor();


		/// <summary>
		/// Gets a list of <see cref="ArchiveQueue"/> items with specified criteria
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public IList<ArchiveQueue> FindArchiveQueue(WebQueryArchiveQueueParameters parameters)
		{
			try
			{
				IList<ArchiveQueue> list;

				IWebQueryArchiveQueue broker = HttpContext.Current.GetSharedPersistentContext().GetBroker<IWebQueryArchiveQueue>();
				list = broker.Find(parameters);

				return list;
			}
			catch (Exception e)
			{
				Platform.Log(LogLevel.Error, "FindArchiveQueue failed", e);
				return new List<ArchiveQueue>();
			}
		}

        public bool DeleteArchiveQueueItem(ArchiveQueue item)
        {
            return _adaptor.Delete(item.Key);
        }

		public bool ResetArchiveQueueItem(IList<ArchiveQueue> items, DateTime time)
		{
			if (items == null || items.Count == 0)
				return false;

			ArchiveQueueUpdateColumns columns = new ArchiveQueueUpdateColumns();
			columns.ArchiveQueueStatusEnum = ArchiveQueueStatusEnum.Pending;
			columns.ProcessorId = "";
			columns.ScheduledTime = time;

			bool result = true;
			IPersistentStore store = PersistentStoreRegistry.GetDefaultStore();
			using (IUpdateContext ctx = store.OpenUpdateContext(UpdateContextSyncMode.Flush))
			{
				IArchiveQueueEntityBroker archiveQueueBroker = ctx.GetBroker<IArchiveQueueEntityBroker>();
				
				foreach (ArchiveQueue item in items)
				{
					// Only do an update if its a failed status currently
					ArchiveQueueSelectCriteria criteria = new ArchiveQueueSelectCriteria();
					criteria.ArchiveQueueStatusEnum.EqualTo(ArchiveQueueStatusEnum.Failed);
					criteria.StudyStorageKey.EqualTo(item.StudyStorageKey);

					if (!archiveQueueBroker.Update(criteria, columns))
					{
						result = false;
						break;
					}
				}

				if (result)
					ctx.Commit();
			}

			return result;
		}
	}
}
