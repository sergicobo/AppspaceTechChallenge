using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AppspaceTechChallenge.Infrastructure.Context;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Services;
using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Domain.Repositories;
using AppspaceTechChallenge.Infrastructure.Proxies;
using AppspaceTechChallenge.Infrastructure.Repositories;
using System.IO;
using System.Reflection;
using System;

namespace AppspaceTechChallenge
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IBillboardService, BillboardService>();
            services.AddScoped<ITMDBProxy, TMDBProxy>();
            services.AddScoped<IBeezyCinemaRepository, BeezyCinemaRepository>();

            services.AddSwaggerGen(
                options =>
                {
                    options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                    options.TagActionsBy(api => new[] { api.GroupName });
                    options.IncludeXmlComments(XmlCommentsFilePath);
                }
            );

            services.AddDbContext<BeezyCinemaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BeezyCinema")));
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
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Appspace Tech Challenge v1");
                options.RoutePrefix = string.Empty;
            });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
