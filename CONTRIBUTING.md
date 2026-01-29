# Contributing to Diagram Analyzer

Thank you for your interest in contributing to the Diagram Analyzer project! 🎉

## Getting Started

1. **Fork the repository**
2. **Clone your fork:**
   ```bash
   git clone https://github.com/YOUR-USERNAME/AzureAITalk-.git
   cd AzureAITalk-
   ```

3. **Set up your development environment:**
   - Install .NET 8 SDK
   - Create Azure Computer Vision and Azure OpenAI resources
   - Configure user secrets (see Setup section below)

## Setup User Secrets

```bash
cd DiagramAnalyzer.Web
dotnet user-secrets init
dotnet user-secrets set "AzureVision:Endpoint" "https://YOUR-NAME.cognitiveservices.azure.com/"
dotnet user-secrets set "AzureVision:ApiKey" "YOUR_KEY_HERE"
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-NAME.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_KEY_HERE"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"
```

## Making Changes

1. **Create a new branch:**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes:**
   - Follow the existing code style
   - Add XML documentation comments for public APIs
   - Use async/await for I/O operations
   - Add error handling and logging

3. **Build and test:**
   ```bash
   dotnet build
   dotnet run --project DiagramAnalyzer.Web
   ```

4. **Commit your changes:**
   ```bash
   git add .
   git commit -m "Add: Brief description of your changes"
   ```

## Code Style

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Use dependency injection
- Implement proper error handling
- Add structured logging

## Pull Request Process

1. **Update documentation** if you've made significant changes
2. **Ensure the build succeeds** with no warnings
3. **Create a Pull Request** with a clear description of changes
4. **Link related issues** in your PR description
5. **Wait for review** - maintainers will review your PR

## Reporting Issues

- Use the GitHub issue tracker
- Include clear steps to reproduce
- Provide error messages and logs
- Specify your environment (.NET version, OS, etc.)

## Code of Conduct

- Be respectful and inclusive
- Focus on constructive feedback
- Help others learn and grow

## Questions?

Feel free to open an issue for questions or reach out to [@Ashahet1](https://github.com/Ashahet1).

Thank you for contributing! 🚀
