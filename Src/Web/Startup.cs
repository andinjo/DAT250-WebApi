using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repositories;
using Services;

namespace Web
{
    public class Startup
    {
        private readonly string _connectionString;

        public Startup(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Forum");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<ForumContext>(options =>
                    options
                        .UseSqlServer(_connectionString)
                        .UseLazyLoadingProxies());

            services
                .AddScoped<IForumRepository, ForumRepository>();

            services
                .AddScoped<IForumService, ForumService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
