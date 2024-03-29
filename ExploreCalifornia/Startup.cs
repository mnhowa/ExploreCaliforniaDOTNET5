using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            // ^ configuration will be populated by ASP.NET core when it runs
            this.configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // AddTransient instances will have the shortest lifespan, as .NET Core will create a new instance
            // every time it is requested

            // AddScoped instances will tell .NET Core to only create one instance per each web request
            // Good for keeping instances separate per user request

            // AddSingleton instance will create one instance for the entire lifetime of the application
            // Good for common/shared data


            services.AddTransient<FeatureToggles>(x => new FeatureToggles {
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
            });

            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<FormattingService>();


            // Configures Identity middleware
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

            // Configures Mvc middleware
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            FeatureToggles features)
        {
            if (features.DeveloperExceptions)
            {
                // Note that if a configuration doesn't exist, it will return false

                // shows a nice page with diagnostics about the exception
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Tells .net core to log the exception and redirect to provided path
                app.UseExceptionHandler("/error.html");
            }

            app.UseRouting();


            // Tells to map any unhandled request to the wwwroot/static folder
            app.UseFileServer();

            // Tells .net core to use authorization
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("/invalid", context =>
                {
                    throw new Exception("ERROR!");
                });

                // default controller part of url to home
                // default action part of url to index
                // default parameter assignment of first value to id
                // id must be int, but can be optional
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id:int?}");
            });
        }
    }
}
