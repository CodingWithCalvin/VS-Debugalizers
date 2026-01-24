using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.DataFormats.XmlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: XML")]

namespace CodingWithCalvin.Debugalizers.Visualizers.DataFormats;

/// <summary>
/// Debug visualizer for XML content.
/// </summary>
public class XmlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "XML";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Xml;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Formatted, ViewType.Tree, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Formatted;
}
