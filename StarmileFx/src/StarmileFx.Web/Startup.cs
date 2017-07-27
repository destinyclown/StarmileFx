using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System;
using StarmileFx.Models.Json;
using StarmileFx.Web.Middleware;
using StarmileFx.Server;

namespace StarmileFx.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //检查当前主机环境名称是否为“开发”。
            if (env.IsDevelopment())
            {
                //添加用户配置源的秘密。
                //builder.AddUserSecrets();

                // 这将通过应用程序的见解管道更快地推动遥测数据，让您立即查看结果。
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            //增加了一个microsoft.extensions.configuration.iconfigurationprovider读取环境变量的配置值。
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // 这个方法被运行时调用。使用此方法将服务添加到容器中。
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加框架服务。
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton<IConfiguration>(this.Configuration);

            //注册输入格式
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //注册Context
            //services.AddDbContext<BaseContext>(options => options.UseMySQL(Configuration.GetConnectionString("BaseConnection"), builder => builder.MigrationsAssembly("StarmileFx.Web")));
            //services.AddDbContext<InsuranceStudioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InsuranceStudioConnection"), builder => builder.MigrationsAssembly("StarmileFx.Web")));

            services.AddSession();
            services.AddMvc();

            //读取配置
            services.Configure<WebConfigModel>(Configuration.GetSection("WebConfig"));
            services.Configure<RedisModel>(Configuration.GetSection("Redis"));
            //services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));

            // 添加应用程序服务。
            services.AddCoreServices();
        }

        // 这种方法被运行时调用。用这个方法来配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug((c, l) => l >= LogLevel.Warning);
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                //捕获所有的程序异常错误，并将请求跳转至指定的页面，以达到友好提示的目的
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(30) });

            //开启静态文件也能走该Pipeline管线处理流程的功能。
            //app.UseStaticFiles();

            //不可查看静态文件目录
            app.UseFileServer();


            //自定义中间件
            //app.UseCustomMddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}
