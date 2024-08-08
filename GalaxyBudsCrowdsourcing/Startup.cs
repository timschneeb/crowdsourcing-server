using System;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;

namespace GalaxyBudsCrowdsourcing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { private set; get; } = null!;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExperimentContext>(opt =>
                opt.UseMySql(Configuration["Connection"],
                    new MySqlServerVersion(new Version(8, 0, 21))) // use MariaDbServerVersion for MariaDB
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());
            
            services.AddDbContext<ResultContext>(opt =>
                opt.UseMySql(Configuration["Connection"],
                        new MySqlServerVersion(new Version(8, 0, 21))) // use MariaDbServerVersion for MariaDB
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());
            
            services.AddDbContext<CoredumpContext>(opt =>
                opt.UseMySql(Configuration["Connection"],
                        new MySqlServerVersion(new Version(8, 0, 21))) // use MariaDbServerVersion for MariaDB
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());

            services.AddRazorPages()
                .AddNewtonsoftJson(options =>
                { 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                }); 
            
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                { 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                }); 
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}