# Beyond the Text: Diagrams and Flowcharts in Azure Vision and AI

## 🎯 Project Overview

This production-ready Blazor Server application demonstrates how to analyze diagrams and flowcharts using:
- **Azure Computer Vision** - OCR, object detection, and image captioning
- **Azure OpenAI GPT-4 Vision** - Understanding diagram structure and relationships

**Use Case:** Business Process Mining - Convert hand-drawn/whiteboard flowcharts into structured, machine-readable data.

---

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- Azure Computer Vision resource
- Azure OpenAI resource with GPT-4 Vision deployment

### Configuration

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

---

## 🤝 Contributing

We welcome contributions! Please see our contributing guidelines:

- **[CONTRIBUTING.md](CONTRIBUTING.md)** - How to contribute and the review process
- **[CODE_REVIEW_GUIDE.md](CODE_REVIEW_GUIDE.md)** - Guide for code reviewers
- **[Issue Templates](.github/ISSUE_TEMPLATE/)** - Report bugs or request features
- **[Pull Request Template](.github/pull_request_template.md)** - Submit your changes

### Quick Contribution Steps

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes and commit (`git commit -m 'Add amazing feature'`)
4. Push to your branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
6. Wait for review and address feedback
7. Get merged! 🎉

For detailed steps on how the review process works, see [CONTRIBUTING.md](CONTRIBUTING.md).

---

## 👥 Author

**Ashahet1** - [@Ashahet1](https://github.com/Ashahet1)

## 📝 License

MIT License