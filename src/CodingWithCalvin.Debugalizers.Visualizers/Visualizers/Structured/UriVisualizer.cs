using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Structured.UriVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: URI/URL")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Structured;

/// <summary>
/// Debug visualizer for URIs and URLs.
/// </summary>
public class UriVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "URI/URL";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Uri;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
