using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.UnicodeEscapeVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Unicode Escape")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for Unicode escape sequences.
/// </summary>
public class UnicodeEscapeVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Unicode Escape";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.UnicodeEscape;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Decoded, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Decoded;
}
