using Microsoft.Extensions.Configuration;
using Microwave.Domain.Models;
using Microwave.Infrastructure.Auth;

namespace Microwave.Infrastructure;

public static class AppSettingsService
{
    static AppSettingsService()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        JwtSettings = configuration.GetSection("Jwt").Get<TokenModel>() ?? new TokenModel();
    }

    public static TokenModel JwtSettings { get; }
}