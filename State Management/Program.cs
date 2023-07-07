using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace State_Management
{
    public class Program
    {
        public const string NewLine = "\r\n";
        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 8080);
            tcpListener.Start();


            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                //int byteLength = 0;
                
                ProcessClient(client);
            }
        }


        public static async Task ProcessClient(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[4096];
                var length = stream.Read(buffer, 0, buffer.Length);

                string requestString = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(requestString);

                var sid = Guid.NewGuid().ToString();
                var match = Regex.Match(requestString, @"sid=[^\n]*\r\n");
                if (match.Success)
                {
                    sid = match.Value.Substring(4);
                }
                Console.WriteLine(sid);
                bool session = false;
                if (requestString.Contains("sid="))
                {
                    session= true;
                }

                //Thread.Sleep(5000);
                string html = $"<h1>Hello from Deyan at {DateTime.Now}</h1>" +NewLine + 
                    "<h2>Form</h2>" + NewLine+
                    "<form method=post><input type=username/> <input type=password/>" + NewLine +
                    " <input id=\"submit\" type=\"submit\" value=\"Submit\" class=\"btn\" /> </form>";


                string response = "HTTP/1.1 200 OK" + NewLine +
                    "Server: DidoServer2023" + NewLine +
                    "Content-Type:text/html; charset=utf-8" + NewLine +
                    "Set-Cookie: sid=12345678987654ghfjh; Path=/account; Domain=localhost; Expires= Thu, 22 Jun 2023 09:22:52 +0000" + NewLine +
                    "Content-Type: text/html; charset=utf-8" + NewLine +
                    "Content-Length: " + html.Length +NewLine +
                   NewLine + html;


                byte[] responseByBites = Encoding.UTF8.GetBytes(response);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                stream.Write(responseByBites);

                Console.WriteLine(new string('=', 70));
            }
        }
    }
}