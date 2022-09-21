using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AppSpaceTechChallenge.Infrastructure.Context;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Services;

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

            services.AddSwaggerGen(
                options =>
                {
                    options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                    options.TagActionsBy(api => new[] { api.GroupName });
                }
            );

            services.AddDbContext<BeezyCinemaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BeezyCinemaCNX")));
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
    }
}
