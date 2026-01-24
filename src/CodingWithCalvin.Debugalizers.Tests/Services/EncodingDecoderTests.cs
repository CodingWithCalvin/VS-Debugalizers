using CodingWithCalvin.Debugalizers.Core;
using FluentAssertions;
using Xunit;

namespace CodingWithCalvin.Debugalizers.Tests;

public class EncodingDecoderTests
{
    #region Base64

    [Theory]
    [InlineData("SGVsbG8gV29ybGQ=", "Hello World")]
    [InlineData("VGVzdCBTdHJpbmc=", "Test String")]
    [InlineData("", "")]
    public void DecodeBase64_ValidInput_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeBase64(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void DecodeBase64_InvalidBase64_ReturnsOriginal()
    {
        var input = "not valid base64!!!";
        var result = EncodingDecoder.DecodeBase64(input);
        result.Should().Be(input);
    }

    [Fact]
    public void DecodeBase64_Null_ReturnsNull()
    {
        var result = EncodingDecoder.DecodeBase64(null);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("Hello World", "SGVsbG8gV29ybGQ=")]
    [InlineData("Test", "VGVzdA==")]
    public void EncodeBase64_ValidInput_ReturnsEncodedString(string input, string expected)
    {
        var result = EncodingDecoder.EncodeBase64(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void Base64_RoundTrip_ReturnsOriginal()
    {
        var original = "This is a test string with special chars: <>&\"'";
        var encoded = EncodingDecoder.EncodeBase64(original);
        var decoded = EncodingDecoder.DecodeBase64(encoded);
        decoded.Should().Be(original);
    }

    #endregion

    #region URL Encoding

    [Theory]
    [InlineData("Hello%20World", "Hello World")]
    [InlineData("test%26value", "test&value")]
    [InlineData("a%3Db%26c%3Dd", "a=b&c=d")]
    public void DecodeUrl_ValidInput_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeUrl(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World", "Hello+World")]
    [InlineData("test&value", "test%26value")]
    public void EncodeUrl_ValidInput_ReturnsEncodedString(string input, string expected)
    {
        var result = EncodingDecoder.EncodeUrl(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void EncodeUrl_Null_ReturnsNull()
    {
        var result = EncodingDecoder.EncodeUrl(null);
        result.Should().BeNull();
    }

    [Fact]
    public void Url_RoundTrip_ReturnsOriginal()
    {
        var original = "Hello World & Test = Value";
        var encoded = EncodingDecoder.EncodeUrl(original);
        var decoded = EncodingDecoder.DecodeUrl(encoded);
        decoded.Should().Be(original);
    }

    #endregion

    #region HTML Entities

    [Theory]
    [InlineData("&lt;div&gt;", "<div>")]
    [InlineData("&amp;", "&")]
    [InlineData("&quot;test&quot;", "\"test\"")]
    [InlineData("&apos;", "'")]
    [InlineData("&nbsp;", "\u00A0")]
    public void DecodeHtmlEntities_ValidInput_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeHtmlEntities(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("<div>", "&lt;div&gt;")]
    [InlineData("\"test\"", "&quot;test&quot;")]
    [InlineData("&", "&amp;")]
    public void EncodeHtmlEntities_ValidInput_ReturnsEncodedString(string input, string expected)
    {
        var result = EncodingDecoder.EncodeHtmlEntities(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void HtmlEntities_RoundTrip_ReturnsOriginal()
    {
        var original = "<script>alert('XSS');</script>";
        var encoded = EncodingDecoder.EncodeHtmlEntities(original);
        var decoded = EncodingDecoder.DecodeHtmlEntities(encoded);
        decoded.Should().Be(original);
    }

    #endregion

    #region Unicode Escape

    [Theory]
    [InlineData(@"\u0041", "A")]
    [InlineData(@"\u0048\u0065\u006C\u006C\u006F", "Hello")]
    [InlineData(@"Hello \u0057orld", "Hello World")]
    public void DecodeUnicodeEscape_ValidInput_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeUnicodeEscape(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(@"\x41", "A")]
    [InlineData(@"\x48\x65\x6C\x6C\x6F", "Hello")]
    public void DecodeUnicodeEscape_HexEscape_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeUnicodeEscape(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void DecodeUnicodeEscape_NoEscapes_ReturnsOriginal()
    {
        var input = "Hello World";
        var result = EncodingDecoder.DecodeUnicodeEscape(input);
        result.Should().Be(input);
    }

    [Fact]
    public void DecodeUnicodeEscape_Null_ReturnsNull()
    {
        var result = EncodingDecoder.DecodeUnicodeEscape(null);
        result.Should().BeNull();
    }

    #endregion

    #region Hex String

    [Theory]
    [InlineData("48656C6C6F", "Hello")]
    [InlineData("576F726C64", "World")]
    [InlineData("48656C6C6F20576F726C64", "Hello World")]
    public void DecodeHexString_ValidInput_ReturnsDecodedString(string input, string expected)
    {
        var result = EncodingDecoder.DecodeHexString(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void DecodeHexString_WithSpaces_ReturnsDecodedString()
    {
        var input = "48 65 6C 6C 6F";
        var result = EncodingDecoder.DecodeHexString(input);
        result.Should().Be("Hello");
    }

    [Fact]
    public void DecodeHexString_WithDashes_ReturnsDecodedString()
    {
        var input = "48-65-6C-6C-6F";
        var result = EncodingDecoder.DecodeHexString(input);
        result.Should().Be("Hello");
    }

    [Fact]
    public void DecodeHexString_OddLength_ReturnsOriginal()
    {
        var input = "48656C6C6"; // Odd length
        var result = EncodingDecoder.DecodeHexString(input);
        result.Should().Be(input);
    }

    [Theory]
    [InlineData("Hello", "48656C6C6F")]
    [InlineData("World", "576F726C64")]
    public void EncodeHexString_ValidInput_ReturnsEncodedString(string input, string expected)
    {
        var result = EncodingDecoder.EncodeHexString(input);
        result.Should().Be(expected);
    }

    [Fact]
    public void HexString_RoundTrip_ReturnsOriginal()
    {
        var original = "Test String 123";
        var encoded = EncodingDecoder.EncodeHexString(original);
        var decoded = EncodingDecoder.DecodeHexString(encoded);
        decoded.Should().Be(original);
    }

    #endregion

    #region GZip

    [Fact]
    public void GZip_RoundTrip_ReturnsOriginal()
    {
        var original = "This is a test string that will be compressed and decompressed.";
        var compressed = EncodingDecoder.CompressGZip(original);
        var decompressed = EncodingDecoder.DecompressGZip(compressed);
        decompressed.Should().Be(original);
    }

    [Fact]
    public void CompressGZip_Null_ReturnsNull()
    {
        var result = EncodingDecoder.CompressGZip(null);
        result.Should().BeNull();
    }

    [Fact]
    public void CompressGZip_Empty_ReturnsEmpty()
    {
        var result = EncodingDecoder.CompressGZip(string.Empty);
        result.Should().BeEmpty();
    }

    [Fact]
    public void DecompressGZip_InvalidData_ReturnsOriginal()
    {
        var input = "not valid gzip base64";
        var result = EncodingDecoder.DecompressGZip(input);
        result.Should().Be(input);
    }

    #endregion

    #region Deflate

    [Fact]
    public void Deflate_RoundTrip_ReturnsOriginal()
    {
        var original = "This is another test string for deflate compression.";
        var compressed = EncodingDecoder.CompressDeflate(original);
        var decompressed = EncodingDecoder.DecompressDeflate(compressed);
        decompressed.Should().Be(original);
    }

    [Fact]
    public void CompressDeflate_Null_ReturnsNull()
    {
        var result = EncodingDecoder.CompressDeflate(null);
        result.Should().BeNull();
    }

    [Fact]
    public void DecompressDeflate_InvalidData_ReturnsOriginal()
    {
        var input = "not valid deflate base64";
        var result = EncodingDecoder.DecompressDeflate(input);
        result.Should().Be(input);
    }

    #endregion

    #region Hex Dump

    [Fact]
    public void ToHexDump_ValidBytes_ReturnsFormattedDump()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello World");
        var result = EncodingDecoder.ToHexDump(bytes);

        result.Should().Contain("00000000");
        result.Should().Contain("48");  // 'H'
        result.Should().Contain("Hello World");
    }

    [Fact]
    public void ToHexDump_String_ReturnsFormattedDump()
    {
        var result = EncodingDecoder.ToHexDump("Hello");

        result.Should().Contain("00000000");
        result.Should().Contain("Hello");
    }

    [Fact]
    public void ToHexDump_EmptyBytes_ReturnsEmpty()
    {
        var result = EncodingDecoder.ToHexDump(new byte[0]);
        result.Should().BeEmpty();
    }

    [Fact]
    public void ToHexDump_NullBytes_ReturnsEmpty()
    {
        var result = EncodingDecoder.ToHexDump((byte[])null);
        result.Should().BeEmpty();
    }

    [Fact]
    public void ToHexDump_NonPrintableChars_ShowsDots()
    {
        var bytes = new byte[] { 0x00, 0x01, 0x02, 0x41 }; // Non-printable + 'A'
        var result = EncodingDecoder.ToHexDump(bytes);

        result.Should().Contain("...A");
    }

    [Fact]
    public void ToHexDump_LongContent_HasMultipleLines()
    {
        var bytes = new byte[32]; // More than 16 bytes per line
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)(0x41 + (i % 26)); // A-Z repeating
        }

        var result = EncodingDecoder.ToHexDump(bytes);

        result.Should().Contain("00000000");
        result.Should().Contain("00000010"); // Second line offset
    }

    #endregion
}
