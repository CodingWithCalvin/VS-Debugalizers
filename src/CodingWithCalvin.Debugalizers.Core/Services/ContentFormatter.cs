using System;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tomlyn;
using YamlDotNet.Serialization;

namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Service for formatting and pretty-printing various content types.
/// </summary>
public static class ContentFormatter
{
    /// <summary>
    /// Formats the content based on the specified visualizer type.
    /// </summary>
    /// <param name="content">The content to format.</param>
    /// <param name="type">The type of content.</param>
    /// <returns>The formatted content.</returns>
    public static string Format(string content, VisualizerType type)
    {
        if (string.IsNullOrEmpty(content))
        {
            return content;
        }

        try
        {
            return type switch
            {
                VisualizerType.Json => FormatJson(content),
                VisualizerType.Xml => FormatXml(content),
                VisualizerType.Html => FormatXml(content), // HTML uses XML formatting
                VisualizerType.Yaml => FormatYaml(content),
                VisualizerType.Toml => FormatToml(content),
                VisualizerType.Sql => FormatSql(content),
                _ => content
            };
        }
        catch
        {
            // If formatting fails, return original content
            return content;
        }
    }

    /// <summary>
    /// Formats JSON content with indentation.
    /// </summary>
    /// <param name="json">The JSON string to format.</param>
    /// <returns>The formatted JSON string.</returns>
    public static string FormatJson(string json)
    {
        try
        {
            var obj = JToken.Parse(json);
            return obj.ToString(Newtonsoft.Json.Formatting.Indented);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// Minifies JSON content by removing whitespace.
    /// </summary>
    /// <param name="json">The JSON string to minify.</param>
    /// <returns>The minified JSON string.</returns>
    public static string MinifyJson(string json)
    {
        try
        {
            var obj = JToken.Parse(json);
            return obj.ToString(Newtonsoft.Json.Formatting.None);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// Formats XML content with indentation.
    /// </summary>
    /// <param name="xml">The XML string to format.</param>
    /// <returns>The formatted XML string.</returns>
    public static string FormatXml(string xml)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = !xml.TrimStart().StartsWith("<?xml", StringComparison.OrdinalIgnoreCase)
            };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }

            return sb.ToString();
        }
        catch
        {
            return xml;
        }
    }

    /// <summary>
    /// Formats YAML content.
    /// </summary>
    /// <param name="yaml">The YAML string to format.</param>
    /// <returns>The formatted YAML string.</returns>
    public static string FormatYaml(string yaml)
    {
        try
        {
            var deserializer = new DeserializerBuilder().Build();
            var obj = deserializer.Deserialize<object>(yaml);

            var serializer = new SerializerBuilder()
                .WithIndentedSequences()
                .Build();

            return serializer.Serialize(obj);
        }
        catch
        {
            return yaml;
        }
    }

    /// <summary>
    /// Formats TOML content.
    /// </summary>
    /// <param name="toml">The TOML string to format.</param>
    /// <returns>The formatted TOML string.</returns>
    public static string FormatToml(string toml)
    {
        try
        {
            var model = Toml.ToModel(toml);
            return Toml.FromModel(model);
        }
        catch
        {
            return toml;
        }
    }

    /// <summary>
    /// Formats SQL content with basic indentation.
    /// </summary>
    /// <param name="sql">The SQL string to format.</param>
    /// <returns>The formatted SQL string.</returns>
    public static string FormatSql(string sql)
    {
        // Basic SQL formatting - add newlines before major keywords
        var keywords = new[] { "SELECT", "FROM", "WHERE", "AND", "OR", "ORDER BY", "GROUP BY", "HAVING", "JOIN", "LEFT JOIN", "RIGHT JOIN", "INNER JOIN", "OUTER JOIN", "ON", "INSERT INTO", "VALUES", "UPDATE", "SET", "DELETE", "CREATE", "ALTER", "DROP" };

        var result = sql;
        foreach (var keyword in keywords)
        {
            result = System.Text.RegularExpressions.Regex.Replace(
                result,
                $@"\b{keyword}\b",
                match => Environment.NewLine + match.Value,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        // Clean up multiple newlines and leading whitespace
        result = System.Text.RegularExpressions.Regex.Replace(result, @"(\r?\n)+", Environment.NewLine);
        return result.TrimStart();
    }

    /// <summary>
    /// Gets statistics about the content.
    /// </summary>
    /// <param name="content">The content to analyze.</param>
    /// <returns>A string containing content statistics.</returns>
    public static string GetStatistics(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return "Empty content";
        }

        var lineCount = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Length;
        var charCount = content.Length;
        var byteCount = Encoding.UTF8.GetByteCount(content);

        return $"Lines: {lineCount:N0} | Characters: {charCount:N0} | Bytes: {byteCount:N0}";
    }
}
