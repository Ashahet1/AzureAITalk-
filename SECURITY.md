# Security Policy

## Supported Versions

We release security updates for the following versions:

| Version | Supported          |
| ------- | ------------------ |
| 2.0.x   | :white_check_mark: |
| 1.0.x   | :x:                |

## Reporting a Vulnerability

We take the security of Manufacturing Vision Analyzer seriously. If you believe you have found a security vulnerability, please report it to us as described below.

### How to Report

**Please do NOT report security vulnerabilities through public GitHub issues.**

Instead, please report them via:
1. GitHub Security Advisories (preferred)
2. Create a private security advisory at: https://github.com/Ashahet1/AzureAITalk-/security/advisories/new
3. Or email the maintainers (if contact email is available in the repository)

### What to Include

Please include the following information in your report:
- Type of vulnerability
- Full paths of source file(s) related to the vulnerability
- Location of the affected source code (tag/branch/commit or direct URL)
- Step-by-step instructions to reproduce the issue
- Proof-of-concept or exploit code (if possible)
- Impact of the issue, including how an attacker might exploit it

### Response Timeline

- **Initial Response**: Within 48 hours of receiving your report
- **Status Update**: Within 7 days with an assessment of the report
- **Fix Timeline**: Depends on severity and complexity
  - Critical: Within 7 days
  - High: Within 30 days
  - Medium: Within 90 days
  - Low: Next scheduled release

### What to Expect

- Confirmation of receipt within 48 hours
- Regular updates on the progress of fixing the vulnerability
- Credit in the security advisory (if you wish)
- Notification when the vulnerability is fixed and disclosed

## Security Best Practices

When using Manufacturing Vision Analyzer, follow these security best practices:

### 1. Protect Azure Credentials

**Never commit credentials to source control:**
```csharp
// ❌ BAD - Don't hardcode credentials
string azureKey = "abc123...";

// ✅ GOOD - Use environment variables
string azureKey = Environment.GetEnvironmentVariable("VISION_KEY");
```

### 2. Use Environment Variables

Store sensitive configuration in environment variables:
```bash
export VISION_ENDPOINT="https://your-resource.cognitiveservices.azure.com/"
export VISION_KEY="your-key-here"
```

### 3. Secure API Keys

- Use Azure Key Vault for production deployments
- Rotate keys regularly
- Use separate keys for development and production
- Implement proper access controls

### 4. Input Validation

- Validate all file paths before processing
- Sanitize user inputs
- Implement rate limiting for API calls
- Handle errors securely without exposing sensitive information

### 5. Keep Dependencies Updated

```bash
# Regularly update NuGet packages
dotnet list package --outdated
dotnet add package Azure.AI.Vision.ImageAnalysis --version <latest>
```

### 6. Network Security

- Use HTTPS for all Azure API calls (enforced by SDK)
- Implement proper firewall rules
- Use Azure Private Endpoints for enhanced security

### 7. Data Protection

- Handle image data securely
- Comply with data privacy regulations (GDPR, etc.)
- Implement proper data retention policies
- Secure exported dashboard files

## Known Security Considerations

### Azure AI Vision Integration

- API calls are made over HTTPS (handled by Azure SDK)
- Rate limiting is implemented (3.5s delay between calls)
- Credentials must be managed securely by the user

### File System Operations

- Application reads from and writes to the local file system
- Users should ensure proper file permissions
- Dashboard exports create timestamped files in the working directory

### Dataset Processing

- Application processes images from the MVTec dataset
- No user authentication is implemented (console application)
- File paths should be validated before processing

## Security Updates

We will:
- Publish security advisories for confirmed vulnerabilities
- Release patches as soon as possible
- Notify users through GitHub releases
- Update documentation with security recommendations

## Disclosure Policy

- Vulnerabilities will be disclosed after a fix is available
- We aim for coordinated disclosure with security researchers
- Credit will be given to reporters (unless they prefer anonymity)
- Severity will be assessed using CVSS v3.1

## Additional Resources

- [Azure Security Best Practices](https://docs.microsoft.com/azure/security/fundamentals/best-practices-and-patterns)
- [.NET Security Guidelines](https://docs.microsoft.com/dotnet/standard/security/)
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)

## Questions

If you have questions about security that are not vulnerabilities, please:
- Open a GitHub Discussion
- Create an issue with the "security" label
- Check existing documentation first

---

Thank you for helping keep Manufacturing Vision Analyzer secure! 🔒
