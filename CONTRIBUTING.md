# Contributing to AzureAITalk

Thank you for your interest in contributing to this project! This document explains the review process and how to contribute effectively.

## 🔍 How the Review Process Works

### Overview
All contributions go through a review process to ensure code quality, maintain consistency, and prevent issues. Here's how it works:

### Step-by-Step Review Process

#### 1. **Before You Start**
   - Check existing issues to avoid duplicate work
   - Fork the repository to your GitHub account
   - Clone your fork locally
   - Create a new branch for your changes
   
   ```bash
   git clone https://github.com/YOUR-USERNAME/AzureAITalk-.git
   cd AzureAITalk-
   git checkout -b feature/your-feature-name
   ```

#### 2. **Making Changes**
   - Follow the existing code style and patterns
   - Write clear, descriptive commit messages
   - Keep changes focused and atomic
   - Test your changes thoroughly
   - Update documentation if needed

#### 3. **Testing Your Changes**
   Before submitting a pull request, ensure:
   - ✅ Code builds successfully
   - ✅ All existing tests pass
   - ✅ New features have appropriate tests
   - ✅ No warnings or errors in the console
   
   ```bash
   # Build the solution
   dotnet build DiagramAnalyzer.sln
   
   # Run tests (if available)
   dotnet test
   ```

#### 4. **Submitting a Pull Request**
   - Push your branch to your fork
   - Open a pull request against the main repository
   - Fill out the PR template completely
   - Link any related issues
   
   ```bash
   git push origin feature/your-feature-name
   ```

#### 5. **The Review Process**

   **Phase 1: Automated Checks**
   - CI/CD pipelines run automatically
   - Build verification
   - Test execution
   - Code quality checks

   **Phase 2: Maintainer Review**
   - A maintainer will review your code
   - They may request changes or ask questions
   - Address feedback promptly and professionally
   - Push additional commits to the same branch

   **Phase 3: Approval & Merge**
   - Once approved, your PR will be merged
   - Your contribution becomes part of the project!

#### 6. **After Review: Addressing Feedback**
   
   When reviewers request changes:
   
   1. **Read all comments carefully**
      - Understand what needs to be changed
      - Ask questions if anything is unclear
   
   2. **Make the requested changes**
      ```bash
      # Make your changes
      git add .
      git commit -m "Address review feedback: [description]"
      git push origin feature/your-feature-name
      ```
   
   3. **Respond to comments**
      - Mark conversations as resolved when addressed
      - Explain your approach if you took a different solution
   
   4. **Re-request review**
      - Once all changes are made, request another review

## 📋 Review Checklist

Reviewers will check for:

### Code Quality
- [ ] Code follows existing patterns and conventions
- [ ] No unnecessary complexity
- [ ] Proper error handling
- [ ] No code duplication
- [ ] Meaningful variable and method names

### Functionality
- [ ] Changes work as intended
- [ ] No breaking changes to existing functionality
- [ ] Edge cases are handled
- [ ] Performance implications considered

### Documentation
- [ ] Code is well-commented where necessary
- [ ] README updated if needed
- [ ] API documentation updated
- [ ] Configuration changes documented

### Testing
- [ ] Changes are testable
- [ ] Tests are included (if applicable)
- [ ] All tests pass
- [ ] No test coverage regression

### Security
- [ ] No security vulnerabilities introduced
- [ ] API keys and secrets not committed
- [ ] Input validation implemented
- [ ] Dependencies are secure

## 🎯 What Makes a Good Pull Request?

1. **Clear Description**
   - Explain what changes you made
   - Why the changes are necessary
   - How to test the changes

2. **Small and Focused**
   - One feature/fix per PR
   - Easy to review and understand
   - Reduces merge conflicts

3. **Well Tested**
   - Include tests for new features
   - Verify existing functionality still works
   - Test edge cases

4. **Clean History**
   - Logical commit messages
   - Each commit builds successfully
   - No "fix typo" commits (squash if needed)

## 🚀 Types of Contributions

### Bug Fixes
- Describe the bug clearly
- Include steps to reproduce
- Provide the fix with tests

### New Features
- Discuss in an issue first
- Ensure it aligns with project goals
- Include documentation and tests

### Documentation
- Fix typos or unclear sections
- Add missing documentation
- Improve examples

### Performance Improvements
- Provide benchmarks
- Explain the optimization
- Ensure no functionality is lost

## 💬 Communication

- Be respectful and professional
- Assume positive intent
- Ask questions if you're unsure
- Respond to feedback in a timely manner

## 🔧 Development Setup

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code
- Azure account (for testing with real services)

### Local Development
1. Clone the repository
2. Open `DiagramAnalyzer.sln` in your IDE
3. Configure Azure credentials in `appsettings.json`
4. Run the application:
   ```bash
   cd DiagramAnalyzer.Web
   dotnet run
   ```

### Project Structure
- `DiagramAnalyzer.Core/` - Core business logic and services
- `DiagramAnalyzer.Web/` - Blazor Server UI

## 📞 Getting Help

- **Questions?** Open a discussion or issue
- **Stuck?** Comment on your PR and ask for guidance
- **Found a bug?** Open an issue with details

## 📄 License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to AzureAITalk! 🎉
