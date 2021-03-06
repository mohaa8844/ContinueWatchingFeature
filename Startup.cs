using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContinueWatchingFeature.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContinueWatchingFeature.Models;
using Microsoft.Extensions.Options;
using ContinueWatchingFeature.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ContinueWatchingFeature
{
   
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});

            services.AddControllersWithViews();
            services.Configure<watchingsDatabaseSettings>(Configuration.GetSection(nameof(watchingsDatabaseSettings)));

            services.AddSingleton<IwatchingsDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<watchingsDatabaseSettings>>().Value);
            //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));

            services.AddSingleton<WatchingsService>();



            //services.AddDbContextPool<ContinueWatchingFeatureContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddMvc();
            services.AddDbContext<ContinueWatchingFeatureContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("ContinueWatchingFeatureContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //    endpoints.MapControllerRoute(
                //        name: "default",
                //        pattern: "{controller=Users}/{action=Index}");

                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
