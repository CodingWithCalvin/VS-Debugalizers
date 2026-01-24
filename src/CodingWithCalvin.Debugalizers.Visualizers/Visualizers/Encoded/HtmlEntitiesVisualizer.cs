using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.HtmlEntitiesVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: HTML Entities")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for HTML entities encoded content.
/// </summary>
public class HtmlEntitiesVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "HTML Entities";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.HtmlEntities;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Decoded, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Decoded;
}
