# Contributing to DiagramAnalyzer

Thank you for your interest in contributing to the DiagramAnalyzer project! This document provides guidelines for contributing to this project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [How to Contribute](#how-to-contribute)
- [Coding Standards](#coding-standards)
- [Testing Guidelines](#testing-guidelines)
- [Pull Request Process](#pull-request-process)

## Code of Conduct

This project adheres to a code of conduct that all contributors are expected to follow. Please be respectful and constructive in all interactions.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR-USERNAME/AzureAITalk-.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Test your changes
6. Commit with clear messages: `git commit -m "Add: Description of your changes"`
7. Push to your fork: `git push origin feature/your-feature-name`
8. Open a Pull Request

## Development Setup

### Prerequisites

- .NET 8 SDK or later
- Visual Studio 2022, VS Code, or Rider
- Azure Computer Vision resource (for testing)
- Azure OpenAI resource with GPT-4 Vision deployment (for testing)

### Setup Steps

1. **Install Dependencies**
   ```bash
   dotnet restore
   ```

2. **Configure Azure Credentials**
   
   For development, use user secrets:
   ```bash
   cd DiagramAnalyzer.Web
   dotnet user-secrets init
   dotnet user-secrets set "AzureVision:Endpoint" "YOUR-ENDPOINT"
   dotnet user-secrets set "AzureVision:ApiKey" "YOUR-KEY"
   dotnet user-secrets set "AzureOpenAI:Endpoint" "YOUR-ENDPOINT"
   dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR-KEY"
   dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   cd DiagramAnalyzer.Web
   dotnet run
   ```

5. **Navigate to** `https://localhost:5001` or `http://localhost:5000`

## How to Contribute

### Reporting Bugs

- Use the GitHub Issues page
- Include a clear title and description
- Provide steps to reproduce
- Include error messages and screenshots if applicable
- Specify your environment (OS, .NET version, etc.)

### Suggesting Enhancements

- Use the GitHub Issues page with the "enhancement" label
- Clearly describe the feature and its benefits
- Provide examples or mockups if possible

### Contributing Code

We welcome contributions in these areas:

1. **Bug Fixes** - Fix existing issues
2. **New Features** - Add new functionality
3. **Documentation** - Improve docs, add examples
4. **Tests** - Add or improve test coverage
5. **Performance** - Optimize code performance
6. **UI/UX** - Improve user interface and experience

## Coding Standards

### C# Guidelines

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods small and focused
- Use async/await for I/O operations
- Handle exceptions appropriately

### Example

```csharp
/// <summary>
/// Analyzes a diagram image and extracts structured information.
/// </summary>
/// <param name="imageData">The image bytes to analyze.</param>
/// <returns>A DiagramResult containing nodes, edges, and metadata.</returns>
/// <exception cref="ArgumentNullException">Thrown when imageData is null.</exception>
public async Task<DiagramResult> AnalyzeDiagramAsync(byte[] imageData)
{
    if (imageData == null)
        throw new ArgumentNullException(nameof(imageData));
    
    // Implementation
}
```

### Blazor/Razor Guidelines

- Use meaningful component names
- Keep components focused and single-purpose
- Use `@code` blocks at the end of files
- Follow naming conventions: PascalCase for components

### Git Commit Messages

- Use present tense: "Add feature" not "Added feature"
- Use imperative mood: "Move cursor to..." not "Moves cursor to..."
- Start with a capital letter
- Limit first line to 50 characters
- Add detailed description after a blank line if needed

Examples:
```
Add export to JSON functionality
Fix null reference in diagram processor
Update README with deployment instructions
```

## Testing Guidelines

### Unit Tests

- Write tests for all new functionality
- Use xUnit or NUnit
- Mock external dependencies (Azure APIs)
- Aim for >70% code coverage
- Name tests clearly: `MethodName_Scenario_ExpectedResult`

Example:
```csharp
[Fact]
public async Task ProcessDiagramAsync_WithValidImage_ReturnsResult()
{
    // Arrange
    var mockVision = new Mock<IAzureVisionService>();
    var processor = new DiagramProcessorService(mockVision.Object, ...);
    var imageData = GetTestImageBytes();
    
    // Act
    var result = await processor.ProcessDiagramAsync(imageData);
    
    // Assert
    Assert.NotNull(result);
    Assert.True(result.Nodes.Count > 0);
}
```

### Integration Tests

- Test complete workflows
- Use TestServer for web app testing
- Clean up resources after tests

## Pull Request Process

1. **Before Submitting**
   - Ensure your code builds without warnings
   - Run existing tests and add new ones
   - Update documentation if needed
   - Follow the coding standards

2. **PR Description**
   - Clearly describe what changes were made
   - Reference any related issues: "Fixes #123"
   - Include screenshots for UI changes
   - List any breaking changes

3. **Review Process**
   - Maintainers will review your PR
   - Address feedback and requested changes
   - Keep discussions professional and constructive

4. **After Approval**
   - Maintainers will merge your PR
   - Your contribution will be acknowledged in release notes

## Project Structure

```
AzureAITalk-/
├── DiagramAnalyzer.Core/          # Core business logic
│   ├── Configuration/              # Settings classes
│   ├── Models/                     # Data models
│   └── Services/                   # Service implementations
├── DiagramAnalyzer.Web/            # Blazor web application
│   ├── Components/                 # Razor components
│   │   ├── Layout/                 # Layout components
│   │   └── Pages/                  # Page components
│   └── wwwroot/                    # Static files
└── DiagramAnalyzer.Tests/          # Test project (to be created)
```

## Questions?

If you have questions:
- Open an issue on GitHub
- Check existing issues and documentation
- Contact the maintainers

## Recognition

Contributors will be recognized in:
- Release notes
- README.md contributors section
- GitHub contributors page

Thank you for contributing to DiagramAnalyzer! 🎉
