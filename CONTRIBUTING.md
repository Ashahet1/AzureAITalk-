# Contributing to Manufacturing Vision Analyzer

Thank you for your interest in contributing to Manufacturing Vision Analyzer! This document provides guidelines and information for contributors.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [How to Contribute](#how-to-contribute)
- [Development Setup](#development-setup)
- [Pull Request Process](#pull-request-process)
- [Coding Standards](#coding-standards)
- [Testing Guidelines](#testing-guidelines)

## Code of Conduct

By participating in this project, you agree to abide by our Code of Conduct. Please read [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before contributing.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR-USERNAME/AzureAITalk-.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Test your changes thoroughly
6. Commit with clear messages: `git commit -m "Add feature: description"`
7. Push to your fork: `git push origin feature/your-feature-name`
8. Open a Pull Request

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue using the bug report template and include:
- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Environment details (.NET version, OS, etc.)
- Screenshots if applicable

### Suggesting Features

Feature requests are welcome! Please create an issue using the feature request template and describe:
- The problem you're trying to solve
- Your proposed solution
- Any alternatives you've considered
- Additional context

### Code Contributions

We welcome code contributions! Areas where you can help:
- Bug fixes
- New features
- Performance improvements
- Documentation improvements
- Test coverage
- Code refactoring

## Development Setup

### Prerequisites

- .NET 10.0 SDK or later
- Azure AI Vision resource (Free F0 tier works)
- MVTec Anomaly Detection dataset
- Git

### Initial Setup

```bash
# Clone the repository
git clone https://github.com/Ashahet1/AzureAITalk-.git
cd AzureAITalk-

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

### Environment Variables

Set up your Azure credentials:

```bash
export VISION_ENDPOINT="https://your-resource.cognitiveservices.azure.com/"
export VISION_KEY="your-key-here"
```

## Pull Request Process

1. **Update Documentation**: Ensure any new features or changes are documented in the README.md
2. **Follow Coding Standards**: Your code should follow the existing style and conventions
3. **Test Your Changes**: Add tests for new features and ensure all existing tests pass
4. **Update Version**: If applicable, update version numbers following [SemVer](https://semver.org/)
5. **Clear PR Description**: Use the PR template and provide a clear description of changes
6. **Link Issues**: Reference any related issues in your PR description
7. **Request Review**: Tag maintainers for review once ready

### PR Review Criteria

Your PR will be reviewed based on:
- Code quality and readability
- Test coverage
- Documentation completeness
- Performance impact
- Security considerations
- Alignment with project goals

## Coding Standards

### C# Style Guidelines

- Use meaningful variable and method names
- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use async/await for asynchronous operations
- Add XML documentation comments for public APIs
- Keep methods focused and concise
- Use proper exception handling

### Example

```csharp
/// <summary>
/// Analyzes an image for manufacturing defects using Azure AI Vision.
/// </summary>
/// <param name="imagePath">Path to the image file</param>
/// <returns>Defect information extracted from the image</returns>
public async Task<DefectInfo> AnalyzeImageAsync(string imagePath)
{
    // Implementation
}
```

## Testing Guidelines

### Writing Tests

- Write unit tests for new functionality
- Ensure tests are deterministic and repeatable
- Use meaningful test names that describe the scenario
- Test edge cases and error conditions
- Mock external dependencies (Azure API calls)

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "FullyQualifiedName~YourTestName"
```

## Documentation

When contributing, please update:
- README.md for user-facing changes
- Code comments for implementation details
- XML documentation for public APIs
- CHANGELOG.md for notable changes

## Questions?

If you have questions about contributing:
- Open a GitHub Discussion
- Create an issue with the "question" label
- Check existing documentation and issues first

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to Manufacturing Vision Analyzer! 🎉
