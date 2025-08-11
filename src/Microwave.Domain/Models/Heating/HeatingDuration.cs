using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

[Owned]
public abstract class HeatingDuration
{
    public HeatingDuration() { } // ef
    
    protected const int DefaultDuration = 30;
    
    public static NoHeatingDuration None => new();
    public int Value { get; protected set; }

    public static HeatingDuration operator +(HeatingDuration me, HeatingDuration other)
    {
        var sum =  (int)(me.Value + other.Value);
        return CustomHeatingDuration.Create(sum);
    }
}

[Owned]
public class PresetHeatingDuration : HeatingDuration
{
    public PresetHeatingDuration() { } // Entity Framework

    public PresetHeatingDuration(int durationInSeconds)
    {
        Value = durationInSeconds;
    }
}

[Owned]
public class CustomHeatingDuration : HeatingDuration
{
    public CustomHeatingDuration() { } // Entity Framework

    public const byte MaxDurationInSeconds = 120;
    public const byte MinDurationInSeconds = 1;
    private CustomHeatingDuration(int durationInSeconds)
    {
        if (durationInSeconds is < 1 or > 120)
            throw new DomainException(nameof(CustomHeatingDuration), "Heat time must be between 1 and 120 seconds.");

        Value = durationInSeconds;
    }
    
    public static HeatingDuration Create(int? durationInSeconds) => durationInSeconds == null ? None : new CustomHeatingDuration(durationInSeconds.Value);
}

[Owned]
public class NoHeatingDuration : HeatingDuration
{
    public NoHeatingDuration()
    {
        Value = DefaultDuration;
    }
}