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
        private const string DatabaseKey = "Forum";
        private const string AuthorityKey = "Auth0:AuthorityKey";
        private const string AudienceKey = "Auth0:AudienceKey";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddAuthentication(services);
            services.AddControllers();
            services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<ForumContext>(options =>
                    options
                        .UseSqlServer(Configuration.GetConnectionString(DatabaseKey)))
                .AddHttpContextAccessor()
                .AddScoped(s =>
                    s.GetService<IHttpContextAccessor>().HttpContext.User);

            services
                .AddScoped<IForumRepository, ForumRepository>()
                .AddScoped<IPostRepository, PostRepository>();

            services
                .AddScoped<IForumService, ForumService>()
                .AddScoped<IPostService, PostService>()
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
                options.Authority = Configuration[AuthorityKey];
                options.Audience = Configuration[AudienceKey];
            });
        }
    }
}
