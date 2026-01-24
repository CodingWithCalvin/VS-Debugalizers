using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Binary.HexDumpVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Hex Dump")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Binary;

/// <summary>
/// Debug visualizer for hex dump display.
/// </summary>
public class HexDumpVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Hex Dump";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.HexDump;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Hex, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Hex;
}
