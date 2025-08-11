using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Microwave.Api;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddIdentity(builder);

        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Microwave API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme());
            });
    }
    
    private static void AddIdentity(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var signingKey = builder.Configuration.GetValue<string>("Jwt:AccessToken");
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = new TimeSpan(0),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
                };
            });
    }
}