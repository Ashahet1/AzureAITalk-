# Beyond the Text: Diagrams and Flowcharts in Azure Vision and AI

![Build Status](https://github.com/Ashahet1/AzureAITalk-/workflows/Build%20and%20Test/badge.svg)
![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

## 🎯 Project Overview

This production-ready Blazor Server application demonstrates how to analyze diagrams and flowcharts using:
- **Azure Computer Vision** - OCR, object detection, and image captioning
- **Azure OpenAI GPT-4 Vision** - Understanding diagram structure and relationships

**Use Case:** Business Process Mining - Convert hand-drawn/whiteboard flowcharts into structured, machine-readable data.

> 📘 For a comprehensive analysis of the project's current state, completion status, and roadmap, see [PROJECT_STATUS.md](PROJECT_STATUS.md)

---

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- Azure Computer Vision resource
- Azure OpenAI resource with GPT-4 Vision deployment

### Option 1: Local Development with User Secrets (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/Ashahet1/AzureAITalk-.git
   cd AzureAITalk-
   ```

2. **Configure Azure credentials using User Secrets**
   ```bash
   cd DiagramAnalyzer.Web
   dotnet user-secrets init
   dotnet user-secrets set "AzureVision:Endpoint" "https://YOUR-RESOURCE-NAME.cognitiveservices.azure.com/"
   dotnet user-secrets set "AzureVision:ApiKey" "YOUR_AZURE_VISION_KEY"
   dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-RESOURCE-NAME.openai.azure.com/"
   dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_AZURE_OPENAI_KEY"
   dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"
   ```

3. **Build and run**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

4. **Navigate to** `https://localhost:5001` or `http://localhost:5000`

### Option 2: Docker Deployment

1. **Copy and configure environment variables**
   ```bash
   cp .env.example .env
   # Edit .env with your Azure credentials
   ```

2. **Run with Docker Compose**
   ```bash
   docker-compose up -d
   ```

3. **Access the application** at `http://localhost:8080`

### Option 3: Manual Configuration (Not Recommended)

Edit `DiagramAnalyzer.Web/appsettings.json` and add your Azure credentials:

```json
{
  "AzureVision": {
    "Endpoint": "https://YOUR-RESOURCE-NAME.cognitiveservices.azure.com/",
    "ApiKey": "YOUR_AZURE_VISION_KEY_HERE"
  },
  "AzureOpenAI": {
    "Endpoint": "https://YOUR-RESOURCE-NAME.openai.azure.com/",
    "ApiKey": "YOUR_AZURE_OPENAI_KEY_HERE",
    "DeploymentName": "gpt-4-vision"
  }
}
```

⚠️ **Warning:** Do not commit your API keys to source control!

---

## 📊 Case Study Scenarios

### 1. Software Documentation Automation
- **Input:** Flowcharts/architecture diagrams from legacy docs
- **Output:** Auto-generated documentation, code scaffolding
- **Value:** Modernize old systems by digitizing diagram knowledge

### 2. Business Process Mining ⭐ (This Demo)
- **Input:** Hand-drawn or whiteboard process flowcharts
- **Output:** Structured BPMN/workflow definitions
- **Value:** Convert meeting sketches into executable workflows

### 3. Compliance & Audit Trail
- **Input:** Engineering diagrams, network topologies
- **Output:** Validate against standards, detect missing components
- **Value:** Automated compliance checking for regulated industries

### 4. Education/Training Platform
- **Input:** Student-drawn flowcharts or diagrams
- **Output:** Automated grading, feedback on logic/structure
- **Value:** Scale technical education assessment

### 5. Enterprise Knowledge Extraction
- **Input:** Scanned process diagrams from PDFs/images
- **Output:** Searchable knowledge graph
- **Value:** Make tribal knowledge discoverable

---

## ✨ Features

- ✅ Retry Logic with Polly (exponential backoff)
- ✅ Structured Logging using ILogger
- ✅ Error Handling with custom exceptions
- ✅ Async/Await throughout
- ✅ Dependency Injection
- ✅ Drag-and-drop image upload UI
- ✅ Real-time processing indicators
- ✅ Visual results display
- ✅ JSON output viewer
- ✅ Docker support
- ✅ CI/CD with GitHub Actions

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────┐
│         Blazor Server Web App               │
│  (DiagramAnalyzer.Web)                      │
│  - Drag & Drop Upload                       │
│  - Real-time Results Display                │
└─────────────┬───────────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────────┐
│      Diagram Processor Service              │
│  (Orchestration Layer)                      │
└─────┬───────────────────────┬───────────────┘
      │                       │
      ▼                       ▼
┌─────────────────┐   ┌──────────────────────┐
│ Azure Vision    │   │ GPT-4 Vision         │
│ Service         │   │ Service              │
│ - OCR           │   │ - Diagram Analysis   │
│ - Captioning    │   │ - Node/Edge Extract  │
│ - Objects       │   │ - Structure Analysis │
└─────────────────┘   └──────────────────────┘
```

---

## 📖 Documentation

- [**PROJECT_STATUS.md**](PROJECT_STATUS.md) - Comprehensive project analysis and completion status
- [**CONTRIBUTING.md**](CONTRIBUTING.md) - Contributing guidelines
- [**LICENSE**](LICENSE) - MIT License

---

## 🚢 Deployment

### Azure App Service

1. **Create an Azure App Service** (Linux, .NET 8)

2. **Configure App Settings** in Azure Portal:
   ```
   AzureVision__Endpoint = https://YOUR-RESOURCE.cognitiveservices.azure.com/
   AzureVision__ApiKey = YOUR_KEY
   AzureOpenAI__Endpoint = https://YOUR-RESOURCE.openai.azure.com/
   AzureOpenAI__ApiKey = YOUR_KEY
   AzureOpenAI__DeploymentName = gpt-4-vision
   ```

3. **Deploy using GitHub Actions** (see `.github/workflows/build.yml`)

### Docker Container

Build and run the Docker container:
```bash
docker build -t diagramanalyzer .
docker run -p 8080:8080 \
  -e AzureVision__Endpoint="YOUR_ENDPOINT" \
  -e AzureVision__ApiKey="YOUR_KEY" \
  -e AzureOpenAI__Endpoint="YOUR_ENDPOINT" \
  -e AzureOpenAI__ApiKey="YOUR_KEY" \
  diagramanalyzer
```

---

## 🧪 Testing

Run tests:
```bash
dotnet test
```

Run with coverage (once tests are added):
```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

---

## 👥 Author

**Ashahet1** - [@Ashahet1](https://github.com/Ashahet1)

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- Azure AI Services for powerful vision and language capabilities
- Blazor team for the excellent web framework
- Community contributors

---

## 📞 Support

If you encounter issues:
1. Check [PROJECT_STATUS.md](PROJECT_STATUS.md) for known issues
2. Search existing [GitHub Issues](https://github.com/Ashahet1/AzureAITalk-/issues)
3. Open a new issue with detailed information

---

## ⭐ Star this repository

If you find this project useful, please give it a star ⭐ on GitHub!