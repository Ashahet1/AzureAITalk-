using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Vision.ImageAnalysis;

public static class FlowchartFolderProcessor
{
    // Public API called by Program.cs (case 14 method)
    public static async Task<FlowchartExtractionResult> ProcessSingleImageAsync(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("imagePath is required.");

        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image file not found.", imagePath);

        // Read credentials exactly like your existing app expects
        // (Same names you used in README and in your defects analyzer)
        var endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");
        var key = Environment.GetEnvironmentVariable("VISION_KEY");

        if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Missing VISION_ENDPOINT or VISION_KEY environment variables.");

        // Create client
        var client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));

        // Load image
        using var stream = File.OpenRead(imagePath);
        var imageData = BinaryData.FromStream(stream);

        // Ask for: Caption, Tags, Objects, Read (OCR)
        var options = new ImageAnalysisOptions
        {
            Features =
                ImageAnalysisFeature.Caption |
                ImageAnalysisFeature.Tags |
                ImageAnalysisFeature.Objects |
                ImageAnalysisFeature.Read,

            // Optional: set language if you want; default works for most
            // Language = "en"
        };

        ImageAnalysisResult result;
        try
        {
            result = await client.AnalyzeAsync(imageData, options);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException($"Azure Vision request failed: {ex.Message}", ex);
        }

        // Build structured output
        var output = new FlowchartExtractionResult
        {
            ImagePath = imagePath,
            ImageName = Path.GetFileName(imagePath),
            Caption = result.Caption?.Text,
            Tags = result.Tags?.Select(t => t.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            Objects = result.Objects?.Select(o => o.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            OcrLines = ExtractOcrLines(result),
        };

        // Minimal "steps": sort OCR lines top-to-bottom then left-to-right
        // (Good enough for a demo; later we can add box/arrow parsing)
        output.Steps = output.OcrLines
            .OrderBy(l => l.BoundingBoxTop)
            .ThenBy(l => l.BoundingBoxLeft)
            .Select(l => l.Text)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .ToList();

        // Minimal "decisions": detect Yes/No or question mark
        output.Decisions = output.Steps
            .Where(t => t.Contains("?") ||
                        t.Contains("yes", StringComparison.OrdinalIgnoreCase) ||
                        t.Contains("no", StringComparison.OrdinalIgnoreCase) ||
                        t.Contains("approve", StringComparison.OrdinalIgnoreCase) ||
                        t.Contains("reject", StringComparison.OrdinalIgnoreCase))
            .Distinct()
            .ToList();

        return output;
    }

    private static List<OcrLine> ExtractOcrLines(ImageAnalysisResult result)
    {
        var lines = new List<OcrLine>();

        // OCR lives under result.Read depending on SDK version
        // We handle both common shapes safely.
        var read = result.Read;
        if (read == null)
            return lines;

        // Some versions expose Blocks -> Lines
        if (read.Blocks != null)
        {
            foreach (var block in read.Blocks)
            {
                if (block.Lines == null) continue;

                foreach (var line in block.Lines)
                {
                    var text = line.Text ?? "";
                    var (left, top, width, height) = GetBoundingRect(line.BoundingPolygon);

                    lines.Add(new OcrLine
                    {
                        Text = text,
                        BoundingBoxLeft = left,
                        BoundingBoxTop = top,
                        BoundingBoxWidth = width,
                        BoundingBoxHeight = height
                    });
                }
            }
        }

        return lines;
    }

    private static (double left, double top, double width, double height) GetBoundingRect(IReadOnlyList<PointF>? polygon)
    {
        if (polygon == null || polygon.Count == 0)
            return (0, 0, 0, 0);

        double minX = polygon.Min(p => p.X);
        double minY = polygon.Min(p => p.Y);
        double maxX = polygon.Max(p => p.X);
        double maxY = polygon.Max(p => p.Y);

        return (minX, minY, maxX - minX, maxY - minY);
    }
}

// Output model saved as JSON
public class FlowchartExtractionResult
{
    public string ImageName { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public string? Caption { get; set; }

    public List<string> Tags { get; set; } = new();
    public List<string> Objects { get; set; } = new();

    public List<OcrLine> OcrLines { get; set; } = new();

    // Demo-friendly derived fields
    public List<string> Steps { get; set; } = new();
    public List<string> Decisions { get; set; } = new();
}

public class OcrLine
{
    public string Text { get; set; } = "";

    // Pixel coordinates in the image space
    public double BoundingBoxLeft { get; set; }
    public double BoundingBoxTop { get; set; }
    public double BoundingBoxWidth { get; set; }
    public double BoundingBoxHeight { get; set; }
}
