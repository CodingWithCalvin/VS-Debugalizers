using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Structured.RegexVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Regex")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Structured;

/// <summary>
/// Debug visualizer for regular expressions.
/// </summary>
public class RegexVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Regular Expression";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Regex;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
