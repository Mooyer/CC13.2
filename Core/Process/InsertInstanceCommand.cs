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
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;

namespace MatrixPACS.ImageServer.Core.Process
{
	/// <summary>
	/// ServerCommand for inserting a DICOM File into the persistent store.
	/// </summary>
	public class InsertInstanceCommand : ServerDatabaseCommand
	{
		#region Private Members

		private readonly DicomFile _file;
		private readonly StudyStorageLocation _storageLocation;
		private InstanceKeys _insertKey;
		#endregion

		#region Public Properties
		public InstanceKeys InsertKeys
		{
			get { return _insertKey; }
		}
		#endregion

		public InsertInstanceCommand(DicomFile file, StudyStorageLocation location)
			: base("Insert Instance into Database")
		{
			Platform.CheckForNullReference(file, "Dicom File object");
			Platform.CheckForNullReference(location, "Study Storage Location");

			_file = file;
			_storageLocation = location;
		}

		protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
		{
			// Setup the insert parameters
			var parms = new InsertInstanceParameters();
			_file.LoadDicomFields(parms);
			parms.ServerPartitionKey = _storageLocation.ServerPartitionKey;
			parms.StudyStorageKey = _storageLocation.Key;

            // Get any extensions that exist and process them
		    var ep = new ProcessorInsertExtensionPoint();
		    var extensions = ep.CreateExtensions();
            foreach (IInsertExtension e in extensions)
                e.InsertExtension(_storageLocation.ServerPartitionKey, parms, _file);
            
			// Get the Insert Instance broker and do the insert
			var insert = updateContext.GetBroker<IInsertInstance>();

			if (_file.DataSet.Contains(DicomTags.SpecificCharacterSet))
			{
				string cs = _file.DataSet[DicomTags.SpecificCharacterSet].ToString();
				parms.SpecificCharacterSet = cs;
			}

			_insertKey = insert.FindOne(parms);

			// If the Request Attributes Sequence is in the dataset, do an insert.
			if (_file.DataSet.Contains(DicomTags.RequestAttributesSequence))
			{
				var attribute = _file.DataSet[DicomTags.RequestAttributesSequence] as DicomAttributeSQ;
				if (attribute != null && !attribute.IsEmpty)
				{
					foreach (DicomSequenceItem sequenceItem in (DicomSequenceItem[]) attribute.Values)
					{
						var requestParms = new RequestAttributesInsertParameters();
						sequenceItem.LoadDicomFields(requestParms);
						requestParms.SeriesKey = _insertKey.SeriesKey;

						var insertRequest = updateContext.GetBroker<IInsertRequestAttributes>();
						insertRequest.Execute(requestParms);
					}
				}
			}
		}
	}
}