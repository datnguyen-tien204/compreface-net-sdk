﻿using Exadel.Compreface.Clients;
using Exadel.Compreface.Configuration;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.AddBase64ExampleSubject;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.AddExampleSubject;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.DeleteAllSubjectExamples;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.DeleteImageById;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.DeleteMultipleExamples;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.DownloadImageById;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.DownloadImageBySubjectId;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.ListAllExampleSubject;
using Exadel.Compreface.DTOs.SubjectDTOs.AddSubject;
using Exadel.Compreface.DTOs.SubjectDTOs.DeleteSubject;
using static Exadel.Compreface.AcceptenceTests.UrlConstConfig;

namespace Exadel.Compreface.AcceptenceTests.Services
{
    public class SubjectExampleServiceTest
    {
        private readonly FaceRecognitionClient faceRecognitionClient;
        private readonly AddBase64SubjectExampleRequest addBase64SubjectExampleRequest;
        public SubjectExampleServiceTest()
        {
            faceRecognitionClient = new FaceRecognitionClient(new ComprefaceConfiguration(API_KEY_RECOGNITION_SERVICE, DOMAIN, PORT));
            addBase64SubjectExampleRequest = new AddBase64SubjectExampleRequest()
            {
                DetProbThreShold = 0.81m,
                File = IMAGE_BASE64_STRING
            };
        }

        [Fact]
        public async Task AddSubjectExampleAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            var subjectExample = new AddSubjectExampleRequest()
            {
                DetProbThreShold = 0.81m,
                FilePath = FILE_PATH,
                Subject = addNewSubjectResponse.Subject,
                FileName = FILE_NAME
            };

            var expectedAddExampleSubjectResponse = await faceRecognitionClient.SubjectExampleService.AddSubjectExampleAsync(subjectExample);

            //Act
            var resultList = await faceRecognitionClient.SubjectExampleService.GetAllSubjectExamplesAsync(
                new ListAllSubjectExamplesRequest() { Subject = addNewSubjectResponse.Subject });

            var actualSubjectExample = resultList.Faces
                .First(x => x.ImageId == expectedAddExampleSubjectResponse.ImageId & x.Subject == expectedAddExampleSubjectResponse.Subject);

            //Assert
            Assert.Equal(expectedAddExampleSubjectResponse.Subject, actualSubjectExample.Subject);
            Assert.Equal(expectedAddExampleSubjectResponse.ImageId, actualSubjectExample.ImageId);

            await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });
        }

        [Fact]
        public async Task AddBase64SubjectExampleAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;
           
            var expectedAddBase64SubjectExampleResponse = await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);

            //Act
            var resultList = await faceRecognitionClient.SubjectExampleService.GetAllSubjectExamplesAsync(
                new ListAllSubjectExamplesRequest() { Subject = addNewSubjectResponse.Subject });

            var actualSubjectExample = resultList.Faces
                    .First(x => x.ImageId == expectedAddBase64SubjectExampleResponse.ImageId & x.Subject == expectedAddBase64SubjectExampleResponse.Subject);

            //Assert
            Assert.Equal(expectedAddBase64SubjectExampleResponse.Subject, actualSubjectExample.Subject);
            Assert.Equal(expectedAddBase64SubjectExampleResponse.ImageId, actualSubjectExample.ImageId);

            await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });
        }

        [Fact]
        public async Task GetAllSubjectExamplesAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            var allSubjectExamples = new ListAllSubjectExamplesRequest()
            {
                Page = 0,
                Size = 0,
                Subject = addNewSubjectResponse.Subject
            };

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;

            var expectedCount = 3;
            for (int counter = expectedCount; counter > 0; counter--)
            {
                await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);
            }

            //Act
            var actualAllSubjectExamplesResponse = await faceRecognitionClient.SubjectExampleService.GetAllSubjectExamplesAsync(allSubjectExamples);
            var actualCount = actualAllSubjectExamplesResponse.Faces.Count;

            //Assert
            Assert.Equal(actualCount, expectedCount);

            await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(
                new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });
        }

        [Fact]
        public async Task ClearSubjectAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;

            var expectedCount = 3;

            for (int counter = expectedCount; counter > 0; counter--)
            {
                await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);
            }

            //Act
            var actualResponse = await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(
                new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });

            //Assert
            Assert.Equal(expectedCount, actualResponse.Deleted);
        }

        [Fact]
        public async Task DeleteImageByIdAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;
            var testImage = await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);

            var deleteImageByIdRequest = new DeleteImageByIdRequest()
            {
                ImageId = testImage.ImageId
            };

            //Act
            var actualDeleteImageByIdResponse = await faceRecognitionClient.SubjectExampleService.DeleteImageByIdAsync(deleteImageByIdRequest);

            //Assert
            Assert.Equal(testImage.Subject, actualDeleteImageByIdResponse.Subject);
            Assert.Equal(testImage.ImageId, actualDeleteImageByIdResponse.ImageId);

            await faceRecognitionClient.SubjectService.DeleteSubject(
                new DeleteSubjectRequest() { ActualSubject = testImage.Subject });
        }

        [Fact]
        public async Task DeletMultipleExamplesAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;

            var expectedCount = 3;
            List<Guid> unnecessaryExampleList = new List<Guid>();

            for (int counter = expectedCount; counter > 0; counter--)
            {
                var response = await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);

                unnecessaryExampleList.Add(response.ImageId);
            }

            var expectedFacesCount = expectedCount;

            //Act
            var actualResponse = await faceRecognitionClient.SubjectExampleService.DeletMultipleExamplesAsync(
                new DeleteMultipleExampleRequest() { ImageIdList = unnecessaryExampleList });

            var actualFacesCount = actualResponse.Faces.Count;

            //Assert
            Assert.Equal(expectedFacesCount, actualFacesCount);

            await faceRecognitionClient.SubjectService.DeleteSubject(
                new DeleteSubjectRequest() { ActualSubject = addNewSubjectResponse.Subject });
        }

        [Fact]
        public async Task DownloadImageByIdAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;

            var testImage = await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);

            var expectedResult = Convert.FromBase64String(IMAGE_BASE64_STRING);

            //Act
            var actualResult = await faceRecognitionClient.SubjectExampleService.DownloadImageByIdAsync(
                new DownloadImageByIdRequest() { ImageId = testImage.ImageId, RecognitionApiKey = Guid.Parse(API_KEY_RECOGNITION_SERVICE) });

            //Assert
            Assert.Equal(expectedResult, actualResult);

            await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });
        }

        [Fact]
        public async Task DownloadImageBySubjectIdAsync_TakesRequestModel_ReturnsProperResponseModel()
        {
            //Arrange
            var addNewSubjectResponse = await faceRecognitionClient.SubjectService.AddSubject(
                new AddSubjectRequest() { Subject = TEST_SUBJECT_NAME });

            addBase64SubjectExampleRequest.Subject = addNewSubjectResponse.Subject;

            var testImage = await faceRecognitionClient.SubjectExampleService.AddBase64SubjectExampleAsync(addBase64SubjectExampleRequest);

            var expectedResult = Convert.FromBase64String(IMAGE_BASE64_STRING);

            //Act
            var actualResult = await faceRecognitionClient.SubjectExampleService.DownloadImageBySubjectIdAsync(
                new DownloadImageBySubjectIdRequest() { ImageId = testImage.ImageId });

            //Assert
            Assert.Equal(expectedResult, actualResult);

            await faceRecognitionClient.SubjectExampleService.ClearSubjectAsync(
                new DeleteAllExamplesRequest() { Subject = addNewSubjectResponse.Subject });
        }
    }
}
