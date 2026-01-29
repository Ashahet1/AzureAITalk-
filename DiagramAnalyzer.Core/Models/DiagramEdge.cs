namespace DiagramAnalyzer.Core.Models;

/// <summary>
/// Represents a connection/relationship between nodes
/// </summary>
public class DiagramEdge
{
    /// <summary>
    /// Unique identifier for the edge
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Source node ID
    /// </summary>
    public string SourceNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Target node ID
    /// </summary>
    public string TargetNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Label on the edge (e.g., "Yes", "No", condition text)
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Type of edge (e.g., "flow", "dependency", "association")
    /// </summary>
    public string Type { get; set; } = "flow";

    /// <summary>
    /// Confidence score (0-1) for this connection
    /// </summary>
    public double Confidence { get; set; }

    /// <summary>
    /// Additional metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new();
}