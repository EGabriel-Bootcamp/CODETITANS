using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mauser.Extensions
{
    public static class JwtServiceRegistration
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

          // Adding Jwt Bearer
          .AddJwtBearer(options =>
          {
              options.SaveToken = true;
              options.RequireHttpsMetadata = false;
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidAudience = configuration["JwtConfig:ValidAudience"],
                  ValidIssuer = configuration["JwtConfig:ValidIssuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"]!))
              };
          });
        }

    }
}
