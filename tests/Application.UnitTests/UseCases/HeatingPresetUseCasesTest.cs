using Microwave.Application.Contracts.Requests;
using Microwave.Application.Providers;
using Microwave.Application.UseCases;
using Microwave.Domain.Contracts.Requests;
using Microwave.Domain.Exceptions;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.Heating;
using Moq;

namespace Application.UnitTests.UseCases;

public class HeatingPresetUseCasesTest
{
    private readonly Mock<IDataService<HeatingPreset>> _dataServiceMock;
    private readonly IHeatingPresetsProvider _heatingPresetsProvider;
    private readonly HeatingPresetUseCases _useCase;

    public HeatingPresetUseCasesTest()
    {
        _dataServiceMock = new Mock<IDataService<HeatingPreset>>();
        _heatingPresetsProvider = new HeatingPresetsProvider();
        _dataServiceMock.Setup(ds => ds.CreateAsync(It.IsAny<HeatingPreset>())).Returns(Task.CompletedTask);

        _useCase = new HeatingPresetUseCases(_dataServiceMock.Object, _heatingPresetsProvider);
    }
    
    [Theory]
    [InlineData("teste")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("                ")]
    public async Task RegisterAsync_ShouldCreateAndReturnResponse(string instructions)
    {
        var request = new CreateHeantigPresetRequest
        {
            Identifier = "teste",
            Name = "teste",
            Duration = 30,
            Food = "teste",
            Instructions = instructions,
            Potency = 10
        };
        
        var response = await _useCase.CreateAsync(request);
        
        Assert.NotNull(response);
        Assert.Equal(request.Identifier, response.Identifier);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("                ")]
    public async Task RegisterAsync_ShouldErrorIdentifier(string identifier)
    {
        var request = new CreateHeantigPresetRequest
        {
            Identifier = identifier,
            Name = "teste",
            Duration = 30,
            Food = "teste",
            Instructions = string.Empty,
            Potency = 10
        };
        
        var exception = await Assert.ThrowsAsync<DomainException>(() => _useCase.CreateAsync(request));
        
        Assert.Equal(HeatingPresetIdentifier.IdentifierNull, exception.Message);
    }
    
    [Theory]
    [InlineData(".")]
    public async Task RegisterAsync_ShouldErrorIdentifierDot(string identifier)
    {
        var request = new CreateHeantigPresetRequest
        {
            Identifier = identifier,
            Name = "teste",
            Duration = 30,
            Food = "teste",
            Instructions = string.Empty,
            Potency = 10
        };
        
        var exception = await Assert.ThrowsAsync<DomainException>(() => _useCase.CreateAsync(request));
        
        Assert.Equal(HeatingPresetIdentifier.IdentifierEqualsDOt, exception.Message);
    }
    
    [Fact]
    public async Task GetPresetsAsync_ShouldReturnResponse()
    {
        var response = await _useCase.GetPresetsAsync().ToListAsync();
        
        Assert.NotNull(response);
        Assert.Equal(response.Count, _heatingPresetsProvider.Collection.Presets.Count);
    }
}