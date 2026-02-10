# Manufacturing Knowledge Graph - Cross-Modal Intelligence for Quality Control

## 🎯 Project Overview

This project demonstrates a **novel approach** to manufacturing quality control by building a **cross-modal knowledge graph** that connects:
- Visual defect data from inspection images
- Manufacturing process knowledge
- Equipment requirements
- Quality standards (ISO 9001)

### 🌟 What Makes This Novel?

**Traditional Systems:**
- Analyze each product type separately
- Simple defect detection without context
- Manual documentation of inspection procedures

**Our System:**
- Discovers patterns **across different product types**
- Automatically links defects → equipment → standards
- Enables knowledge transfer between manufacturing lines
- Answers complex queries like "What equipment is needed for bottle defects?" or "Find similar defects across all products"

---

## 📊 Dataset

**MVTec Anomaly Detection (MVTec AD)**
- Download from: https://www.mvtec.com/company/research/datasets/mvtec-ad/downloads
- Contains 15 product categories with real industrial defects
- Products: bottle, cable, capsule, carpet, grid, hazelnut, leather, metal_nut, pill, screw, tile, toothbrush, transistor, wood, zipper
- Each category has good (defect-free) and defective images with ground truth masks

**Dataset Structure:**
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
└── [other products...]
```

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────┐
│           KNOWLEDGE GRAPH                       │
│                                                 │
│  [Metal Nut Image] ──connected to──> [Defect: Scratch]
│         │                                  │
│         │                                  │
│    related_to                         caused_by
│         │                                  │
│         ▼                                  ▼
│  [Inspection Step 3] ◄──part_of── [QC Flowchart]
│         │                                  │
│    uses_equipment                    leads_to
│         │                                  │
│         ▼                                  ▼
│  [Magnifying Lens]              [Rejection Decision]
│         │
│    specified_in
│         │
│         ▼
│  [ISO 9001 Section 8.5]
└─────────────────────────────────────────────────┘
```

### Components:

1. **AzureVisionAnalyzer.cs** - Analyzes images using Azure AI Vision SDK
2. **KnowledgeGraph.cs** - Core graph structure (nodes, relationships, queries)
3. **GraphBuilder.cs** - Processes MVTec dataset and builds the graph
4. **Program.cs** - Main demo with example queries

---

## 🚀 Quick Start

### Prerequisites

- .NET 8.0 SDK or later
- Azure AI Vision resource (Free tier works!)
- MVTec AD dataset downloaded and extracted

### 1. Create Azure AI Vision Resource

```bash
# Option A: Azure Portal
1. Go to https://portal.azure.com
2. Create new resource → AI + Machine Learning → Computer Vision
3. Choose Free tier (F0) - 20 calls/min, 5K calls/month
4. Copy the Endpoint and Key from "Keys and Endpoint" section

# Option B: Azure CLI
az cognitiveservices account create \
  --name MyVisionResource \
  --resource-group MyResourceGroup \
  --kind ComputerVision \
  --sku F0 \
  --location eastus
```

**Your credentials will look like:**
- Endpoint: `https://your-resource-name.cognitiveservices.azure.com/`
- Key: `abc123def456...` (32 characters)

### 2. Setup Project

```bash
# Clone or create project directory
mkdir ManufacturingKnowledgeGraph
cd ManufacturingKnowledgeGraph

# Initialize .NET console project
dotnet new console

# Install required packages
dotnet add package Azure.AI.Vision.ImageAnalysis --version 1.0.0-beta.3
dotnet add package Newtonsoft.Json
```

### 3. Add the Code Files

Copy these 4 files into your project directory:
- `KnowledgeGraph.cs`
- `AzureVisionAnalyzer.cs`
- `GraphBuilder.cs`
- `Program.cs`

(See full code in sections below)

### 4. Configure Credentials

**Option A: Environment Variables (Recommended)**

```bash
# Windows PowerShell
$env:VISION_ENDPOINT="https://your-resource.cognitiveservices.azure.com/"
$env:VISION_KEY="your-key-here"

# Windows CMD
set VISION_ENDPOINT=https://your-resource.cognitiveservices.azure.com/
set VISION_KEY=your-key-here

# Linux/Mac
export VISION_ENDPOINT="https://your-resource.cognitiveservices.azure.com/"
export VISION_KEY="your-key-here"
```

**Option B: Hard-code in Program.cs** (for quick testing only)

```csharp
string azureEndpoint = "https://your-resource.cognitiveservices.azure.com/";
string azureKey = "your-key-here";
```

### 5. Run the Demo

```bash
# Run with dataset path as argument
dotnet run "C:\path\to\mvtec_anomaly_detection"

# Or run and enter path when prompted
dotnet run
```

---

## 📋 Project Structure

```
ManufacturingKnowledgeGraph/
├── README.md                    # This file
├── ManufacturingKnowledgeGraph.csproj
├── Program.cs                   # Main entry point with demo queries
├── KnowledgeGraph.cs           # Graph data structure and query engine
├── AzureVisionAnalyzer.cs      # Azure Vision API integration
├── GraphBuilder.cs             # Dataset processing logic
└── bin/
    └── Debug/
        └── net8.0/
```

---

## 🎯 Demo Queries

The system demonstrates 3 types of intelligent queries:

### Query 1: Defects by Product Type
```csharp
var bottleDefects = graph.QueryDefectsByProduct("bottle");
// Returns: All defect types found in bottle images
// Output: crack, contamination, broken_large, etc.
```

### Query 2: Equipment Recommendations
```csharp
var equipment = graph.GetEquipmentRecommendations();
// Returns: Required inspection equipment for each defect type
// Output: "scratch" → "High-resolution microscope"
//         "crack" → "Backlight illumination"
```

### Query 3: Cross-Product Pattern Discovery
```csharp
var similarities = graph.FindSimilarDefectsAcrossProducts();
// Returns: Similar defects across different products
// Output: "Scratch in metal_nut ↔ Scratch in zipper"
//         → Same inspection technique can be applied!
```

---

## 🔧 Customization

### Adjust Processing Speed

In `Program.cs`, change the number of images processed per product:

```csharp
await builder.ProcessMVTecDataset(mvtecPath, maxImagesPerProduct: 3); // Default: 3 for speed

// For full analysis (slower):
await builder.ProcessMVTecDataset(mvtecPath, maxImagesPerProduct: 20);
```

### Add More Domain Knowledge

In `GraphBuilder.cs`, add custom equipment or standards:

```csharp
private void AddDomainKnowledge()
{
    // Add your custom equipment
    graph.AddNode(new Node
    {
        Id = "eq_laser_scanner",
        Type = "equipment",
        Properties = new() { ["name"] = "3D Laser Scanner" }
    });

    // Add industry-specific standards
    graph.AddNode(new Node
    {
        Id = "standard_asme",
        Type = "standard",
        Properties = new() { ["name"] = "ASME Y14.5", ["topic"] = "GD&T" }
    });
}
```

### Customize Defect Detection Logic

In `AzureVisionAnalyzer.cs`, adjust the defect classification:

```csharp
public (string defectType, string description) ExtractDefectInfo(ImageAnalysisResult result)
{
    var caption = result.Caption?.Text ?? "unknown";
    
    // Add your custom defect types
    if (caption.Contains("misalignment"))
        return ("misalignment", caption);
    
    // Add more sophisticated logic here
}
```

---

## 📊 Expected Output

```
╔════════════════════════════════════════════════╗
║  Manufacturing Knowledge Graph Demo           ║
║  Cross-Modal Intelligence for Quality Control ║
╚════════════════════════════════════════════════╝

🔍 Processing MVTec Dataset...

📦 Processing product: bottle
... (3 images)
📦 Processing product: metal_nut
... (3 images)
[continues for all products]

📊 Knowledge Graph Statistics:
   Total Nodes: 127
   Total Relationships: 184
   Images: 45
   Defects: 45
   Equipment: 4

============================================================
🔍 DEMO QUERY 1: Show all defect types for 'bottle'
============================================================
   • crack (Severity: high)
   • contamination (Severity: medium)
   • broken_large (Severity: high)

============================================================
🔍 DEMO QUERY 2: Equipment recommendations for defects
============================================================
   Defect: crack
   Equipment needed: High-resolution microscope

   Defect: contamination
   Equipment needed: Backlight illumination

============================================================
🔍 DEMO QUERY 3: Find similar defects across products
============================================================
   'scratch' in metal_nut ↔ 'scratch' in zipper
   → Same inspection technique can be applied!

   'crack' in bottle ↔ 'crack' in tile
   → Same inspection technique can be applied!

✅ Demo complete!
```

---

## 🎬 Demo Presentation Script

### Introduction (30 seconds)
> "Traditional manufacturing quality control systems analyze each product type in isolation. I've built a **cross-modal knowledge graph** that discovers patterns across different manufacturing processes, enabling knowledge transfer between production lines."

### Key Innovation (45 seconds)
> "Watch as the system processes images from 15 different product categories—bottles, metal nuts, textiles, electronics. It's not just detecting defects; it's **building relationships** between visual data, required equipment, and quality standards. 
>
> For example, it discovered that a scratch defect on a metal nut requires the same inspection technique as a scratch on a zipper—something that would take human experts significant time to document."

### Technical Highlight (30 seconds)
> "The system uses Azure AI Vision for image analysis, but the innovation is in how it structures this knowledge. The graph enables queries like 'What equipment is needed for bottle defects?' or 'Find all defects that violate ISO 9001 section 8.5'—connecting visual intelligence with manufacturing domain knowledge."

### Business Value (15 seconds)
> "This reduces documentation time, enables faster onboarding of new products, and allows manufacturers to leverage quality control expertise across different production lines."

---

## ⏱️ Development Timeline

**Total Time: 6-8 hours for working demo**

- **Hour 1**: Setup (Azure resource, project, credentials) - 30 min
- **Hour 2-3**: Implement core code (copy provided files) - 1 hour
- **Hour 3-4**: Test with small dataset subset - 1 hour
- **Hour 4-5**: Run full analysis, debug - 1.5 hours
- **Hour 6-7**: Refine queries and output - 1 hour
- **Hour 7-8**: Prepare demo presentation - 1 hour

**For 1-2 day deadline:**
- Day 1: Hours 1-5 (core functionality)
- Day 2: Hours 6-8 (polish and practice)

---

## 🐛 Troubleshooting

### Issue: "Authentication failed"
```
Solution: Check your endpoint and key
- Endpoint must include https:// and trailing /
- Key is exactly 32 characters
- No extra spaces or quotes
```

### Issue: "Rate limit exceeded"
```
Solution: You're on Free tier (20 calls/min)
- Reduce maxImagesPerProduct to 2-3
- Add delay: await Task.Delay(3000); between images
- Or upgrade to S1 tier ($1/1000 images)
```

### Issue: "Directory not found"
```
Solution: Check MVTec dataset path
- Use absolute path: C:\Users\You\Downloads\mvtec_anomaly_detection
- Use forward slashes: C:/Users/You/Downloads/mvtec_anomaly_detection
- Or use @"..." for Windows paths with backslashes
```

### Issue: "No defects found"
```
Solution: Azure Vision might not detect defects in caption
- This is expected for subtle defects
- The system uses folder names as fallback
- Defect category comes from MVTec folder structure
```

---

## 📚 Technical Details

### Technologies Used
- **Language**: C# (.NET 8.0)
- **Cloud**: Azure AI Vision (Image Analysis 4.0 API)
- **SDK**: Azure.AI.Vision.ImageAnalysis 1.0.0-beta.3
- **Data Structure**: In-memory graph (List<Node>, List<Relationship>)
- **Serialization**: Newtonsoft.Json

### API Calls
- **Per image**: 1 API call
- **Features used**: Caption, Tags, Objects, OCR (Read)
- **Free tier**: 5,000 calls/month (enough for full MVTec dataset)
- **Rate limit**: 20 calls/minute (Free tier)

### Performance
- **Processing time**: ~30-60 seconds per product category (3 images)
- **Full dataset**: 15-20 minutes (45 images total)
- **Memory**: <100 MB for complete graph
- **Scalability**: Can handle 1000+ images in-memory

---

## 🔮 Future Enhancements

### Short-term (add if you have extra time)
1. **JSON Export**: Save graph to file for later analysis
2. **Visual Graph**: Generate HTML/SVG visualization
3. **More Queries**: "Show inspection workflow for product X"
4. **Confidence Scores**: Show AI confidence in relationships

### Medium-term (for full project)
1. **Persistent Storage**: Use Neo4j or Azure Cosmos DB (Gremlin API)
2. **Web UI**: ASP.NET Core dashboard with query interface
3. **Real-time Processing**: Watch folder for new images
4. **Custom Vision**: Fine-tune model on MVTec for better defect detection

### Long-term (research directions)
1. **Causal Inference**: Learn why defects occur
2. **Process Mining**: Extract workflows from image sequences
3. **Anomaly Prediction**: Predict defects before they occur
4. **Multi-modal Fusion**: Combine images, sensor data, text logs

---

## 📖 References

### Azure AI Vision
- Official Docs: https://learn.microsoft.com/azure/ai-services/computer-vision/
- SDK Reference: https://learn.microsoft.com/dotnet/api/azure.ai.vision.imageanalysis
- Samples: https://github.com/Azure-Samples/azure-ai-vision-sdk

### MVTec AD Dataset
- Paper: "MVTec AD — A Comprehensive Real-World Dataset for Unsupervised Anomaly Detection"
- Citation: Bergmann et al., CVPR 2019
- Download: https://www.mvtec.com/company/research/datasets/mvtec-ad

### Knowledge Graphs in Manufacturing
- "Knowledge Graphs for Manufacturing" - Elsevier 2021
- "Industrial Knowledge Graphs" - Springer 2022

---

## 🤝 Contact & Support

If you get stuck during development:

1. **Check this README** - Most answers are here
2. **Error messages** - Copy exact error and search Azure docs
3. **Dataset issues** - Verify MVTec folder structure matches expected format
4. **Azure issues** - Check Azure Portal → Your Resource → Metrics for API call status

---

## 📄 License

This is a demo/educational project. 

**Azure AI Vision**: Requires Azure subscription (Free tier available)
**MVTec AD Dataset**: Free for research and educational use (see MVTec license)

---

## 🎯 Key Talking Points for Demo

1. **Problem**: Traditional quality control systems can't share knowledge between product lines
2. **Solution**: Cross-modal knowledge graph connects visual defects with process knowledge
3. **Innovation**: Automatic discovery of similar defects across different products
4. **Impact**: Reduces documentation time, enables knowledge transfer, speeds up new product onboarding
5. **Technology**: Azure AI Vision + graph-based reasoning + manufacturing domain knowledge

---

## ✅ Pre-Demo Checklist

- [ ] Azure AI Vision resource created and tested
- [ ] MVTec dataset downloaded and extracted
- [ ] All 4 code files copied to project
- [ ] NuGet packages installed successfully
- [ ] Credentials configured (environment variables or hard-coded)
- [ ] Test run completed on 1-2 product categories
- [ ] Full run completed (15 product categories)
- [ ] Demo script prepared and practiced
- [ ] Backup plan if internet/Azure fails (use pre-recorded output)

---

**Last Updated**: 2026-02-06
**Version**: 1.0 - Initial Demo Release
**Status**: Ready for 1-2 day demo development

---

## 🚀 Next Steps

1. **Right now**: Set up Azure AI Vision resource
2. **Next 30 min**: Copy code files and install packages
3. **Next 2 hours**: Test with small dataset subset
4. **Tomorrow**: Full run and demo polish

**You've got this! The code is ready, the dataset is ready, just need to plug it all together.** 💪
