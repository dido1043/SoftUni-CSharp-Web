using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SIS.HTTP
{
    public class HttpServer : IHttpServer
    {
        
        IDictionary<string, Func<HttpRequest,HttpResponse>> routeTable =
            new Dictionary<string, Func<HttpRequest,HttpResponse>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routeTable.ContainsKey(path))
            {
                routeTable[path] = action;
            }
            else
            {
                routeTable.Add(path, action);
            }
        }

        public async Task StartAsync(int port)
        {
            TcpListener listener = 
                new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();

                List<byte> data = new List<byte>();
                int position = 0;
                byte[] buffer = new byte[4092];
                //byte[] data = new byte[0];
                while (true)
                {
                    int count = await stream.ReadAsync(buffer, position, buffer.Length);
                    position += count;
                    if (count == 0)
                    {
                        break;
                    }
                    if (count < buffer.Length)
                    {
                        var partialBuffer = new byte[count];
                        Array.Copy(buffer, partialBuffer, count);
                        data.AddRange(partialBuffer);
                        break;
                    }
                    else
                    {
                        data.AddRange(buffer);
                    }


                }

                var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                var request = new HttpRequest(requestAsString);
                Console.WriteLine(requestAsString);

                var responseHtml = "<h1>Welcome</h1>" +
                    request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;

                var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

                var resonseHttp = "HTTP/1.1 200 ok" + HttpConstants.NewLine +
                    "Server: SIS Server 1.0" + HttpConstants.NewLine +
                    "Content-Type: text/html" + HttpConstants.NewLine +
                    "Content-Length: " + responseBodyBytes.Length + HttpConstants.NewLine +
                    HttpConstants.NewLine;

                var responseHeaderBytes = Encoding.UTF8.GetBytes(resonseHttp);
                await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await stream.WriteAsync(responseBodyBytes, 0, responseBodyBytes.Length);

                client.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
        
    }
}
