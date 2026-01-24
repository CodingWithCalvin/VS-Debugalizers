using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Structured.ConnectionStringVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Connection String")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Structured;

/// <summary>
/// Debug visualizer for database connection strings.
/// </summary>
public class ConnectionStringVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Connection String";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.ConnectionString;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
