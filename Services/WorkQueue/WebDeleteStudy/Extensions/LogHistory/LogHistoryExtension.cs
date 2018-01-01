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
using System.Xml.Serialization;
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common.StudyHistory;
using MatrixPACS.ImageServer.Common.Utilities;
using MatrixPACS.ImageServer.Core.Data;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;

namespace MatrixPACS.ImageServer.Services.WorkQueue.WebDeleteStudy.Extensions.LogHistory
{
    [ExtensionOf(typeof(WebDeleteProcessorExtensionPoint))]
    class LogHistoryExtension:IWebDeleteProcessorExtension
    {
        private StudyInformation _studyInfo;
        
        #region IWebDeleteProcessorExtension Members

        public void OnSeriesDeleting(WebDeleteProcessorContext context, Series series)
        {
            _studyInfo = StudyInformation.CreateFrom(context.StorageLocation.Study);
        }

        public void OnSeriesDeleted(WebDeleteProcessorContext context, Series _series)
        {
            
        }

        #endregion

        #region IWebDeleteProcessorExtension Members


        public void OnCompleted(WebDeleteProcessorContext context, IList<Series> series)
        {
            if (series.Count > 0)
            {
                Platform.Log(LogLevel.Info, "Logging history..");
                DateTime now = Platform.Time;
                using(IUpdateContext ctx = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
                {
                    IStudyHistoryEntityBroker broker = ctx.GetBroker<IStudyHistoryEntityBroker>();
                    StudyHistoryUpdateColumns columns = new StudyHistoryUpdateColumns();
                    columns.InsertTime = Platform.Time;
                    columns.StudyHistoryTypeEnum = StudyHistoryTypeEnum.SeriesDeleted;
                    columns.StudyStorageKey = context.StorageLocation.Key;
                    columns.DestStudyStorageKey = context.StorageLocation.Key;
                    columns.StudyData = XmlUtils.SerializeAsXmlDoc(_studyInfo);
                    SeriesDeletionChangeLog changeLog =  new SeriesDeletionChangeLog();
                    changeLog.TimeStamp = now;
                    changeLog.Reason = context.Reason;
                    changeLog.UserId = context.UserId;
                    changeLog.UserName = context.UserName;
                    changeLog.Series = CollectionUtils.Map(series,
                                      delegate(Series ser)
                                          {
                                              ServerEntityAttributeProvider seriesWrapper = new ServerEntityAttributeProvider(ser);
                                              return new SeriesInformation(seriesWrapper);
                                          });
                    columns.ChangeDescription = XmlUtils.SerializeAsXmlDoc(changeLog);
                    StudyHistory history = broker.Insert(columns);
                    if (history!=null)
                        ctx.Commit();
                    
                }
            }
            
        }

        #endregion
    }

	[ImageServerStudyHistoryType("DC4991D3-457E-4753-913B-DC90654B07F3")]
    public class SeriesDeletionChangeLog : ImageServerStudyHistory
    {
		public DateTime TimeStamp { get; set; }

		public string UserId { get; set; }

		public string Reason { get; set; }

		[XmlArray("DeletedSeries")]
		public List<SeriesInformation> Series { get; set; }

		public string UserName { get; set; }
    }
}
