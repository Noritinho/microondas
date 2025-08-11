using Microwave.Application.Contracts.Requests;
using Microwave.Application.UseCases;
using Microwave.Domain.Exceptions;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.User;
using Microwave.Domain.Security;
using Moq;

namespace Application.UnitTests.UseCases;

public class RegisterUseCaseTests
{
    private readonly Mock<IDataService<User>> _dataServiceMock;
    private readonly RegisterUseCase _useCase;

    public RegisterUseCaseTests()
    {
        _dataServiceMock = new Mock<IDataService<User>>();
        _dataServiceMock.Setup(ds => ds.CreateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        _useCase = new RegisterUseCase(_dataServiceMock.Object);
    }
    
    [Theory]
    [InlineData("testuser1", "123456")]
    public async Task RegisterAsync_ShouldCreateUserAndReturnResponse(string userName, string password)
    {
        var request = new RegisterUserRequest
        {
            UserName = userName,
            Password = password
        };
        
        var response = await _useCase.RegisterAsync(request);
        
        Assert.NotNull(response);
        Assert.Equal(userName, response.Username);
    }
    
    [Theory]
    [InlineData("", "123456")]
    [InlineData(null, "123456")]
    [InlineData("           ", "123456")]
    public async Task RegisterAsync_ShouldDomainErrorUserNameNull(string userName, string password)
    {
        var request = new RegisterUserRequest
        {
            UserName = userName,
            Password = password
        };

        var exception = await Assert.ThrowsAsync<DomainException>(() => _useCase.RegisterAsync(request));

        Assert.Contains(UserName.UserNameNull, exception.Message);
    }
    
    [Theory]
    [InlineData("testuser1", "")]
    [InlineData("testuser1", null)]
    [InlineData("testuser1", "                 ")]
    public async Task RegisterAsync_ShouldDomainErrorPasswordNull(string userName, string password)
    {
        var request = new RegisterUserRequest
        {
            UserName = userName,
            Password = password
        };

        var exception = await Assert.ThrowsAsync<DomainException>(() => _useCase.RegisterAsync(request));

        Assert.Contains(Cryptograph.PasswordNull, exception.Message);
    }
    
    [Theory]
    [InlineData("testuser1", "")]
    [InlineData("testuser1", null)]
    [InlineData("testuser1", "                 ")]
    public void DomainPassword_ShouldDomainErrorPasswordNull(string userName, string password)
    {
        var exception = Assert.Throws<DomainException>(() => User.Create(userName, password));

        Assert.Contains(UserPassword.PasswordNull, exception.Message);
    }
}