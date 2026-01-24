using System;
using CodingWithCalvin.Debugalizers.Core;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Xml;
using CodingWithCalvin.Debugalizers.Visualizers;
using Newtonsoft.Json.Linq;
using YamlDotNet.RepresentationModel;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays hierarchical content in a tree structure.
/// </summary>
public partial class TreeViewControl : UserControl
{
    /// <summary>
    /// Initializes a new instance of the TreeViewControl.
    /// </summary>
    /// <param name="content">The content to display.</param>
    /// <param name="type">The visualizer type.</param>
    public TreeViewControl(string content, VisualizerType type)
    {
        InitializeComponent();

        try
        {
            var nodes = type switch
            {
                VisualizerType.Json => ParseJson(content),
                VisualizerType.Xml or VisualizerType.Html => ParseXml(content),
                VisualizerType.Yaml => ParseYaml(content),
                VisualizerType.Toml => ParseToml(content),
                _ => new List<TreeNode> { new TreeNode { Key = "Content", Value = content } }
            };

            ContentTree.ItemsSource = nodes;
        }
        catch (Exception ex)
        {
            ContentTree.ItemsSource = new List<TreeNode>
            {
                new TreeNode { Key = "Error", Value = ex.Message }
            };
        }
    }

    private List<TreeNode> ParseJson(string content)
    {
        var token = JToken.Parse(content);
        return new List<TreeNode> { TokenToNode("root", token) };
    }

    private TreeNode TokenToNode(string key, JToken token)
    {
        var node = new TreeNode { Key = key };

        switch (token.Type)
        {
            case JTokenType.Object:
                node.TypeHint = "{object}";
                node.Children = new List<TreeNode>();
                foreach (var property in ((JObject)token).Properties())
                {
                    node.Children.Add(TokenToNode(property.Name, property.Value));
                }
                break;

            case JTokenType.Array:
                var array = (JArray)token;
                node.TypeHint = $"[{array.Count} items]";
                node.Children = new List<TreeNode>();
                for (int i = 0; i < array.Count; i++)
                {
                    node.Children.Add(TokenToNode($"[{i}]", array[i]));
                }
                break;

            default:
                node.Value = token.ToString();
                node.TypeHint = $"({token.Type.ToString().ToLower()})";
                break;
        }

        return node;
    }

    private List<TreeNode> ParseXml(string content)
    {
        var doc = new XmlDocument();
        doc.LoadXml(content);
        return new List<TreeNode> { XmlNodeToTreeNode(doc.DocumentElement) };
    }

    private TreeNode XmlNodeToTreeNode(XmlNode xmlNode)
    {
        var node = new TreeNode { Key = xmlNode.Name };

        if (xmlNode.Attributes != null && xmlNode.Attributes.Count > 0)
        {
            node.Children = node.Children ?? new List<TreeNode>();
            foreach (XmlAttribute attr in xmlNode.Attributes)
            {
                node.Children.Add(new TreeNode
                {
                    Key = $"@{attr.Name}",
                    Value = attr.Value,
                    TypeHint = "(attribute)"
                });
            }
        }

        if (xmlNode.HasChildNodes)
        {
            node.Children = node.Children ?? new List<TreeNode>();
            foreach (XmlNode child in xmlNode.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Text)
                {
                    node.Value = child.Value?.Trim();
                }
                else if (child.NodeType == XmlNodeType.Element)
                {
                    node.Children.Add(XmlNodeToTreeNode(child));
                }
            }
        }

        if (node.Children?.Count == 0)
        {
            node.Children = null;
        }

        return node;
    }

    private List<TreeNode> ParseYaml(string content)
    {
        var yaml = new YamlStream();
        using (var reader = new System.IO.StringReader(content))
        {
            yaml.Load(reader);
        }

        var nodes = new List<TreeNode>();
        foreach (var document in yaml.Documents)
        {
            nodes.Add(YamlNodeToTreeNode("document", document.RootNode));
        }

        return nodes;
    }

    private TreeNode YamlNodeToTreeNode(string key, YamlNode yamlNode)
    {
        var node = new TreeNode { Key = key };

        switch (yamlNode)
        {
            case YamlMappingNode mapping:
                node.TypeHint = "{mapping}";
                node.Children = new List<TreeNode>();
                foreach (var entry in mapping.Children)
                {
                    var entryKey = ((YamlScalarNode)entry.Key).Value;
                    node.Children.Add(YamlNodeToTreeNode(entryKey, entry.Value));
                }
                break;

            case YamlSequenceNode sequence:
                node.TypeHint = $"[{sequence.Children.Count} items]";
                node.Children = new List<TreeNode>();
                for (int i = 0; i < sequence.Children.Count; i++)
                {
                    node.Children.Add(YamlNodeToTreeNode($"[{i}]", sequence.Children[i]));
                }
                break;

            case YamlScalarNode scalar:
                node.Value = scalar.Value;
                break;
        }

        return node;
    }

    private List<TreeNode> ParseToml(string content)
    {
        var model = Tomlyn.Toml.ToModel(content);
        return new List<TreeNode> { ObjectToTreeNode("root", model) };
    }

    private TreeNode ObjectToTreeNode(string key, object obj)
    {
        var node = new TreeNode { Key = key };

        if (obj is IDictionary<string, object> dict)
        {
            node.TypeHint = "{table}";
            node.Children = new List<TreeNode>();
            foreach (var kvp in dict)
            {
                node.Children.Add(ObjectToTreeNode(kvp.Key, kvp.Value));
            }
        }
        else if (obj is IList<object> list)
        {
            node.TypeHint = $"[{list.Count} items]";
            node.Children = new List<TreeNode>();
            for (int i = 0; i < list.Count; i++)
            {
                node.Children.Add(ObjectToTreeNode($"[{i}]", list[i]));
            }
        }
        else
        {
            node.Value = obj?.ToString() ?? "null";
            node.TypeHint = obj != null ? $"({obj.GetType().Name.ToLower()})" : "(null)";
        }

        return node;
    }
}

/// <summary>
/// Represents a node in the tree view.
/// </summary>
public class TreeNode
{
    /// <summary>
    /// Gets or sets the node key/name.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the node value (for leaf nodes).
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets a hint about the node type.
    /// </summary>
    public string TypeHint { get; set; }

    /// <summary>
    /// Gets or sets the child nodes.
    /// </summary>
    public List<TreeNode> Children { get; set; }

    /// <summary>
    /// Gets whether this node has a value.
    /// </summary>
    public bool HasValue => !string.IsNullOrEmpty(Value);
}
