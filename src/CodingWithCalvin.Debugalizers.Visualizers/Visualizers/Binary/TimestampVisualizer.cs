using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Binary.TimestampVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Timestamp")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Binary;

/// <summary>
/// Debug visualizer for Unix timestamps.
/// </summary>
public class TimestampVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Timestamp";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Timestamp;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
