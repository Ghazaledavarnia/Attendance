using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
      
            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 6;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                });
            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});

            services.ConfigureApplicationCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    // If the LoginPath isn't set, ASP.NET Core defaults 
                    // the path to /Account/Login.
                    options.LoginPath = "/Account/Login";
                    // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                    // the path to /Account/AccessDenied.
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });

                // Add application services.
                services.AddTransient<IEmailSender, EmailSender>();
            services
            .AddMvc()
            .AddJsonOptions(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Add Kendo UI services to the services container
            services.AddKendo();

            services.AddMvc();
        
        }

        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //services.AddIdentity<ApplicationUser, IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultTokenProviders();

        //// Add application services.
        //services.AddTransient<IEmailSender, EmailSender>();

        //services.AddMvc();


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseBrowserLink();
        //        app.UseDatabaseErrorPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    app.UseStaticFiles();

        //    app.UseAuthentication();

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseKendo(env);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
