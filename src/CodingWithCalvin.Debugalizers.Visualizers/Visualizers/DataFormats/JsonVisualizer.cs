using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.JsonVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: JSON")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for JSON content.
/// </summary>
public class JsonVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "JSON";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Json;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Tree, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
