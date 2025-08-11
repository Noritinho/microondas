using Microwave.Application.Contracts.Requests;
using Microwave.Application.Contracts.Responses;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models;
using Microwave.Domain.Models.User;
using Microwave.Domain.Security;

namespace Microwave.Application.UseCases;

public interface IRegisterUseCase
{
    Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request);
}

public class RegisterUseCase(
    IDataService<User> dataService) : IRegisterUseCase
{
    public async Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request)
    {
        var user = User.Create(request.UserName, Cryptograph.Encrypt(request.Password));

        await dataService.CreateAsync(user);

        return new RegisterUserResponse()
        {
            Username = user.Name.Value,
        };
    }
}