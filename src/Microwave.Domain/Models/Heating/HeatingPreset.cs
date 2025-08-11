using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

public class HeatingPreset
{
    public HeatingPreset() { }  // Entity Framework 
    
    public HeatingPreset(
        string identifier,
        string name,
        string food,
        int durationInSeconds,
        byte? potency,
        string instructions
        )
    {
        Identifier = new HeatingPresetIdentifier(identifier);
        Name = name;
        Food = food;
        Duration = new PresetHeatingDuration(durationInSeconds);
        Potency = (HeatingPotency)HeatingPotency.Create(potency);
        Instructions = instructions;
    }
    
    public int Id { get; private set; }
    public HeatingPresetIdentifier Identifier { get; private set; }
    public string Name { get; private set; }
    public string Food { get; private set; }
    public PresetHeatingDuration Duration { get; private set; }
    public HeatingPotency Potency { get; private set; }
    public string Instructions { get; private set; }
}

public class HeatingPresetCollection
{
    private readonly List<HeatingPreset> _presets = [];

    public IReadOnlyCollection<HeatingPreset> Presets => _presets.AsReadOnly();

    public void Add(HeatingPreset preset)
    {
        if (_presets.Any(p => p.Identifier.Equals(preset.Identifier)))
            throw new DomainException(
                nameof(HeatingPreset), "Identifier already exists.");

        _presets.Add(preset);
    }
}