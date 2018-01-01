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
using System.Collections.Generic;
using MatrixPACS.Common;
using MatrixPACS.Dicom;
using MatrixPACS.Dicom.Iod.Sequences;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.ImageServer.Core.Edit;

namespace MatrixPACS.ImageServer.Services.WorkQueue.ProcessDuplicate
{
    internal class UpdateDuplicateSopCommand : CommandBase
    {
        #region Private Members

        private readonly List<BaseImageLevelUpdateCommand> _commands;
        private readonly DicomFile _file;

        #endregion

        #region Constructors

        public UpdateDuplicateSopCommand(DicomFile file, List<BaseImageLevelUpdateCommand> commands)
            :base("Duplicate SOP demographic update command", true)
        {
            _file = file;
            _commands = commands;
        }

        #endregion

        #region Overridden Protected Methods

        protected override void OnExecute(CommandProcessor theProcessor)
        {
            if (_commands!=null)
            {
	            var sq = new OriginalAttributesSequence
		            {
			            ModifiedAttributesSequence = new DicomSequenceItem(),
			            ModifyingSystem = ProductInformation.Component,
			            ReasonForTheAttributeModification = "CORRECT",
			            AttributeModificationDatetime = Platform.Time,
			            SourceOfPreviousValues = _file.SourceApplicationEntityTitle
		            };

	            foreach (BaseImageLevelUpdateCommand command in _commands)
                {
                    if (!command.Apply(_file, sq))
                        throw new ApplicationException(
                            String.Format("Unable to update the duplicate sop. Command={0}", command));
                }

				var sqAttrib = _file.DataSet[DicomTags.OriginalAttributesSequence] as DicomAttributeSQ;
				if (sqAttrib != null)
					sqAttrib.AddSequenceItem(sq.DicomSequenceItem);
            }
            
        }

        
        protected override void OnUndo()
        {
        }

        #endregion

    }
}