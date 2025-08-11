using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

public readonly struct HeatingStartDateTime
{
    private readonly DateTimeOffset _value;

    public HeatingStartDateTime(DateTimeOffset dateTimeOffset)
    {
        if (_value > DateTimeOffset.UtcNow)
            throw new DomainException(nameof(HeatingStartDateTime), "Start time cannot be in in the future.");
        
        _value = dateTimeOffset;
    }
    
    public static explicit operator DateTimeOffset(HeatingStartDateTime heatingStartDateTime) => heatingStartDateTime._value;
}