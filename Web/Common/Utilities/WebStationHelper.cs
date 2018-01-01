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
using System.Web;
using MatrixPACS.Common;

namespace MatrixPACS.ImageServer.Web.Common.Utilities
{
    public interface IWebStationHelper
    {
        string GetSplashScreenXamlAbsolutePath();
    }

    [ExtensionPoint]
    public sealed class WebStationHelperExtensionPoint:ExtensionPoint<IWebStationHelper>{}

    public static class EmbeddedWebStationHelper
    {
        private static readonly IWebStationHelper _helper;

        static EmbeddedWebStationHelper()
        {
            try
            {
                _helper = new WebStationHelperExtensionPoint().CreateExtension() as IWebStationHelper;
            }
            catch(Exception){
                // ignore
            }
        }

        public static string GetSplashScreenXamlAbsolutePath()
        {
            if (_helper==null)
            {
                return GetDefaultSplashScreenXamlAbsolutePath();
            }
            else
            {
                return _helper.GetSplashScreenXamlAbsolutePath();
            }
        }

        public static string GetDefaultSplashScreenXamlAbsolutePath()
        {
            string path = string.Format("~/App_Themes/{0}/WebStation/SplashScreen.xaml", ThemeManager.CurrentTheme);
            return VirtualPathUtility.ToAbsolute(path);
        }
    }
}