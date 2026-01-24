using CodingWithCalvin.Debugalizers.Core;
using FluentAssertions;
using Xunit;

namespace CodingWithCalvin.Debugalizers.Tests;

public class ContentFormatterTests
{
    #region JSON Formatting

    [Fact]
    public void FormatJson_MinifiedJson_ReturnsFormattedJson()
    {
        var input = "{\"name\":\"test\",\"value\":123}";
        var result = ContentFormatter.FormatJson(input);

        result.Should().Contain("\n");
        result.Should().Contain("  "); // Indentation
        result.Should().Contain("\"name\"");
        result.Should().Contain("\"test\"");
    }

    [Fact]
    public void FormatJson_Array_ReturnsFormattedArray()
    {
        var input = "[1,2,3,4,5]";
        var result = ContentFormatter.FormatJson(input);

        result.Should().Contain("\n");
        result.Should().Contain("1");
        result.Should().Contain("5");
    }

    [Fact]
    public void FormatJson_NestedObject_ReturnsProperlyIndented()
    {
        var input = "{\"outer\":{\"inner\":{\"deep\":\"value\"}}}";
        var result = ContentFormatter.FormatJson(input);

        result.Should().Contain("outer");
        result.Should().Contain("inner");
        result.Should().Contain("deep");
        result.Should().Contain("value");
    }

    [Fact]
    public void FormatJson_InvalidJson_ReturnsOriginal()
    {
        var input = "not valid json";
        var result = ContentFormatter.FormatJson(input);

        result.Should().Be(input);
    }

    [Fact]
    public void MinifyJson_FormattedJson_ReturnsMinified()
    {
        var input = @"{
  ""name"": ""test"",
  ""value"": 123
}";
        var result = ContentFormatter.MinifyJson(input);

        result.Should().NotContain("\n");
        result.Should().NotContain("  ");
        result.Should().Contain("\"name\"");
    }

    #endregion

    #region XML Formatting

    [Fact]
    public void FormatXml_MinifiedXml_ReturnsFormattedXml()
    {
        var input = "<root><child>value</child></root>";
        var result = ContentFormatter.FormatXml(input);

        result.Should().Contain("\n");
        result.Should().Contain("<root>");
        result.Should().Contain("<child>");
    }

    [Fact]
    public void FormatXml_WithAttributes_PreservesAttributes()
    {
        var input = "<root attr=\"value\"><child/></root>";
        var result = ContentFormatter.FormatXml(input);

        result.Should().Contain("attr=\"value\"");
    }

    [Fact]
    public void FormatXml_WithDeclaration_PreservesDeclaration()
    {
        var input = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root/>";
        var result = ContentFormatter.FormatXml(input);

        result.Should().Contain("<?xml");
    }

    [Fact]
    public void FormatXml_InvalidXml_ReturnsOriginal()
    {
        var input = "<not valid xml";
        var result = ContentFormatter.FormatXml(input);

        result.Should().Be(input);
    }

    #endregion

    #region SQL Formatting

    [Fact]
    public void FormatSql_SelectQuery_AddsNewlines()
    {
        var input = "SELECT * FROM users WHERE id = 1 ORDER BY name";
        var result = ContentFormatter.FormatSql(input);

        result.Should().Contain("SELECT");
        result.Should().Contain("FROM");
        result.Should().Contain("WHERE");
        result.Should().Contain("ORDER BY");
    }

    [Fact]
    public void FormatSql_InsertQuery_AddsNewlines()
    {
        var input = "INSERT INTO users (name, email) VALUES ('John', 'john@example.com')";
        var result = ContentFormatter.FormatSql(input);

        result.Should().Contain("INSERT INTO");
        result.Should().Contain("VALUES");
    }

    [Fact]
    public void FormatSql_JoinQuery_AddsNewlines()
    {
        var input = "SELECT * FROM users JOIN orders ON users.id = orders.user_id";
        var result = ContentFormatter.FormatSql(input);

        result.Should().Contain("SELECT");
        result.Should().Contain("FROM");
        result.Should().Contain("JOIN");
        result.Should().Contain("ON");
    }

    #endregion

    #region Format Method (Generic)

    [Fact]
    public void Format_JsonType_FormatsAsJson()
    {
        var input = "{\"key\":\"value\"}";
        var result = ContentFormatter.Format(input, VisualizerType.Json);

        result.Should().Contain("\n"); // Should be formatted
    }

    [Fact]
    public void Format_XmlType_FormatsAsXml()
    {
        var input = "<root><child/></root>";
        var result = ContentFormatter.Format(input, VisualizerType.Xml);

        result.Should().Contain("\n"); // Should be formatted
    }

    [Fact]
    public void Format_UnknownType_ReturnsOriginal()
    {
        var input = "some content";
        var result = ContentFormatter.Format(input, VisualizerType.Base64);

        result.Should().Be(input);
    }

    [Fact]
    public void Format_Null_ReturnsNull()
    {
        var result = ContentFormatter.Format(null, VisualizerType.Json);

        result.Should().BeNull();
    }

    [Fact]
    public void Format_Empty_ReturnsEmpty()
    {
        var result = ContentFormatter.Format(string.Empty, VisualizerType.Json);

        result.Should().BeEmpty();
    }

    #endregion

    #region Statistics

    [Fact]
    public void GetStatistics_SingleLine_ReturnsCorrectStats()
    {
        var input = "Hello, World!";
        var result = ContentFormatter.GetStatistics(input);

        result.Should().Contain("Lines: 1");
        result.Should().Contain("Characters: 13");
    }

    [Fact]
    public void GetStatistics_MultipleLines_ReturnsCorrectLineCount()
    {
        var input = "Line 1\nLine 2\nLine 3";
        var result = ContentFormatter.GetStatistics(input);

        result.Should().Contain("Lines: 3");
    }

    [Fact]
    public void GetStatistics_WindowsLineEndings_ReturnsCorrectLineCount()
    {
        var input = "Line 1\r\nLine 2\r\nLine 3";
        var result = ContentFormatter.GetStatistics(input);

        result.Should().Contain("Lines: 3");
    }

    [Fact]
    public void GetStatistics_Empty_ReturnsEmptyMessage()
    {
        var result = ContentFormatter.GetStatistics(string.Empty);

        result.Should().Be("Empty content");
    }

    [Fact]
    public void GetStatistics_Null_ReturnsEmptyMessage()
    {
        var result = ContentFormatter.GetStatistics(null);

        result.Should().Be("Empty content");
    }

    #endregion
}
