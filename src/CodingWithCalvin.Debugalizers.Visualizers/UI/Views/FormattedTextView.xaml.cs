using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays formatted text with optional syntax highlighting.
/// </summary>
public partial class FormattedTextView : UserControl, IWordWrapSupport, ISearchSupport
{
    /// <summary>
    /// Initializes a new instance of the FormattedTextView.
    /// </summary>
    /// <param name="content">The content to display.</param>
    /// <param name="highlighting">Optional syntax highlighting definition.</param>
    public FormattedTextView(string content, IHighlightingDefinition highlighting = null)
    {
        InitializeComponent();
        TextEditor.Text = content ?? string.Empty;

        if (highlighting != null)
        {
            TextEditor.SyntaxHighlighting = highlighting;
        }
    }

    /// <summary>
    /// Gets or sets whether word wrap is enabled.
    /// </summary>
    public bool WordWrap
    {
        get => TextEditor.WordWrap;
        set => TextEditor.WordWrap = value;
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

        var text = TextEditor.Text;
        var startIndex = TextEditor.CaretOffset;

        var index = text.IndexOf(searchText, startIndex, System.StringComparison.OrdinalIgnoreCase);

        if (index >= 0)
        {
            TextEditor.Select(index, searchText.Length);
            TextEditor.ScrollToLine(TextEditor.Document.GetLineByOffset(index).LineNumber);
            TextEditor.CaretOffset = index + searchText.Length;
            return true;
        }

        // Wrap around
        if (startIndex > 0)
        {
            TextEditor.CaretOffset = 0;
            return FindNext(searchText);
        }

        return false;
    }
}
