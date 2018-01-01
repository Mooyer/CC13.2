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
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common.ServiceModel;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.Utilities.Manifest;

namespace MatrixPACS.ImageServer.Services.Common.Misc
{
    [ServiceImplementsContract(typeof(IProductVerificationService))]
    [ExtensionOf(typeof(ApplicationServiceExtensionPoint),Enabled=true)]
    public class ProductVerificationService : IApplicationServiceLayer, IProductVerificationService
    {
        private readonly TimeSpan ManifestRecheckTimeSpan = TimeSpan.FromMinutes(15);
        private DateTime? _lastCheckTimestamp;

        public ProductVerificationResponse Verify(ProductVerificationRequest request)
        {
            if (_lastCheckTimestamp == null || (DateTime.Now - _lastCheckTimestamp.Value) > ManifestRecheckTimeSpan)
            {
                ManifestVerification.Invalidate();
            }

            _lastCheckTimestamp = DateTime.Now;

            return new ProductVerificationResponse
                       {
                            IsManifestValid = ManifestVerification.Valid,
                            ComponentName = ProductInformation.Component,
                            Edition = ProductInformation.Edition
                       };
        }
    }
}