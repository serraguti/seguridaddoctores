using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeguridadDoctores.Data;
using SeguridadDoctores.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //TEMPDATA
            services.AddSingleton<ITempDataProvider,
                CookieTempDataProvider>();
            services.AddSession();
            //AUTHENTICATION
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();
            //ACCESO A DATOS
            String cadena = this.Configuration.GetConnectionString("conexionhospitalsql");
            services.AddTransient<RepositoryDoctores>();
            services.AddDbContext<DoctoresContext>(options =>
            options.UseSqlServer(cadena));

            //VIEWS AND CONTROLLERS Y TEMPDATA
            services.AddControllersWithViews
                (options => options.EnableEndpointRouting = false)
                .AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default"
                    , template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
