namespace DiagramAnalyzer.Core.Models;

/// <summary>
/// Complete result of diagram analysis
/// </summary>
public class DiagramResult
{
    /// <summary>
    /// Detected nodes in the diagram
    /// </summary>
    public List<DiagramNode> Nodes { get; set; } = new();

    /// <summary>
    /// Detected edges/connections
    /// </summary>
    public List<DiagramEdge> Edges { get; set; } = new();

    /// <summary>
    /// All extracted text elements
    /// </summary>
    public List<ExtractedText> ExtractedText { get; set; } = new();

    /// <summary>
    /// Diagram type (e.g., "flowchart", "org-chart", "network-diagram")
    /// </summary>
    public string DiagramType { get; set; } = string.Empty;

    /// <summary>
    /// Overall description/summary of the diagram
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Processing metadata
    /// </summary>
    public DiagramMetadata Metadata { get; set; } = new();
}

/// <summary>
/// Metadata about the diagram analysis process
/// </summary>
public class DiagramMetadata
{
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public TimeSpan ProcessingTime { get; set; }
    public string ProcessorVersion { get; set; } = "1.0.0";
    public Dictionary<string, object> AdditionalInfo { get; set; } = new();
}