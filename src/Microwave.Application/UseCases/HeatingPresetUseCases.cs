using Microwave.Application.Contracts.Requests;
using Microwave.Application.Contracts.Responses;
using Microwave.Application.Providers;
using Microwave.Domain.Contracts.Requests;
using Microwave.Domain.Enums;
using Microwave.Domain.Extensions;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.Heating;

namespace Microwave.Application.UseCases;

public interface IHeatingPresetUseCases
{
    Task<CreateHeatingPresetResponse> CreateAsync(CreateHeantigPresetRequest request);
    IAsyncEnumerable<HeatingPresetResponse> GetPresetsAsync();
}

public class HeatingPresetUseCases(
    IDataService<HeatingPreset> dataService,
    IHeatingPresetsProvider heatingPresetsProvider) : IHeatingPresetUseCases
{
    public async Task<CreateHeatingPresetResponse> CreateAsync(CreateHeantigPresetRequest request)
    {
        var heatingPreset = 
            heatingPresetsProvider.
                Collection.Presets.FirstOrDefault(hp => hp.Identifier == request.Identifier) ?? 
            await dataService
                .GetByAsync(hp => hp.Identifier.Value == request.Identifier);

        if (heatingPreset != null)
            throw new InvalidOperationException("Heating preset already exists");
        
        heatingPreset = new HeatingPreset(
            request.Identifier,
            request.Name,
            request.Food,
            request.Duration,
            request.Potency,
            request.Instructions);
        
        await dataService.CreateAsync(heatingPreset);
        
        return new CreateHeatingPresetResponse()
        {
            Identifier = heatingPreset.Identifier.Value,
            Name = heatingPreset.Name,
            Food = heatingPreset.Food,
            Duration = TimeSpan.FromSeconds(heatingPreset.Duration.Value).ToTimeString(),
            Potency = heatingPreset.Potency.Value,
            Instructions = heatingPreset.Instructions,
        };
    }

    public async IAsyncEnumerable<HeatingPresetResponse> GetPresetsAsync()
    {
        foreach (var preset in GetDefaultPresets())
            yield return preset;

        await foreach (var preset in GetCustomPresets())
            yield return preset;
    }

    private IEnumerable<HeatingPresetResponse> GetDefaultPresets() => 
        HeatingPresetResponse.FromDefaultHeatingPresetProvider(heatingPresetsProvider);

    private async IAsyncEnumerable<HeatingPresetResponse> GetCustomPresets()
    {
        var customPresets = await dataService.GetAllAsync();

        foreach (var customPreset in customPresets)
            yield return HeatingPresetResponse.FromHeatingPreset(customPreset, HeatingPresetType.Custom);
    }
}