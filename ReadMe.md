# Manufacturing Knowledge Graph — Cross-Modal Intelligence for Quality Control

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Azure AI Vision](https://img.shields.io/badge/Azure-AI%20Vision-0089D6)](https://azure.microsoft.com/en-us/products/ai-services/ai-vision/)
[![GitHub Issues](https://img.shields.io/github/issues/Ashahet1/AzureAITalk-)](https://github.com/Ashahet1/AzureAITalk-/issues)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/Ashahet1/AzureAITalk-)](https://github.com/Ashahet1/AzureAITalk-/pulls)
[![GitHub Stars](https://img.shields.io/github/stars/Ashahet1/AzureAITalk-?style=social)](https://github.com/Ashahet1/AzureAITalk-/stargazers)

> An AI-powered manufacturing quality control system that builds cross-modal knowledge graphs from visual defect data, enabling intelligent pattern discovery across product types.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Quick Start](#quick-start)
- [Installation](#installation)
- [Dataset](#dataset)
- [Interactive Menu](#interactive-menu)
- [Analytics & Visualizations](#analytics--visualizations)
- [API Usage](#api-usage--queries)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

A **complete .NET 10 console application** that builds a cross-modal knowledge graph for manufacturing quality control. The system connects visual defect data from Azure AI Vision with process knowledge, inspection equipment, and ISO 9001 standards — then exposes an **interactive 13-option menu** with analytics, console-based visualizations, and exportable dashboards.

### What Makes This Novel?

| Traditional Systems | This System |
|---|---|
| Analyze each product in isolation | Discovers patterns **across** product types |
| Simple defect detection without context | Links defects → equipment → standards automatically |
| Manual documentation of procedures | Enables knowledge transfer between manufacturing lines |
| No cross-line intelligence | Answers complex queries like *"Find similar defects across all products"* |

---

## Features

- **Azure AI Vision integration** — Caption, Tag, and Object detection on industrial images
- **Cross-modal knowledge graph** — Nodes (image, defect, equipment, standard) connected by typed relationships
- **Cross-product pattern discovery** — Finds similar defects across different product categories
- **Equipment recommendations** — Maps defect types to required inspection equipment
- **Console-based visualizations** — Bar charts, pie charts, heatmaps, and network diagrams via `ChartGenerator`
- **Analytics dashboard** — Key metrics, severity distribution, defect frequency, equipment usage, quality heatmap, and AI-generated insights
- **Graph persistence** — Save / load / delete the knowledge graph as JSON for instant startup
- **Dashboard export** — Export the full analytics dashboard to a timestamped text file
- **Interactive menu** — 13 options covering queries, analytics, cache management, and export

---

## Dataset

**MVTec Anomaly Detection (MVTec AD)**
- Download: https://www.mvtec.com/company/research/datasets/mvtec-ad/downloads
- 15 product categories with real industrial defects
- Products: bottle, cable, capsule, carpet, grid, hazelnut, leather, metal_nut, pill, screw, tile, toothbrush, transistor, wood, zipper

```
mvtec_anomaly_detection/
├── bottle/
│   ├── ground_truth/
│   ├── test/
│   │   ├── broken_large/
│   │   ├── broken_small/
│   │   ├── contamination/
│   │   └── good/
│   └── train/
├── metal_nut/
│   ├── test/
│   │   ├── bent/
│   │   ├── color/
│   │   ├── flip/
│   │   ├── scratch/
│   │   └── good/
│   └── train/
└── ...
```

---

## Architecture

```
┌───────────────────────────────────────────────────────────┐
│                    KNOWLEDGE GRAPH                         │
│                                                           │
│  [Image Node] ──has_defect──► [Defect Node]              │
│       │                            │                      │
│   product: metal_nut          requires_equipment          │
│                                    │                      │
│                                    ▼                      │
│                          [Equipment Node]                  │
│                       (High-res microscope)                │
│                                                           │
│  [Defect A: scratch]                                      │
│       │              ◄── similar_defect ──►               │
│  (metal_nut)              [Defect B: scratch]             │
│                              (zipper)                     │
│                                                           │
│  [ISO 9001 Section 8.5] ◄── specified_in ── [Equipment]  │
└───────────────────────────────────────────────────────────┘
```

### Source Files

| File | Purpose |
|---|---|
| **Program.cs** | Entry point, interactive 13-option menu, all query/export workflows |
| **KnowledgeGraph.cs** | Graph data structure, query engine, analytics methods, JSON persistence |
| **AzureVisionAnalyzer.cs** | Azure AI Vision SDK integration (Caption + Tags + Objects) |
| **GraphBuilder.cs** | MVTec dataset processor, domain knowledge rules, severity logic |
| **ChartGenerator.cs** | Console-based bar charts, pie charts, heatmaps, network diagrams, dashboards |

---

## Quick Start

### Installation Options

#### Option 1: Clone from GitHub (Recommended for Development)

```bash
git clone https://github.com/Ashahet1/AzureAITalk-.git
cd AzureAITalk-
dotnet restore
dotnet build
```

#### Option 2: NuGet Package (Coming Soon)

```bash
# Install via NuGet (when published)
dotnet add package ManufacturingVisionAnalyzer
```

#### Option 3: GitHub Packages

```bash
# Add GitHub Packages source
dotnet nuget add source https://nuget.pkg.github.com/Ashahet1/index.json \
  -n github \
  -u YOUR_GITHUB_USERNAME \
  -p YOUR_GITHUB_TOKEN

# Install the package
dotnet add package ManufacturingVisionAnalyzer --version 2.0.0
```

### Prerequisites

- **.NET 10.0 SDK** (or later)
- **Azure AI Vision** resource (Free F0 tier works)
- **MVTec AD** dataset downloaded and extracted

### 1. Create Azure AI Vision Resource

```bash
# Azure Portal
# 1. https://portal.azure.com → Create resource → AI + Machine Learning → Computer Vision
# 2. Choose Free tier (F0) — 20 calls/min, 5K calls/month
# 3. Copy Endpoint and Key from "Keys and Endpoint"

# Or via Azure CLI
az cognitiveservices account create \
  --name MyVisionResource \
  --resource-group MyResourceGroup \
  --kind ComputerVision \
  --sku F0 \
  --location eastus
```

### 2. Configure Credentials

**Option A — Environment Variables (recommended)**

```powershell
# PowerShell
$env:VISION_ENDPOINT = "https://your-resource.cognitiveservices.azure.com/"
$env:VISION_KEY      = "your-key-here"
```

```bash
# Bash
export VISION_ENDPOINT="https://your-resource.cognitiveservices.azure.com/"
export VISION_KEY="your-key-here"
```

**Option B — Hard-code in Program.cs** (quick testing only)

```csharp
string azureEndpoint = "https://your-resource.cognitiveservices.azure.com/";
string azureKey = "your-key-here";
```

### 3. Run

```bash
# Pass the dataset path as an argument
dotnet run -- "C:\path\to\mvtec_anomaly_detection"

# Or run and enter the path when prompted
dotnet run
```

On first run the system builds the knowledge graph from Azure Vision API calls (~10-15 min on free tier) and caches the result to `knowledge_graph.json`. Subsequent runs load instantly from cache.

---

## Interactive Menu

Once the graph is loaded, the application presents:

```
🔍 INTERACTIVE QUERY MENU
══════════════════════════════════════════════════════
 1. 📦 Find defects by product type
 2. 🔧 Get equipment recommendations
 3. 🔗 Find similar defects across products (NOVEL!)
 4. 📋 View all products in database
 5. 🎯 Custom search by defect type
 6. 📊 Generate visual diagram
 7. 💾 Export results to file
 8. 🔄 Show sample insights
 9. 📊 VIEW COMPLETE DASHBOARD WITH VISUALIZATIONS ⭐
10. 💾 Save current graph to cache
11. 🔄 Rebuild graph from dataset
12. 🗑️  Delete cache file
13. ❌ Exit
══════════════════════════════════════════════════════
```

### Highlights

| Option | Description |
|--------|-------------|
| **1** | Query defects for a specific product (e.g. `bottle`, `metal_nut`) |
| **2** | Map defect types to recommended inspection equipment |
| **3** | **Novel feature** — find the same defect type occurring in *different* products |
| **5** | Free-text search across all defects (e.g. `scratch`, `crack`) |
| **9** | Full analytics dashboard: key metrics, bar charts, pie chart, heatmap, network diagram, AI insights, with optional file export |
| **10–12** | Cache management — save, rebuild, or delete the persisted graph |

---

## Analytics & Visualizations

Option **9** renders a full dashboard directly in the console:

- **Key Metrics** — images analyzed, total defects, product categories, equipment types, cross-product patterns
- **Defect Type Distribution** — horizontal bar chart of defect frequencies
- **Severity Breakdown** — pie chart (Low / Medium / High)
- **Defects by Product** — bar chart per product category
- **Equipment Usage** — bar chart showing which equipment covers the most defects
- **Quality Heatmap** — products × severity matrix
- **Cross-Product Network** — relationship diagram showing knowledge-transfer links
- **AI-Generated Insights** — most common defect, critical products, equipment investment priorities

The dashboard can be exported to a timestamped `.txt` file from within the menu.

---

## Project Structure

```
ManufacturingVisionAnalyzer/
├── ReadMe.md                              # This file
├── ManufacturingVisionAnalyzer.csproj     # .NET 10 project, Azure Vision SDK
├── Program.cs                             # Entry point + 13-option interactive menu
├── KnowledgeGraph.cs                      # Graph model, queries, analytics, JSON I/O
├── AzureVisionAnalyzer.cs                 # Azure AI Vision integration
├── GraphBuilder.cs                        # MVTec dataset processing + domain rules
├── ChartGenerator.cs                      # Console charts, heatmaps, dashboards
├── knowledge_graph.json                   # Cached graph (auto-generated)
├── Dashboard_Export_*.txt                 # Exported dashboards (auto-generated)
└── bin/ / obj/                            # Build artifacts
```

---

## API Usage & Queries

### Defects by Product
```csharp
var bottleDefects = graph.QueryDefectsByProduct("bottle");
// → crack, contamination, broken_large, …
```

### Equipment Recommendations
```csharp
var equipment = graph.GetEquipmentRecommendations();
// "scratch" → "High-resolution microscope"
// "crack"   → "Backlight illumination"
```

### Cross-Product Pattern Discovery
```csharp
var similarities = graph.FindSimilarDefectsAcrossProducts();
// scratch in metal_nut ↔ scratch in zipper → same inspection technique
```

### Analytics
```csharp
graph.GetDefectFrequency();       // defect name → count
graph.GetSeverityDistribution();  // Low/Medium/High → count
graph.GetProductDefectCounts();   // product → defect count
graph.GetEquipmentUsage();        // equipment → usage count
graph.GetQualityHeatmap();        // products × severities matrix
graph.GenerateInsights();         // list of AI-generated insight strings
```

### Persistence
```csharp
graph.SaveToFile("knowledge_graph.json");
var loaded = KnowledgeGraph.LoadFromFile("knowledge_graph.json");
KnowledgeGraph.CacheExists("knowledge_graph.json"); // bool
```

---

## Customization

### Processing Speed

```csharp
// Fewer images = faster (default 2 per product)
await builder.ProcessMVTecDataset(mvtecPath, maxImagesPerProduct: 2);

// Full analysis
await builder.ProcessMVTecDataset(mvtecPath, maxImagesPerProduct: 20);
```

### Domain Knowledge

Add equipment or standards in `GraphBuilder.AddDomainKnowledge()`:

```csharp
graph.AddNode(new Node
{
    Id = "eq_laser_scanner",
    Type = "equipment",
    Properties = new() { ["name"] = "3D Laser Scanner" }
});
```

### Defect Detection Logic

Extend the classification in `AzureVisionAnalyzer.ExtractDefectInfo()`:

```csharp
if (caption.Contains("misalignment"))
    defectType = "misalignment";
```

---

## Technical Details

| | |
|---|---|
| **Language** | C# / .NET 10.0 |
| **Cloud Service** | Azure AI Vision — Image Analysis 4.0 (Caption, Tags, Objects) |
| **NuGet** | `Azure.AI.Vision.ImageAnalysis 1.0.0-beta.3` |
| **Serialization** | `System.Text.Json` (built-in) |
| **Data Structure** | In-memory graph (`List<Node>`, `List<Relationship>`) |
| **Persistence** | JSON file cache (`knowledge_graph.json`) |
| **Rate Limiting** | 3.5 s delay between API calls (safe for F0 tier — 20 calls/min) |
| **Memory** | < 100 MB for the full MVTec graph |

---

## Troubleshooting

| Problem | Solution |
|---|---|
| **Authentication failed** | Verify endpoint includes `https://` and trailing `/`. Key is 32 characters, no extra spaces. |
| **Rate limit exceeded** | Reduce `maxImagesPerProduct` or add `await Task.Delay(3500)`. Upgrade to S1 for higher limits. |
| **Directory not found** | Use an absolute path. On Windows use `@"C:\..."` or forward slashes. |
| **No defects found** | Expected for subtle defects — the system falls back to MVTec folder names as the defect category. |

---

## References

- **Azure AI Vision** — https://learn.microsoft.com/azure/ai-services/computer-vision/
- **Azure Vision SDK** — https://learn.microsoft.com/dotnet/api/azure.ai.vision.imageanalysis
- **MVTec AD Dataset** — Bergmann et al., *CVPR 2019* — https://www.mvtec.com/company/research/datasets/mvtec-ad
- **Knowledge Graphs for Manufacturing** — Elsevier 2021
- **Industrial Knowledge Graphs** — Springer 2022

---

## Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details on how to:
- Report bugs
- Suggest features
- Submit pull requests
- Follow coding standards

Please read our [Code of Conduct](CODE_OF_CONDUCT.md) before contributing.

## Security

For security concerns, please see our [Security Policy](SECURITY.md).

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

### Third-Party Components

- **Azure AI Vision**: Requires Azure subscription (Free F0 tier available)
- **MVTec AD Dataset**: Free for research and educational use (see [MVTec license](https://www.mvtec.com/company/research/datasets/mvtec-ad))

## Support

- 📖 [Documentation](https://github.com/Ashahet1/AzureAITalk-/blob/main/ReadMe.md)
- 🐛 [Issue Tracker](https://github.com/Ashahet1/AzureAITalk-/issues)
- 💬 [Discussions](https://github.com/Ashahet1/AzureAITalk-/discussions)
- ⭐ [Star this repo](https://github.com/Ashahet1/AzureAITalk-/stargazers) if you find it useful!

## Acknowledgments

- Azure AI Services team for the Vision API
- MVTec for the Anomaly Detection dataset
- .NET community for excellent tooling and libraries

---

**Last Updated**: 2026-02-10  
**Version**: 2.0 — Complete project with analytics dashboard and extended features  
**Status**: Active Development  
**Repository**: [github.com/Ashahet1/AzureAITalk-](https://github.com/Ashahet1/AzureAITalk-)
