using System.ComponentModel.DataAnnotations;

namespace DiagramAnalyzer.Core.Configuration;

public class AzureOpenAISettings
{
    [Required]
    public string Endpoint { get; set; } = string.Empty;
    
    [Required]
    public string ApiKey { get; set; } = string.Empty;
    
    [Required]
    public string DeploymentName { get; set; } = "gpt-4-vision";
    
    public int MaxRetryAttempts { get; set; } = 3;
    public int TimeoutSeconds { get; set; } = 120;
    public int MaxTokens { get; set; } = 4000;
    public float Temperature { get; set; } = 0.3f;
}