using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.SqlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: SQL")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for SQL queries.
/// </summary>
public class SqlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "SQL";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Sql;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.SyntaxHighlighted, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
