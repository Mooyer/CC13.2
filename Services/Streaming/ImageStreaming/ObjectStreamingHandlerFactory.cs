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

using System.Net;
using MatrixPACS.ImageServer.Services.Streaming.ImageStreaming;
using MatrixPACS.ImageServer.Services.Streaming.ImageStreaming.Handlers;

namespace MatrixPACS.ImageServer.Services.Streaming.ImageStreaming
{
    /// <summary>
    /// Represents a factory that creates handler for streaming objects from the server to web clients.
    /// </summary>
    class ObjectStreamingHandlerFactory
    {
        /// <summary>
        /// Instantiates a handler based on the specified context.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IObjectStreamingHandler CreateHandler(HttpListenerRequest request)
        {
            // TODO: other type of objects (such as text, report) may be supported in the future
            return new ImageStreamingHandler();
        }
    }
}