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
using System.Web.UI;
using MatrixPACS.ImageServer.Web.Common.Data.Model;

namespace MatrixPACS.ImageServer.Web.Application.Pages.Admin.Audit.DeletedStudies
{
    public partial class DeletedStudyDetailsDialogGeneralPanel : UserControl
    {
        #region Private Fields

        private DeletedStudyDetailsDialogViewModel _viewModel;

        #endregion

        #region Internal Properties

        internal DeletedStudyDetailsDialogViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        #endregion

        #region Public Methods

        public override void DataBind()
        {
            IList<DeletedStudyInfo> dataSource = new List<DeletedStudyInfo>();
            if (_viewModel != null)
                dataSource.Add(_viewModel.DeletedStudyRecord);
            StudyDetailView.DataSource = dataSource;
            base.DataBind();
        }

        #endregion
    }
}