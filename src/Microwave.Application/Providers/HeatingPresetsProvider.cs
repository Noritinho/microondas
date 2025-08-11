using Microwave.Application.Contracts.Requests;
using Microwave.Application.Helpers;
using Microwave.Domain.Contracts.Requests;
using Microwave.Domain.Models.Heating;

namespace Microwave.Application.Providers;

public interface IHeatingPresetsProvider
{
    public HeatingPresetCollection Collection { get; }
}

public class HeatingPresetsProvider : IHeatingPresetsProvider
{
    public HeatingPresetsProvider()
    {
        Collection = MountFromJson();
    }
    
    public HeatingPresetCollection Collection { get; }

    private static HeatingPresetCollection MountFromJson()
    {
        var presets = JsonHelper.DeserializeFromFileTo<IEnumerable<CreateHeantigPresetRequest>>("HeatingPresets.json");
        var collection = new HeatingPresetCollection();
        
        foreach (var preset in presets)
            collection.Add(new HeatingPreset(
                preset.Identifier,
                preset.Name,
                preset.Food,
                preset.Duration,
                preset.Potency,
                preset.Instructions));
        
        return collection;
    }
}