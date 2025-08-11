using Microwave.Domain.Enums;

namespace Microwave.Domain.Models.Heating;

public class Heating : BaseModel // TODO: transformar em classe abstrata, para comportamento preset ou manual
{
    public Heating(
        int? heatDurationInSeconds,
        int? potency)
    {
        HeatingTimer = new HeatingTimer(heatDurationInSeconds, DateTimeOffset.UtcNow);
        Potency = HeatingPotency.Create(potency);
    }

    private Heating() { }
    
    public int Id { get; private set; }
    public IHEatingPotency Potency { get; private set; }
    public HeatingTimer HeatingTimer { get; private set; }

    public HeatingStatus GetStatus() => HeatingTimer.IsHeating() ? HeatingStatus.Running : HeatingStatus.Paused;

    public static Heating FromPreset(HeatingPreset preset)
    {
        return new Heating
        {
            Potency = preset.Potency,
            HeatingTimer = HeatingTimer.FromPreset(preset.Duration.Value, DateTimeOffset.UtcNow)
        };
    }
}