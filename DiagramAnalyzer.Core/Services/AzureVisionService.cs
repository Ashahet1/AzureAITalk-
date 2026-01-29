using Azure;
using Azure.AI.Vision.ImageAnalysis;
using DiagramAnalyzer.Core.Configuration;
using DiagramAnalyzer.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace DiagramAnalyzer.Core.Services;

public class AzureVisionService : IAzureVisionService
{
    private readonly ImageAnalysisClient _client;
    private readonly ILogger<AzureVisionService> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public AzureVisionService(
        IOptions<AzureVisionSettings> settings,
        ILogger<AzureVisionService> logger)
    {
        _logger = logger;
        var config = settings.Value;

        _client = new ImageAnalysisClient(
            new Uri(config.Endpoint),
            new AzureKeyCredential(config.ApiKey));

        _retryPolicy = Policy
            .Handle<RequestFailedException>()
            .WaitAndRetryAsync(
                config.MaxRetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "Retry {RetryCount} after {TimeSpan}s",
                        retryCount, timeSpan.TotalSeconds);
                });
    }

    public async Task<List<ExtractedText>> ExtractTextAsync(byte[] imageData)
    {
        _logger.LogInformation("Extracting text from image...");

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.AnalyzeAsync(
                BinaryData.FromBytes(imageData),
                VisualFeatures.Read,
                new ImageAnalysisOptions { Language = "en" });

            var extractedTexts = new List<ExtractedText>();

            if (result.Value.Read?.Blocks != null)
            {
                foreach (var block in result.Value.Read.Blocks)
                {
                    foreach (var line in block.Lines)
                    {
                        var boundingBox = line.BoundingPolygon.Count >= 4
                            ? new BoundingBox
                            {
                                X = line.BoundingPolygon[0].X,
                                Y = line.BoundingPolygon[0].Y,
                                Width = line.BoundingPolygon[1].X - line.BoundingPolygon[0].X,
                                Height = line.BoundingPolygon[3].Y - line.BoundingPolygon[0].Y
                            }
                            : null;

                        extractedTexts.Add(new ExtractedText
                        {
                            Text = line.Text,
                            BoundingBox = boundingBox,
                            Confidence = 0.9
                        });
                    }
                }
            }

            _logger.LogInformation("Extracted {Count} text elements", extractedTexts.Count);
            return extractedTexts;
        });
    }

    public async Task<List<string>> DetectObjectsAsync(byte[] imageData)
    {
        _logger.LogInformation("Detecting objects...");

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.AnalyzeAsync(
                BinaryData.FromBytes(imageData),
                VisualFeatures.Objects);

            var objects = result.Value.Objects?
                .Select(obj => obj.Tags.FirstOrDefault()?.Name ?? "unknown")
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList() ?? new List<string>();

            _logger.LogInformation("Detected {Count} objects", objects.Count);
            return objects;
        });
    }

    public async Task<string> GetImageCaptionAsync(byte[] imageData)
    {
        _logger.LogInformation("Generating caption...");

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var result = await _client.AnalyzeAsync(
                BinaryData.FromBytes(imageData),
                VisualFeatures.Caption);

            var caption = result.Value.Caption?.Text ?? "No caption available";
            _logger.LogInformation("Caption: {Caption}", caption);
            return caption;
        });
    }
}