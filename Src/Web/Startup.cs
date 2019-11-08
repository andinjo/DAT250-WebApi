using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly string _authority;
        private readonly string _audience;

        public Startup(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Forum");
            _authority = configuration.GetValue<string>("Auth0:Authority");
            _audience = configuration.GetValue<string>("Auth0:Audience");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddAuthentication(services);
            services.AddControllers();
            services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<ForumContext>(options =>
                    options
                        .UseSqlServer(_connectionString)
                        .UseLazyLoadingProxies())
                .AddHttpContextAccessor()
                .AddScoped(s =>
                    s.GetService<IHttpContextAccessor>().HttpContext.User);

            services
                .AddScoped<IForumRepository, ForumRepository>();

            services
                .AddScoped<IForumService, ForumService>()
                .AddScoped<IUserService, UserService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = _authority;
                options.Audience = _audience;
            });
        }
    }
}
