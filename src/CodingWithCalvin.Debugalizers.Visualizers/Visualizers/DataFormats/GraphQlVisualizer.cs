using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.GraphQlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: GraphQL")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for GraphQL queries.
/// </summary>
public class GraphQlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "GraphQL";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.GraphQl;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.SyntaxHighlighted, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
