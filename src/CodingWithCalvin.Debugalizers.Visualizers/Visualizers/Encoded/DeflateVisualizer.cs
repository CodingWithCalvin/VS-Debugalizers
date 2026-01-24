using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.DeflateVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Deflate")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for Deflate compressed content.
/// </summary>
public class DeflateVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Deflate";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Deflate;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Decoded, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Decoded;
}
