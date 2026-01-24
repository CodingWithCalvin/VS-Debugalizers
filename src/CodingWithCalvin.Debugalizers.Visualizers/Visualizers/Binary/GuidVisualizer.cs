using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Binary.GuidVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: GUID/UUID")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Binary;

/// <summary>
/// Debug visualizer for GUIDs/UUIDs.
/// </summary>
public class GuidVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "GUID/UUID";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Guid;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
