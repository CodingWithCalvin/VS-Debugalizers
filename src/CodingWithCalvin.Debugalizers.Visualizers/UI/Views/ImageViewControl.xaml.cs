using System;
using System.Windows;
using System.Windows.Controls;
using CodingWithCalvin.Debugalizers.Core;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays images decoded from Base64 or data URIs.
/// </summary>
public partial class ImageViewControl : UserControl
{
    private int _originalWidth;
    private int _originalHeight;

    /// <summary>
    /// Initializes a new instance of the ImageViewControl.
    /// </summary>
    /// <param name="content">The Base64 or data URI image content.</param>
    public ImageViewControl(string content)
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(content))
        {
            ShowError("No image content provided");
            return;
        }

        try
        {
            var image = ImageDecoder.Decode(content);
            if (image == null)
            {
                ShowError("Failed to decode image. Content may not be a valid Base64-encoded image.");
                return;
            }

            _originalWidth = image.PixelWidth;
            _originalHeight = image.PixelHeight;

            ImageContent.Source = image;
            CheckerboardBackground.Width = _originalWidth;
            CheckerboardBackground.Height = _originalHeight;

            // Display metadata
            var metadata = ImageDecoder.GetMetadata(content);
            if (metadata != null)
            {
                MetadataText.Text = metadata.ToString();
                if (!string.IsNullOrEmpty(metadata.MimeType))
                {
                    MetadataText.Text += $" | {metadata.MimeType}";
                }
            }
        }
        catch (Exception ex)
        {
            ShowError($"Error loading image: {ex.Message}");
        }
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.Visibility = Visibility.Visible;
        ImageContent.Visibility = Visibility.Collapsed;
        CheckerboardBackground.Visibility = Visibility.Collapsed;
    }

    private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (ImageScale == null)
        {
            return;
        }

        var zoom = e.NewValue / 100.0;
        ImageScale.ScaleX = zoom;
        ImageScale.ScaleY = zoom;

        CheckerboardBackground.Width = _originalWidth * zoom;
        CheckerboardBackground.Height = _originalHeight * zoom;

        ZoomText.Text = $"{e.NewValue:F0}%";
    }

    private void FitButton_Click(object sender, RoutedEventArgs e)
    {
        if (_originalWidth == 0 || _originalHeight == 0)
        {
            return;
        }

        // Calculate zoom to fit
        var container = (ScrollViewer)ImageContent.Parent.GetParentOfType<ScrollViewer>();
        if (container == null)
        {
            return;
        }

        var availableWidth = container.ActualWidth - 40; // Account for padding
        var availableHeight = container.ActualHeight - 40;

        var zoomX = availableWidth / _originalWidth;
        var zoomY = availableHeight / _originalHeight;
        var zoom = Math.Min(zoomX, zoomY) * 100;

        ZoomSlider.Value = Math.Max(10, Math.Min(500, zoom));
    }

    private void ActualSizeButton_Click(object sender, RoutedEventArgs e)
    {
        ZoomSlider.Value = 100;
    }
}

/// <summary>
/// Extension methods for visual tree traversal.
/// </summary>
internal static class VisualTreeExtensions
{
    /// <summary>
    /// Gets the first parent of the specified type.
    /// </summary>
    public static T GetParentOfType<T>(this DependencyObject element) where T : DependencyObject
    {
        var parent = System.Windows.Media.VisualTreeHelper.GetParent(element);
        while (parent != null)
        {
            if (parent is T result)
            {
                return result;
            }
            parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
        }
        return null;
    }
}
