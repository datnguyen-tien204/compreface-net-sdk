using Exadel.Compreface.Configuration;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.AddBase64ExampleSubject;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.AddExampleSubject;
using Exadel.Compreface.Services;
using Flurl.Http.Content;
using Moq;
using Tynamix.ObjectFiller;

namespace Exadel.Compreface.UnitTests;

public class ExampleSubjectServiceTests
{
    private readonly Mock<IApiClient> _apiClientMock;
    private readonly ExampleSubjectService _exampleSubjectService;

    public ExampleSubjectServiceTests()
    {
        _apiClientMock = new Mock<IApiClient>();
        var randomString = GetRandomString();
        var apiKey = randomString;
        var baseUrl = randomString;
        var configuration = new ComprefaceConfiguration(apiKey, baseUrl);
        
        _exampleSubjectService = new ExampleSubjectService(configuration, _apiClientMock.Object);
    }

    private string GetRandomString()
    {
        return new Filler<string>().Create();
    }
    
    [Fact]
    public async Task AddExampleSubjectAsync_TakesRequestModel_ReturnsProperResponseModel()
    {
        // Arrange
        var request = new AddExampleSubjectRequest();

        _apiClientMock.Setup(apiClient =>
            apiClient.PostMultipartAsync<AddExampleSubjectResponse>(
                It.IsAny<Flurl.Url>(),
                It.IsAny<Action<CapturedMultipartContent>>(),
                It.IsAny<HttpCompletionOption>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AddExampleSubjectResponse());
        
        // Act
        var response = await _exampleSubjectService.AddExampleSubjectAsync(request);
        
        // Assert
        Assert.IsType<AddExampleSubjectResponse>(response);

        _apiClientMock.Verify(client => 
            client.PostMultipartAsync<AddExampleSubjectResponse>(
                It.IsAny<Flurl.Url>(), 
                It.IsAny<Action<CapturedMultipartContent>>(), 
                It.IsAny<HttpCompletionOption>(), 
                It.IsAny<CancellationToken>()), Times.Once);
        
        _apiClientMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task AddBase64ExampleSubjectAsync_TakesRequestModel_ReturnsProperResponseModel()
    {
        // Arrange
        var request = new AddBase64ExampleSubjectRequest();

        _apiClientMock.Setup(apiClient =>
                apiClient.PostJsonAsync<AddBase64ExampleSubjectResponse>(
                    It.IsAny<Flurl.Url>(),
                    It.IsAny<Action<CapturedMultipartContent>>(),
                    It.IsAny<HttpCompletionOption>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AddBase64ExampleSubjectResponse());
        
        
        //Act
        var response = await _exampleSubjectService.AddBase64ExampleSubjectAsync(request);
        
        // Assert
        Assert.IsType<AddBase64ExampleSubjectResponse>(response);
        
        _apiClientMock.Verify(client => 
            client.PostJsonAsync<AddBase64ExampleSubjectResponse>(
                It.IsAny<Flurl.Url>(), 
                It.IsAny<Action<CapturedMultipartContent>>(), 
                It.IsAny<HttpCompletionOption>(), 
                It.IsAny<CancellationToken>()), Times.Once);
        
        _apiClientMock.VerifyNoOtherCalls();
    }
}