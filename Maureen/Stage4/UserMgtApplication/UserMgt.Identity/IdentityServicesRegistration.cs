using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Identity;
using UserMgt.Application.Models.Identity;
using UserMgt.Identity.DbContext;
using UserMgt.Identity.Models;
using UserMgt.Identity.Services;

namespace UserMgt.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,
            IConfiguration configuration)
        {
            //adding the jwt configuration
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<UserMgtIdentityDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("UserMgtConnection")
                ));

            //adding the identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserMgtIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();

            //Configure authentication options
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration["JwtSettings:Key"]))
                };
            });

            return services;
        }
    }
}
