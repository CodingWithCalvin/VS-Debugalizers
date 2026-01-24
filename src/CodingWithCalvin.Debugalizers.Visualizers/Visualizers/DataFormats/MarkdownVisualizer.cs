using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.MarkdownVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Markdown")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for Markdown content.
/// </summary>
public class MarkdownVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Markdown";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Markdown;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Rendered, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Rendered;
}
