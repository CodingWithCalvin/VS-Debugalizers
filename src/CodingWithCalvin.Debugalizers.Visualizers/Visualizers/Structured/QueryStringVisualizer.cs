using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Structured.QueryStringVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Query String")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Structured;

/// <summary>
/// Debug visualizer for URL query strings.
/// </summary>
public class QueryStringVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Query String";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.QueryString;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
