using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Models;
using Repositories.Contexts;
using Repositories.Repositories;
using Services.BaseServices;

namespace DAT250_WebApi
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(ModelProfile))
                .AddDbContext<TodoContext>(options =>
                    options.UseSqlServer(_config.GetConnectionString("Database"))
                )
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddTransient<IItemRepository, ItemRepository>()
                .AddTransient<IItemService, ItemService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Temp
            app.UseCors(policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
