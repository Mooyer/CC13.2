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

namespace MatrixPACS.ImageServer.Common.Exceptions
{
	public class StudyNotFoundException : SopInstanceProcessingException
	{
		public StudyNotFoundException(string studyInstanceUid)
			   : base("Study not found: " + studyInstanceUid)
        {
			StudyInstanceUid = studyInstanceUid;
        }

        /// <summary>
        /// The study instance UID of the study that is nearline.
        /// </summary>		
        public string StudyInstanceUid { get; set; }
	}
}
