using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models;
using Microwave.Domain.Models.User;

namespace Microwave.Infrastructure.Auth;

public class TokenService : ITokenService
{
    public TokenModel GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(AppSettingsService.JwtSettings.AccessToken);
        
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name.Value),
            ]),
            
            Expires = DateTime.UtcNow.AddHours(AppSettingsService.JwtSettings.ExpiresIn),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var result = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenModel()
        {
            AccessToken = tokenHandler.WriteToken(result),
            ExpiresIn = AppSettingsService.JwtSettings.ExpiresIn,
            TokenType = AppSettingsService.JwtSettings.TokenType
        };
    }
}