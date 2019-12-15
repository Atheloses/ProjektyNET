using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.DTOApp;
using WebApp.Models;

namespace WebApp
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
            services.AddScoped<UkolService>();
            services.AddScoped<SkupinaService>();
            services.AddScoped<UzivatelService>();
            services.AddScoped<UzivatelUkolService>();
            services.AddTransient<IUserStore<UzivatelApp>, UzivatelService>();
            services.AddTransient<IRoleStore<RoleApp>, RoleService>();
            services.AddIdentity<UzivatelApp,RoleApp>()
                .AddDefaultTokenProviders();

            services.AddRazorPages();
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/UzivatelUkol/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default3",
                    pattern: "xml",
                    defaults: new { controller = "Content", Action = "GetXml" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UzivatelUkol}/{action=Index}/{p_Id?}");
            });
        }
    }
}
