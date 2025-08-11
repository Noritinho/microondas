namespace Microwave.Domain.Models;

public class TokenModel
{
    public string AccessToken { get; init; }
    public string TokenType { get; init; }
    public int ExpiresIn { get; init; }
}