using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.HtmlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: HTML")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for HTML content.
/// </summary>
public class HtmlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "HTML";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Html;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Rendered, ViewType.Tree, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Rendered;
}
