﻿using Exadel.Compreface.Clients.Interfaces;
using Exadel.Compreface.Configuration;
using Exadel.Compreface.DTOs.ExampleSubjectDTOs.AddBase64ExampleSubject;
using Exadel.Compreface.DTOs.RecognitionDTOs.RecognizeFaceFromImage;
using Exadel.Compreface.DTOs.RecognitionDTOs.RecognizeFacesFromImageWithBase64;
using Exadel.Compreface.DTOs.RecognitionDTOs.VerifyFacesFromImage;
using Exadel.Compreface.DTOs.RecognitionDTOs.VerifyFacesFromImageWithBase64;
using Exadel.Compreface.Helpers;
using Flurl;
using Flurl.Http;

namespace Exadel.Compreface.Services;

public class RecognitionService : AbstractBaseService
{
    public RecognitionService(IComprefaceConfiguration configuration, IApiClient apiClient)
            : base(configuration, apiClient) { }

    public async Task<RecognizeFaceFromImageResponse> RecognizeFaceFromImage(RecognizeFaceFromImageRequest request, bool isFileInTheRemoteServer = false)
    {
        var requestUrl = $"{Configuration.Domain}:{Configuration.Port}/api/v1/recognition/recognize";
        var requestUrlWithQueryParameters = requestUrl
            .SetQueryParams(new
            {
                limit = request.Limit,
                prediction_count = request.PredictionCount,
                det_prob_threshold = request.DetProbThreshold,
                face_plugins = string.Join(",", request.FacePlugins),
                status = request.Status,
            });

        RecognizeFaceFromImageResponse? response = null;

        if (isFileInTheRemoteServer)
        {
            var fileStream = await request.FilePath.GetBytesAsync();
            var fileInBase64String = Convert.ToBase64String(fileStream);

            var addBase64SubjectExampleRequest = new AddBase64SubjectExampleRequest()
            {
                DetProbThreShold = request.DetProbThreshold,
                File = fileInBase64String,
            };

            response = await ApiClient.PostJsonAsync<RecognizeFaceFromImageResponse>(Configuration.ApiKey, requestUrlWithQueryParameters, body: addBase64SubjectExampleRequest);

            return response;
        }

        response = await
            ApiClient.PostMultipartAsync<RecognizeFaceFromImageResponse>(
                apiKey: Configuration.ApiKey,
                requestUrl: requestUrlWithQueryParameters,
                buildContent: mp =>
                mp.AddFile("file", fileName: FileHelpers.GenerateFileName(request.FilePath), path: request.FilePath));

        return response;
    }

    public async Task<RecognizeFaceFromImageResponse> RecognizeFaceFromBase64File(
        RecognizeFacesFromImageWithBase64Request request)
    {
        var requestUrl = $"{Configuration.Domain}:{Configuration.Port}/api/v1/recognition/recognize";
        var requestUrlWithQueryParameters = requestUrl
            .SetQueryParams(new
            {
                limit = request.Limit,
                prediction_count = request.PredictionCount,
                det_prob_threshold = request.DetProbThreshold,
                face_plugins = string.Join(",", request.FacePlugins),
                status = request.Status,
            });

        var response = await 
            ApiClient.PostJsonAsync<RecognizeFaceFromImageResponse>(
                apiKey: Configuration.ApiKey,
                requestUrl: requestUrlWithQueryParameters, 
                body: new { file = request.FileBase64Value });

        return response;
    }

    public async Task<VerifyFacesFromImageResponse> VerifyFacesFromImage(VerifyFacesFromImageRequest request)
    {
        var requestUrl = $"{Configuration.Domain}:{Configuration.Port}/api/v1/recognition/faces/{request.ImageId}/verify";
        var requestUrlWithQueryParameters = requestUrl
            .SetQueryParams(new
            {
                limit = request.Limit,
                det_prob_threshold = request.DetProbThreshold,
                face_plugins = string.Join(",", request.FacePlugins),
                status = request.Status,
            });

        var response = await 
            ApiClient.PostMultipartAsync<VerifyFacesFromImageResponse>(
                apiKey: Configuration.ApiKey,
                requestUrl: requestUrlWithQueryParameters,
                buildContent: mp =>
                mp.AddFile("file", fileName: FileHelpers.GenerateFileName(request.FilePath), path: request.FilePath));

        return response;
    }
    
    public async Task<VerifyFacesFromImageResponse> VerifyFacesFromBase64File(VerifyFacesFromImageWithBase64Request request)
    {
        var requestUrl = $"{Configuration.Domain}:{Configuration.Port}/api/v1/recognition/faces/{request.ImageId}/verify";
        var requestUrlWithQueryParameters = requestUrl
            .SetQueryParams(new
            {
                limit = request.Limit,
                det_prob_threshold = request.DetProbThreshold,
                face_plugins = string.Join(",", request.FacePlugins),
                status = request.Status,
            });

        var response = await 
            ApiClient.PostJsonAsync<VerifyFacesFromImageResponse>(
                apiKey: Configuration.ApiKey,
                requestUrl: requestUrlWithQueryParameters,
                body: new { file = request.FileBase64Value });

        return response;
    }
}