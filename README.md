# Beyond the Text: Diagrams and Flowcharts in Azure Vision and AI

[![Build Status](https://github.com/Ashahet1/AzureAITalk-/workflows/Build%20and%20Test/badge.svg)](https://github.com/Ashahet1/AzureAITalk-/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download)

## 🎯 Project Overview

This production-ready Blazor Server application demonstrates how to analyze diagrams and flowcharts using:
- **Azure Computer Vision** - OCR, object detection, and image captioning
- **Azure OpenAI GPT-4 Vision** - Understanding diagram structure and relationships

**Use Case:** Business Process Mining - Convert hand-drawn/whiteboard flowcharts into structured, machine-readable data.

---

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))
- Azure Computer Vision resource
- Azure OpenAI resource with GPT-4 Vision deployment

### Setup

⚠️ **Important:** See [SETUP_GUIDE.md](SETUP_GUIDE.md) for detailed setup instructions.

**Quick Setup with User Secrets (Recommended):**

```bash
# Clone the repository
git clone https://github.com/Ashahet1/AzureAITalk-.git
cd AzureAITalk-

# Navigate to web project
cd DiagramAnalyzer.Web

# Initialize user secrets
dotnet user-secrets init

# Configure Azure Computer Vision
dotnet user-secrets set "AzureVision:Endpoint" "https://YOUR-NAME.cognitiveservices.azure.com/"
dotnet user-secrets set "AzureVision:ApiKey" "YOUR_KEY_HERE"

# Configure Azure OpenAI
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-NAME.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_KEY_HERE"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "gpt-4-vision"

# Build and run
dotnet restore
dotnet build
dotnet run
```

Navigate to `https://localhost:5001` in your browser.

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
- ✅ Error Handling with graceful fallbacks
- ✅ Async/Await throughout
- ✅ Dependency Injection
- ✅ Drag-and-drop image upload UI
- ✅ Real-time processing indicators
- ✅ Visual results display
- ✅ JSON output viewer
- ✅ Bootstrap 5 responsive design

---

## 📚 Documentation

- **[SETUP_GUIDE.md](SETUP_GUIDE.md)** - Detailed setup instructions
- **[ANALYSIS_REPORT.md](ANALYSIS_REPORT.md)** - Comprehensive project analysis
- **[CONTRIBUTING.md](CONTRIBUTING.md)** - How to contribute

---

## 🏗️ Architecture

```
DiagramAnalyzer/
├── DiagramAnalyzer.Core/         # Core business logic
│   ├── Configuration/            # Settings classes
│   ├── Models/                   # Data models
│   └── Services/                 # Azure service integrations
└── DiagramAnalyzer.Web/          # Blazor Server UI
    ├── Components/               # Razor components
    │   ├── Layout/              # Layouts
    │   └── Pages/               # Pages
    └── wwwroot/                 # Static files
```

---

## 🛠️ Technology Stack

- **Framework:** .NET 8.0, Blazor Server
- **Azure Services:** Computer Vision, OpenAI (GPT-4 Vision)
- **UI:** Bootstrap 5, Razor Components
- **Patterns:** Dependency Injection, Repository Pattern
- **Resilience:** Polly for retry logic
- **Logging:** ILogger structured logging

---

## 🤝 Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for details.

---

## 👥 Author

**Ashahet1** - [@Ashahet1](https://github.com/Ashahet1)

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## ⚡ Status

- ✅ **Build:** Passing
- ✅ **Compilation:** No errors, no warnings
- ✅ **Core Features:** Fully implemented
- 📊 **Code Quality:** 4.3/5.0 (Production-Ready)

---

## 🔗 Links

- [Documentation](ANALYSIS_REPORT.md)
- [Setup Guide](SETUP_GUIDE.md)
- [Contributing](CONTRIBUTING.md)
- [License](LICENSE)
- [Issues](https://github.com/Ashahet1/AzureAITalk-/issues)