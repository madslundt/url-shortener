using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace datamodel
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            string postgres = Configuration["DataSettings:PostgresConnectionString"];
            string mssql = Configuration["DataSettings:MSSQLConnectionString"];

            if (!string.IsNullOrWhiteSpace(postgres))
            {
                services.AddDbContext<DataContext>(
                    options => options.UseNpgsql(postgres)
                );
            }
            else if (!string.IsNullOrWhiteSpace(mssql))
            {
                services.AddDbContext<DataContext>(
                    options => options.UseSqlServer(mssql)
                );
            }
            else
            {
                throw new Exception("No connection string provided in appsettings.");
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
