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
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;

namespace StarmileFx.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog("nlog.config");
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //读取配置
            services.Configure<EmailModel>(Configuration.GetSection("EmailConfig"));
            services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddMvc();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "StarmileFx.Api",
                    Description = "服务于StarmileFx系统的Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "starmile", Email = "starmilefx@163.com", Url = "" }
                    //License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "StarmileFx.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });
            services.AddCoreServices();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
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
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarmileFx.Api V1");
            });
        }
    }
}
