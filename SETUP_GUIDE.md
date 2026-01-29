# Setup Guide - Diagram Analyzer

This guide will help you get the Diagram Analyzer application up and running.

## Prerequisites

- **.NET 8 SDK** ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Azure Subscription** ([Free trial](https://azure.microsoft.com/free/))
- **Git** for cloning the repository

## Step 1: Clone the Repository

```bash
git clone https://github.com/Ashahet1/AzureAITalk-.git
cd AzureAITalk-
```

## Step 2: Create Azure Resources

### 2.1 Create Azure Computer Vision Resource

1. Go to [Azure Portal](https://portal.azure.com)
2. Click "Create a resource"
3. Search for "Computer Vision"
4. Click "Create"
5. Fill in the details:
   - Subscription: Your subscription
   - Resource group: Create new or use existing
   - Region: Choose your region
   - Name: e.g., "diagram-analyzer-vision"
   - Pricing tier: S1 (or Free F0 for testing)
6. Click "Review + create" then "Create"
7. After deployment, go to the resource
8. **Copy the Endpoint** (e.g., https://YOUR-NAME.cognitiveservices.azure.com/)
9. Click "Keys and Endpoint" in the left menu
10. **Copy Key 1**

### 2.2 Create Azure OpenAI Resource

1. Go to [Azure Portal](https://portal.azure.com)
2. Click "Create a resource"
3. Search for "Azure OpenAI"
4. Click "Create"
5. Fill in the details:
   - Subscription: Your subscription
   - Resource group: Same as above
   - Region: Choose a region that supports GPT-4 Vision
   - Name: e.g., "diagram-analyzer-openai"
   - Pricing tier: Standard S0
6. Click "Review + create" then "Create"
7. After deployment, go to the resource
8. Click "Go to Azure OpenAI Studio"
9. Click "Deployments" in the left menu
10. Click "Create new deployment"
11. Select model: **gpt-4** (vision-enabled)
12. Deployment name: **gpt-4-vision** (or your choice)
13. Click "Create"
14. Go back to the resource in Azure Portal
15. **Copy the Endpoint** (e.g., https://YOUR-NAME.openai.azure.com/)
16. Click "Keys and Endpoint"
17. **Copy Key 1**

## Step 3: Configure Application

### Option A: User Secrets (Recommended for Development)

```bash
cd DiagramAnalyzer.Web

# Initialize user secrets
dotnet user-secrets init

# Set Azure Computer Vision credentials
dotnet user-secrets set "AzureVision:Endpoint" "https://YOUR-VISION-NAME.cognitiveservices.azure.com/"
dotnet user-secrets set "AzureVision:ApiKey" "YOUR_VISION_KEY_HERE"

# Set Azure OpenAI credentials
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-OPENAI-NAME.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_OPENAI_KEY_HERE"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"
```

### Option B: appsettings.Development.json (Alternative)

⚠️ **WARNING:** Never commit real credentials to source control!

1. Copy the template:
   ```bash
   cp appsettings.Development.json.template appsettings.Development.json
   ```

2. Edit `appsettings.Development.json` and replace placeholders with your values

3. This file is in `.gitignore` and won't be committed

### Option C: Environment Variables (For Production)

```bash
export AzureVision__Endpoint="https://YOUR-NAME.cognitiveservices.azure.com/"
export AzureVision__ApiKey="YOUR_KEY_HERE"
export AzureOpenAI__Endpoint="https://YOUR-NAME.openai.azure.com/"
export AzureOpenAI__ApiKey="YOUR_KEY_HERE"
export AzureOpenAI__DeploymentName="gpt-4-vision"
```

## Step 4: Build and Run

```bash
# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the application
cd DiagramAnalyzer.Web
dotnet run
```

The application will start and display URLs like:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

## Step 5: Test the Application

1. Open your browser and navigate to https://localhost:5001
2. You should see the Diagram Analyzer home page
3. Drag and drop a diagram image or click to browse
4. Click "Analyze Diagram"
5. View the results:
   - Detected nodes
   - Connections between nodes
   - Extracted text
   - JSON output

## Troubleshooting

### Build Errors

**Error:** SDK not found
- **Solution:** Install .NET 8 SDK from [here](https://dotnet.microsoft.com/download/dotnet/8.0)

**Error:** NuGet packages not found
- **Solution:** Run `dotnet restore`

### Configuration Errors

**Error:** "Endpoint cannot be null"
- **Solution:** Check that your user secrets or appsettings are configured correctly
- **Check:** Run `dotnet user-secrets list` in DiagramAnalyzer.Web directory

**Error:** "Authentication failed"
- **Solution:** Verify your API keys are correct
- **Check:** Make sure you copied the entire key without extra spaces

### Azure API Errors

**Error:** "Resource not found" or "Unauthorized"
- **Solution:** Verify endpoints and API keys are correct
- **Check:** Ensure your Azure resources are deployed and active

**Error:** "Deployment not found"
- **Solution:** Check that your GPT-4 Vision deployment name matches the configuration
- **Default:** "gpt-4-vision"

**Error:** Rate limit exceeded
- **Solution:** Wait a few moments and try again
- **Note:** Free tier has rate limits

### Runtime Errors

**Error:** Cannot analyze diagram
- **Check:** Image file size (must be < 10MB)
- **Check:** Image format (PNG, JPEG, GIF, BMP)
- **Check:** Azure API quotas and limits

## Development Tips

### VS Code Setup

1. Install C# extension
2. Install C# Dev Kit extension
3. Open folder in VS Code
4. Press F5 to debug

### Visual Studio Setup

1. Open `DiagramAnalyzer.sln`
2. Set `DiagramAnalyzer.Web` as startup project
3. Press F5 to debug

### Hot Reload

When running the app, you can edit Razor files and see changes immediately without restarting.

## Azure Costs

**Estimated Monthly Costs (Light Usage):**

- Computer Vision S1: ~$1-5/month
- Azure OpenAI Standard: Pay-per-token (~$10-50/month depending on usage)

**Free Tier Options:**
- Computer Vision: F0 tier (20 calls/minute)
- Azure OpenAI: No free tier, but very low cost for testing

## Next Steps

- Read [ANALYSIS_REPORT.md](ANALYSIS_REPORT.md) for detailed project analysis
- See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution guidelines
- Check [README.md](README.md) for feature documentation

## Getting Help

- **Issues:** [GitHub Issues](https://github.com/Ashahet1/AzureAITalk-/issues)
- **Discussions:** [GitHub Discussions](https://github.com/Ashahet1/AzureAITalk-/discussions)
- **Azure Docs:** [Azure AI Services](https://docs.microsoft.com/azure/cognitive-services/)

## Security Notes

⚠️ **Important Security Practices:**

1. **Never commit credentials** to source control
2. **Use User Secrets** for local development
3. **Use Azure Key Vault** for production
4. **Rotate keys regularly**
5. **Use Managed Identity** when deploying to Azure

---

**Happy Analyzing!** 🎨📊
