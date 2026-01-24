using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Service for providing syntax highlighting definitions for different content types.
/// </summary>
public static class SyntaxHighlighter
{
    /// <summary>
    /// Gets the syntax highlighting definition for the specified visualizer type.
    /// </summary>
    /// <param name="type">The visualizer type.</param>
    /// <returns>The highlighting definition, or null if none available.</returns>
    public static IHighlightingDefinition GetHighlightingDefinition(VisualizerType type)
    {
        var name = GetHighlightingName(type);
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return HighlightingManager.Instance.GetDefinition(name);
    }

    /// <summary>
    /// Gets the built-in AvalonEdit highlighting name for the visualizer type.
    /// </summary>
    /// <param name="type">The visualizer type.</param>
    /// <returns>The highlighting definition name.</returns>
    public static string GetHighlightingName(VisualizerType type)
    {
        return type switch
        {
            VisualizerType.Json => "JavaScript", // JSON is close to JavaScript syntax
            VisualizerType.Xml => "XML",
            VisualizerType.Html => "HTML",
            VisualizerType.Yaml => null, // No built-in YAML highlighting
            VisualizerType.Toml => null, // No built-in TOML highlighting
            VisualizerType.Sql => null, // No built-in SQL highlighting
            VisualizerType.Markdown => "MarkDown",
            VisualizerType.Csv => null,
            VisualizerType.Tsv => null,
            VisualizerType.Ini => null,
            VisualizerType.GraphQl => null,
            _ => null
        };
    }

    /// <summary>
    /// Gets the file extension associated with the visualizer type (for syntax detection).
    /// </summary>
    /// <param name="type">The visualizer type.</param>
    /// <returns>The file extension including the dot.</returns>
    public static string GetFileExtension(VisualizerType type)
    {
        return type switch
        {
            VisualizerType.Json => ".json",
            VisualizerType.Xml => ".xml",
            VisualizerType.Html => ".html",
            VisualizerType.Yaml => ".yaml",
            VisualizerType.Toml => ".toml",
            VisualizerType.Sql => ".sql",
            VisualizerType.Markdown => ".md",
            VisualizerType.Csv => ".csv",
            VisualizerType.Tsv => ".tsv",
            VisualizerType.Ini => ".ini",
            VisualizerType.GraphQl => ".graphql",
            _ => ".txt"
        };
    }

    /// <summary>
    /// Gets the MIME type for the visualizer type.
    /// </summary>
    /// <param name="type">The visualizer type.</param>
    /// <returns>The MIME type string.</returns>
    public static string GetMimeType(VisualizerType type)
    {
        return type switch
        {
            VisualizerType.Json => "application/json",
            VisualizerType.Xml => "application/xml",
            VisualizerType.Html => "text/html",
            VisualizerType.Yaml => "application/x-yaml",
            VisualizerType.Toml => "application/toml",
            VisualizerType.Sql => "application/sql",
            VisualizerType.Markdown => "text/markdown",
            VisualizerType.Csv => "text/csv",
            VisualizerType.Tsv => "text/tab-separated-values",
            VisualizerType.Ini => "text/plain",
            VisualizerType.GraphQl => "application/graphql",
            _ => "text/plain"
        };
    }
}
