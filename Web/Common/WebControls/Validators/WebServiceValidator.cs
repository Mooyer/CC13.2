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

namespace MatrixPACS.ImageServer.Web.Common.WebControls.Validators
{
    /// <summary>
    /// Provide validation functionality which is carried out by a Web Service.
    /// </summary>
    /// <remarks>
    /// The Web service must have [ScriptService] attribute in order to be accessible by the javascript generated by this control.
    /// The web service url is specified in <see cref="ServicePath"/> and the method to called is specified by <see cref="ServiceOperation"/>.
    /// For client-side validation, the parameter(s) which will be passed to the web service is generated by the javascript method whose name
    /// is specified in <see cref="ParamsFunction"/>. For server-side validation, a derived class must implement <see cref="BaseValidator.OnServerSideEvaluate"/>.
    /// </remarks>
    public class WebServiceValidator : BaseValidator
    {
        #region Private members

        private string _serviceOperation = "Not Specified";
        private string _servicePath = "Not Specified";

        #endregion Private members

        #region Public Properties

        /// <summary>
        /// The relative path to the asmx web service 
        /// eg. /Services/ValidationServices.asmx
        /// </summary>
        public string ServicePath
        {
            get { return _servicePath; }
            set { _servicePath = value; }
        }


        /// <summary>
        /// Name of the service operation
        /// </summary>
        public string ServiceOperation
        {
            get { return _serviceOperation; }
            set { _serviceOperation = value; }
        }

        /// <summary>
        /// Name of the javascript function used to create the parameter object passed to the service.
        /// </summary>
        /// 
        /// <example>
        /// The following example shows the javascript function that create the parameter object
        /// which contains a single parameter 'path' (as specified in ValidateFilesystemPath argument).
        /// The value for this parameter is taken from the text box with ID of 'PathTextBox'.
        /// 
        /// 
        /// Web service code:
        /// <code>
        /// [WebMethod]
        /// public ValidationResult ValidateFilesystemPath(string path)
        /// {
        ///     .....
        /// }
        /// </code>
        /// 
        /// Javascript code:
        /// 
        /// <code>
        ///  function CreateParams()
        ///  {
        ///     control = document.getElementById('PathTextBox');
        ///     params = new Array();
        ///     params.path=control.value;
        ///  
        ///     return params;
        ///  }
        ///  </code>
        /// 
        /// ASPX code:
        /// 
        /// <code>
        /// <MatrixPACS:WebServiceValidator 
        ///       runat="server" ID="PathValidator" 
        ///       ControlToValidate="PathTextBox" 
        ///       InvalidInputBackColor="#FAFFB5" 
        ///       ServicePath="/Services/ValidationServices.asmx" 
        ///       ServiceOperation="ValidateFilesystemPath" 
        ///       ParamsFunction="CreateParams"
        ///       Display="None" 
        ///       ValidationGroup="vg1" />
        /// </code>
        /// 
        /// </example>
        public string ParamsFunction { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected string ServiceURL
        {
            get
            {
                string baseUrl = Page.Request.Url.Scheme + "://" + Page.Request.Url.Authority +
                                 Page.Request.ApplicationPath.TrimEnd('/');
                return baseUrl + Page.ResolveUrl(ServicePath);
            }
        }

        #endregion Protected Properties

        #region Protected Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (EnableClientScript)
            {
                RegisterWebServiceInitScripts();
            }
        }


        protected void RegisterWebServiceInitScripts()
        {
            var template = new ScriptTemplate(GetType().Assembly,
                                              "MatrixPACS.ImageServer.Web.Common.WebControls.Validators.WebServiceValidator_Init.js");
            template.Replace("@@WEBSERVICE_URL@@", ServiceURL);
            Page.ClientScript.RegisterStartupScript(GetType(), "WebServiceInit", template.Script, true);
        }


        protected override void RegisterClientSideValidationExtensionScripts()
        {
            var template = new ScriptTemplate(this,
                                              "MatrixPACS.ImageServer.Web.Common.WebControls.Validators.WebServiceValidator.js");
            template.Replace("@@WEBSERVICE_OPERATION@@", ServiceOperation);
            template.Replace("@@WEBSERVICE_URL@@", ServiceURL);
            template.Replace("@@PARAMETER_FUNCTION@@", ParamsFunction);

            Page.ClientScript.RegisterClientScriptBlock(GetType(), ClientID + "_ValidatorClass", template.Script, true);
        }

        protected override bool OnServerSideEvaluate()
        {
            return true;
        }

        #endregion Protected Methods
    }
}