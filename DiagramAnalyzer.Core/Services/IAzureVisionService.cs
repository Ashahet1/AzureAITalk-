using DiagramAnalyzer.Core.Models;

namespace DiagramAnalyzer.Core.Services;

public interface IAzureVisionService
{
    Task<List<ExtractedText>> ExtractTextAsync(byte[] imageData);
    Task<List<string>> DetectObjectsAsync(byte[] imageData);
    Task<string> GetImageCaptionAsync(byte[] imageData);
}