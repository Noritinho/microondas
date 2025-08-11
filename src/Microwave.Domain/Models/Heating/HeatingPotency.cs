using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

[Owned]
public abstract class IHEatingPotency
{
    public IHEatingPotency() { } // Entity Framework

    public static IHEatingPotency None => new NoHeatingPotency();
    public abstract byte Value { get; protected set; }
    
    public const byte MaxPotency = 10;
    public const byte MinPotency = 0;
    public const byte DefaultPotencty = 10;
}

[Owned]
public class HeatingPotency : IHEatingPotency
{
    public HeatingPotency() { } // Entity Framework

    public override byte Value { get; protected set; }

    private HeatingPotency(int potency)
    {
        if (potency is < IHEatingPotency.MinPotency or > IHEatingPotency.MaxPotency)
            throw new DomainException(nameof(HeatingPotency), "Potency must be between 0 and 10.");

        Value = (byte)potency;
    }
    
    public static explicit operator byte(HeatingPotency heatingPotency) => heatingPotency.Value;

    public static IHEatingPotency Create(int? potency) => potency == null ? IHEatingPotency.None : new HeatingPotency(potency.Value);
}

[Owned]
public class NoHeatingPotency : IHEatingPotency
{
    public NoHeatingPotency() { }

    public override byte Value { get; protected set; } = IHEatingPotency.DefaultPotencty;

    
    public static explicit operator byte(NoHeatingPotency heatingPotency) => heatingPotency.Value;
}