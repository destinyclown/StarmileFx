using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace StarmileFx.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:8001")
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))//正式使用
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
