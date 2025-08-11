namespace Microwave.Application.Contracts.Responses;

public record LoginResponse
{
    public string Token { get; init; }
}