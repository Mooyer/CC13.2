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

using System.Collections.Generic;
using MatrixPACS.Dicom.Audit;
using MatrixPACS.Dicom.Network;
using MatrixPACS.Dicom.Network.Scu;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.Helpers;

namespace MatrixPACS.ImageServer.Services.Dicom
{
	public static class AssociationAuditLogger
	{
		public static void BeginInstancesTransferAuditLogger(List<StorageInstance> instances, AssociationParameters parms)
		{
			Dictionary<string, AuditPatientParticipantObject> list = new Dictionary<string, AuditPatientParticipantObject>();

			foreach (StorageInstance instance in instances)
			{
				string key = instance.PatientId + instance.PatientsName;
				if (!list.ContainsKey(key))
				{
					AuditPatientParticipantObject patient =
						new AuditPatientParticipantObject(instance.PatientsName, instance.PatientId);
					list.Add(key, patient);
				}
			}

			foreach (AuditPatientParticipantObject patient in list.Values)
			{
				// Audit Log
				BeginTransferringDicomInstancesAuditHelper audit =
					new BeginTransferringDicomInstancesAuditHelper(ServerPlatform.AuditSource,
					                                               EventIdentificationContentsEventOutcomeIndicator.Success,
					                                               parms, patient);

				foreach (StorageInstance instance in instances)
				{
					if (patient.PatientId.Equals(instance.PatientId)
					    && patient.PatientsName.Equals(instance.PatientsName))
					{
						audit.AddStorageInstance(instance);
					}
				}

				ServerAuditHelper.LogAuditMessage(audit);
			}
		}

		public static void InstancesTransferredAuditLogger(DicomScpContext context, ServerAssociationParameters assocParams, List<StorageInstance> instances)
		{
			Dictionary<string, AuditPatientParticipantObject> list = new Dictionary<string, AuditPatientParticipantObject>();

			foreach (StorageInstance instance in instances)
			{
				string key = instance.PatientId + instance.PatientsName;
				if (!list.ContainsKey(key))
				{
					AuditPatientParticipantObject patient =
						new AuditPatientParticipantObject(instance.PatientsName, instance.PatientId);
					list.Add(key, patient);
				}
			}

			foreach (AuditPatientParticipantObject patient in list.Values)
			{
				// Audit Log
				DicomInstancesTransferredAuditHelper helper =
					new DicomInstancesTransferredAuditHelper(ServerPlatform.AuditSource,
					                                         EventIdentificationContentsEventOutcomeIndicator.Success,
					                                         EventIdentificationContentsEventActionCode.E,
					                                         assocParams);

				foreach (StorageInstance instance in instances)
				{
					if (patient.PatientId.Equals(instance.PatientId)
					    && patient.PatientsName.Equals(instance.PatientsName))
					{
						helper.AddStorageInstance(instance);
					}
				}

                ServerAuditHelper.LogAuditMessage(helper);
			}
		}
	}
}
