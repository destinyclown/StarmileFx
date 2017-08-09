using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarmileFx.Models.Json;
using StarmileFx.Web.Server;
using System;

namespace StarmileFx.Web
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSession();
            services.Configure<WebConfigModel>(Configuration.GetSection("WebConfig"));
            services.Configure<RedisModel>(Configuration.GetSection("Redis"));
            services.AddMvc();
            services.AddAuthorization();
            // 添加应用程序服务。
            services.AddCoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/home/Login"),
                AccessDeniedPath = new PathString("/home/Error"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug((c, l) => l >= LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(30) });
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
