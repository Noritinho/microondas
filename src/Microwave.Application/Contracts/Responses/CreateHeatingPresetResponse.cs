namespace Microwave.Application.Contracts.Responses;

public record CreateHeatingPresetResponse()
{
    public string Identifier { get; init; }
    
    public string Name { get; init; }

    public string Food { get; init; }

    public string Duration { get; init; }

    public byte Potency { get; init; }
    
    public string Instructions { get; init; }
}