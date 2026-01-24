using CodingWithCalvin.Debugalizers.Core;
using FluentAssertions;
using Xunit;

namespace CodingWithCalvin.Debugalizers.Tests;

public class SyntaxHighlighterTests
{
    [Theory]
    [InlineData(VisualizerType.Json, "JavaScript")]
    [InlineData(VisualizerType.Xml, "XML")]
    [InlineData(VisualizerType.Html, "HTML")]
    [InlineData(VisualizerType.Markdown, "MarkDown")]
    public void GetHighlightingName_SupportedTypes_ReturnsCorrectName(VisualizerType type, string expected)
    {
        var result = SyntaxHighlighter.GetHighlightingName(type);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(VisualizerType.Yaml)]
    [InlineData(VisualizerType.Toml)]
    [InlineData(VisualizerType.Sql)]
    [InlineData(VisualizerType.Csv)]
    [InlineData(VisualizerType.Ini)]
    public void GetHighlightingName_UnsupportedTypes_ReturnsNull(VisualizerType type)
    {
        var result = SyntaxHighlighter.GetHighlightingName(type);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(VisualizerType.Json, ".json")]
    [InlineData(VisualizerType.Xml, ".xml")]
    [InlineData(VisualizerType.Html, ".html")]
    [InlineData(VisualizerType.Yaml, ".yaml")]
    [InlineData(VisualizerType.Toml, ".toml")]
    [InlineData(VisualizerType.Sql, ".sql")]
    [InlineData(VisualizerType.Markdown, ".md")]
    [InlineData(VisualizerType.Csv, ".csv")]
    [InlineData(VisualizerType.Tsv, ".tsv")]
    [InlineData(VisualizerType.Ini, ".ini")]
    [InlineData(VisualizerType.GraphQl, ".graphql")]
    public void GetFileExtension_AllTypes_ReturnsCorrectExtension(VisualizerType type, string expected)
    {
        var result = SyntaxHighlighter.GetFileExtension(type);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(VisualizerType.Base64)]
    [InlineData(VisualizerType.Jwt)]
    [InlineData(VisualizerType.Guid)]
    public void GetFileExtension_NonTextTypes_ReturnsTxt(VisualizerType type)
    {
        var result = SyntaxHighlighter.GetFileExtension(type);
        result.Should().Be(".txt");
    }

    [Theory]
    [InlineData(VisualizerType.Json, "application/json")]
    [InlineData(VisualizerType.Xml, "application/xml")]
    [InlineData(VisualizerType.Html, "text/html")]
    [InlineData(VisualizerType.Yaml, "application/x-yaml")]
    [InlineData(VisualizerType.Toml, "application/toml")]
    [InlineData(VisualizerType.Sql, "application/sql")]
    [InlineData(VisualizerType.Markdown, "text/markdown")]
    [InlineData(VisualizerType.Csv, "text/csv")]
    [InlineData(VisualizerType.Tsv, "text/tab-separated-values")]
    public void GetMimeType_TextTypes_ReturnsCorrectMimeType(VisualizerType type, string expected)
    {
        var result = SyntaxHighlighter.GetMimeType(type);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(VisualizerType.Json)]
    [InlineData(VisualizerType.Xml)]
    [InlineData(VisualizerType.Html)]
    public void GetHighlightingDefinition_SupportedTypes_ReturnsDefinition(VisualizerType type)
    {
        var result = SyntaxHighlighter.GetHighlightingDefinition(type);
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(VisualizerType.Yaml)]
    [InlineData(VisualizerType.Toml)]
    [InlineData(VisualizerType.Sql)]
    public void GetHighlightingDefinition_UnsupportedTypes_ReturnsNull(VisualizerType type)
    {
        var result = SyntaxHighlighter.GetHighlightingDefinition(type);
        result.Should().BeNull();
    }
}
