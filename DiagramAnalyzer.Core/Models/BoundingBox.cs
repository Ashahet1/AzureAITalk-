namespace DiagramAnalyzer.Core.Models;

/// <summary>
/// Represents a bounding box for spatial positioning
/// </summary>
public class BoundingBox
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public int CenterX => X + Width / 2;
    public int CenterY => Y + Height / 2;
}