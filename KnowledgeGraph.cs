using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace ManufacturingKnowledgeGraph
{
    public class Node
    {
        public string Id { get; set; }
        public string Type { get; set; } // "image", "defect", "equipment", "standard"
        public Dictionary<string, object> Properties { get; set; } = new();
    }

    public class Relationship
    {
        public string FromNodeId { get; set; }
        public string ToNodeId { get; set; }
        public string RelationType { get; set; } // "has_defect", "requires_equipment", etc.
        public double Confidence { get; set; }
    }

    public class KnowledgeGraph
    {
        private List<Node> nodes = new();
        private List<Relationship> relationships = new();

        public void AddNode(Node node)
        {
            if (!nodes.Any(n => n.Id == node.Id))
                nodes.Add(node);
        }

        public void AddRelationship(Relationship rel)
        {
            relationships.Add(rel);
        }

        public List<Node> GetNodesByType(string type)
        {
            return nodes.Where(n => n.Type == type).ToList();
        }

        public List<Node> GetRelatedNodes(string nodeId, string relationType)
        {
            var relatedIds = relationships
                .Where(r => r.FromNodeId == nodeId && r.RelationType == relationType)
                .Select(r => r.ToNodeId);
            
            return nodes.Where(n => relatedIds.Contains(n.Id)).ToList();
        }

        public List<(Node, Node, string)> FindSimilarDefectsAcrossProducts()
        {
            var results = new List<(Node, Node, string)>();
            var defectNodes = nodes.Where(n => n.Type == "defect").ToList();

            for (int i = 0; i < defectNodes.Count; i++)
            {
                for (int j = i + 1; j < defectNodes.Count; j++)
                {
                    var defect1 = defectNodes[i];
                    var defect2 = defectNodes[j];
                    
                    // Get products for each defect
                    var product1 = GetProductForDefect(defect1.Id);
                    var product2 = GetProductForDefect(defect2.Id);

                    // If different products but similar defect name
                    if (product1 != product2 && 
                        IsSimilarDefect(defect1.Properties["name"].ToString(), 
                                       defect2.Properties["name"].ToString()))
                    {
                        results.Add((defect1, defect2, "similar_defect_type"));
                    }
                }
            }

            return results;
        }

        private string GetProductForDefect(string defectId)
        {
            var imageNode = relationships
                .Where(r => r.ToNodeId == defectId && r.RelationType == "has_defect")
                .Select(r => nodes.FirstOrDefault(n => n.Id == r.FromNodeId))
                .FirstOrDefault();

            return imageNode?.Properties.ContainsKey("product") == true 
                ? imageNode.Properties["product"].ToString() 
                : "unknown";
        }

        private bool IsSimilarDefect(string defect1, string defect2)
        {
            // Simple similarity check
            var keywords = new[] { "scratch", "crack", "dent", "hole", "contamination", "bent", "broken", "color" };
            
            foreach (var keyword in keywords)
            {
                if (defect1.Contains(keyword, StringComparison.OrdinalIgnoreCase) &&
                    defect2.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            
            return false;
        }

        public void PrintGraph()
        {
            Console.WriteLine($"\n📊 Knowledge Graph Statistics:");
            Console.WriteLine($"   Total Nodes: {nodes.Count}");
            Console.WriteLine($"   Total Relationships: {relationships.Count}");
            Console.WriteLine($"   Images: {nodes.Count(n => n.Type == "image")}");
            Console.WriteLine($"   Defects: {nodes.Count(n => n.Type == "defect")}");
            Console.WriteLine($"   Equipment: {nodes.Count(n => n.Type == "equipment")}");
        }

        public List<Node> QueryDefectsByProduct(string productName)
        {
            var imageNodes = nodes
                .Where(n => n.Type == "image" && 
                           n.Properties.ContainsKey("product") &&
                           n.Properties["product"].ToString().Contains(productName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var defectIds = relationships
                .Where(r => imageNodes.Select(img => img.Id).Contains(r.FromNodeId) && 
                           r.RelationType == "has_defect")
                .Select(r => r.ToNodeId)
                .Distinct();

            return nodes.Where(n => defectIds.Contains(n.Id)).ToList();
        }

        public Dictionary<string, List<string>> GetEquipmentRecommendations()
        {
            var recommendations = new Dictionary<string, List<string>>();

            var defectNodes = nodes.Where(n => n.Type == "defect").ToList();

            foreach (var defect in defectNodes)
            {
                var defectName = defect.Properties["name"].ToString();
                var equipmentIds = relationships
                    .Where(r => r.FromNodeId == defect.Id && r.RelationType == "requires_equipment")
                    .Select(r => r.ToNodeId);

                var equipment = nodes
                    .Where(n => equipmentIds.Contains(n.Id))
                    .Select(n => n.Properties["name"].ToString())
                    .ToList();

                if (equipment.Any())
                {
                    if (!recommendations.ContainsKey(defectName))
                        recommendations[defectName] = new List<string>();
                    
                    recommendations[defectName].AddRange(equipment);
                }
            }

            return recommendations;
        }

        // Add to KnowledgeGraph.cs class

/// <summary>
/// Get defect frequency distribution
/// </summary>
public Dictionary<string, int> GetDefectFrequency()
{
    var defectNodes = nodes.Where(n => n.Type == "defect");
    return defectNodes
        .GroupBy(d => d.Properties["name"].ToString())
        .ToDictionary(g => g.Key, g => g.Count());
}

/// <summary>
/// Get severity distribution
/// </summary>
public Dictionary<string, int> GetSeverityDistribution()
{
    var defectNodes = nodes.Where(n => n.Type == "defect");
    return defectNodes
        .GroupBy(d => d.Properties["severity"].ToString())
        .ToDictionary(
            g => char.ToUpper(g.Key[0]) + g.Key.Substring(1) + " Severity",
            g => g.Count()
        );
}

/// <summary>
/// Get product defect counts
/// </summary>
public Dictionary<string, int> GetProductDefectCounts()
{
    var imageNodes = nodes.Where(n => n.Type == "image");
    var products = imageNodes
        .Select(img => img.Properties["product"].ToString())
        .Distinct();

    var result = new Dictionary<string, int>();
    foreach (var product in products)
    {
        result[product] = QueryDefectsByProduct(product).Count;
    }

    return result;
}

/// <summary>
/// Get equipment usage statistics
/// </summary>
public Dictionary<string, int> GetEquipmentUsage()
{
    var result = new Dictionary<string, int>();
    var equipmentNodes = nodes.Where(n => n.Type == "equipment");

    foreach (var equipment in equipmentNodes)
    {
        var usageCount = relationships.Count(r => 
            r.ToNodeId == equipment.Id && 
            r.RelationType == "requires_equipment");
        
        result[equipment.Properties["name"].ToString()] = usageCount;
    }

    return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
}

/// <summary>
/// Get product quality heatmap data
/// </summary>
public (string[] products, string[] severities, int[,] data) GetQualityHeatmap()
{
    var imageNodes = nodes.Where(n => n.Type == "image");
    var products = imageNodes
        .Select(img => img.Properties["product"].ToString())
        .Distinct()
        .OrderBy(p => p)
        .ToArray();

    var severities = new[] { "Low", "Medium", "High" };
    var data = new int[products.Length, severities.Length];

    for (int i = 0; i < products.Length; i++)
    {
        var productDefects = QueryDefectsByProduct(products[i]);
        
        for (int j = 0; j < severities.Length; j++)
        {
            data[i, j] = productDefects.Count(d => 
                d.Properties["severity"].ToString().Equals(severities[j], StringComparison.OrdinalIgnoreCase));
        }
    }

    return (products, severities, data);
}

/// <summary>
/// Generate AI insights from the graph
/// </summary>
public List<string> GenerateInsights()
{
    var insights = new List<string>();

    // Insight 1: Most common defect
    var defectFreq = GetDefectFrequency();
    if (defectFreq.Any())
    {
        var mostCommon = defectFreq.OrderByDescending(x => x.Value).First();
        insights.Add($"Most common defect: '{mostCommon.Key}' found in {mostCommon.Value} instances");
    }

    // Insight 2: Cross-product patterns
    var similarities = FindSimilarDefectsAcrossProducts();
    if (similarities.Any())
    {
        insights.Add($"Found {similarities.Count} cross-product defect patterns - enabling knowledge transfer!");
    }

    // Insight 3: High severity products
    var (products, _, heatmapData) = GetQualityHeatmap();
    if (products.Any())
    {
        var highSevIndex = 2; // "High" column
        var maxHighSev = 0;
        var criticalProduct = "";
        
        for (int i = 0; i < products.Length; i++)
        {
            if (heatmapData[i, highSevIndex] > maxHighSev)
            {
                maxHighSev = heatmapData[i, highSevIndex];
                criticalProduct = products[i];
            }
        }
        
        if (maxHighSev > 0)
        {
            insights.Add($"Product '{criticalProduct}' has {maxHighSev} high-severity defects - needs priority review");
        }
    }

    // Insight 4: Equipment recommendation
    var equipmentUsage = GetEquipmentUsage();
    if (equipmentUsage.Any())
    {
        var topEquipment = equipmentUsage.First();
        var percentage = (double)topEquipment.Value / GetNodesByType("defect").Count * 100;
        insights.Add($"{topEquipment.Key} required for {percentage:F0}% of defects - critical investment");
    }

    // Insight 5: Standardization opportunity
    var defectTypes = defectFreq.Count;
    var productCount = products.Length;
    if (productCount > 0)
    {
        var avgDefectsPerProduct = (double)defectTypes / productCount;
        insights.Add($"Average {avgDefectsPerProduct:F1} defect types per product - standardization opportunities exist");
    }

    return insights;
    }
    /// <summary>
/// Save knowledge graph to JSON file
/// </summary>
public void SaveToFile(string filename = "knowledge_graph.json")
{
    var data = new
    {
        nodes = this.nodes,
        relationships = this.relationships,
        saved_at = DateTime.Now,
        version = "1.0"
    };

    var options = new JsonSerializerOptions 
    { 
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var json = JsonSerializer.Serialize(data, options);
    File.WriteAllText(filename, json);
    
    Console.WriteLine($"\n💾 Knowledge graph saved to: {filename}");
    Console.WriteLine($"   Nodes: {nodes.Count}, Relationships: {relationships.Count}");
}

/// <summary>
/// Load knowledge graph from JSON file
/// </summary>
public static KnowledgeGraph LoadFromFile(string filename = "knowledge_graph.json")
{
    if (!File.Exists(filename))
    {
        return null;
    }

    try
    {
        var json = File.ReadAllText(filename);
        var options = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var data = JsonSerializer.Deserialize<GraphData>(json, options);
        
        var graph = new KnowledgeGraph();
        graph.nodes = data.Nodes.ToList();
        graph.relationships = data.Relationships.ToList();

        Console.WriteLine($"\n✅ Knowledge graph loaded from: {filename}");
        Console.WriteLine($"   Nodes: {graph.nodes.Count}, Relationships: {graph.relationships.Count}");
        Console.WriteLine($"   Last saved: {data.SavedAt}");

        return graph;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  Error loading graph: {ex.Message}");
        return null;
    }
}

/// <summary>
/// Check if cached graph exists and is recent
/// </summary>
public static bool CacheExists(string filename = "knowledge_graph.json")
{
    return File.Exists(filename);
}

// Helper class for deserialization
private class GraphData
{
    public List<Node> Nodes { get; set; }
    public List<Relationship> Relationships { get; set; }
    public DateTime SavedAt { get; set; }
    public string Version { get; set; }
}
    }
}