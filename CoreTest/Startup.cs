using System;
using CoreTest.Context;
using CoreTest.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreTest
{
    public class Startup
    {
        /// <summary>
        /// The configurations
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add our database context into the IoC container.
            var driver = Configuration["DbDriver"];
            // Use SQLite?
            if (driver == "sqlite")
            {
                services.AddDbContext<CDEContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            }
            if (driver == "memory")
            {
                services.AddDbContext<CDEContext>(opt => opt.UseInMemoryDatabase(Configuration["memroyLabel"]));
            }
            if (driver == "mssql")
            {
                services.AddDbContextPool<CDEContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            SettupLogger(app, env, loggerFactory);

            app.UseDeveloperExceptionPage();


            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            app.UseMvc();
        }

        private void SettupLogger(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Serilog configuration          
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile(pathFormat: "logs\\log-{Date}.log")
                    .CreateLogger();

            loggerFactory
                .AddConsole()
                .AddSerilog();
        }
    }
}
