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

using MatrixPACS.Common;
using MatrixPACS.Dicom;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Core.Process
{
	public interface IInsertExtension
	{
		void InsertExtension(ServerEntityKey partitionKey, InsertInstanceParameters parameters, DicomFile theFile);
        void UpdateExtension(ServerEntityKey partitionKey, PatientUpdateColumns patientUpdate, StudyUpdateColumns studyUpdate, SeriesUpdateColumns seriesUpdate, DicomFile theFile);
	}

	public class ProcessorInsertExtensionPoint : ExtensionPoint<IInsertExtension>
	{
	}
}
