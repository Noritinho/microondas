namespace Microwave.View.Contracts.Responses;

public class HeatTimerResponse
{
    public string TimeRemaining { get; set; }
    public int TimeRemainingValue { get; set; }
    public byte Potency { get; set; }
}

public class HeatTimerCompletedResponse
{
    public string Message { get; set; }
}