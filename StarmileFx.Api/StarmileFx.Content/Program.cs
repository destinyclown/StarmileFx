using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace StarmileFx.Content
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8004")
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
