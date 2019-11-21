using System;
using System.Net.Http.Headers;
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
using Clients;
using Repositories.Forum;
using Services;
using Services.ClientWrappers;

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

            services
                .AddHttpClient("user", client =>
                    {
                        client.BaseAddress = new Uri(Configuration[Auth0Endpoint]);
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ik1ESkRNemszUkRsQk1qSTVORVJHUVRsR1F6TTVRMFE0TURWRFFUaEJNa1pDTnpKRU56WTJRZyJ9.eyJpc3MiOiJodHRwczovL2Rldi1qN2xlbjl0ei5ldS5hdXRoMC5jb20vIiwic3ViIjoiN2dnMnVDWjdYSWtsVU01M0VxZEpiRmd5dUs4cGhFT2JAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGV2LWo3bGVuOXR6LmV1LmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNTc0MjY0MzIzLCJleHAiOjE1NzQzNTA3MjMsImF6cCI6IjdnZzJ1Q1o3WElrbFVNNTNFcWRKYkZneXVLOHBoRU9iIiwic2NvcGUiOiJyZWFkOnVzZXJzIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIn0.Si8fOlNkepKuMsb-lsLthrfeNKM1ekmWtt5GJ_eL3o_F78Q2NxwDisdXxr0OhL3MGYtZ1Ff1UHJgEWX2O2x4MQxNtBpK5IY-oZnYRdHCAABMAFqujXqWZBAg4eEoV9slg_CRHSRQb9SCHBS_q3obh397Xz21OPWohWGMDTg9PH_0wbfa3vPqO6WQ2LTlRq8ALFa-77hAU9GjRIaZFMuWRYYtxi3FO-YQywijbcnPMG19ZH7qLDKiRZ_ivK4GoIFHzRNWFt7R3I0BQpsUz2JXeSJ3Cddtpj9tOdfouO38BCM7Tk7SZQ0dNsOeTVmkGq0rzD20du9nrVhtCMfpqdgtYg");
                    })
                .AddTypedClient(Refit.RestService.For<IUserClient>);

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
            }).AddJwtBearer(options => {
                options.Authority = Configuration[AuthorityKey];
                options.Audience = Configuration[AudienceKey];
            });
        }
    }
}
