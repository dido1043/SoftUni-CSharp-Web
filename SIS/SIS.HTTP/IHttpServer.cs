using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP
{
    public interface IHttpServer
    {
        void AddRoute(string path, Func<HttpReques, HttpResponse> action);
        void Start(int port);
    }
}
