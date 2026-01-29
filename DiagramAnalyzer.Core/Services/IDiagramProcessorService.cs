using DiagramAnalyzer.Core.Models;

namespace DiagramAnalyzer.Core.Services;

public interface IDiagramProcessorService
{
    Task<DiagramResult> ProcessDiagramAsync(byte[] imageData);
}