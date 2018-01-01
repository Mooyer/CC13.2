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
using System.Net;
using System.Web;
using MatrixPACS.Common;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.Server.ShredHost;

namespace MatrixPACS.ImageServer.Core
{
    /// <summary>
    /// Represents a http server that accepts and processes http requests.
    /// </summary>
    public abstract class HttpServer : HttpListenerShred
    {
        private readonly string _name;
        
        /// <summary>
        /// Creates an instance of <see cref="HttpServer"/> on a specified address.
        /// </summary>
        /// <param name="serverName">Name of the Http server</param>
        /// <param name="port"></param>
        /// <param name="path"></param>
        protected HttpServer(string serverName, int port, string path) : 
            base(port, path)
        {
            _name = serverName;
        }

        #region Overridden Public Methods

        public override void Start()
        {
        	StartListeningAsync(ListenerCallback);
        }

		protected override void OnStartError(string message)
		{
			ServerPlatform.Alert(AlertCategory.Application, AlertLevel.Critical, GetDisplayName(), AlertTypeCodes.UnableToStart, null, TimeSpan.Zero, message);
		}

        protected abstract void HandleRequest(HttpListenerContext context);

        #endregion

        #region Private Methods
        /// <summary>
        /// Handles incoming http request asynchronously
        /// </summary>
		private void ListenerCallback(HttpListenerContext context)
        {
            try
            {
				if (Platform.IsLogLevelEnabled(LogLevel.Debug))
				{
					Platform.Log(LogLevel.Debug, "Handling http request");
					Platform.Log(LogLevel.Debug, "{0}", context.Request.Url.AbsoluteUri);
				}

                HandleRequest(context);
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, e, "Error while handling http request:");

                if (context!=null)
                {
                    try
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.StatusDescription = e.InnerException != null ? HttpUtility.HtmlEncode(e.InnerException.Message) : HttpUtility.HtmlEncode(e.Message);
                    }
                    catch(Exception ex)
                    {
                        Platform.Log(LogLevel.Error, ex, "Unable to set response status description");
                    }
                }
            }
        }
        #endregion

    }
}


