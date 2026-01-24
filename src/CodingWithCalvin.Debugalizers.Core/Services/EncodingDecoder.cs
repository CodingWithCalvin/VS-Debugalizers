using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Service for decoding various encoded string formats.
/// </summary>
public static class EncodingDecoder
{
    private static readonly Regex UnicodeEscapePattern = new Regex(
        @"\\u([0-9A-Fa-f]{4})",
        RegexOptions.Compiled);

    private static readonly Regex HexEscapePattern = new Regex(
        @"\\x([0-9A-Fa-f]{2})",
        RegexOptions.Compiled);

    /// <summary>
    /// Decodes a Base64 encoded string to plain text.
    /// </summary>
    /// <param name="base64">The Base64 encoded string.</param>
    /// <returns>The decoded text, or the original string if decoding fails.</returns>
    public static string DecodeBase64(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
        {
            return base64;
        }

        try
        {
            var bytes = Convert.FromBase64String(base64.Trim());
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return base64;
        }
    }

    /// <summary>
    /// Encodes a string to Base64.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string EncodeBase64(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a URL encoded string.
    /// </summary>
    /// <param name="urlEncoded">The URL encoded string.</param>
    /// <returns>The decoded string.</returns>
    public static string DecodeUrl(string urlEncoded)
    {
        if (string.IsNullOrWhiteSpace(urlEncoded))
        {
            return urlEncoded;
        }

        try
        {
            return HttpUtility.UrlDecode(urlEncoded);
        }
        catch
        {
            return urlEncoded;
        }
    }

    /// <summary>
    /// Encodes a string for use in URLs.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <returns>The URL encoded string.</returns>
    public static string EncodeUrl(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return HttpUtility.UrlEncode(text);
    }

    /// <summary>
    /// Decodes HTML entities in a string.
    /// </summary>
    /// <param name="htmlEncoded">The HTML encoded string.</param>
    /// <returns>The decoded string.</returns>
    public static string DecodeHtmlEntities(string htmlEncoded)
    {
        if (string.IsNullOrWhiteSpace(htmlEncoded))
        {
            return htmlEncoded;
        }

        try
        {
            return HttpUtility.HtmlDecode(htmlEncoded);
        }
        catch
        {
            return htmlEncoded;
        }
    }

    /// <summary>
    /// Encodes special characters as HTML entities.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <returns>The HTML encoded string.</returns>
    public static string EncodeHtmlEntities(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return HttpUtility.HtmlEncode(text);
    }

    /// <summary>
    /// Decodes Unicode escape sequences (\uXXXX) in a string.
    /// </summary>
    /// <param name="unicodeEscaped">The string with Unicode escape sequences.</param>
    /// <returns>The decoded string.</returns>
    public static string DecodeUnicodeEscape(string unicodeEscaped)
    {
        if (string.IsNullOrWhiteSpace(unicodeEscaped))
        {
            return unicodeEscaped;
        }

        try
        {
            var result = UnicodeEscapePattern.Replace(unicodeEscaped, match =>
            {
                var hexValue = match.Groups[1].Value;
                var codePoint = Convert.ToInt32(hexValue, 16);
                return char.ConvertFromUtf32(codePoint);
            });

            // Also handle \xXX patterns
            result = HexEscapePattern.Replace(result, match =>
            {
                var hexValue = match.Groups[1].Value;
                var byteValue = Convert.ToByte(hexValue, 16);
                return ((char)byteValue).ToString();
            });

            return result;
        }
        catch
        {
            return unicodeEscaped;
        }
    }

    /// <summary>
    /// Decodes a hex string to plain text.
    /// </summary>
    /// <param name="hexString">The hex string (e.g., "48656C6C6F").</param>
    /// <returns>The decoded text.</returns>
    public static string DecodeHexString(string hexString)
    {
        if (string.IsNullOrWhiteSpace(hexString))
        {
            return hexString;
        }

        try
        {
            var hex = hexString.Trim().Replace(" ", "").Replace("-", "");
            if (hex.Length % 2 != 0)
            {
                return hexString; // Invalid hex string
            }

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return hexString;
        }
    }

    /// <summary>
    /// Encodes a string as a hex string.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <returns>The hex encoded string.</returns>
    public static string EncodeHexString(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    /// <summary>
    /// Decompresses a GZip compressed Base64 string.
    /// </summary>
    /// <param name="gzipBase64">The GZip compressed data encoded as Base64.</param>
    /// <returns>The decompressed string.</returns>
    public static string DecompressGZip(string gzipBase64)
    {
        if (string.IsNullOrWhiteSpace(gzipBase64))
        {
            return gzipBase64;
        }

        try
        {
            var compressedBytes = Convert.FromBase64String(gzipBase64.Trim());
            using (var compressedStream = new MemoryStream(compressedBytes))
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                gzipStream.CopyTo(resultStream);
                return Encoding.UTF8.GetString(resultStream.ToArray());
            }
        }
        catch
        {
            return gzipBase64;
        }
    }

    /// <summary>
    /// Compresses a string using GZip and returns Base64.
    /// </summary>
    /// <param name="text">The text to compress.</param>
    /// <returns>The GZip compressed Base64 string.</returns>
    public static string CompressGZip(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        using (var resultStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(resultStream, CompressionMode.Compress))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }
            return Convert.ToBase64String(resultStream.ToArray());
        }
    }

    /// <summary>
    /// Decompresses a Deflate compressed Base64 string.
    /// </summary>
    /// <param name="deflateBase64">The Deflate compressed data encoded as Base64.</param>
    /// <returns>The decompressed string.</returns>
    public static string DecompressDeflate(string deflateBase64)
    {
        if (string.IsNullOrWhiteSpace(deflateBase64))
        {
            return deflateBase64;
        }

        try
        {
            var compressedBytes = Convert.FromBase64String(deflateBase64.Trim());
            using (var compressedStream = new MemoryStream(compressedBytes))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                deflateStream.CopyTo(resultStream);
                return Encoding.UTF8.GetString(resultStream.ToArray());
            }
        }
        catch
        {
            return deflateBase64;
        }
    }

    /// <summary>
    /// Compresses a string using Deflate and returns Base64.
    /// </summary>
    /// <param name="text">The text to compress.</param>
    /// <returns>The Deflate compressed Base64 string.</returns>
    public static string CompressDeflate(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        using (var resultStream = new MemoryStream())
        {
            using (var deflateStream = new DeflateStream(resultStream, CompressionMode.Compress))
            {
                deflateStream.Write(bytes, 0, bytes.Length);
            }
            return Convert.ToBase64String(resultStream.ToArray());
        }
    }

    /// <summary>
    /// Converts bytes to a hex dump string.
    /// </summary>
    /// <param name="bytes">The bytes to convert.</param>
    /// <param name="bytesPerLine">Number of bytes per line (default 16).</param>
    /// <returns>A formatted hex dump string.</returns>
    public static string ToHexDump(byte[] bytes, int bytesPerLine = 16)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i += bytesPerLine)
        {
            // Offset
            sb.Append($"{i:X8}  ");

            // Hex bytes
            for (int j = 0; j < bytesPerLine; j++)
            {
                if (i + j < bytes.Length)
                {
                    sb.Append($"{bytes[i + j]:X2} ");
                }
                else
                {
                    sb.Append("   ");
                }

                if (j == 7)
                {
                    sb.Append(' ');
                }
            }

            sb.Append(' ');

            // ASCII
            for (int j = 0; j < bytesPerLine && i + j < bytes.Length; j++)
            {
                var b = bytes[i + j];
                sb.Append(b >= 32 && b < 127 ? (char)b : '.');
            }

            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Converts a string to a hex dump.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>A formatted hex dump string.</returns>
    public static string ToHexDump(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return ToHexDump(Encoding.UTF8.GetBytes(text));
    }
}
