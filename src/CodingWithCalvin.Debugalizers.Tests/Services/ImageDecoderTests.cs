using CodingWithCalvin.Debugalizers.Core;
using FluentAssertions;
using Xunit;

namespace CodingWithCalvin.Debugalizers.Tests;

public class ImageDecoderTests
{
    // 1x1 transparent PNG
    private const string ValidPngBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==";

    // 1x1 red GIF
    private const string ValidGifBase64 = "R0lGODlhAQABAIAAAP8AAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";

    [Fact]
    public void DecodeFromBase64_ValidPng_ReturnsImage()
    {
        var result = ImageDecoder.DecodeFromBase64(ValidPngBase64);

        result.Should().NotBeNull();
        result.PixelWidth.Should().Be(1);
        result.PixelHeight.Should().Be(1);
    }

    [Fact]
    public void DecodeFromBase64_ValidGif_ReturnsImage()
    {
        var result = ImageDecoder.DecodeFromBase64(ValidGifBase64);

        result.Should().NotBeNull();
        result.PixelWidth.Should().Be(1);
        result.PixelHeight.Should().Be(1);
    }

    [Fact]
    public void DecodeFromBase64_InvalidBase64_ReturnsNull()
    {
        var result = ImageDecoder.DecodeFromBase64("not valid base64!!!");

        result.Should().BeNull();
    }

    [Fact]
    public void DecodeFromBase64_ValidBase64ButNotImage_ReturnsNull()
    {
        var textBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("Hello World"));
        var result = ImageDecoder.DecodeFromBase64(textBase64);

        result.Should().BeNull();
    }

    [Fact]
    public void DecodeFromBase64_Null_ReturnsNull()
    {
        var result = ImageDecoder.DecodeFromBase64(null);

        result.Should().BeNull();
    }

    [Fact]
    public void DecodeFromBase64_Empty_ReturnsNull()
    {
        var result = ImageDecoder.DecodeFromBase64(string.Empty);

        result.Should().BeNull();
    }

    [Fact]
    public void DecodeFromDataUri_ValidPngDataUri_ReturnsImage()
    {
        var dataUri = $"data:image/png;base64,{ValidPngBase64}";
        var result = ImageDecoder.DecodeFromDataUri(dataUri);

        result.Should().NotBeNull();
        result.PixelWidth.Should().Be(1);
    }

    [Fact]
    public void DecodeFromDataUri_ValidGifDataUri_ReturnsImage()
    {
        var dataUri = $"data:image/gif;base64,{ValidGifBase64}";
        var result = ImageDecoder.DecodeFromDataUri(dataUri);

        result.Should().NotBeNull();
    }

    [Fact]
    public void DecodeFromDataUri_InvalidDataUri_ReturnsNull()
    {
        var result = ImageDecoder.DecodeFromDataUri("not a data uri");

        result.Should().BeNull();
    }

    [Fact]
    public void DecodeFromDataUri_Null_ReturnsNull()
    {
        var result = ImageDecoder.DecodeFromDataUri(null);

        result.Should().BeNull();
    }

    [Fact]
    public void Decode_DataUri_DecodesCorrectly()
    {
        var dataUri = $"data:image/png;base64,{ValidPngBase64}";
        var result = ImageDecoder.Decode(dataUri);

        result.Should().NotBeNull();
    }

    [Fact]
    public void Decode_RawBase64_DecodesCorrectly()
    {
        var result = ImageDecoder.Decode(ValidPngBase64);

        result.Should().NotBeNull();
    }

    [Fact]
    public void GetMetadata_ValidImage_ReturnsMetadata()
    {
        var image = ImageDecoder.DecodeFromBase64(ValidPngBase64);
        var metadata = ImageDecoder.GetMetadata(image);

        metadata.Should().NotBeNull();
        metadata.Width.Should().Be(1);
        metadata.Height.Should().Be(1);
        metadata.DpiX.Should().BeGreaterThan(0);
        metadata.DpiY.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetMetadata_NullImage_ReturnsNull()
    {
        var metadata = ImageDecoder.GetMetadata((System.Windows.Media.Imaging.BitmapImage)null);

        metadata.Should().BeNull();
    }

    [Fact]
    public void GetMetadata_FromString_ReturnsMetadataWithMimeType()
    {
        var dataUri = $"data:image/png;base64,{ValidPngBase64}";
        var metadata = ImageDecoder.GetMetadata(dataUri);

        metadata.Should().NotBeNull();
        metadata.MimeType.Should().Be("image/png");
        metadata.EstimatedSizeBytes.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetMimeTypeFromDataUri_ValidPng_ReturnsMimeType()
    {
        var dataUri = $"data:image/png;base64,{ValidPngBase64}";
        var mimeType = ImageDecoder.GetMimeTypeFromDataUri(dataUri);

        mimeType.Should().Be("image/png");
    }

    [Fact]
    public void GetMimeTypeFromDataUri_ValidJpeg_ReturnsMimeType()
    {
        var dataUri = "data:image/jpeg;base64,/9j/4AAQ";
        var mimeType = ImageDecoder.GetMimeTypeFromDataUri(dataUri);

        mimeType.Should().Be("image/jpeg");
    }

    [Fact]
    public void GetMimeTypeFromDataUri_InvalidUri_ReturnsNull()
    {
        var mimeType = ImageDecoder.GetMimeTypeFromDataUri("not a data uri");

        mimeType.Should().BeNull();
    }

    [Fact]
    public void ImageMetadata_ToString_ReturnsFormattedString()
    {
        var metadata = new ImageMetadata
        {
            Width = 100,
            Height = 200,
            Format = "Bgra32",
            EstimatedSizeBytes = 1024
        };

        var result = metadata.ToString();

        result.Should().Contain("100 x 200");
        result.Should().Contain("Bgra32");
        result.Should().Contain("1,024");
    }
}
