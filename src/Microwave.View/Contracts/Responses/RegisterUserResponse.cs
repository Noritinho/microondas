namespace Microwave.Application.Contracts.Responses;

public record RegisterUserResponse
{
    public string Username { get; init; }
}