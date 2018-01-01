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

using MatrixPACS.ImageServer.Core;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Services.Dicom
{
    /// <summary>
    /// Class used to pass parameters to the ImageServer SCP extensions.
    /// </summary>
    public class DicomScpContext
    {
        #region Constructors
        public DicomScpContext(ServerPartition partition)
        {
            Partition = partition;
        }
        #endregion

        #region Private Members

        #endregion

        #region Properties

		public Device Device { get; set; }

        public ServerPartition Partition { get; set; }

        public ServerPartitionAlternateAeTitle AlternateAeTitle { get; set; }

        public bool AllowStorage
        {
            get
            {
                if (AlternateAeTitle != null)
                    return AlternateAeTitle.AllowStorage;

                return true;
            }
        }

        public bool AllowQuery
        {
            get
            {
                if (AlternateAeTitle != null)
                    return AlternateAeTitle.AllowQuery;

                return true;
            }
        }

        public bool AllowRetrieve
        {
            get
            {
                if (AlternateAeTitle != null)
                    return AlternateAeTitle.AllowRetrieve;

                return true;
            }
        }

        public bool AllowKOPR
        {
            get
            {
                if (AlternateAeTitle != null)
                    return AlternateAeTitle.AllowKOPR;

                return false;
            }
        }

	    public SopInstanceImporterContext FileStreamImportContext
	    {
		    get; set;
	    }

        #endregion
    }
}