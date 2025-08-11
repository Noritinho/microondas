using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Microwave.Api;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddIdentity(builder);
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