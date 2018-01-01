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
using System.IO;
using System.Threading;
using System.Xml;
using MatrixPACS.Common.Utilities;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.Dicom.Utilities.Xml;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Core.Command;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Services.WorkQueue.ProcessDuplicate
{
    internal class RemoveInstanceFromStudyXmlCommand : CommandBase, IAggregateCommand
    {
        #region Private Members

        private readonly StudyStorageLocation _studyLocation;
        private readonly string _seriesInstanceUid;
        private readonly string _sopInstanceUid;
        private readonly StudyXml _studyXml;
        private InstanceXml _instanceXml;
        private readonly Stack<ICommand> _aggregateCommands = new Stack<ICommand>();

        #endregion

        #region Public Properties

        public Stack<ICommand> AggregateCommands { get { return _aggregateCommands; } }

        #endregion

        #region Constructors

        public RemoveInstanceFromStudyXmlCommand(StudyStorageLocation location, StudyXml studyXml, string seriesInstanceUid, string sopInstanceUid)
            : base("RemoveInstanceFromStudyXmlCommand", true)
        {
            _studyLocation = location;
            _seriesInstanceUid = seriesInstanceUid;
            _sopInstanceUid = sopInstanceUid;
            _studyXml = studyXml;
        }

        #endregion

        #region Overridden Protected Methods

        protected override void OnExecute(CommandProcessor commandProcessor)
        {
            _instanceXml = _studyXml.FindInstanceXml(_seriesInstanceUid, _sopInstanceUid);

            _studyXml.RemoveInstance(_seriesInstanceUid, _sopInstanceUid);

            // flush it into disk
            // Write it back out.  We flush it out with every added image so that if a failure happens,
            // we can recover properly.
            if (!commandProcessor.ExecuteSubCommand(this, new SaveXmlCommand(_studyXml, _studyLocation)))
                throw new ApplicationException(commandProcessor.FailureReason);
        }

        protected override void OnUndo()
        {
            // No specific undo, rollback of the sub-commands handles it.
        }

        #endregion
    }
}