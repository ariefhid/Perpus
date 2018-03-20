using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using Perpus.Domain.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using Perpus.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace Perpus
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<PerpusContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PerpusContext"), sql => sql.MigrationsAssembly(migrationsAssembly)));

            DependencyServiceBinder.ConfigureServices(services, this.Configuration);

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Perpus", Version = "v1" });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //DatabaseInitializer.Initialize(app);

            if (env.IsDevelopment())
            {
                DatabaseInitializer.InitializeDevData(app);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Perpus");
                });
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                DatabaseInitializer.InitializeDevData(app);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Perpus");
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
