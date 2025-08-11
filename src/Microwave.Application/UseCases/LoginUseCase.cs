using Microwave.Application.Contracts.Requests;
using Microwave.Application.Contracts.Responses;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.User;
using Microwave.Domain.Security;

namespace Microwave.Application.UseCases;

public interface ILoginUseCase
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}

internal class LoginUseCase(
    ITokenService tokenService,
    IDataService<User> dataService) : ILoginUseCase
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await dataService.GetByAsync(u => u.Name.Value == request.UserName)
                   ?? throw new UnauthorizedAccessException(); ;
        
        if (!Cryptograph.Verify(request.Password, user.Password.Value))
            throw new UnauthorizedAccessException();

        var token = tokenService.GenerateToken(user);

        return new LoginResponse()
        {
            Token = token.AccessToken
        };
    }
}