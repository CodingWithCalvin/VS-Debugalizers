using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.UrlEncodedVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: URL Encoded")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for URL encoded content.
/// </summary>
public class UrlEncodedVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "URL Encoded";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.UrlEncoded;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Decoded, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Decoded;
}
