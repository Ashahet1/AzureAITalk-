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
        string endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT")
                ?? "VISION_ENDPOINT";
        string key = Environment.GetEnvironmentVariable("VISION_KEY")
            ?? "VISION_KEY";
        if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Missing VISION_ENDPOINT or VISION_KEY environment variables.");

        // Create client
        var client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));

        // Load image
        using var stream = File.OpenRead(imagePath);
        var imageData = BinaryData.FromStream(stream);

        // Ask for: Caption, Tags, Objects, Read (OCR)
        var visualFeatures =
            VisualFeatures.Caption |
            VisualFeatures.Tags |
            VisualFeatures.Objects |
            VisualFeatures.Read;

        ImageAnalysisResult result;
        try
        {
            result = await client.AnalyzeAsync(imageData, visualFeatures);
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
            //Tags = result.Tags?.Values.Select(t => t.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            //Objects = result.Objects?.Values?.Where(static o => !string.IsNullOrWhiteSpace(o.Name)).Select(o => o.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            Tags = result.Tags?.Values.Select(t => t.Name).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            Objects = result.Objects?.Values.Select(o => o.Tags?.FirstOrDefault()?.Name).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>(),
            OcrLines = ExtractOcrLines(result),
        };

        // Minimal "steps": sort OCR lines top-to-bottom then left-to-right
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
                    t.Contains("reject", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("pass", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("fail", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("rework", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("scrap", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("verify", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("check", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("threshold", StringComparison.OrdinalIgnoreCase) ||
                    t.Contains("meet", StringComparison.OrdinalIgnoreCase))
        .Distinct()
        .ToList();

        return output;
    }

    private static List<OcrLine> ExtractOcrLines(ImageAnalysisResult result)
    {
        var lines = new List<OcrLine>();

        var read = result.Read;
        if (read == null)
            return lines;

        // read.Blocks -> block.Lines -> line.BoundingPolygon (shape varies by SDK)
        if (read.Blocks == null)
            return lines;

        foreach (var block in read.Blocks)
        {
            if (block.Lines == null) continue;

            foreach (var line in block.Lines)
            {
                var text = line.Text ?? string.Empty;

                // IMPORTANT: Do NOT type the polygon as PointF or any specific type.
                // We treat it as object and read X/Y via reflection.
                var (left, top, width, height) = GetBoundingRect((object?)line.BoundingPolygon);

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

        return lines;
    }

    private static (double left, double top, double width, double height) GetBoundingRect(object? polygonObj)
    {
        if (polygonObj is not System.Collections.IEnumerable polygon)
            return (0, 0, 0, 0);

        double minX = double.MaxValue, minY = double.MaxValue;
        double maxX = double.MinValue, maxY = double.MinValue;

        bool any = false;

        foreach (var p in polygon)
        {
            if (p == null) continue;

            var t = p.GetType();
            var xProp = t.GetProperty("X");
            var yProp = t.GetProperty("Y");
            if (xProp == null || yProp == null) continue;

            var xVal = xProp.GetValue(p);
            var yVal = yProp.GetValue(p);
            if (xVal == null || yVal == null) continue;

            var x = Convert.ToDouble(xVal);
            var y = Convert.ToDouble(yVal);

            any = true;
            if (x < minX) minX = x;
            if (y < minY) minY = y;
            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
        }

        if (!any)
            return (0, 0, 0, 0);

        return (minX, minY, maxX - minX, maxY - minY);
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
}