using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Security.CertificateVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: Certificate")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Security;

/// <summary>
/// Debug visualizer for X.509 certificates (PEM/DER format).
/// </summary>
public class CertificateVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "X.509 Certificate";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.Certificate;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
