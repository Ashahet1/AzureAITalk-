namespace DiagramAnalyzer.Core.Models;

/// <summary>
/// Represents text extracted from the diagram with positioning
/// </summary>
public class ExtractedText
{
    public string Text { get; set; } = string.Empty;
    public BoundingBox? BoundingBox { get; set; }
    public double Confidence { get; set; }
}