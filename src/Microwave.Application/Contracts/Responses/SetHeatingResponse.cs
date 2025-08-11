namespace Microwave.Domain.Contracts.Responses;

public record SetHeatingResponse
{
    public SetHeatingResponse(
        int timeRemaining, byte potency)
    {
        TimeRemaining = string.Concat(Enumerable.Repeat(new string('.', timeRemaining) + " ", potency));
        TimeRemainingValue = timeRemaining;
    }
    
    public string TimeRemaining { get; }
    public int TimeRemainingValue { get; }
}