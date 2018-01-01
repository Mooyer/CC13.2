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
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.EntityBrokers;
using MatrixPACS.ImageServer.Web.Common.Data;

namespace MatrixPACS.ImageServer.Web.Common.WebControls.Validators
{
    public class PartitionArchiveValidator : BaseValidator
    {
        private string _originalAeTitle = "";

        public string OriginalAeTitle
        {
            get { return _originalAeTitle; }
            set { _originalAeTitle = value; }
        }

        protected override void RegisterClientSideValidationExtensionScripts()
        {
        }

        protected override bool OnServerSideEvaluate()
        {
            String aeTitle = GetControlValidationValue(ControlToValidate);

            if (String.IsNullOrEmpty(aeTitle))
            {
                ErrorMessage = "AE Title must be specified";
                return false;
            }

            if (OriginalAeTitle.Equals(aeTitle))
                return true;

            var controller = new ServerPartitionConfigController();
            var criteria = new ServerPartitionSelectCriteria();
            criteria.AeTitle.EqualTo(aeTitle);

            IList<ServerPartition> list = controller.GetPartitions(criteria);

            if (list.Count > 0)
            {
                ErrorMessage = String.Format(ValidationErrors.AETitleIsInUse, aeTitle);
                return false;
            }

            return true;
        }
    }
}