using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace StarmileFx.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                //.UseKestrel(option => {
                //    option.UseHttps("server.pfx", "linezero");
                //})
                .UseKestrel()
                .UseUrls("http://*:8001")
                //.UseUrls("https://*:443")//正式使用
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))//正式使用
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
