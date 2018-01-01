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

using System.Collections.Specialized;
using System.IO;
using System.Net;
using MatrixPACS.Dicom;

namespace MatrixPACS.ImageServer.Services.Streaming.ImageStreaming.Handlers
{
    public class ImageStreamingContext : StreamingContext
    {
        private DicomPixelData _pd;
        private string _serverAE;
        private int _frameNumber;

        public ImageStreamingContext(HttpListenerContext context)
        {
            Request = context.Request;
            Response = context.Response;
            NameValueCollection query = Request.QueryString;

            _frameNumber = 0;
            if (query["FrameNumber"] != null)
                int.TryParse(query["FrameNumber"], out _frameNumber);
        }
       
        
        public string ImagePath
        {
            get
            {
                return StorageLocation.GetSopInstancePath(SeriesInstanceUid, ObjectUid);
            }
        }
        
        public DicomPixelData PixelData
        {
            get
            {
                if (_pd == null)
                {
                    PixelDataManager manager = PixelDataManager.GetInstance(StorageLocation);
                    _pd = manager.GetPixelData(SeriesInstanceUid, ObjectUid);
                }
                return _pd;
            }
        }

        public bool IsMultiFrame
        {
            get
            {
                return PixelData.NumberOfFrames > 1;
            }
        }

        public string ServerAE
        {
            get { return _serverAE; }
            set { _serverAE = value; }
        }

        public int FrameNumber
        {
            get { return _frameNumber; }
            set { _frameNumber = value; }
        }

    }
}
