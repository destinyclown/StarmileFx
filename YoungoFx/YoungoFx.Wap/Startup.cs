using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarmileFx.Models.Json;
using StarmileFx.Wap.Middleware;
using YoungoFx.Web.Server;

namespace YoungoFx.Wap
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // 添加框架服务。
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSession();
            services.AddMvc();

            //读取节点为WebConfig的配置
            services.Configure<WebConfigModel>(Configuration.GetSection("WebConfig"));
            services.Configure<RedisModel>(Configuration.GetSection("Redis"));
            services.UserMongoLog(Configuration.GetSection("Mongo.Log"));
            //services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));

            // 添加应用程序服务。
            services.AddCoreServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //自定义中间件
            app.UseCustomMddleware();
            app.UseStaticFiles();
            //不可查看静态文件目录
            app.UseFileServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
