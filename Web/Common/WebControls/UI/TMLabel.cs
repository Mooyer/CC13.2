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
using System.ComponentModel;
using System.Web.UI;
using MatrixPACS.Dicom;
using MatrixPACS.Dicom.Utilities;
using MatrixPACS.ImageServer.Web.Common.Utilities;

namespace MatrixPACS.ImageServer.Web.Common.WebControls.UI
{
    /// <summary>
    /// Represents a text label control which displays a DICOM TM value on a Web page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use the <see cref="TMLabel"/> to display a time on a web page for a DICOM TM value. Unlike the TM value, the time displayed on the web page will
    /// be more user-friendly. The time to be displayed (TM value) is set through  the <see cref="Value"/> property. The format of the time can be set through the <see cref="DateTimeLabel.Format"/> property. If <see cref="DateTimeLabel.Format"/> 
    /// is not set, the date/time format will set to one of the followings, in the listed order:
    /// </para>
    /// 
    /// - The time format specified in the web configuration. 
    /// - The default time format for the UI culture specified in <globalization>
    /// - The default time format for the region and langugage of the system.
    /// 
    /// <para>
    /// If the TM value is empty or null, the text specified in <see cref="EmptyValueText"/> will be displayed. If the <see cref="Value"/> is an invalid TM value,
    /// the text specified in <see cref="InvalidValueText"/> will be displayed.
    /// </para>
    /// 
    /// </remarks>
    /// <example>
    /// </example>
    [DefaultProperty("Value")]
    [ToolboxData("<{0}:TMLabel runat=server></{0}:TMLabel>")]
    public class TMLabel : DateTimeLabel
    {

        /// <summary>
        /// The TM value for the time displayed on the web page.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public new string Value
        {
            get
            {
                return ViewState["ValueTM"] as string;
            }

            set
            {
                ViewState["ValueTM"] = value;
            }
        }


        /// <summary>
        /// The text to be displayed on the web page when the TM value is empty
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string EmptyValueText
        {
            get
            {
                return ViewState["EmptyValueText"] as string;
            }

            set
            {
                ViewState["EmptyValueText"] = value;
            }
        }

        

        /// <summary>
        /// The text to be displayed on the web page when the TM value is invalid
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string InvalidValueText
        {
            get
            {
                return ViewState["InvalidValueText"] as string;
            }

            set
            {
                ViewState["InvalidValueText"] = value;
            }
        }


        protected override string GetRenderedDateTimeText()
        {
            if (String.IsNullOrEmpty(Value))
                return EmptyValueText;

            DateTime? dt = TimeParser.Parse(Value);

            if (dt != null)
            {
                if (String.IsNullOrEmpty(Format))
                    return DateTimeFormatter.Format(dt.Value, DateTimeFormatter.Style.Time);
                else
                    return dt.Value.ToString(Format);
            }
            else
                return null;
           

        }

    }
}
