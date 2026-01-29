using System.ComponentModel.DataAnnotations;

namespace DiagramAnalyzer.Core.Configuration;

public class AzureVisionSettings
{
    [Required]
    public string Endpoint { get; set; } = string.Empty;
    
    [Required]
    public string ApiKey { get; set; } = string.Empty;
    
    public int MaxRetryAttempts { get; set; } = 3;
    public int TimeoutSeconds { get; set; } = 60;
}