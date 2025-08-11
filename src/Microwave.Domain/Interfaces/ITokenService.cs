using Microwave.Domain.Models;
using Microwave.Domain.Models.User;

namespace Microwave.Domain.Interfaces;

public interface ITokenService
{
    public TokenModel GenerateToken(User user);
}