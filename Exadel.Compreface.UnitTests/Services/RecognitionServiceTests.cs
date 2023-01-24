﻿using Exadel.Compreface.DTOs.RecognitionDTOs.RecognizeFaceFromImage;
using Exadel.Compreface.DTOs.RecognitionDTOs.RecognizeFacesFromImageWithBase64;
using Exadel.Compreface.DTOs.RecognitionDTOs.VerifyFacesFromImage;
using Exadel.Compreface.DTOs.RecognitionDTOs.VerifyFacesFromImageWithBase64;
using Exadel.Compreface.Services;
using Flurl;

namespace Exadel.Compreface.UnitTests.Services
{
    public class RecognitionServiceTests : AbstractBaseServiceTests
    {
        private readonly RecognitionService _recognitionService;

        public RecognitionServiceTests()
        {
            _recognitionService = new RecognitionService(Configuration, ApiClientMock.Object);
        }

        [Fact]
        public async Task RecognizeFaceFromImage_TakesRequestModel_ReturnsProperResponseModel()
        {
            // Arrange
            var request = new RecognizeFaceFromImageRequest()
            {
                FacePlugins = new List<string>()
            };

            SetupPostMultipart<RecognizeFaceFromImageResponse>();

            // Act
            var response = await _recognitionService.RecognizeFaceFromImage(request);

            // Assert
            Assert.IsType<RecognizeFaceFromImageResponse>(response);

            VerifyPostMultipart<RecognizeFaceFromImageResponse>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RecognizeFaceFromImage_TakesRequestModel_ReturnsNotNull()
        {
            // Arrange
            var request = new RecognizeFaceFromImageRequest()
            {
                FacePlugins = new List<string>()
            };

            SetupPostMultipart<RecognizeFaceFromImageResponse>();

            // Act
            var response = await _recognitionService.RecognizeFaceFromImage(request);

            // Assert
            Assert.NotNull(response);

            VerifyPostMultipart<RecognizeFaceFromImageResponse>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RecognizeFaceFromImage_TakesNullRequestModel_ThrowsNullReferenceException()
        {
            // Arrange
            SetupPostMultipart<RecognizeFaceFromImageResponse>();

            // Act
            var func = async () => await _recognitionService.RecognizeFaceFromImage(null!);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(func);
        }

        [Fact]
        public async Task RecognizeFaceFromImage_TakesIncorrectRequestModel_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new RecognizeFaceFromImageRequest();

            SetupPostMultipart<RecognizeFaceFromImageResponse>();

            // Act
            var func = async () => await _recognitionService.RecognizeFaceFromImage(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task RecognizeFaceFromBase64File_TakesRequestModel_ReturnsProperResponseModel()
        {
            // Arrange
            var request = new RecognizeFacesFromImageWithBase64Request()
            {
                FacePlugins = new List<string>()
            };

            SetupPostJson<RecognizeFaceFromImageResponse, Url>();

            // Act
            var response = await _recognitionService.RecognizeFaceFromBase64File(request);

            // Assert
            Assert.IsType<RecognizeFaceFromImageResponse>(response);

            VerifyPostJson<RecognizeFaceFromImageResponse, Url>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RecognizeFaceFromBase64File_TakesRequestModel_ReturnsNotNull()
        {
            // Arrange
            var request = new RecognizeFacesFromImageWithBase64Request()
            {
                FacePlugins = new List<string>()
            };

            SetupPostJson<RecognizeFaceFromImageResponse, Url>();

            // Act
            var response = await _recognitionService.RecognizeFaceFromBase64File(request);

            // Assert
            Assert.NotNull(response);

            VerifyPostJson<RecognizeFaceFromImageResponse, Url>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RecognizeFaceFromBase64File_TakesNullRequestModel_ThrowsNullReferenceException()
        {
            // Arrange
            SetupPostJson<RecognizeFaceFromImageResponse, Url>();

            // Act
            var func = async () => await _recognitionService.RecognizeFaceFromBase64File(null!);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(func);
        }

        [Fact]
        public async Task RecognizeFaceFromBase64File_TakesIncorrectRequestModel_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new RecognizeFacesFromImageWithBase64Request();

            SetupPostJson<RecognizeFaceFromImageResponse, Url>();

            // Act
            var func = async () => await _recognitionService.RecognizeFaceFromBase64File(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task VerifyFacesFromImage_TakesRequestModel_ReturnsProperResponseModel()
        {
            // Arrange
            var request = new VerifyFacesFromImageRequest()
            {
                FacePlugins = new List<string>()
            };

            SetupPostMultipart<VerifyFacesFromImageResponse>();

            // Act
            var response = await _recognitionService.VerifyFacesFromImage(request);

            // Assert
            Assert.IsType<VerifyFacesFromImageResponse>(response);

            VerifyPostMultipart<VerifyFacesFromImageResponse>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task VerifyFacesFromImage_TakesRequestModel_ReturnsNotNull()
        {
            // Arrange
            var request = new VerifyFacesFromImageRequest()
            {
                FacePlugins = new List<string>()
            };

            SetupPostMultipart<VerifyFacesFromImageResponse>();

            // Act
            var response = await _recognitionService.VerifyFacesFromImage(request);

            // Assert
            Assert.NotNull(response);

            VerifyPostMultipart<VerifyFacesFromImageResponse>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task VerifyFacesFromImage_TakesNullRequestModel_ThrowsNullReferenceException()
        {
            // Arrange
            SetupPostMultipart<VerifyFacesFromImageResponse>();

            // Act
            var func = async () => await _recognitionService.VerifyFacesFromImage(null!);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(func);
        }

        [Fact]
        public async Task VerifyFacesFromBase64File_TakesRequestModel_ReturnsProperResponseModel()
        {
            // Arrange
            var request = new VerifyFacesFromImageWithBase64Request()
            {
                FacePlugins = new List<string>()
            };

            SetupPostJson<VerifyFacesFromImageResponse, Url>();

            // Act
            var response = await _recognitionService.VerifyFacesFromBase64File(request);

            // Assert
            Assert.IsType<VerifyFacesFromImageResponse>(response);

            VerifyPostJson<VerifyFacesFromImageResponse, Url>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task VerifyFacesFromBase64File_TakesRequestModel_ReturnsNotNull()
        {
            // Arrange
            var request = new VerifyFacesFromImageWithBase64Request()
            {
                FacePlugins = new List<string>()
            };

            SetupPostJson<VerifyFacesFromImageResponse, Url>();

            // Act
            var response = await _recognitionService.VerifyFacesFromBase64File(request);

            // Assert
            Assert.NotNull(response);

            VerifyPostJson<VerifyFacesFromImageResponse, Url>();
            ApiClientMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task VerifyFacesFromBase64File_TakesNullRequestModel_ThrowsNullReferenceException()
        {
            // Arrange
            SetupPostJson<VerifyFacesFromImageResponse, Url>();

            // Act
            var func = async () => await _recognitionService.VerifyFacesFromBase64File(null!);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(func);
        }
    }
}
