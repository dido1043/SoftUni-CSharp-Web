using SIS.HTTP;
using System.Text;

namespace FirstApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();


            server.AddRoute("/", HomePage);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);

            server.StartAsync(80);
        }

       

        static HttpResponse HomePage(HttpRequest request)
        {
            var responseHtml = "<h1>Welcome Bro!</h1>";
            
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            
            var response = new HttpResponse("text/html", responseBodyBytes);
            
            
            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));
            
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) 
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
            
            return response;    
        }
        static HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About...</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

            return response;
        }

        static HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login...</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

            return response;
        }
    }
}