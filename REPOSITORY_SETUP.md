# Repository Setup Guide

This guide provides step-by-step instructions for configuring your GitHub repository with the proper About section, visibility settings, releases, and packages.

## Table of Contents

1. [Setting the About Section](#setting-the-about-section)
2. [Making the Repository Public](#making-the-repository-public)
3. [Creating a New Release](#creating-a-new-release)
4. [Publishing Packages](#publishing-packages)
5. [Additional Configuration](#additional-configuration)

---

## Setting the About Section

The About section appears at the top of your repository and helps visitors understand your project at a glance.

### Steps:

1. Navigate to your repository: `https://github.com/Ashahet1/AzureAITalk-`
2. Click the **⚙️ gear icon** next to "About" (top right of the repository page)
3. Fill in the following information:

#### Description
```
AI-powered manufacturing quality control system using Azure AI Vision and cross-modal knowledge graphs for intelligent defect pattern discovery
```

#### Website (Optional)
```
https://github.com/Ashahet1/AzureAITalk-
```

#### Topics (Tags)
Add the following topics to make your repository more discoverable:
```
azure
ai-vision
manufacturing
quality-control
knowledge-graph
dotnet
csharp
computer-vision
defect-detection
industrial-ai
mvtec-dataset
analytics
dashboard
azure-cognitive-services
net10
```

#### Options to Enable
- ✅ **Use your GitHub repo description for the repo image**
- ✅ **Releases** (to show releases on the sidebar)
- ✅ **Packages** (to show published packages)
- ❌ **Deployments** (not needed for this project)
- ❌ **Environments** (not needed for this project)

4. Click **Save changes**

---

## Making the Repository Public

If your repository is currently private, follow these steps to make it public:

### ⚠️ Warning
Making a repository public will:
- Allow anyone to view and clone your code
- Make all commit history visible
- Make all issues and pull requests visible

**Before making public:**
- Ensure no sensitive data (API keys, passwords, credentials) is in the code or commit history
- Review all files, especially configuration files
- Check the `.gitignore` is properly configured
- Review all commit messages for sensitive information

### Steps:

1. Go to your repository: `https://github.com/Ashahet1/AzureAITalk-`
2. Click **Settings** (tab at the top)
3. Scroll down to the **Danger Zone** section (bottom of the page)
4. Click **Change visibility**
5. Select **Make public**
6. Read the warnings carefully
7. Type the repository name to confirm: `AzureAITalk-`
8. Click **I understand, make this repository public**

### Verification
After making the repository public:
- ✅ Visit the repository in an incognito/private browser window
- ✅ Verify all files are appropriate for public viewing
- ✅ Check that no secrets are exposed
- ✅ Review the About section and README

---

## Creating a New Release

Releases allow you to package and distribute versions of your software.

### Option 1: Using GitHub UI

1. Go to your repository: `https://github.com/Ashahet1/AzureAITalk-`
2. Click **Releases** in the right sidebar (or go to `/releases`)
3. Click **Create a new release** or **Draft a new release**
4. Fill in the release form:

   **Choose a tag:**
   ```
   v2.0.0
   ```
   - Click **Create new tag: v2.0.0 on publish**

   **Release title:**
   ```
   Release v2.0.0 - Complete Knowledge Graph System
   ```

   **Description:**
   ```markdown
   ## Manufacturing Vision Analyzer v2.0.0
   
   ### What's New
   - ✨ Complete cross-modal knowledge graph implementation
   - 🔍 Azure AI Vision integration for defect detection
   - 📊 Interactive analytics dashboard with visualizations
   - 🔗 Cross-product pattern discovery
   - 📦 NuGet package support
   - 📝 Comprehensive documentation
   
   ### Features
   - 13-option interactive menu system
   - Real-time console visualizations
   - Equipment recommendation engine
   - JSON graph persistence
   - Dashboard export functionality
   
   ### Installation
   
   ```bash
   git clone https://github.com/Ashahet1/AzureAITalk-.git
   cd AzureAITalk-
   dotnet restore
   dotnet build
   dotnet run
   ```
   
   ### Requirements
   - .NET 10.0 SDK or later
   - Azure AI Vision resource (Free F0 tier works)
   - MVTec Anomaly Detection dataset
   
   ### Documentation
   See the [README](https://github.com/Ashahet1/AzureAITalk-/blob/main/ReadMe.md) for detailed setup and usage instructions.
   
   ### Breaking Changes
   None - This is the initial public release.
   ```

5. **Optional:** Upload additional files (binaries, installers, etc.)
6. **Optional:** Check **This is a pre-release** if not production-ready
7. Click **Publish release**

### Option 2: Using GitHub Actions (Automated)

The repository includes a release workflow (`.github/workflows/release.yml`) that automatically creates releases when you push a version tag:

```bash
# Create and push a tag
git tag -a v2.0.0 -m "Release version 2.0.0"
git push origin v2.0.0
```

The workflow will automatically:
- Build the project
- Create a GitHub release
- Package as NuGet
- Upload release assets
- Publish to GitHub Packages

### Option 3: Manual Workflow Trigger

You can also manually trigger the release workflow:

1. Go to **Actions** tab
2. Select **Release and Publish** workflow
3. Click **Run workflow**
4. Enter the version number (e.g., `2.0.0`)
5. Click **Run workflow**

---

## Publishing Packages

### GitHub Packages (Recommended - Free)

GitHub Packages is automatically configured in the release workflow. When you create a release (via tag or manual trigger), the package will be published to GitHub Packages.

#### Using GitHub Packages

To install from GitHub Packages, users need to:

```bash
# Add GitHub Packages source
dotnet nuget add source https://nuget.pkg.github.com/Ashahet1/index.json \
  -n github \
  -u GITHUB_USERNAME \
  -p GITHUB_TOKEN

# Install the package
dotnet add package ManufacturingVisionAnalyzer --version 2.0.0
```

### NuGet.org (Optional - Wider Distribution)

To publish to the public NuGet.org:

#### Setup (One-time):

1. Create an account at https://www.nuget.org/
2. Generate an API key:
   - Go to https://www.nuget.org/account/apikeys
   - Click **Create**
   - Give it a name: "Manufacturing Vision Analyzer"
   - Set expiration: Choose appropriate duration
   - Select packages: Choose appropriate scopes
   - Click **Create**
   - **Copy the API key** (you won't see it again!)

3. Add the API key to GitHub Secrets:
   - Go to repository **Settings** → **Secrets and variables** → **Actions**
   - Click **New repository secret**
   - Name: `NUGET_API_KEY`
   - Value: Paste your NuGet API key
   - Click **Add secret**

#### Publishing:

Once the API key is configured, the release workflow will automatically publish to NuGet.org when you create a release (if not marked as pre-release).

### Verification

After publishing:
- ✅ Check GitHub Packages: `https://github.com/Ashahet1?tab=packages`
- ✅ Check NuGet.org: `https://www.nuget.org/packages/ManufacturingVisionAnalyzer`
- ✅ Test installation: `dotnet add package ManufacturingVisionAnalyzer`

---

## Additional Configuration

### Enable GitHub Discussions (Optional)

1. Go to **Settings** → **General**
2. Scroll to **Features**
3. Check ✅ **Discussions**
4. Click **Set up discussions**
5. Create initial categories (Q&A, Ideas, Announcements, etc.)

### Configure Branch Protection (Recommended for Public Repos)

1. Go to **Settings** → **Branches**
2. Click **Add rule** under "Branch protection rules"
3. Branch name pattern: `main`
4. Enable:
   - ✅ Require pull request reviews before merging
   - ✅ Require status checks to pass before merging
   - ✅ Require branches to be up to date before merging
5. Click **Create**

### Set Up Code Scanning (Security)

1. Go to **Security** tab
2. Click **Set up code scanning**
3. Choose **CodeQL Analysis**
4. Click **Set up this workflow**
5. Commit the workflow file

### Add Social Preview Image

1. Go to **Settings** → **General**
2. Scroll to **Social preview**
3. Click **Edit**
4. Upload an image (recommended: 1280x640px)
   - Could be a screenshot of your dashboard
   - Or a diagram of your architecture
5. Click **Upload**

---

## Verification Checklist

After completing all setup steps, verify:

- [ ] About section is filled with description and topics
- [ ] Repository is public (if desired)
- [ ] README.md is comprehensive and includes badges
- [ ] LICENSE file exists (MIT)
- [ ] CONTRIBUTING.md, CODE_OF_CONDUCT.md, SECURITY.md are present
- [ ] .gitignore is properly configured
- [ ] At least one release is published
- [ ] GitHub Packages shows the published package
- [ ] NuGet.org shows the package (if publishing there)
- [ ] All badges in README are working
- [ ] No secrets or credentials in the repository
- [ ] GitHub Actions workflows are configured

---

## Troubleshooting

### Release Creation Fails

**Problem:** GitHub Actions workflow fails during release creation

**Solutions:**
- Ensure you have permissions to create releases
- Check that the tag follows the format `v*.*.*` (e.g., v2.0.0)
- Verify GITHUB_TOKEN has sufficient permissions
- Check workflow logs for specific errors

### Package Publishing Fails

**Problem:** NuGet package fails to publish

**Solutions:**
- Verify NUGET_API_KEY secret is set correctly
- Ensure API key has not expired
- Check package name doesn't conflict with existing packages
- Verify .csproj has correct package metadata
- Check NuGet.org for rate limits or service status

### Can't Make Repository Public

**Problem:** "Make public" button is disabled

**Solutions:**
- Ensure you're the repository owner
- Check if organization settings allow public repositories
- Verify account is in good standing
- Contact GitHub support if issue persists

---

## Resources

- [GitHub Docs - About Repositories](https://docs.github.com/en/repositories)
- [GitHub Docs - Managing Releases](https://docs.github.com/en/repositories/releasing-projects-on-github)
- [GitHub Packages Documentation](https://docs.github.com/en/packages)
- [NuGet.org Publishing Guide](https://docs.microsoft.com/en-us/nuget/nuget-org/publish-a-package)
- [Semantic Versioning](https://semver.org/)

---

**Need Help?**

If you encounter issues not covered in this guide:
1. Check [GitHub Status](https://www.githubstatus.com/)
2. Review [GitHub Community Forum](https://github.community/)
3. Create an issue in the repository
4. Contact repository maintainers

---

*Last Updated: 2026-02-10*
