using CoreTest.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

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

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
