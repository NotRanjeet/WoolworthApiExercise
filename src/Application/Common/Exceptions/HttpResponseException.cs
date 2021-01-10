using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Woolworth.Application.Common.Exceptions
{
    public class HttpResponseException: Exception
    {
        public HttpResponseMessage ResponseMessage { get; set; }

        public HttpResponseException(HttpResponseMessage message)
        {
            ResponseMessage = message;
        }
    }
}
