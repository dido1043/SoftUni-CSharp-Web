using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatus statusCode = HttpStatus.OK)
        {
            this.StatusCode = statusCode;
            this.Body = body;
            this.Header = new List<Header>
            {
                {new Header("Content-Type",contentType)},
                { new Header("Content-Length", body.Length.ToString())}
            };
        }

        public HttpStatus StatusCode { get; set; }
        public ICollection<Header> Header { get; set; }
        public byte[] Body { get; set; }

    }
}
