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
using System.Xml;
using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model.Parameters
{
    public class InsertDuplicateSopReceivedQueueParameters: ProcedureParameters
    {
        public InsertDuplicateSopReceivedQueueParameters()
            : base("InsertDuplicateSopReceivedQueueParameters")
        {

        }
        public String Description
        {
            set { SubCriteria["Description"] = new ProcedureParameter<String>("Description", value); }
        }

        public ServerEntityKey StudyStorageKey
        {
            set { SubCriteria["StudyStorageKey"] = new ProcedureParameter<ServerEntityKey>("StudyStorageKey", value); }
        }
        public ServerEntityKey ServerPartitionKey
        {
            set { SubCriteria["ServerPartitionKey"] = new ProcedureParameter<ServerEntityKey>("ServerPartitionKey", value); }
        }

        public String StudyInstanceUid
        {
            set { SubCriteria["StudyInstanceUid"] = new ProcedureParameter<String>("StudyInstanceUid", value); }
        }

        public String SeriesInstanceUid
        {
            set { SubCriteria["SeriesInstanceUid"] = new ProcedureParameter<String>("SeriesInstanceUid", value); }
        }

        public String SopInstanceUid
        {
            set { SubCriteria["SopInstanceUid"] = new ProcedureParameter<String>("SopInstanceUid", value); }
        }

        public String SeriesDescription
        {
            set { SubCriteria["SeriesDescription"] = new ProcedureParameter<String>("SeriesDescription", value); }
        }

        public String GroupID
        {
            set { SubCriteria["GroupID"] = new ProcedureParameter<String>("GroupID", value); }
        }

        public String UidRelativePath
        {
            set { SubCriteria["UidRelativePath"] = new ProcedureParameter<String>("UidRelativePath", value); }
        }
	
        public XmlDocument StudyData
        {
            set { SubCriteria["StudyData"] = new ProcedureParameter<XmlDocument>("StudyData", value); }
        }
		public XmlDocument Details
        {
            set { SubCriteria["Details"] = new ProcedureParameter<XmlDocument>("Details", value); }
        }
    }
}
