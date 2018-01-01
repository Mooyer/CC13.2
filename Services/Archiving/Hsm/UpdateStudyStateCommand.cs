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

using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Enterprise.Command;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;

namespace MatrixPACS.ImageServer.Services.Archiving.Hsm
{
	public class UpdateStudyStateCommand : ServerDatabaseCommand
	{
		private readonly StudyStatusEnum _newStatus;
		private readonly ServerTransferSyntax _newSyntax;
		private readonly StudyStorageLocation _location;

		public UpdateStudyStateCommand(StudyStorageLocation location, StudyStatusEnum status, ServerTransferSyntax newSyntax) : base("Update the StudyState")
		{
			_location = location;
			_newStatus = status;
			_newSyntax = newSyntax;
		}

		protected override void OnExecute(CommandProcessor theProcessor, IUpdateContext updateContext)
		{
			// Update StudyStatusEnum in the StudyStorageTable
			IStudyStorageEntityBroker studyStorageUpdate = updateContext.GetBroker<IStudyStorageEntityBroker>();
			StudyStorageUpdateColumns studyStorageUpdateColumns = new StudyStorageUpdateColumns();
			studyStorageUpdateColumns.StudyStatusEnum = _newStatus;
			studyStorageUpdate.Update(_location.Key, studyStorageUpdateColumns);

			// Update ServerTransferSyntaxGUID in FilesystemStudyStorage
			IFilesystemStudyStorageEntityBroker filesystemUpdate = updateContext.GetBroker<IFilesystemStudyStorageEntityBroker>();
			FilesystemStudyStorageUpdateColumns filesystemUpdateColumns = new FilesystemStudyStorageUpdateColumns();
			filesystemUpdateColumns.ServerTransferSyntaxKey = _newSyntax.Key;
			filesystemUpdate.Update(_location.FilesystemStudyStorageKey, filesystemUpdateColumns);
		}
	}
}