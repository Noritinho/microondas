using Microwave.Application.Providers;
using Microwave.Domain.Enums;
using Microwave.Domain.Models.Heating;

namespace Microwave.Application.Contracts.Responses;
public record HeatingPresetResponse
{
    public string Identifier { get; init; }

    public string Name { get; init; }

    public string Food { get; init; }

    public int Duration { get; init; }

    public byte Potency { get; init; }

    public string Instructions { get; init; }

    public HeatingPresetType Type { get; init; }

    public static IEnumerable<HeatingPresetResponse> FromDefaultHeatingPresetProvider(
        IHeatingPresetsProvider provider)
    {
        var defaultPresets = provider.Collection.Presets;

        foreach (var defaultPreset in defaultPresets)
            yield return FromHeatingPreset(defaultPreset, HeatingPresetType.Default);
    }

    public static HeatingPresetResponse FromHeatingPreset(HeatingPreset hp, HeatingPresetType type) //TODO: Refatorar pra tirar o type, talvez tornar heatingpreset abstrata
    {
        return new HeatingPresetResponse()
        {
            Identifier = hp.Identifier.Value,
            Name = hp.Name,
            Food = hp.Food,
            Duration = hp.Duration.Value,
            Potency = hp.Potency.Value,
            Instructions = hp.Instructions,
            Type = type
        };
    }
}