namespace Microwave.Application.Contracts.Responses;

public record HeatingTimerResponse
{
    public HeatingTimerResponse(
        int timeRemaining, byte potency)
    {
        TimeRemaining = string.Join(" ", Enumerable.Repeat(new string('.', potency), timeRemaining)); //TODO: Passar pro dominio
        TimeRemainingValue = timeRemaining;
    }

    public string TimeRemaining { get; }
    public int TimeRemainingValue { get; }
}

public class HeatTimerCompletedResponse
{
    public string Message { get; set; }
}