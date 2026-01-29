using System.Diagnostics;
using System.Text.Json;
using DiagramAnalyzer.Core.Models;
using Microsoft.Extensions.Logging;

namespace DiagramAnalyzer.Core.Services;

public class DiagramProcessorService : IDiagramProcessorService
{
    private readonly IAzureVisionService _visionService;
    private readonly IGptVisionService _gptVisionService;
    private readonly ILogger<DiagramProcessorService> _logger;

    public DiagramProcessorService(
        IAzureVisionService visionService,
        IGptVisionService gptVisionService,
        ILogger<DiagramProcessorService> logger)
    {
        _visionService = visionService;
        _gptVisionService = gptVisionService;
        _logger = logger;
    }

    public async Task<DiagramResult> ProcessDiagramAsync(byte[] imageData)
    {
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("Starting diagram processing...");

        try
        {
            var extractedText = await _visionService.ExtractTextAsync(imageData);
            var caption = await _visionService.GetImageCaptionAsync(imageData);
            var gptAnalysis = await _gptVisionService.AnalyzeDiagramStructureAsync(imageData, extractedText);
            var result = ParseGptResponse(gptAnalysis, extractedText, caption);

            stopwatch.Stop();
            result.Metadata.ProcessingTime = stopwatch.Elapsed;

            _logger.LogInformation(
                "Diagram processing complete in {ElapsedMs}ms. Found {NodeCount} nodes and {EdgeCount} edges",
                stopwatch.ElapsedMilliseconds, result.Nodes.Count, result.Edges.Count);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process diagram");
            throw;
        }
    }

    private DiagramResult ParseGptResponse(string gptResponse, List<ExtractedText> extractedText, string caption)
    {
        try
        {
            var cleanedResponse = gptResponse.Replace("```json", "").Replace("```", "").Trim();
            var jsonDoc = JsonDocument.Parse(cleanedResponse);
            var root = jsonDoc.RootElement;

            var result = new DiagramResult
            {
                DiagramType = root.GetProperty("diagramType").GetString() ?? "unknown",
                Description = root.GetProperty("description").GetString() ?? caption,
                ExtractedText = extractedText
            };

            if (root.TryGetProperty("nodes", out var nodesElement))
            {
                foreach (var nodeElement in nodesElement.EnumerateArray())
                {
                    result.Nodes.Add(new DiagramNode
                    {
                        Id = nodeElement.GetProperty("id").GetString() ?? Guid.NewGuid().ToString(),
                        Label = nodeElement.GetProperty("label").GetString() ?? "",
                        Type = nodeElement.TryGetProperty("type", out var typeElement) 
                            ? typeElement.GetString() ?? "process" 
                            : "process",
                        Confidence = 0.85
                    });
                }
            }

            if (root.TryGetProperty("edges", out var edgesElement))
            {
                foreach (var edgeElement in edgesElement.EnumerateArray())
                {
                    result.Edges.Add(new DiagramEdge
                    {
                        SourceNodeId = edgeElement.GetProperty("sourceNodeId").GetString() ?? "",
                        TargetNodeId = edgeElement.GetProperty("targetNodeId").GetString() ?? "",
                        Label = edgeElement.TryGetProperty("label", out var labelElement) 
                            ? labelElement.GetString() ?? "" 
                            : "",
                        Confidence = 0.80
                    });
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse GPT response");
            return new DiagramResult
            {
                DiagramType = "unknown",
                Description = caption,
                ExtractedText = extractedText
            };
        }
    }
}