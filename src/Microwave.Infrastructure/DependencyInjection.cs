using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave.Domain.Interfaces;
using Microwave.Infrastructure.Auth;
using Microwave.Infrastructure.Data;

namespace Microwave.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddSingleton<ICache, Cache>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserContext, UserContext>();
        
        services.AddDbContext<MicroWaveDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        
        services.AddScoped(typeof(IDataService<>), typeof(DataService<>));
    }
}