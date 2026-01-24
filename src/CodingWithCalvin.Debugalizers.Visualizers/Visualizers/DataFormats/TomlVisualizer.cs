using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.TomlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: TOML")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for TOML content.
/// </summary>
public class TomlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "TOML";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Toml;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Tree, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
