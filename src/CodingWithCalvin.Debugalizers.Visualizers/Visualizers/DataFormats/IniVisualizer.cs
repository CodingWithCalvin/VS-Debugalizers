using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.IniVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: INI")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for INI configuration files.
/// </summary>
public class IniVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "INI";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Ini;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
