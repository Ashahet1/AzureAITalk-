using DiagramAnalyzer.Core.Models;

namespace DiagramAnalyzer.Core.Services;

public interface IGptVisionService
{
    Task<string> AnalyzeDiagramStructureAsync(byte[] imageData, List<ExtractedText> extractedText);
}