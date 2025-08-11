namespace Microwave.Domain.Contracts.Responses;

public record SetHeatingResponse
{
    public SetHeatingResponse(
        string timeRemaining)
    {
        TimeRemaining = timeRemaining;
    }
    
    public string TimeRemaining { get; }
    public string TimeRemainingValue { get; }
}

//na response, retornar o tempo datetimeutcnow em que o microondas deve interromper