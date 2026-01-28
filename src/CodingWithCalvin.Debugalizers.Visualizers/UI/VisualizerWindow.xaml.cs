using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodingWithCalvin.Debugalizers.Core;
using CodingWithCalvin.Debugalizers.UI.Views;
using CodingWithCalvin.Debugalizers.Visualizers;
using Microsoft.Win32;

namespace CodingWithCalvin.Debugalizers.UI;

/// <summary>
/// Main visualizer window that displays content with switchable views.
/// </summary>
public partial class VisualizerWindow : Window
{
    private readonly string _content;
    private string _formattedContent;
    private readonly VisualizerType _type;
    private readonly IEnumerable<ViewType> _supportedViews;
    private readonly Dictionary<ViewType, RadioButton> _viewTabs;
    private readonly Dictionary<ViewType, Func<UserControl>> _viewFactories;
    private ViewType _currentView;
    private UserControl _currentViewControl;
    private bool _wordWrap;
    private readonly ViewType _defaultView;

    /// <summary>
    /// Initializes a new instance of the VisualizerWindow.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="content">The content to display.</param>
    /// <param name="type">The visualizer type.</param>
    /// <param name="supportedViews">The supported view types.</param>
    /// <param name="defaultView">The default view to show.</param>
    public VisualizerWindow(string title, string content, VisualizerType type, IEnumerable<ViewType> supportedViews, ViewType defaultView = ViewType.Formatted)
    {
        InitializeComponent();

        Title = $"Debugalizers: {title}";
        _content = content ?? string.Empty;
        _type = type;
        _supportedViews = supportedViews;
        _viewTabs = new Dictionary<ViewType, RadioButton>();
        _currentView = defaultView;
        _defaultView = defaultView;

        // Initialize view factories
        _viewFactories = new Dictionary<ViewType, Func<UserControl>>
        {
            { ViewType.Raw, () => CreateRawView() },
            { ViewType.Formatted, () => CreateFormattedView() },
            { ViewType.SyntaxHighlighted, () => CreateFormattedView() },
            { ViewType.Tree, () => CreateTreeView() },
            { ViewType.Table, () => CreateTableView() },
            { ViewType.Hex, () => CreateHexView() },
            { ViewType.Rendered, () => CreateRenderedView() },
            { ViewType.Image, () => CreateImageView() },
            { ViewType.Claims, () => CreateTableView() },
            { ViewType.Decoded, () => CreateDecodedView() }
        };

        // Show loading overlay and create tabs immediately
        LoadingOverlay.Visibility = Visibility.Visible;
        CreateViewTabs();

        // Keyboard shortcuts
        PreviewKeyDown += Window_PreviewKeyDown;

        // Load content asynchronously
        Loaded += VisualizerWindow_Loaded;
    }

    private async void VisualizerWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Format content on background thread
            _formattedContent = await Task.Run(() => ContentFormatter.Format(_content, _type));

            // Update UI on main thread
            UpdateStatistics();
            SwitchToView(_defaultView);
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Error: {ex.Message}";
        }
        finally
        {
            LoadingOverlay.Visibility = Visibility.Collapsed;
        }
    }

    private void CreateViewTabs()
    {
        ViewTabsPanel.Children.Clear();
        _viewTabs.Clear();

        foreach (var viewType in _supportedViews)
        {
            var tab = new RadioButton
            {
                Content = GetViewTypeName(viewType),
                Tag = viewType,
                GroupName = "ViewTabs",
                Style = (Style)FindResource("ViewTabStyle")
            };

            tab.Checked += ViewTab_Checked;
            ViewTabsPanel.Children.Add(tab);
            _viewTabs[viewType] = tab;

            if (viewType == _currentView)
            {
                tab.IsChecked = true;
            }
        }
    }

    private string GetViewTypeName(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Raw => "Raw",
            ViewType.Formatted => "Formatted",
            ViewType.SyntaxHighlighted => "Syntax",
            ViewType.Tree => "Tree",
            ViewType.Table => "Table",
            ViewType.Hex => "Hex",
            ViewType.Rendered => "Rendered",
            ViewType.Image => "Image",
            ViewType.Claims => "Claims",
            ViewType.Decoded => "Decoded",
            _ => viewType.ToString()
        };
    }

    private void ViewTab_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton tab && tab.Tag is ViewType viewType)
        {
            SwitchToView(viewType);
        }
    }

    private void SwitchToView(ViewType viewType)
    {
        if (!_viewFactories.TryGetValue(viewType, out var factory))
        {
            return;
        }

        _currentView = viewType;
        _currentViewControl = factory();
        ContentArea.Content = _currentViewControl;

        UpdateWordWrap();
        StatusText.Text = GetViewTypeName(viewType);
    }

    private UserControl CreateRawView()
    {
        return new RawTextView(_content);
    }

    private UserControl CreateFormattedView()
    {
        var highlighting = SyntaxHighlighter.GetHighlightingDefinition(_type);
        return new FormattedTextView(_formattedContent, highlighting);
    }

    private UserControl CreateTreeView()
    {
        return new TreeViewControl(_content, _type);
    }

    private UserControl CreateTableView()
    {
        return new TableViewControl(_content, _type);
    }

    private UserControl CreateHexView()
    {
        return new HexViewControl(_content);
    }

    private UserControl CreateRenderedView()
    {
        return new RenderedViewControl(_content, _type);
    }

    private UserControl CreateImageView()
    {
        return new ImageViewControl(_content);
    }

    private UserControl CreateDecodedView()
    {
        var decodedContent = DecodeContent();
        return new RawTextView(decodedContent);
    }

    private string DecodeContent()
    {
        try
        {
            return _type switch
            {
                VisualizerType.Base64 => EncodingDecoder.DecodeBase64(_content),
                VisualizerType.UrlEncoded => EncodingDecoder.DecodeUrl(_content),
                VisualizerType.HtmlEntities => EncodingDecoder.DecodeHtmlEntities(_content),
                VisualizerType.UnicodeEscape => EncodingDecoder.DecodeUnicodeEscape(_content),
                VisualizerType.HexString => EncodingDecoder.DecodeHexString(_content),
                VisualizerType.GZip => DecodeGZipContent(),
                VisualizerType.Deflate => DecodeDeflateContent(),
                VisualizerType.Jwt => DecodeJwtPayload(),
                _ => _content
            };
        }
        catch (Exception ex)
        {
            return $"Error decoding content: {ex.Message}\n\nOriginal content:\n{_content}";
        }
    }

    private string DecodeGZipContent()
    {
        // GZip content is typically Base64 encoded
        var bytes = Convert.FromBase64String(_content);
        using var inputStream = new System.IO.MemoryStream(bytes);
        using var gzipStream = new System.IO.Compression.GZipStream(inputStream, System.IO.Compression.CompressionMode.Decompress);
        using var reader = new System.IO.StreamReader(gzipStream);
        return reader.ReadToEnd();
    }

    private string DecodeDeflateContent()
    {
        // Deflate content is typically Base64 encoded
        var bytes = Convert.FromBase64String(_content);
        using var inputStream = new System.IO.MemoryStream(bytes);
        using var deflateStream = new System.IO.Compression.DeflateStream(inputStream, System.IO.Compression.CompressionMode.Decompress);
        using var reader = new System.IO.StreamReader(deflateStream);
        return reader.ReadToEnd();
    }

    private string DecodeJwtPayload()
    {
        // JWT has 3 parts: header.payload.signature - decode the payload
        var parts = _content.Split('.');
        if (parts.Length >= 2)
        {
            var header = DecodeBase64Url(parts[0]);
            var payload = DecodeBase64Url(parts[1]);
            return $"Header:\n{ContentFormatter.Format(header, VisualizerType.Json)}\n\nPayload:\n{ContentFormatter.Format(payload, VisualizerType.Json)}";
        }
        return _content;
    }

    private static string DecodeBase64Url(string base64Url)
    {
        // Convert Base64Url to standard Base64
        var base64 = base64Url.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        var bytes = Convert.FromBase64String(base64);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    private void UpdateStatistics()
    {
        StatisticsText.Text = ContentFormatter.GetStatistics(_content);

        // Validation status
        var isValid = ValidateContent();
        ValidationText.Text = isValid ? "Valid" : "Invalid";

        var successBrush = (System.Windows.Media.SolidColorBrush)FindResource("SuccessBrush");
        var errorBrush = (System.Windows.Media.SolidColorBrush)FindResource("ErrorBrush");

        ValidationText.Foreground = isValid ? successBrush : errorBrush;
        ValidationIndicator.Fill = isValid ? successBrush : errorBrush;
    }

    private bool ValidateContent()
    {
        try
        {
            switch (_type)
            {
                case VisualizerType.Json:
                    Newtonsoft.Json.Linq.JToken.Parse(_content);
                    return true;
                case VisualizerType.Xml:
                case VisualizerType.Html:
                    var doc = new System.Xml.XmlDocument();
                    doc.LoadXml(_content);
                    return true;
                default:
                    return true; // Assume valid for types without validation
            }
        }
        catch
        {
            return false;
        }
    }

    private void UpdateWordWrap()
    {
        if (_currentViewControl is IWordWrapSupport wordWrapControl)
        {
            wordWrapControl.WordWrap = _wordWrap;
        }
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_content);
        StatusText.Text = "Copied to clipboard";
    }

    private void CopyFormattedButton_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_formattedContent);
        StatusText.Text = "Formatted content copied to clipboard";
    }

    private void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            DefaultExt = SyntaxHighlighter.GetFileExtension(_type),
            FileName = $"export{SyntaxHighlighter.GetFileExtension(_type)}"
        };

        if (dialog.ShowDialog() == true)
        {
            System.IO.File.WriteAllText(dialog.FileName, _formattedContent);
            StatusText.Text = $"Exported to {dialog.FileName}";
        }
    }

    private void WordWrapCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        _wordWrap = WordWrapCheckBox.IsChecked == true;
        UpdateWordWrap();
    }

    private void SearchBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            PerformSearch();
        }
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        PerformSearch();
    }

    private void PerformSearch()
    {
        var searchText = SearchBox.Text;
        if (string.IsNullOrEmpty(searchText))
        {
            return;
        }

        if (_currentViewControl is ISearchSupport searchControl)
        {
            var found = searchControl.FindNext(searchText);
            StatusText.Text = found ? $"Found: {searchText}" : $"Not found: {searchText}";
        }
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            SearchBox.Focus();
            SearchBox.SelectAll();
            e.Handled = true;
        }
        else if (e.Key == Key.Escape)
        {
            Close();
        }
    }
}

/// <summary>
/// Interface for controls that support word wrap.
/// </summary>
public interface IWordWrapSupport
{
    /// <summary>
    /// Gets or sets whether word wrap is enabled.
    /// </summary>
    bool WordWrap { get; set; }
}

/// <summary>
/// Interface for controls that support search.
/// </summary>
public interface ISearchSupport
{
    /// <summary>
    /// Finds the next occurrence of the search text.
    /// </summary>
    /// <param name="searchText">The text to find.</param>
    /// <returns>True if found, false otherwise.</returns>
    bool FindNext(string searchText);
}
