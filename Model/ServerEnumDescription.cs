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

using System.Resources;
using MatrixPACS.Common;
using MatrixPACS.ImageServer.Enterprise;

namespace MatrixPACS.ImageServer.Model
{
    [ExtensionOf(typeof(ServerEnumExtensionPoint))]
    public class ServerEnumDescription : IServerEnumDescriptionTranslator
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("MatrixPACS.ImageServer.Model.ServerEnumDescriptions", typeof(ServerEnumDescription).Assembly);

        
        public static string GetLocalizedDescription(ServerEnum enumValue)
        {
            return GetLocalizedText(GetDescriptionResKey(enumValue.Name, enumValue.Lookup));
        }

        public static string GetLocalizedLongDescription(ServerEnum enumValue)
        {
            return GetLocalizedText(GetLongDescriptionResKey(enumValue.Name, enumValue.Lookup));
        }

        #region IServerEnumDescriptionTranslator implementation

        string IServerEnumDescriptionTranslator.GetLocalizedLongDescription(ServerEnum serverEnum)
        {
            return GetLocalizedLongDescription(serverEnum);
        }

        string IServerEnumDescriptionTranslator.GetLocalizedDescription(ServerEnum serverEnum)
        {
            return GetLocalizedDescription(serverEnum);
        }

        #endregion

        #region Helper Methods


        static private string GetDescriptionResKey(string enumName, string enumLookupValue)
        {
            // This should match what's in ResxGenerator
            return string.Format("{0}_{1}_Description", enumName, enumLookupValue);
        }
        static private string GetLongDescriptionResKey(string enumName, string enumLookupValue)
        {
            // This should match what's in ResxGenerator
            return string.Format("{0}_{1}_LongDescription", enumName, enumLookupValue);
        }

        static private string GetLocalizedText(string key)
        {
            return _resourceManager.GetString(key);
        }

        #endregion

    }
}
