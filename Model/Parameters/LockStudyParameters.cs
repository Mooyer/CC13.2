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

using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model.Parameters
{
    public class LockStudyParameters: ProcedureParameters
    {
        public LockStudyParameters()
            : base("LockStudy")
        {
            // This is output from the stored procedure
            SubCriteria["Successful"] = new ProcedureParameter<bool>("Successful");
            SubCriteria["FailureReason"] = new ProcedureParameter<string>("FailureReason");  
        }

        public ServerEntityKey StudyStorageKey
        {
            set { SubCriteria["StudyStorageKey"] = new ProcedureParameter<ServerEntityKey>("StudyStorageKey", value); }
        }

        public bool WriteLock
        {
            set { SubCriteria["WriteLock"] = new ProcedureParameter<bool>("WriteLock", value); }
        }

		public bool ReadLock
		{
			set { SubCriteria["ReadLock"] = new ProcedureParameter<bool>("ReadLock", value); }
		}

		public QueueStudyStateEnum QueueStudyStateEnum
		{
			set { SubCriteria["QueueStudyStateEnum"] = new ProcedureParameter<ServerEnum>("QueueStudyStateEnum", value); }
		}

        public bool Successful
        {
            get
            {
                return (SubCriteria["Successful"] as ProcedureParameter<bool>).Value;
            }
        }

        public string FailureReason
        {
            get
            {
                return (SubCriteria["FailureReason"] as ProcedureParameter<string>).Value;
            }
        }
    }
}
