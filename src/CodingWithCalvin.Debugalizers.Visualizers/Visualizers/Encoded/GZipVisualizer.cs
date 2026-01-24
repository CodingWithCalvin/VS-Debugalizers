using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.GZipVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: GZip")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for GZip compressed content.
/// </summary>
public class GZipVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "GZip";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.GZip;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Decoded, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Decoded;
}
