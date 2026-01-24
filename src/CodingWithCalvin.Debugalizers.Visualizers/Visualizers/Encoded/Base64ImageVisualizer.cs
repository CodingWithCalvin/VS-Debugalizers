using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Encoded.Base64ImageVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Base64 Image")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Encoded;

/// <summary>
/// Debug visualizer for Base64 encoded images.
/// </summary>
public class Base64ImageVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "Base64 Image";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Base64Image;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Image, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Image;
}
