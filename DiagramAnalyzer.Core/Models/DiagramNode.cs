namespace DiagramAnalyzer.Core.Models;

/// <summary>
/// Represents a node (shape/element) in a diagram
/// </summary>
public class DiagramNode
{
    /// <summary>
    /// Unique identifier for the node
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Text label/content of the node
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Type of node (e.g., "start", "process", "decision", "end", "connector")
    /// </summary>
    public string Type { get; set; } = "process";

    /// <summary>
    /// Bounding box indicating position in the image
    /// </summary>
    public BoundingBox? BoundingBox { get; set; }

    /// <summary>
    /// Confidence score (0-1) for this node detection
    /// </summary>
    public double Confidence { get; set; }

    /// <summary>
    /// Additional metadata or properties
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new();
}