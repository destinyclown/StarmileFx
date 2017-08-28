using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarmileFx.Models.Json;
using StarmileFx.Web.Handler;
using StarmileFx.Web.Server;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace StarmileFx.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.Configure<WebConfig>(Configuration.GetSection("WebConfig"));
            services.Configure<RedisModel>(Configuration.GetSection("Redis"));
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //.AddScheme<HasPasswordHandler, IAuthorizationHandler>("")
                .AddCookie(a =>
                {
                    a.LoginPath = new PathString("/home/login");
                    a.AccessDeniedPath = new PathString("/home/Forbidden");

                });
            // 添加应用程序服务。
            services.AddCoreServices();
            //services.AddSingleton<IAuthorizationHandler, HasPasswordHandler>();
            //services.AddSingleton<IAuthorizationHandler, HasAccessTokenHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
