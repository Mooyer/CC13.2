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

using MatrixPACS.Dicom;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Core
{
    /// <summary>
    /// Defines the interface of the Pre-Processors to execute on a file
    /// </summary>
    internal interface IStudyPreProcessor
    {
        /// <summary>
        /// Called to process a DICOM file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>An instance of <see cref="InstancePreProcessingResult"/> containing the result of the processing. NULL if 
        /// the change has been made to the file.</returns>
        InstancePreProcessingResult Process(DicomFile file);

        /// <summary>
        /// Gets or sets the <see cref="StudyStorageLocation"/> of the study which the 
        /// DICOM file(s) belong to.
        /// </summary>
        StudyStorageLocation StorageLocation { get; set;}

        /// <summary>
        /// Gets or sets the description of the pre-processor.
        /// </summary>
        string Description
        {
            get;
            set;
        }
    }
}