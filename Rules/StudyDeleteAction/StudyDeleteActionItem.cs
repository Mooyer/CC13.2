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
using MatrixPACS.Common;
using MatrixPACS.Common.Specifications;
using MatrixPACS.Dicom.Utilities.Rules;
using MatrixPACS.ImageServer.Model;

namespace MatrixPACS.ImageServer.Rules.StudyDeleteAction
{
    public class StudyDeleteActionItem : ActionItemBase<ServerActionContext>
    {
        private static readonly FilesystemQueueTypeEnum _queueType = FilesystemQueueTypeEnum.DeleteStudy;
        private readonly Expression _exprScheduledTime;
        private readonly int _offsetTime;
        private readonly TimeUnit _units;

        public StudyDeleteActionItem(int time, TimeUnit unit)
            : this(time, unit, null)
        {
        }

        public StudyDeleteActionItem(int time, TimeUnit unit, Expression exprScheduledTime)
            : base("Study Delete action")
        {
            _offsetTime = time;
            _units = unit;
            _exprScheduledTime = exprScheduledTime;
        }

        protected override bool OnExecute(ServerActionContext context)
        {
            DateTime scheduledTime = Platform.Time;

            if (_exprScheduledTime != null)
            {
                scheduledTime = Evaluate(_exprScheduledTime, context, Platform.Time);
            }

            scheduledTime = CalculateOffsetTime(scheduledTime, _offsetTime, _units);

			context.CommandProcessor.AddCommand(
                new InsertFilesystemQueueCommand(_queueType, context.FilesystemKey, context.StudyLocationKey,
                                                 scheduledTime, null));
            return true;
        }
    }
}