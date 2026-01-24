using System;
using System.Text.RegularExpressions;

namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Service for auto-detecting content type from string content.
/// </summary>
public static class FormatDetector
{
    private static readonly Regex JsonPattern = new Regex(@"^\s*[\[\{]", RegexOptions.Compiled);
    private static readonly Regex XmlPattern = new Regex(@"^\s*<[?!]?\w", RegexOptions.Compiled);
    private static readonly Regex HtmlPattern = new Regex(@"^\s*<!DOCTYPE\s+html|<html|<head|<body", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex YamlPattern = new Regex(@"^---\s*$|^\w+:\s+", RegexOptions.Compiled | RegexOptions.Multiline);
    private static readonly Regex TomlPattern = new Regex(@"^\s*\[[\w.-]+\]|^\s*\w+\s*=", RegexOptions.Compiled | RegexOptions.Multiline);
    private static readonly Regex IniPattern = new Regex(@"^\s*\[[\w\s]+\]\s*$", RegexOptions.Compiled | RegexOptions.Multiline);
    private static readonly Regex CsvPattern = new Regex(@"^[^,\n]+,[^,\n]+", RegexOptions.Compiled);
    private static readonly Regex JwtPattern = new Regex(@"^eyJ[A-Za-z0-9_-]+\.eyJ[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+$", RegexOptions.Compiled);
    private static readonly Regex Base64Pattern = new Regex(@"^[A-Za-z0-9+/]+=*$", RegexOptions.Compiled);
    private static readonly Regex DataUriPattern = new Regex(@"^data:[\w/+-]+;base64,", RegexOptions.Compiled);
    private static readonly Regex UrlEncodedPattern = new Regex(@"%[0-9A-Fa-f]{2}", RegexOptions.Compiled);
    private static readonly Regex GuidPattern = new Regex(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$", RegexOptions.Compiled);
    private static readonly Regex ConnectionStringPattern = new Regex(@"(Data Source|Server|Initial Catalog|Database|User Id|Password|Integrated Security)=", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex UriPattern = new Regex(@"^(https?|ftp|file|mailto|tel)://", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex SqlPattern = new Regex(@"^\s*(SELECT|INSERT|UPDATE|DELETE|CREATE|ALTER|DROP|EXEC|WITH)\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex CronPattern = new Regex(@"^(\*(?:\/\d+)?|[\d,\-\/]+)\s+(\*(?:\/\d+)?|[\d,\-\/]+)\s+(\*(?:\/\d+)?|[\d,\-\/]+)\s+(\*(?:\/\d+)?|[\d,\-\/]+)\s+(\*(?:\/\d+)?|[\d,\-\/]+)", RegexOptions.Compiled);
    private static readonly Regex IpAddressPattern = new Regex(@"^(\d{1,3}\.){3}\d{1,3}$|^([0-9a-fA-F]{0,4}:){2,7}[0-9a-fA-F]{0,4}$", RegexOptions.Compiled);
    private static readonly Regex HexStringPattern = new Regex(@"^[0-9A-Fa-f]+$", RegexOptions.Compiled);
    private static readonly Regex UnixTimestampPattern = new Regex(@"^\d{10,13}$", RegexOptions.Compiled);

    /// <summary>
    /// Attempts to detect the format of the provided content.
    /// </summary>
    /// <param name="content">The string content to analyze.</param>
    /// <returns>The detected visualizer type, or null if no format is detected.</returns>
    public static VisualizerType? DetectFormat(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        var trimmed = content.Trim();

        // Check JWT first (specific pattern)
        if (JwtPattern.IsMatch(trimmed))
        {
            return VisualizerType.Jwt;
        }

        // Check data URI (for base64 images)
        if (DataUriPattern.IsMatch(trimmed))
        {
            return VisualizerType.Base64Image;
        }

        // Check GUID
        if (GuidPattern.IsMatch(trimmed))
        {
            return VisualizerType.Guid;
        }

        // Check IP Address
        if (IpAddressPattern.IsMatch(trimmed))
        {
            return VisualizerType.IpAddress;
        }

        // Check Unix timestamp
        if (UnixTimestampPattern.IsMatch(trimmed))
        {
            return VisualizerType.Timestamp;
        }

        // Check URI
        if (UriPattern.IsMatch(trimmed))
        {
            return VisualizerType.Uri;
        }

        // Check SQL
        if (SqlPattern.IsMatch(trimmed))
        {
            return VisualizerType.Sql;
        }

        // Check cron expression
        if (CronPattern.IsMatch(trimmed))
        {
            return VisualizerType.Cron;
        }

        // Check connection string
        if (ConnectionStringPattern.IsMatch(trimmed))
        {
            return VisualizerType.ConnectionString;
        }

        // Check HTML (before XML since HTML is a subset)
        if (HtmlPattern.IsMatch(trimmed))
        {
            return VisualizerType.Html;
        }

        // Check XML
        if (XmlPattern.IsMatch(trimmed))
        {
            return VisualizerType.Xml;
        }

        // Check JSON
        if (JsonPattern.IsMatch(trimmed))
        {
            return VisualizerType.Json;
        }

        // Check YAML (check before TOML as YAML is more permissive)
        if (YamlPattern.IsMatch(trimmed))
        {
            return VisualizerType.Yaml;
        }

        // Check TOML
        if (TomlPattern.IsMatch(trimmed))
        {
            return VisualizerType.Toml;
        }

        // Check INI
        if (IniPattern.IsMatch(trimmed))
        {
            return VisualizerType.Ini;
        }

        // Check CSV (basic check)
        if (CsvPattern.IsMatch(trimmed) && trimmed.Contains("\n"))
        {
            return VisualizerType.Csv;
        }

        // Check URL encoded
        if (UrlEncodedPattern.IsMatch(trimmed))
        {
            return VisualizerType.UrlEncoded;
        }

        // Check base64 (must be reasonably long and valid)
        if (trimmed.Length >= 4 && Base64Pattern.IsMatch(trimmed))
        {
            return VisualizerType.Base64;
        }

        // Check hex string (must be even length)
        if (trimmed.Length >= 2 && trimmed.Length % 2 == 0 && HexStringPattern.IsMatch(trimmed))
        {
            return VisualizerType.HexString;
        }

        return null;
    }

    /// <summary>
    /// Checks if the content appears to be a base64-encoded image.
    /// </summary>
    /// <param name="content">The content to check.</param>
    /// <returns>True if the content appears to be a base64-encoded image.</returns>
    public static bool IsBase64Image(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return false;
        }

        var trimmed = content.Trim();

        // Check for data URI with image MIME type
        if (trimmed.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        // Try to detect common image magic bytes in base64
        // PNG: iVBORw0KGgo
        // JPEG: /9j/
        // GIF: R0lGOD
        // BMP: Qk0
        // WebP: UklGR
        return trimmed.StartsWith("iVBORw0KGgo", StringComparison.Ordinal) ||
               trimmed.StartsWith("/9j/", StringComparison.Ordinal) ||
               trimmed.StartsWith("R0lGOD", StringComparison.Ordinal) ||
               trimmed.StartsWith("Qk0", StringComparison.Ordinal) ||
               trimmed.StartsWith("UklGR", StringComparison.Ordinal);
    }
}
