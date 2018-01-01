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
using System.Linq;
using System.Reflection;
using MatrixPACS.Common;
using MatrixPACS.Common.Utilities;

namespace MatrixPACS.ImageServer.Common.ExternalRequest
{
    public static class ImageServerExternalRequestTypeProvider
    {
        private static readonly List<Type> KnownTypes = (from p in Platform.PluginManager.Plugins
                                                         from t in p.Assembly.Resolve().GetTypes()
                                                         let a = AttributeUtils.GetAttribute<ImageServerExternalRequestTypeAttribute>(t)
                                                         where (a != null)
                                                         select t).ToList();

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider ignored)
        {
            return KnownTypes;
        }

        public static Type[] GetTypeArray()
        {
            return KnownTypes.ToArray();
        }
    }
}
