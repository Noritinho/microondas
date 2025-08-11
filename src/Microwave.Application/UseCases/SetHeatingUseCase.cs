using Microwave.Application.Contracts.Requests;
using Microwave.Application.Providers;
using Microwave.Domain.Contracts.Requests;
using Microwave.Domain.Contracts.Responses;
using Microwave.Domain.Enums;
using Microwave.Domain.Exceptions;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.Heating;

namespace Microwave.Application.UseCases;

public interface ISetHeatingUseCase
{
    public SetHeatingResponse SetHeating(
        SetHeatingRequest request,
        int userId);

    Task<SetHeatingResponse> SetHeatingPreset(SetHeatingPresetRequest request, int userId);

    void PauseOrCancel(int userId);
}

internal class SetSetHeatingUseCase(
    ICache cache,
    IHeatingPresetsProvider heatingPresets,
    IDataService<HeatingPreset> dataService) : ISetHeatingUseCase
{
    public SetHeatingResponse SetHeating(SetHeatingRequest request, int userId)
    {
        var cachedHeating = cache.TryGetCachedValue<Heating>(userId);
        
        var heating = cachedHeating == null ?
            SetNewHeating(request) :
            AddHeatingDuration(cachedHeating, CustomHeatingDuration.Create((request.HeatingDuration)));
        
        cache.SetValue(userId, heating);
        
        return new SetHeatingResponse(
            (int)heating.HeatingTimer.GetTimeRemaining().TotalSeconds,
            heating.Potency.Value);
    }

    public async Task<SetHeatingResponse> SetHeatingPreset(SetHeatingPresetRequest request, int userId)
    {
        if (cache.TryGetCachedValue<Heating>(userId) != null)
            throw new DomainException(nameof(Heating),"Already heating.");

        var preset = heatingPresets.Collection.Presets
                         .FirstOrDefault(preset => preset.Identifier.Equals(request.Identifier)) ??
                     await dataService.GetByAsync(preset => preset.Identifier.Value == request.Identifier);
                     
        if (preset == null)
            throw new DomainException(nameof(HeatingPreset), "No preset found.");

        var heating = Heating.FromPreset(preset);
        
        cache.SetValue(userId, heating);

        return new SetHeatingResponse(
            (int)heating.HeatingTimer.GetTimeRemaining().TotalSeconds,
            heating.Potency.Value);
    }

    public void PauseOrCancel(int userId)
    {
        var heating = cache.TryGetCachedValue<Heating>(userId)
                      ?? throw new InvalidOperationException("Heating not found");

        switch (heating.GetStatus())
        {
            case HeatingStatus.Running:
                heating.HeatingTimer.PauseTimer();
                return;

            case HeatingStatus.Paused:
                heating.HeatingTimer.StopTimer();
                return;

            case HeatingStatus.Stopped:
                throw new ArgumentOutOfRangeException(nameof(heating), "Heating is already stopped.");

            default:
                throw new ArgumentOutOfRangeException(nameof(heating), "Unknown heating status.");
        }

        cache.SetValue(userId, heating);
    }
    
    private static Heating SetNewHeating(SetHeatingRequest request) => new(request.HeatingDuration, request.Potency);

    private static Heating AddHeatingDuration(Heating heating, HeatingDuration duration)
    {
        heating.HeatingTimer.AddHeatingDuration(duration);
        return heating;
    }
}