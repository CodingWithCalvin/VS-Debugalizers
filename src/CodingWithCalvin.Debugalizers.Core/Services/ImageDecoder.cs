using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Service for decoding images from Base64 and data URIs.
/// </summary>
public static class ImageDecoder
{
    private static readonly Regex DataUriPattern = new Regex(
        @"^data:(?<mime>[\w/+-]+);base64,(?<data>.+)$",
        RegexOptions.Compiled | RegexOptions.Singleline);

    /// <summary>
    /// Decodes a Base64 string to a BitmapImage.
    /// </summary>
    /// <param name="base64">The Base64-encoded image data.</param>
    /// <returns>The decoded BitmapImage, or null if decoding fails.</returns>
    public static BitmapImage DecodeFromBase64(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
        {
            return null;
        }

        try
        {
            var bytes = Convert.FromBase64String(base64.Trim());
            return CreateBitmapImage(bytes);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Decodes a data URI to a BitmapImage.
    /// </summary>
    /// <param name="dataUri">The data URI (e.g., data:image/png;base64,...).</param>
    /// <returns>The decoded BitmapImage, or null if decoding fails.</returns>
    public static BitmapImage DecodeFromDataUri(string dataUri)
    {
        if (string.IsNullOrWhiteSpace(dataUri))
        {
            return null;
        }

        try
        {
            var match = DataUriPattern.Match(dataUri.Trim());
            if (!match.Success)
            {
                return null;
            }

            var base64Data = match.Groups["data"].Value;
            return DecodeFromBase64(base64Data);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Decodes either a data URI or raw Base64 string to a BitmapImage.
    /// </summary>
    /// <param name="content">The content to decode.</param>
    /// <returns>The decoded BitmapImage, or null if decoding fails.</returns>
    public static BitmapImage Decode(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        var trimmed = content.Trim();

        // Check if it's a data URI
        if (trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            return DecodeFromDataUri(trimmed);
        }

        // Try as raw Base64
        return DecodeFromBase64(trimmed);
    }

    /// <summary>
    /// Gets metadata about an image.
    /// </summary>
    /// <param name="image">The BitmapImage to analyze.</param>
    /// <returns>An ImageMetadata object containing image information.</returns>
    public static ImageMetadata GetMetadata(BitmapImage image)
    {
        if (image == null)
        {
            return null;
        }

        return new ImageMetadata
        {
            Width = image.PixelWidth,
            Height = image.PixelHeight,
            DpiX = image.DpiX,
            DpiY = image.DpiY,
            Format = image.Format.ToString()
        };
    }

    /// <summary>
    /// Gets metadata from a Base64 or data URI string.
    /// </summary>
    /// <param name="content">The content to analyze.</param>
    /// <returns>An ImageMetadata object containing image information.</returns>
    public static ImageMetadata GetMetadata(string content)
    {
        var image = Decode(content);
        if (image == null)
        {
            return null;
        }

        var metadata = GetMetadata(image);

        // Try to get MIME type from data URI
        if (content.Trim().StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            var match = DataUriPattern.Match(content.Trim());
            if (match.Success)
            {
                metadata.MimeType = match.Groups["mime"].Value;
            }
        }

        // Estimate original size
        var base64Data = content.Trim();
        if (base64Data.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            var commaIndex = base64Data.IndexOf(',');
            if (commaIndex >= 0)
            {
                base64Data = base64Data.Substring(commaIndex + 1);
            }
        }

        // Base64 encodes 3 bytes to 4 characters
        metadata.EstimatedSizeBytes = (int)(base64Data.Length * 3 / 4);

        return metadata;
    }

    /// <summary>
    /// Extracts the MIME type from a data URI.
    /// </summary>
    /// <param name="dataUri">The data URI.</param>
    /// <returns>The MIME type, or null if not found.</returns>
    public static string GetMimeTypeFromDataUri(string dataUri)
    {
        if (string.IsNullOrWhiteSpace(dataUri))
        {
            return null;
        }

        var match = DataUriPattern.Match(dataUri.Trim());
        return match.Success ? match.Groups["mime"].Value : null;
    }

    private static BitmapImage CreateBitmapImage(byte[] bytes)
    {
        using (var stream = new MemoryStream(bytes))
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze(); // Make it cross-thread accessible
            return image;
        }
    }
}

/// <summary>
/// Contains metadata about an image.
/// </summary>
public class ImageMetadata
{
    /// <summary>
    /// Gets or sets the width in pixels.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height in pixels.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the horizontal DPI.
    /// </summary>
    public double DpiX { get; set; }

    /// <summary>
    /// Gets or sets the vertical DPI.
    /// </summary>
    public double DpiY { get; set; }

    /// <summary>
    /// Gets or sets the pixel format.
    /// </summary>
    public string Format { get; set; }

    /// <summary>
    /// Gets or sets the MIME type.
    /// </summary>
    public string MimeType { get; set; }

    /// <summary>
    /// Gets or sets the estimated size in bytes.
    /// </summary>
    public int EstimatedSizeBytes { get; set; }

    /// <summary>
    /// Returns a formatted string representation of the metadata.
    /// </summary>
    public override string ToString()
    {
        return $"{Width} x {Height} px | {Format} | {EstimatedSizeBytes:N0} bytes";
    }
}
