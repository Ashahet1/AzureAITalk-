# Azure Setup Guide for DiagramAnalyzer

This guide will walk you through setting up the required Azure resources for the DiagramAnalyzer application.

## Prerequisites

- An active Azure subscription
- Azure CLI installed (optional, but recommended)
- Access to Azure Portal

## Estimated Costs

- **Azure Computer Vision**: ~$1-2 per 1,000 images analyzed
- **Azure OpenAI GPT-4 Vision**: ~$0.01-0.03 per image (varies by prompt size)
- **Total for development/testing**: $10-20/month for light usage

## Step 1: Create Azure Computer Vision Resource

### Option A: Using Azure Portal

1. **Go to Azure Portal**: https://portal.azure.com
2. **Click "+ Create a resource"**
3. **Search for "Computer Vision"** and select it
4. **Click "Create"**
5. **Fill in the details**:
   - **Subscription**: Select your subscription
   - **Resource Group**: Create new or select existing (e.g., "rg-diagramanalyzer")
   - **Region**: Choose a region close to you (e.g., "East US", "West Europe")
   - **Name**: Enter a unique name (e.g., "cv-diagramanalyzer-prod")
   - **Pricing Tier**: Select "Free F0" for testing or "Standard S1" for production
6. **Click "Review + Create"** then **"Create"**
7. **Wait for deployment** to complete
8. **Go to the resource** and note:
   - **Endpoint**: Found in "Keys and Endpoint" section (e.g., `https://cv-diagramanalyzer-prod.cognitiveservices.azure.com/`)
   - **API Key**: Found in "Keys and Endpoint" section (use KEY 1)

### Option B: Using Azure CLI

```bash
# Login to Azure
az login

# Create resource group (if needed)
az group create --name rg-diagramanalyzer --location eastus

# Create Computer Vision resource
az cognitiveservices account create \
  --name cv-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --kind ComputerVision \
  --sku F0 \
  --location eastus

# Get the endpoint
az cognitiveservices account show \
  --name cv-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --query "properties.endpoint" -o tsv

# Get the API key
az cognitiveservices account keys list \
  --name cv-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --query "key1" -o tsv
```

## Step 2: Create Azure OpenAI Resource

### Important Notes

- Azure OpenAI is currently in limited access
- You need to apply for access: https://aka.ms/oai/access
- Approval can take several days
- GPT-4 Vision is required for this application

### Option A: Using Azure Portal

1. **Go to Azure Portal**: https://portal.azure.com
2. **Click "+ Create a resource"**
3. **Search for "Azure OpenAI"** and select it
4. **Click "Create"**
5. **Fill in the details**:
   - **Subscription**: Select your subscription
   - **Resource Group**: Use same as Computer Vision (e.g., "rg-diagramanalyzer")
   - **Region**: Choose a region that supports GPT-4 Vision (check availability)
   - **Name**: Enter a unique name (e.g., "oai-diagramanalyzer-prod")
   - **Pricing Tier**: Select "Standard S0"
6. **Click "Review + Create"** then **"Create"**
7. **Wait for deployment** to complete
8. **Go to Azure OpenAI Studio**: https://oai.azure.com
9. **Deploy GPT-4 Vision model**:
   - Click "Deployments" in the left menu
   - Click "+ Create new deployment"
   - Select Model: "gpt-4" with vision capability
   - Deployment name: "gpt-4-vision" (or your preferred name)
   - Click "Create"
10. **Note the deployment details**:
    - **Endpoint**: Found in "Keys and Endpoint" in Azure Portal
    - **API Key**: Found in "Keys and Endpoint" in Azure Portal
    - **Deployment Name**: The name you chose (e.g., "gpt-4-vision")

### Option B: Using Azure CLI

```bash
# Create Azure OpenAI resource
az cognitiveservices account create \
  --name oai-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --kind OpenAI \
  --sku S0 \
  --location eastus

# Get the endpoint
az cognitiveservices account show \
  --name oai-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --query "properties.endpoint" -o tsv

# Get the API key
az cognitiveservices account keys list \
  --name oai-diagramanalyzer-prod \
  --resource-group rg-diagramanalyzer \
  --query "key1" -o tsv

# Deploy GPT-4 Vision model (must be done via Azure OpenAI Studio)
# Visit https://oai.azure.com to deploy the model
```

## Step 3: Configure the Application

### Option 1: User Secrets (Development - Recommended)

```bash
cd DiagramAnalyzer.Web

# Initialize user secrets
dotnet user-secrets init

# Set Computer Vision credentials
dotnet user-secrets set "AzureVision:Endpoint" "https://YOUR-CV-RESOURCE.cognitiveservices.azure.com/"
dotnet user-secrets set "AzureVision:ApiKey" "YOUR_COMPUTER_VISION_KEY"

# Set Azure OpenAI credentials
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-OAI-RESOURCE.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_AZURE_OPENAI_KEY"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"
```

### Option 2: Environment Variables (Docker/Production)

Create a `.env` file:
```bash
cp .env.example .env
```

Edit `.env` with your values:
```env
AZURE_VISION_ENDPOINT=https://YOUR-CV-RESOURCE.cognitiveservices.azure.com/
AZURE_VISION_KEY=YOUR_COMPUTER_VISION_KEY
AZURE_OPENAI_ENDPOINT=https://YOUR-OAI-RESOURCE.openai.azure.com/
AZURE_OPENAI_KEY=YOUR_AZURE_OPENAI_KEY
AZURE_OPENAI_DEPLOYMENT=gpt-4-vision
```

### Option 3: Azure App Service Configuration

In Azure Portal:
1. Go to your App Service
2. Select "Configuration" in the left menu
3. Add Application Settings:
   ```
   AzureVision__Endpoint = https://YOUR-CV-RESOURCE.cognitiveservices.azure.com/
   AzureVision__ApiKey = YOUR_COMPUTER_VISION_KEY
   AzureOpenAI__Endpoint = https://YOUR-OAI-RESOURCE.openai.azure.com/
   AzureOpenAI__ApiKey = YOUR_AZURE_OPENAI_KEY
   AzureOpenAI__DeploymentName = gpt-4-vision
   ```
4. Click "Save"

## Step 4: Verify Configuration

1. **Build the application**:
   ```bash
   dotnet build
   ```

2. **Run the application**:
   ```bash
   cd DiagramAnalyzer.Web
   dotnet run
   ```

3. **Test the application**:
   - Navigate to `https://localhost:5001`
   - Upload a simple flowchart image
   - Verify that analysis completes successfully

## Troubleshooting

### "Unauthorized" or "Invalid API Key" Errors

- Double-check that your API keys are correct
- Ensure there are no extra spaces in the keys
- Verify the endpoint URLs end with a trailing slash
- Confirm your Azure subscription is active

### "Model Deployment Not Found" Errors

- Verify the deployment name matches exactly (case-sensitive)
- Ensure you've deployed GPT-4 Vision in Azure OpenAI Studio
- Check that the model is in a "Succeeded" state

### "Rate Limit Exceeded" Errors

- You may be hitting API rate limits
- For Computer Vision Free tier: 20 calls per minute, 5,000 per month
- For Azure OpenAI: Varies by model and region
- Consider upgrading to a paid tier

### "Resource Not Available in Region" Errors

- Not all Azure regions support all services
- GPT-4 Vision availability is limited
- Check current region availability: https://learn.microsoft.com/azure/ai-services/openai/concepts/models#model-summary-table-and-region-availability

## Best Practices

### Security

1. **Never commit API keys to source control**
2. **Use Azure Key Vault** for production secrets
3. **Rotate API keys regularly** (every 90 days)
4. **Use managed identities** when possible
5. **Restrict network access** to your Azure resources

### Cost Management

1. **Set up cost alerts** in Azure Portal
2. **Monitor API usage** regularly
3. **Use caching** to reduce API calls
4. **Implement rate limiting** in your application
5. **Use the Free tier** for development/testing

### Performance

1. **Choose regions close to your users**
2. **Use Standard tier** for production workloads
3. **Implement retry logic** (already included in this app)
4. **Monitor latency** and adjust regions if needed

## Additional Resources

- [Azure Computer Vision Documentation](https://learn.microsoft.com/azure/ai-services/computer-vision/)
- [Azure OpenAI Documentation](https://learn.microsoft.com/azure/ai-services/openai/)
- [GPT-4 Vision Documentation](https://learn.microsoft.com/azure/ai-services/openai/how-to/gpt-with-vision)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)
- [Azure OpenAI Access Request](https://aka.ms/oai/access)

## Support

If you encounter issues:
1. Check the [Troubleshooting](#troubleshooting) section above
2. Review Azure service health: https://status.azure.com
3. Consult Azure documentation
4. Open an issue on GitHub with detailed error information

---

**Last Updated:** January 2026
