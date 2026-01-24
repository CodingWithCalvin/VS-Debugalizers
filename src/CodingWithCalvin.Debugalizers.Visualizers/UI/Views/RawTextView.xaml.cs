using System.Windows;
using System.Windows.Controls;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays raw unformatted text.
/// </summary>
public partial class RawTextView : UserControl, IWordWrapSupport, ISearchSupport
{
    private int _lastSearchIndex;
    private string _lastSearchText;

    /// <summary>
    /// Initializes a new instance of the RawTextView.
    /// </summary>
    /// <param name="content">The content to display.</param>
    public RawTextView(string content)
    {
        InitializeComponent();
        TextContent.Text = content ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets whether word wrap is enabled.
    /// </summary>
    public bool WordWrap
    {
        get => TextContent.TextWrapping == TextWrapping.Wrap;
        set => TextContent.TextWrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }

    /// <summary>
    /// Finds the next occurrence of the search text.
    /// </summary>
    /// <param name="searchText">The text to find.</param>
    /// <returns>True if found, false otherwise.</returns>
    public bool FindNext(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            return false;
        }

        // Reset search if text changed
        if (_lastSearchText != searchText)
        {
            _lastSearchIndex = 0;
            _lastSearchText = searchText;
        }

        var text = TextContent.Text;
        var index = text.IndexOf(searchText, _lastSearchIndex, System.StringComparison.OrdinalIgnoreCase);

        if (index >= 0)
        {
            TextContent.Focus();
            TextContent.Select(index, searchText.Length);
            TextContent.ScrollToLine(TextContent.GetLineIndexFromCharacterIndex(index));
            _lastSearchIndex = index + searchText.Length;
            return true;
        }

        // Wrap around
        if (_lastSearchIndex > 0)
        {
            _lastSearchIndex = 0;
            return FindNext(searchText);
        }

        return false;
    }
}
