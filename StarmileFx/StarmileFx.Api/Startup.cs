using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarmileFx.Models.Json;
using StarmileFx.Api.Server;
using NLog.Extensions.Logging;
using NLog.Web;
using StarmileFx.Api.Middleware;

namespace StarmileFx.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog("nlog.config");
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //读取配置
            services.Configure<EmailModel>(Configuration.GetSection("EmailConfig"));
            services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddMvc();
            services.AddCoreServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //注册输入格式
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            app.UseCors(builder => builder.WithOrigins("https://*").AllowAnyHeader());
            //自定义中间件
            app.UseCustomMddleware();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=api}/{action=Index}/{id?}");
            });
        }
    }
}
