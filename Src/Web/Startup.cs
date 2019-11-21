using System;
using System.Net.Http.Headers;
using AutoMapper;
using Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Models.Requests;
using Refit;
using Repositories.Forum;
using Services;
using Services.ClientWrappers;
using Web.Middlewares;

namespace Web
{
    public class Startup
    {
        private const string DatabaseKey = "Forum";
        private const string AuthorityKey = "Auth0:AuthorityKey";
        private const string AudienceKey = "Auth0:AudienceKey";
        private const string Auth0Endpoint = "BaseAddresses:Auth0";

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

            services.AddTransient<ReauthenticateAuth0Handler>();

            SetupAuth0Management(services);

            services
                .AddScoped<IForumRepository, ForumRepository>()
                .AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IReplyRepository, ReplyRepository>();

            services
                .AddScoped<IUserClientWrapper, UserClientWrapper>()
                .AddScoped<IForumService, ForumService>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<IReplyService, ReplyService>()
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
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration[AuthorityKey];
                options.Audience = Configuration[AudienceKey];
            });
        }

        private void SetupAuth0Management(IServiceCollection services)
        {
            var tokenRequest = new TokenRequest();
            Configuration.Bind("Auth0Management", tokenRequest);
            services.AddSingleton(tokenRequest);
            services.AddSingleton(new AccessToken());

            services
                .AddHttpClient("access token",
                    client => { client.BaseAddress = new Uri(Configuration[Auth0Endpoint]); })
                .AddTypedClient(RestService.For<IAccessTokenClient>);

            services
                .AddHttpClient("user", client => { client.BaseAddress = new Uri(Configuration[Auth0Endpoint]); })
                .AddHttpMessageHandler<ReauthenticateAuth0Handler>()
                .AddTypedClient(RestService.For<IUserClient>);
        }
    }
}
