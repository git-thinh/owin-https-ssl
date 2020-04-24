using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace owin452
{
    public class HelloController : ApiController
    {
        // GET: api/hello
        public IHttpActionResult Get()
        {
            return Ok("Hello Web API Self Host");
        }
    }

    public class Startup {
        public void Configuration(IAppBuilder app)
        {
            // Configure web api routing
            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    "DefaultApi",
            //    "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional });
            //app.UseWebApi(config);

            app.Run(ctx =>
            {
                var file = "";
                if (ctx.Request.Uri.AbsolutePath == "/Content/anduong.jpg")
                {
                    ctx.Response.ContentType = "image/jpg";
                    ctx.Response.StatusCode = 200;
                    file = ConfigurationManager.AppSettings["IMG_PATH"];
                    var buffer = File.ReadAllBytes(file);


                    return ctx.Response.WriteAsync(buffer);
                }
                file = ConfigurationManager.AppSettings["HTML_PATH"];
                var html = File.ReadAllText(file);
                ctx.Response.ContentType = "text/html";
                return ctx.Response.WriteAsync(html);
                //return ctx.Response.WriteAsync("<!DOCTYPE html><html lang=\"en\"><head><meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\" /><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta name=\"safesai-verification\" content=\"SAFESAIdotCOMverification\" /><title>Trang chủ</title><meta name=\"description\" content=\"mo ta\" /><meta name=\"keywords\" content=\"Keywords\" /><meta property=\"og:site_name\" content=\"Trang tin F88\"><meta property=\"og:url\" content=\"https://test-web.f88.vn\" /><meta property=\"og:type\" content=\"article\" /><meta property=\"og:description\" content=\"Mô tả\"><meta property=\"og:image\" content=\"Content/anduong.jpg\"><meta property=\"og:image:secure_url\" content=\"Content/anduong.jpg\"><meta property=\"og:image:type\" content=\"image/jpg\"></head><body><h1>TEST FACEBOOK THUMBNAIL</h1> <img src=\"/Content/anduong.jpg\"></body></html>");
            });

            // Add welcome page
            //app.UseWelcomePage();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var PORT_API = ConfigurationManager.AppSettings["DOMAIN_LOCALHOST"];
            StartOptions options = new StartOptions();
            options.Urls.Add(PORT_API);
            //options.Urls.Add("https://localhost:4430/");
            //options.Urls.Add("https://127.0.0.1:4431/");
            //options.Urls.Add("https://192.168.8.90:4435/");
            //options.Urls.Add("https://cms.f88.vn:4432/");

            options.Urls.Add("http://localhost:54320/");
            options.Urls.Add("http://127.0.0.1:54321/");

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Web app started:");
                foreach (var uri in options.Urls) Console.WriteLine(uri);
                Console.ReadLine();
            }
        }
    }
}
