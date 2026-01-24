using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Structured.CronVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Cron Expression")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Structured;

/// <summary>
/// Debug visualizer for cron schedule expressions.
/// </summary>
public class CronVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Cron Expression";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Cron;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
