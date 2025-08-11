using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

[Owned]
public class HeatingPresetIdentifier : IEquatable<HeatingPresetIdentifier>
{
    public const string IdentifierEqualsDOt = "Identifier cannot be equal '.'";
    public const string IdentifierNull = "Identifier cannot be empty '.'";
    public HeatingPresetIdentifier() { } // Entity Framework

    public string Value { get; }

    public HeatingPresetIdentifier(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new DomainException(nameof(HeatingPresetIdentifier), IdentifierNull);
        
        if (identifier.Equals("."))
            throw new DomainException(nameof(HeatingPresetIdentifier), IdentifierEqualsDOt);

        Value = identifier;
    }
    
    public static explicit operator string(HeatingPresetIdentifier heatingPresetIdentifier) => heatingPresetIdentifier.Value;

    public bool Equals(HeatingPresetIdentifier other)
    {
        return Value == other.Value;
    }
    
    public bool Equals(string other)
    {
        return Value == other;
    }

    public static bool operator ==(HeatingPresetIdentifier left, HeatingPresetIdentifier right) =>
        left.Equals(right);

    public static bool operator ==(HeatingPresetIdentifier left, string right) =>
        left.Equals(right);

    public static bool operator !=(HeatingPresetIdentifier left, string right) =>
        left.Equals(right);

    public static bool operator !=(HeatingPresetIdentifier left, HeatingPresetIdentifier right) =>
        !(left == right);
}