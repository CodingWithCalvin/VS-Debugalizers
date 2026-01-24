namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Defines the available view types for visualizers.
/// </summary>
public enum ViewType
{
    /// <summary>
    /// Unmodified content display.
    /// </summary>
    Raw,

    /// <summary>
    /// Pretty-printed with indentation.
    /// </summary>
    Formatted,

    /// <summary>
    /// Color-coded by language syntax.
    /// </summary>
    SyntaxHighlighted,

    /// <summary>
    /// Expandable/collapsible hierarchy.
    /// </summary>
    Tree,

    /// <summary>
    /// Grid for key-value or tabular data.
    /// </summary>
    Table,

    /// <summary>
    /// Byte-level view with ASCII sidebar.
    /// </summary>
    Hex,

    /// <summary>
    /// HTML/Markdown preview rendering.
    /// </summary>
    Rendered,

    /// <summary>
    /// Image display with zoom/pan/metadata.
    /// </summary>
    Image,

    /// <summary>
    /// Claims and token details table.
    /// </summary>
    Claims,

    /// <summary>
    /// Decoded content from encoded string.
    /// </summary>
    Decoded
}
