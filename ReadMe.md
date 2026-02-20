# Manufacturing Knowledge Graph — Cross-Modal Intelligence for Quality Control

## Project Overview

A **complete .NET 10 console application** that builds a cross-modal knowledge graph for manufacturing quality control. The system connects visual defect data from **Azure AI Vision** with process knowledge, inspection equipment, and ISO 9001 standards — then enriches the results using **Azure OpenAI (GPT-4.1)** for natural-language insights, defect analysis, and flowchart understanding. Exposed through an **interactive 15-option menu** with analytics, console-based visualizations, AI-powered insights, and exportable dashboards.

## Two Azure AI Services Working Together

| Service | Role | What It Does |
|---|---|---|
| **Azure AI Vision** | The Eyes | Image Analysis (Caption, Tags, Objects), OCR with bounding-box coordinates |
| **Azure OpenAI (GPT-4.1)** | The Brain | Classifies defects, generates insights, merges OCR fragments, analyzes flowcharts |

Azure Vision extracts raw visual data with pixel-precise coordinates. Azure OpenAI reasons on top of that structured data — classifying, merging, summarizing, and generating actionable insights.

## Demo Scope (One repo, two modes)

### Mode 1 — Manufacturing Defects (MVTec AD)
- Azure AI Vision Image Analysis (Caption, Tags, Objects)
- Builds a cross-modal knowledge graph linking images → defects → equipment → standards
- Azure OpenAI generates insights for cross-product defect patterns (options 3, 8)

### Mode 2 — Flowcharts & Technical Diagrams
- Azure AI Vision OCR extracts text with bounding-box positions
- **Spatial merge algorithm** clusters nearby OCR lines into logical text blocks (same flowchart box)
- Azure OpenAI classifies each block as Step / Decision / Branch Label / Terminal
- Produces AI-enhanced caption, clean ordered steps, complete decision questions
- Cross-flowchart analysis identifies common patterns across multiple diagrams

### What Makes This Novel?

| Traditional Systems | This System |
|---|---|
| Analyze each product in isolation | Discovers patterns **across** product types |
| Simple defect detection without context | Links defects → equipment → standards automatically |
| OCR returns fragmented text lines | Spatial merge + AI produces clean steps & decisions |
| Manual documentation of procedures | AI-generated flowchart descriptions and insights |
| No cross-line intelligence | Answers complex queries like *"Find similar defects across all products"* |

---

## Features

### Manufacturing Defects (Knowledge Graph)
- **Azure AI Vision integration** — Caption, Tag, and Object detection on industrial images
- **Cross-modal knowledge graph** — Nodes (image, defect, equipment, standard) connected by typed relationships
- **Azure OpenAI defect analysis** — AI-powered pattern matching and natural-language insights
- **Cross-product pattern discovery** — Finds similar defects across different product categories
- **Equipment recommendations** — Maps defect types to required inspection equipment
- **Console-based visualizations** — Bar charts, pie charts, heatmaps, and network diagrams
- **7 local insights** — Defect frequency, severity risk, equipment priority, product complexity, diversity index, graph connectivity (no API calls)
- **Analytics dashboard** — Key metrics, severity distribution, defect frequency, equipment usage, quality heatmap
- **Graph persistence** — Save / load / delete the knowledge graph as JSON for instant startup
- **Dashboard export** — Export the full analytics dashboard to a timestamped text file

### Flowcharts & Diagrams
- **Azure AI Vision OCR** — Extracts text lines with precise bounding-box coordinates
- **Spatial merge algorithm** — Groups OCR lines into logical text blocks using center-X alignment and vertical-gap proximity
- **Azure OpenAI classification** — Classifies merged blocks as Steps, Decisions, Branch Labels, or Terminals
- **AI-enhanced JSON output** — `AiCaption`, `AiSteps`, `AiDecisions`, `AiBranchLabels` alongside raw OCR data
- **Cross-flowchart summary** — AI identifies common themes, shared patterns, and process improvement opportunities
- **Keyword search** — Search across all processed flowchart JSONs by keyword

---

## Dataset

**MVTec Anomaly Detection (MVTec AD)**
- Download: https://www.mvtec.com/company/research/datasets/mvtec-ad/downloads
- 15 product categories with real industrial defects
- Products: bottle, cable, capsule, carpet, grid, hazelnut, leather, metal_nut, pill, screw, tile, toothbrush, transistor, wood, zipper

**Flowcharts** — Sample manufacturing process diagrams in `datasets/flowchart/`

```
datasets/
├── mvtec_anomaly_detection/
│   ├── bottle/
│   │   ├── ground_truth/
│   │   ├── test/
│   │   │   ├── broken_large/
│   │   │   ├── broken_small/
│   │   │   ├── contamination/
│   │   │   └── good/
│   │   └── train/
│   ├── metal_nut/
│   │   ├── test/
│   │   │   ├── bent/
│   │   │   ├── color/
│   │   │   ├── flip/
│   │   │   ├── scratch/
│   │   │   └── good/
│   │   └── train/
│   └── ...
└── flowchart/
    ├── Flowchart001.png
    ├── Flowchart002.png
    └── Flowchart003.png
```

---

## Architecture

```
┌──────────────────────────────────────────────────────────────────────┐
│                     DUAL-SERVICE AI PIPELINE                         │
│                                                                      │
│  ┌─────────────────┐    ┌──────────────────┐    ┌────────────────┐  │
│  │  Azure AI Vision │───►│  Knowledge Graph  │───►│  Azure OpenAI  │  │
│  │  (Caption, Tags, │    │  (Nodes + Edges)  │    │  (GPT-4.1)     │  │
│  │   Objects, OCR)  │    │                    │    │  (Insights +   │  │
│  └─────────────────┘    │  image ──has───►   │    │   Classification│  │
│                          │    defect          │    └────────────────┘  │
│  Images ──────────────►  │      │             │                       │
│                          │  requires_equip    │    Outputs:           │
│  Flowcharts ──────────►  │      │             │    - AI Insights      │
│    (OCR + Spatial Merge) │  equipment         │    - Named Steps      │
│                          │      │             │    - Clean Decisions   │
│                          │  specified_in      │    - Cross-product     │
│                          │      │             │      patterns          │
│                          │  ISO 9001          │    - Flowchart         │
│                          │                    │      descriptions      │
│                          └──────────────────┘                        │
└──────────────────────────────────────────────────────────────────────┘
```

### Flowchart Processing Pipeline

```
Raw Image
    │
    ▼
Azure Vision OCR ──► Raw OCR Lines (fragmented)
    │                 "Stop current"  "production run"
    ▼
Spatial Merge ──────► Merged Blocks (logical boxes)
    │                 "Stop current production run"
    ▼
Azure OpenAI ──────► Classified Output
                      Steps: ["Stop current production run", ...]
                      Decisions: ["Does setup pass verification check?", ...]
                      Branch Labels: ["Yes", "No"]
                      Caption: "Manufacturing Line Changeover Process..."
```

### Source Files

| File | Purpose |
|---|---|
| **Program.cs** | Entry point, interactive 15-option menu, Azure OpenAI integration, all query/export workflows |
| **KnowledgeGraph.cs** | Graph data structure, query engine, 7 local analytics methods, JSON persistence |
| **AzureVisionAnalyzer.cs** | Azure AI Vision SDK integration (Caption + Tags + Objects) |
| **GraphBuilder.cs** | MVTec dataset processor, domain knowledge rules, severity logic |
| **ChartGenerator.cs** | Console-based bar charts, pie charts, heatmaps, network diagrams, dashboards |
| **FlowchartFolderProcessor.cs** | Azure Vision OCR, bounding-box extraction, spatial merge algorithm |

---

## Quick Start

### Prerequisites

- **.NET 10.0 SDK** (or later)
- **Azure AI Vision** resource (Free F0 tier works)
- **Azure OpenAI** resource with a GPT-4.1 deployment
- **MVTec AD** dataset downloaded and extracted

### 1. Create Azure Resources

```bash
# Azure AI Vision (for image analysis + OCR)
az cognitiveservices account create \
  --name MyVisionResource \
  --resource-group MyResourceGroup \
  --kind ComputerVision \
  --sku F0 \
  --location eastus

# Azure OpenAI (for insights + classification)
az cognitiveservices account create \
  --name MyOpenAIResource \
  --resource-group MyResourceGroup \
  --kind OpenAI \
  --sku S0 \
  --location eastus

# Deploy GPT-4.1 model
az cognitiveservices account deployment create \
  --name MyOpenAIResource \
  --resource-group MyResourceGroup \
  --deployment-name gpt-4.1 \
  --model-name gpt-4.1 \
  --model-version "2025-04-14" \
  --model-format OpenAI
```

### 2. Clone & Restore

```bash
cd ManufacturingVisionAnalyzer
dotnet restore
```

NuGet dependency:

```
Azure.AI.Vision.ImageAnalysis  1.0.0-beta.3
```

### 3. Configure Credentials

**Option A — Environment Variables (recommended)**

```powershell
# Azure AI Vision
$env:VISION_ENDPOINT = "https://your-vision.cognitiveservices.azure.com/"
$env:VISION_KEY      = "your-vision-key"

# Azure OpenAI
$env:AZURE_OPENAI_ENDPOINT = "https://your-openai.openai.azure.com"
$env:AZURE_OPENAI_KEY      = "your-openai-key"
```

**Option B — Hard-code in Program.cs** (quick testing only)

### 4. Run

```bash
# Pass the dataset path or enter interactively
dotnet run -- "C:\path\to\mvtec_anomaly_detection"
dotnet run
```

On first run, the system builds the knowledge graph from Azure Vision API calls (~10-15 min on free tier) and caches to `knowledge_graph.json`. Subsequent runs load instantly from cache.

---

## Interactive Menu

```
🔍 INTERACTIVE QUERY MENU
══════════════════════════════════════════════════════
 1. 📦 Find defects by product type
 2. 🔧 Get equipment recommendations
 3. 🔗 Find similar defects across products (AI-powered)
 4. 📋 View all products in database
 5. 🎯 Custom search by defect type
 6. 📊 Generate visual diagram
 7. 💾 Export results to file
 8. 🔄 Show sample insights (AI-powered)
 9. 📊 VIEW COMPLETE DASHBOARD WITH VISUALIZATIONS ⭐
10. 💾 Save current graph to cache
11. 🔄 Rebuild graph from dataset
12. 🗑️  Delete cache file
13. 🧭 Flowchart/Diagram Folder Mode (AI-powered)
14. 🔎 Search inside flowcharts (keyword)
15. ❌ Exit
══════════════════════════════════════════════════════
```

### Highlights

| Option | Description |
|--------|-------------|
| **1** | Query defects for a specific product (e.g. `bottle`, `metal_nut`) |
| **2** | Map defect types to recommended inspection equipment |
| **3** | Find the same defect type across different products — AI generates comparison insights |
| **5** | Free-text search across all defects (e.g. `scratch`, `crack`) |
| **8** | Azure OpenAI generates 4 actionable business insights from graph data |
| **9** | Full dashboard: metrics, bar charts, pie chart, heatmap, network diagram, 7 local insights |
| **10–12** | Cache management — save, rebuild, or delete the persisted graph |
| **13** | Process flowchart images: OCR → spatial merge → AI classification → JSON output |
| **14** | Keyword search across all processed flowchart JSON files |

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
- **7 Local Insights** — computed directly from graph data without API calls:
  1. Most common defect with product spread
  2. Cross-product knowledge transfer potential
  3. Severity risk analysis with critical products
  4. Equipment investment priority
  5. Product complexity & standardization opportunities
  6. Defect diversity index
  7. Graph connectivity density

The dashboard can be exported to a timestamped `.txt` file from within the menu.

---

## Flowchart Processing

Option **13** processes a folder of flowchart images through a 3-stage pipeline:

### Stage 1: Azure Vision OCR
Extracts raw text lines with bounding-box coordinates (left, top, width, height).

### Stage 2: Spatial Merge
Groups OCR lines that belong to the same flowchart box using:
- **Horizontal center alignment** — lines with center-X within 120px are in the same column
- **Vertical gap** — lines with < 50px gap are in the same box (between-box gaps are typically 80+px)

### Stage 3: Azure OpenAI Classification
Sends merged text blocks to GPT-4.1 which classifies each as:
- **Step** — action/process box (rectangle)
- **Decision** — question box (diamond)
- **Branch Label** — connector text (Yes/No)
- **Terminal** — start/end box

### Output JSON

```json
{
  "ImageName": "Flowchart002.png",
  "AiCaption": "Manufacturing Line Changeover Process with Setup Verification and QC Batch Testing",
  "MergedBlocks": [
    { "Text": "Stop current production run", "BoundingBoxTop": 53 },
    { "Text": "Clear line of previous product and tooling", "BoundingBoxTop": 212 }
  ],
  "AiSteps": [
    "Stop current production run",
    "Clear line of previous product and tooling",
    "Load new product specification into system",
    "Verify tooling and fixture setup",
    "Run test batch of 5 units",
    "Approve line for full production run"
  ],
  "AiDecisions": [
    "Does setup pass verification check?",
    "Does test batch pass QC check?"
  ],
  "AiBranchLabels": ["Yes", "No"]
}
```

---

## Project Structure

```
ManufacturingVisionAnalyzer/
├── ReadMe.md                              # This file
├── DEMO_SCOPE.md                          # Demo scope and goals
├── LIMITS.md                              # Known limitations and mitigations
├── DATASET.md                             # Dataset documentation
├── ManufacturingVisionAnalyzer.csproj     # .NET 10 project
├── Program.cs                             # Entry point + 15-option menu + Azure OpenAI calls
├── KnowledgeGraph.cs                      # Graph model, queries, 7 local insights, JSON I/O
├── AzureVisionAnalyzer.cs                 # Azure AI Vision integration
├── GraphBuilder.cs                        # MVTec dataset processing + domain rules
├── ChartGenerator.cs                      # Console charts, heatmaps, dashboards
├── FlowchartFolderProcessor.cs            # OCR extraction + spatial merge algorithm
├── knowledge_graph.json                   # Cached graph (auto-generated)
├── datasets/
│   ├── mvtec_anomaly_detection/           # MVTec AD dataset (15 product categories)
│   └── flowchart/                         # Sample flowchart images
├── outputs/
│   └── flowcharts/                        # Processed flowchart JSON files
└── Dashboard_Export_*.txt                 # Exported dashboards (auto-generated)
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
graph.GenerateInsights();         // 7 rich local insights (no API calls)
```

### Flowchart Processing
```csharp
var result = await FlowchartFolderProcessor.ProcessSingleImageAsync(imagePath);
// result.OcrLines      — raw OCR with bounding boxes
// result.MergedBlocks  — spatially merged text blocks
// result.AiSteps       — AI-classified process steps
// result.AiDecisions   — AI-classified decision questions
// result.AiCaption     — AI-generated detailed description
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

### Spatial Merge Tuning

```csharp
// Adjust thresholds for different diagram styles
FlowchartFolderProcessor.MergeOcrLinesByProximity(
    ocrLines,
    verticalGapThreshold: 50,       // max px gap between lines in one box
    horizontalCenterThreshold: 120   // max px center-X difference
);
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

---

## Technical Details

| | |
|---|---|
| **Language** | C# / .NET 10.0 |
| **Azure AI Vision** | Image Analysis 4.0 (Caption, Tags, Objects, OCR) |
| **Azure OpenAI** | GPT-4.1 via Chat Completions API (2025-01-01-preview) |
| **NuGet** | `Azure.AI.Vision.ImageAnalysis 1.0.0-beta.3` |
| **Serialization** | `System.Text.Json` (built-in) |
| **HTTP** | `System.Net.Http.HttpClient` for OpenAI API calls |
| **Data Structure** | In-memory graph (`List<Node>`, `List<Relationship>`) |
| **Persistence** | JSON file cache (`knowledge_graph.json`) |
| **Rate Limiting** | 3.5 s delay between Vision API calls (safe for F0 tier) |
| **Memory** | < 100 MB for the full MVTec graph |

---

## Troubleshooting

| Problem | Solution |
|---|---|
| **Vision authentication failed** | Verify endpoint includes `https://` and trailing `/`. Key is 32 characters, no extra spaces. |
| **OpenAI 404 Resource not found** | Check deployment name matches exactly (e.g. `gpt-4.1`). Verify api-version. |
| **Rate limit exceeded** | Reduce `maxImagesPerProduct` or add `await Task.Delay(3500)`. Upgrade to S1 for higher limits. |
| **Directory not found** | Use an absolute path. On Windows use `@"C:\..."` or forward slashes. |
| **No defects found** | Expected for subtle defects — the system falls back to MVTec folder names as the defect category. |
| **OCR merge is wrong** | Adjust `verticalGapThreshold` and `horizontalCenterThreshold` for your diagram style. |

---

## References

- **Azure AI Vision** — https://learn.microsoft.com/azure/ai-services/computer-vision/
- **Azure OpenAI Service** — https://learn.microsoft.com/azure/ai-services/openai/
- **Azure Vision SDK** — https://learn.microsoft.com/dotnet/api/azure.ai.vision.imageanalysis
- **MVTec AD Dataset** — Bergmann et al., *CVPR 2019* — https://www.mvtec.com/company/research/datasets/mvtec-ad

---

## License

Educational / demo project.

- **Azure AI Vision**: Requires Azure subscription (Free tier available)
- **Azure OpenAI**: Requires Azure subscription with OpenAI access
- **MVTec AD Dataset**: Free for research and educational use (see MVTec license)

---

**Last Updated**: 2026-02-20
**Version**: 3.0 — Dual Azure AI pipeline (Vision + OpenAI), spatial merge, AI-enhanced flowcharts, 15-option menu
**Status**: Complete
