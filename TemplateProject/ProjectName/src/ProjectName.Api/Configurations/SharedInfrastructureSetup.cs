using ProjectName.Application.Abstraction.Shared;
using ProjectName.Application.Common.Responses;
using ProjectName.Application.DTOs.Settings;
using ProjectName.Boilerplate.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace ProjectName.Api.Configurations;

public static class SharedInfrastructureSetup
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        //services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
        //services.AddTransient<IDateTimeService, SystemDateTimeService>();
        //services.AddTransient<IMailService, SMTPMailService>();
        //services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
    }

    public static void AddAuthenticationSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                c.Response.StatusCode = 401;
                                c.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("Token has expired"));
                                return c.Response.WriteAsync(result);
                            }
                            else
                            {
                                c.NoResult();
                                c.Response.StatusCode = 500;
                                c.Response.ContentType = "text/plain";
                                return c.Response.WriteAsync(c.Exception.ToString());
                            }
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
    }
}
