# Code Review Guide

This guide is for maintainers and reviewers to ensure consistent, high-quality code reviews.

## 🎯 Goals of Code Review

1. **Catch bugs and issues early**
2. **Maintain code quality and consistency**
3. **Share knowledge across the team**
4. **Ensure security and performance**
5. **Improve documentation**

## 📋 Review Process Steps

### 1. Initial Assessment (2-5 minutes)
- [ ] Read the PR description and linked issues
- [ ] Understand the purpose and scope
- [ ] Check if the change is appropriate for the project
- [ ] Verify the PR follows the template

### 2. Automated Checks (Automated)
- [ ] CI/CD pipeline passes
- [ ] Build succeeds
- [ ] Tests pass
- [ ] No merge conflicts

### 3. Code Review (10-30 minutes)
Go through the code systematically:

#### Architecture & Design
- [ ] Changes fit the overall architecture
- [ ] No unnecessary complexity
- [ ] Proper separation of concerns
- [ ] Appropriate design patterns used

#### Code Quality
- [ ] Follows C# coding conventions
- [ ] Consistent with existing codebase style
- [ ] No code duplication
- [ ] Clear and meaningful names
- [ ] Functions are focused and small
- [ ] Proper use of async/await

#### Error Handling
- [ ] Exceptions are properly handled
- [ ] Error messages are clear and helpful
- [ ] Logging is appropriate
- [ ] No silent failures

#### Security
- [ ] No hardcoded credentials or secrets
- [ ] Input validation is present
- [ ] No SQL injection vulnerabilities
- [ ] API calls are secure
- [ ] Dependencies are safe

#### Performance
- [ ] No obvious performance issues
- [ ] Efficient algorithms used
- [ ] Database queries are optimized
- [ ] Caching used where appropriate
- [ ] No memory leaks

#### Testing
- [ ] Tests cover the new functionality
- [ ] Tests are meaningful and clear
- [ ] Edge cases are tested
- [ ] Mock objects used appropriately

#### Documentation
- [ ] Code comments where necessary
- [ ] XML documentation for public APIs
- [ ] README updated if needed
- [ ] Configuration changes documented

### 4. Testing (5-15 minutes)
- [ ] Checkout the branch locally
- [ ] Build the application
- [ ] Run tests
- [ ] Manually test the changes
- [ ] Verify edge cases

### 5. Providing Feedback

#### Good Review Comments
✅ **Be specific and constructive**
```
Consider extracting this logic into a separate method for better testability:

public async Task<bool> ValidateImageAsync(Stream imageStream)
{
    // validation logic here
}
```

✅ **Explain the "why"**
```
This could cause a memory leak because the HttpClient isn't being disposed. 
Consider using IHttpClientFactory instead for better resource management.
```

✅ **Offer alternatives**
```
Instead of a nested if, you could use a guard clause:
if (!isValid) return null;
```

✅ **Acknowledge good work**
```
Nice use of the repository pattern here! This makes the code much more testable.
```

#### Poor Review Comments
❌ **Vague criticism**
```
This doesn't look right.
```

❌ **Demanding without explaining**
```
Change this immediately.
```

❌ **Personal preferences without justification**
```
I don't like this approach.
```

### 6. Decision Making

#### Approve ✅
When:
- All checks pass
- Code quality is high
- No security issues
- Documentation is complete
- Tests are adequate

#### Request Changes 🔄
When:
- Critical bugs found
- Security vulnerabilities
- Breaking changes
- Missing tests
- Poor code quality

#### Comment 💬
When:
- Minor suggestions
- Questions for clarification
- Non-blocking improvements

### 7. After Review
- [ ] Update PR labels if needed
- [ ] Assign back to contributor if changes requested
- [ ] Merge if approved (or let contributor merge)
- [ ] Close related issues
- [ ] Thank the contributor!

## 🎨 Review Priorities

### Must Address (P0)
- Security vulnerabilities
- Bugs that break functionality
- Performance regressions
- Breaking API changes

### Should Address (P1)
- Code quality issues
- Missing tests
- Unclear documentation
- Error handling gaps

### Nice to Have (P2)
- Minor style issues
- Optional optimizations
- Suggestion for future improvements

## ⏱️ Review Timing

- **Small PRs (< 100 lines)**: Review within 24 hours
- **Medium PRs (100-500 lines)**: Review within 2-3 days
- **Large PRs (> 500 lines)**: Consider breaking up, review within a week

## 💬 Communication Guidelines

1. **Be Kind and Professional**
   - Assume positive intent
   - Focus on the code, not the person
   - Use "we" instead of "you"

2. **Be Clear and Specific**
   - Provide examples
   - Link to documentation
   - Explain reasoning

3. **Be Timely**
   - Review promptly
   - Respond to questions quickly
   - Don't leave PRs hanging

4. **Be Thorough but Pragmatic**
   - Balance perfection with progress
   - Focus on important issues
   - Pick your battles

## 🔍 Common Issues to Check

### C# Specific
- [ ] `using` statements for IDisposable
- [ ] Null reference checks
- [ ] Async methods end with "Async"
- [ ] ConfigureAwait(false) in libraries
- [ ] Proper exception types
- [ ] LINQ usage is appropriate

### Blazor Specific
- [ ] Component lifecycle methods used correctly
- [ ] StateHasChanged called when needed
- [ ] Event handlers properly registered/unregistered
- [ ] Cascading parameters used appropriately

### Azure Specific
- [ ] API keys not hardcoded
- [ ] Retry policies implemented
- [ ] Proper error handling for API calls
- [ ] Resource cleanup (dispose patterns)

## 📚 Resources

- [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [.NET API Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [Blazor Best Practices](https://docs.microsoft.com/en-us/aspnet/core/blazor/best-practices)

## ✅ Review Checklist Template

Copy this for each review:

```markdown
### Review Checklist for PR #XXX

- [ ] Code builds successfully
- [ ] Tests pass
- [ ] Code follows project conventions
- [ ] No security issues
- [ ] Error handling is appropriate
- [ ] Performance is acceptable
- [ ] Tests cover new functionality
- [ ] Documentation is updated
- [ ] No breaking changes (or properly documented)
- [ ] Manually tested locally

### Verdict: [Approve / Request Changes / Comment]
```

---

Remember: Code review is about improving the code and helping each other grow as developers. Be respectful, constructive, and collaborative! 🤝
