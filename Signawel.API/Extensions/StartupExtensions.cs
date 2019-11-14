using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Signawel.Business.Abstractions.Services;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Data.Repositories;
using Signawel.Domain;
using Signawel.Domain.Authentication;

namespace Signawel.API.Extensions
{
    public static class StartupExtensions
    {
        public static void UseDeveloperExceptionJsonResponse(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var exception = context.Features.Get<IExceptionHandlerFeature>()?
                        .Error;
                        if (exception != null)
                        {
                            var json = JsonConvert.SerializeObject(exception);
                            await context.Response.WriteAsync(json).ConfigureAwait(false);
                        }
                    }
                );
            });
        }

        public static IServiceCollection AddSignawelAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Read tokenconfiguration from appsettings
            services.Configure<TokenConfiguration>(configuration.GetSection("TokenSettings"));
            var tokenConfiguration = configuration.GetSection("TokenSettings").Get<TokenConfiguration>();

            services.AddIdentity<User, Role>(options =>
           {
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
               options.Lockout.MaxFailedAccessAttempts = 8;
               options.Lockout.AllowedForNewUsers = true;

               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequiredLength = 8;
               options.Password.RequireUppercase = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireDigit = true;
               options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
               options.User.RequireUniqueEmail = true;

               options.SignIn.RequireConfirmedEmail = false;
               options.SignIn.RequireConfirmedPhoneNumber = false;
           }).AddEntityFrameworkStores<SignawelDbContext>().AddDefaultTokenProviders();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.Secret)),
                ValidIssuer = tokenConfiguration.Issuer,
                ValidateIssuer = true,
                ValidAudience = tokenConfiguration.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();
            services.AddScoped<TokenConfiguration>();

            return services;
        }

        public static IServiceCollection AddSignawelServices(this IServiceCollection services)
        {
            services.AddScoped<IDeterminationRepository, DeterminationGraphRepository>();
            services.AddScoped<IDeterminationService, DeterminationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IRoadworkSchemaService, RoadworkSchemaService>();
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}