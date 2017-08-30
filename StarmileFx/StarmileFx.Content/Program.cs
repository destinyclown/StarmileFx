using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace StarmileFx.Content
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8004")
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))//正式使用
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
