using CodingWithCalvin.Debugalizers.Core;
using FluentAssertions;
using Xunit;

namespace CodingWithCalvin.Debugalizers.Tests;

public class FormatDetectorTests
{
    [Theory]
    [InlineData("{\"name\": \"test\"}", VisualizerType.Json)]
    [InlineData("[1, 2, 3]", VisualizerType.Json)]
    [InlineData("{ \"key\": \"value\" }", VisualizerType.Json)]
    public void DetectFormat_Json_ReturnsJsonType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("<?xml version=\"1.0\"?><root></root>", VisualizerType.Xml)]
    [InlineData("<root><child/></root>", VisualizerType.Xml)]
    [InlineData("<!DOCTYPE config><config></config>", VisualizerType.Xml)]
    public void DetectFormat_Xml_ReturnsXmlType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("<!DOCTYPE html><html></html>", VisualizerType.Html)]
    [InlineData("<html><head></head><body></body></html>", VisualizerType.Html)]
    [InlineData("<body><p>test</p></body>", VisualizerType.Html)]
    public void DetectFormat_Html_ReturnsHtmlType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", VisualizerType.Jwt)]
    public void DetectFormat_Jwt_ReturnsJwtType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("data:image/png;base64,iVBORw0KGgo", VisualizerType.Base64Image)]
    [InlineData("data:image/jpeg;base64,/9j/4AAQ", VisualizerType.Base64Image)]
    public void DetectFormat_DataUri_ReturnsBase64ImageType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000", VisualizerType.Guid)]
    [InlineData("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE", VisualizerType.Guid)]
    public void DetectFormat_Guid_ReturnsGuidType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("192.168.1.1", VisualizerType.IpAddress)]
    [InlineData("10.0.0.1", VisualizerType.IpAddress)]
    [InlineData("255.255.255.255", VisualizerType.IpAddress)]
    public void DetectFormat_IpAddress_ReturnsIpAddressType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("1704067200", VisualizerType.Timestamp)]
    [InlineData("1704067200000", VisualizerType.Timestamp)]
    public void DetectFormat_Timestamp_ReturnsTimestampType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("https://example.com/path?query=value", VisualizerType.Uri)]
    [InlineData("http://localhost:8080/api", VisualizerType.Uri)]
    [InlineData("ftp://files.example.com/file.txt", VisualizerType.Uri)]
    public void DetectFormat_Uri_ReturnsUriType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("SELECT * FROM users WHERE id = 1", VisualizerType.Sql)]
    [InlineData("INSERT INTO table (col) VALUES ('val')", VisualizerType.Sql)]
    [InlineData("UPDATE users SET name = 'test'", VisualizerType.Sql)]
    [InlineData("DELETE FROM users WHERE id = 1", VisualizerType.Sql)]
    public void DetectFormat_Sql_ReturnsSqlType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("* * * * *", VisualizerType.Cron)]
    [InlineData("0 0 * * *", VisualizerType.Cron)]
    [InlineData("*/5 * * * *", VisualizerType.Cron)]
    [InlineData("0 0 1 * *", VisualizerType.Cron)]
    public void DetectFormat_Cron_ReturnsCronType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Server=localhost;Database=test;User Id=sa;Password=pass;", VisualizerType.ConnectionString)]
    [InlineData("Data Source=server;Initial Catalog=db;Integrated Security=True", VisualizerType.ConnectionString)]
    public void DetectFormat_ConnectionString_ReturnsConnectionStringType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello%20World", VisualizerType.UrlEncoded)]
    [InlineData("name%3Dvalue%26foo%3Dbar", VisualizerType.UrlEncoded)]
    public void DetectFormat_UrlEncoded_ReturnsUrlEncodedType(string content, VisualizerType expected)
    {
        var result = FormatDetector.DetectFormat(content);
        result.Should().Be(expected);
    }

    [Fact]
    public void DetectFormat_Null_ReturnsNull()
    {
        var result = FormatDetector.DetectFormat(null);
        result.Should().BeNull();
    }

    [Fact]
    public void DetectFormat_Empty_ReturnsNull()
    {
        var result = FormatDetector.DetectFormat(string.Empty);
        result.Should().BeNull();
    }

    [Fact]
    public void DetectFormat_Whitespace_ReturnsNull()
    {
        var result = FormatDetector.DetectFormat("   ");
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==", true)]
    [InlineData("/9j/4AAQSkZJRgABAQEASABIAAD", true)]
    [InlineData("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7", true)]
    [InlineData("data:image/png;base64,iVBORw0KGgo", true)]
    [InlineData("SGVsbG8gV29ybGQ=", false)]
    [InlineData("notanimage", false)]
    public void IsBase64Image_ReturnsCorrectResult(string content, bool expected)
    {
        var result = FormatDetector.IsBase64Image(content);
        result.Should().Be(expected);
    }
}
