using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using src.Infrastructure;
using src.Infrastructure.Configuration;
using src.Infrastructure.Middleware;
using StructureMap;

namespace src
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment Env { get; set; }

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<UrlSettings>(Configuration.GetSection("UrlSettings"));

            string postgres = Configuration["DataSettings:PostgresConnectionString"];
            string mssql = Configuration["DataSettings:MSSQLConnectionString"];

            Console.WriteLine(postgres);
            Console.WriteLine(mssql);

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

            services.AddConfiguredMvc();
            services.AddConfiguredCors();

            IContainer container = new Container();
            container.Configure(config =>
            {
                config.AddRegistry<DefaultRegistry>();

                config.Populate(services);
            });


            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseDatabaseErrorPage();
            // }

            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: ""
                );
            });

        }
    }
}
