using Microsoft.Extensions.DependencyInjection;
using Microwave.Application.Providers;
using Microwave.Application.UseCases;

namespace Microwave.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        services.AddSingleton<IHeatingPresetsProvider, HeatingPresetsProvider>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ISetHeatingUseCase, SetSetHeatingUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRegisterUseCase, RegisterUseCase>();
        services.AddScoped<IHeatingPresetUseCases, HeatingPresetUseCases>();
    }
}