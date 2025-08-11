namespace Microwave.Application.Contracts.Requests;

public record RegisterUserRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
};