using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.UserRepository;
using Services.AuthServices;
using Services.UserServices;
using System.Text;

namespace Services
{
    public static class MauserServiceRegistration
    {
        public static IServiceCollection AddMauserService(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>((options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                //if (env == "Development")
                //{
                //    options.EnableSensitiveDataLogging();
                //}
                options.UseNpgsql("name=Default");
            }));

            services.AddIdentity<AppUser, IdentityRole>()
           .AddEntityFrameworkStores<DataContext>()
           .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });



            return services;
        }
    }
}
