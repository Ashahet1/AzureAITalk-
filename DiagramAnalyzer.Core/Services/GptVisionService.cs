using System.Text;
using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using DiagramAnalyzer.Core.Configuration;
using DiagramAnalyzer.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace DiagramAnalyzer.Core.Services;

public class GptVisionService : IGptVisionService
{
    private readonly OpenAIClient _client;
    private readonly AzureOpenAISettings _settings;
    private readonly ILogger<GptVisionService> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public GptVisionService(
        IOptions<AzureOpenAISettings> settings,
        ILogger<GptVisionService> logger)
    {
        _logger = logger;
        _settings = settings.Value;

        _client = new OpenAIClient(
            new Uri(_settings.Endpoint),
            new AzureKeyCredential(_settings.ApiKey));

        _retryPolicy = Policy
            .Handle<RequestFailedException>()
            .WaitAndRetryAsync(
                _settings.MaxRetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "GPT-4 Vision retry {RetryCount} after {TimeSpan}s",
                        retryCount, timeSpan.TotalSeconds);
                });
    }

    public async Task<string> AnalyzeDiagramStructureAsync(byte[] imageData, List<ExtractedText> extractedText)
    {
        _logger.LogInformation("Analyzing diagram structure with GPT-4 Vision...");

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var base64Image = Convert.ToBase64String(imageData);
            var imageUrl = $"data:image/png;base64,{base64Image}";

            var textContext = string.Join(", ", extractedText.Select(t => t.Text));

            var prompt = $@"You are an expert at analyzing diagrams and flowcharts. 
Analyze this diagram and provide a structured JSON response with the following:
1. 'diagramType': The type of diagram (flowchart, org-chart, network-diagram, etc.)
2. 'description': A brief description of what the diagram represents
3. 'nodes': An array of nodes with 'id', 'label', and 'type' (start, process, decision, end)
4. 'edges': An array of connections with 'sourceNodeId', 'targetNodeId', and optional 'label'

Extracted text from the image: {textContext}

Return ONLY valid JSON, no markdown or additional text.";

            var chatCompletionsOptions = new ChatCompletionsOptions
            {
                DeploymentName = _settings.DeploymentName,
                Messages =
                {
                    new ChatRequestSystemMessage("You are an expert diagram analyzer that returns structured JSON."),
                    new ChatRequestUserMessage(
                        new ChatMessageTextContentItem(prompt),
                        new ChatMessageImageContentItem(new Uri(imageUrl)))
                },
                MaxTokens = _settings.MaxTokens,
                Temperature = _settings.Temperature
            };

            var response = await _client.GetChatCompletionsAsync(chatCompletionsOptions);
            var result = response.Value.Choices[0].Message.Content;

            _logger.LogInformation("GPT-4 Vision analysis complete");
            return result;
        });
    }
}