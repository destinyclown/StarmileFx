using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace StarmileFx.Api
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:8001")
                //.UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))//正式使用
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
