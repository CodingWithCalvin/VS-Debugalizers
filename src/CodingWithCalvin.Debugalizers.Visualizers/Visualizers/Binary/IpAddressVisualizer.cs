using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(CodingWithCalvin.Debugalizers.Visualizers.Binary.IpAddressVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(string),
    Description = "Debugalizers: IP Address")]

namespace CodingWithCalvin.Debugalizers.Visualizers.Binary;

/// <summary>
/// Debug visualizer for IP addresses (IPv4/IPv6).
/// </summary>
public class IpAddressVisualizer : BaseVisualizer
{
    /// <inheritdoc />
    protected override string Title => "IP Address";

    /// <inheritdoc />
    protected override VisualizerType Type => VisualizerType.IpAddress;

    /// <inheritdoc />
    protected override IEnumerable<ViewType> SupportedViews =>
        new[] { ViewType.Table, ViewType.Raw };

    /// <inheritdoc />
    protected override ViewType DefaultView => ViewType.Table;
}
