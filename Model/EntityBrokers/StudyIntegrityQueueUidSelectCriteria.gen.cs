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

namespace MatrixPACS.ImageServer.Model.EntityBrokers
{
    using System;
    using System.Xml;
    using MatrixPACS.Enterprise.Core;
    using MatrixPACS.ImageServer.Enterprise;

    public partial class StudyIntegrityQueueUidSelectCriteria : EntitySelectCriteria
    {
        public StudyIntegrityQueueUidSelectCriteria()
        : base("StudyIntegrityQueueUid")
        {}
        public StudyIntegrityQueueUidSelectCriteria(StudyIntegrityQueueUidSelectCriteria other)
        : base(other)
        {}
        public override object Clone()
        {
            return new StudyIntegrityQueueUidSelectCriteria(this);
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueueUid", ColumnName="StudyIntegrityQueueGUID")]
        public ISearchCondition<ServerEntityKey> StudyIntegrityQueueKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("StudyIntegrityQueueKey"))
              {
                 SubCriteria["StudyIntegrityQueueKey"] = new SearchCondition<ServerEntityKey>("StudyIntegrityQueueKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["StudyIntegrityQueueKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueueUid", ColumnName="SeriesInstanceUid")]
        public ISearchCondition<String> SeriesInstanceUid
        {
            get
            {
              if (!SubCriteria.ContainsKey("SeriesInstanceUid"))
              {
                 SubCriteria["SeriesInstanceUid"] = new SearchCondition<String>("SeriesInstanceUid");
              }
              return (ISearchCondition<String>)SubCriteria["SeriesInstanceUid"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueueUid", ColumnName="SopInstanceUid")]
        public ISearchCondition<String> SopInstanceUid
        {
            get
            {
              if (!SubCriteria.ContainsKey("SopInstanceUid"))
              {
                 SubCriteria["SopInstanceUid"] = new SearchCondition<String>("SopInstanceUid");
              }
              return (ISearchCondition<String>)SubCriteria["SopInstanceUid"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueueUid", ColumnName="RelativePath")]
        public ISearchCondition<String> RelativePath
        {
            get
            {
              if (!SubCriteria.ContainsKey("RelativePath"))
              {
                 SubCriteria["RelativePath"] = new SearchCondition<String>("RelativePath");
              }
              return (ISearchCondition<String>)SubCriteria["RelativePath"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="StudyIntegrityQueueUid", ColumnName="SeriesDescription")]
        public ISearchCondition<String> SeriesDescription
        {
            get
            {
              if (!SubCriteria.ContainsKey("SeriesDescription"))
              {
                 SubCriteria["SeriesDescription"] = new SearchCondition<String>("SeriesDescription");
              }
              return (ISearchCondition<String>)SubCriteria["SeriesDescription"];
            } 
        }
    }
}
