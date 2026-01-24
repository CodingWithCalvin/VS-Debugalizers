using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Security.SamlVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: SAML")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Security;

/// <summary>
/// Debug visualizer for SAML assertions.
/// </summary>
public class SamlVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "SAML";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Saml;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Tree, ViewType.Claims, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Tree;
}
