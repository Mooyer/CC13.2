using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Common.ExternalRequest;

namespace MatrixPACS.ImageServer.Model.EntityBrokers
{
    public partial class ExternalRequestQueueUpdateColumns
    {
        public ImageServerExternalRequestState SerializeState
        {           
            set { StateXml = ImageServerSerializer.SerializeExternalRequestStateToXmlDocument(value); }
        }

        public ImageServerExternalRequest SerializeRequest
        {
            set { RequestXml = ImageServerSerializer.SerializeExternalRequestToXmlDocument(value); }
        }
    }
}
