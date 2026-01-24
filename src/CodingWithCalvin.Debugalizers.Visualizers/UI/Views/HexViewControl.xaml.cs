using System;
using CodingWithCalvin.Debugalizers.Core;
using System.Text;
using System.Windows.Controls;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays content as a hex dump with ASCII sidebar.
/// </summary>
public partial class HexViewControl : UserControl
{
    private const int BytesPerLine = 16;

    /// <summary>
    /// Initializes a new instance of the HexViewControl.
    /// </summary>
    /// <param name="content">The content to display as hex.</param>
    public HexViewControl(string content)
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        var bytes = Encoding.UTF8.GetBytes(content);
        GenerateHexDump(bytes);
    }

    /// <summary>
    /// Initializes a new instance of the HexViewControl with raw bytes.
    /// </summary>
    /// <param name="bytes">The bytes to display.</param>
    public HexViewControl(byte[] bytes)
    {
        InitializeComponent();

        if (bytes == null || bytes.Length == 0)
        {
            return;
        }

        GenerateHexDump(bytes);
    }

    private void GenerateHexDump(byte[] bytes)
    {
        var offsetBuilder = new StringBuilder();
        var hexBuilder = new StringBuilder();
        var asciiBuilder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i += BytesPerLine)
        {
            // Offset
            offsetBuilder.AppendLine($"{i:X8}");

            // Hex values
            var hexLine = new StringBuilder();
            var asciiLine = new StringBuilder();

            for (int j = 0; j < BytesPerLine; j++)
            {
                if (i + j < bytes.Length)
                {
                    var b = bytes[i + j];
                    hexLine.Append($"{b:X2} ");

                    // ASCII representation (printable characters only)
                    asciiLine.Append(b >= 32 && b < 127 ? (char)b : '.');
                }
                else
                {
                    hexLine.Append("   ");
                    asciiLine.Append(' ');
                }

                // Add extra space after 8 bytes for readability
                if (j == 7)
                {
                    hexLine.Append(' ');
                }
            }

            hexBuilder.AppendLine(hexLine.ToString().TrimEnd());
            asciiBuilder.AppendLine(asciiLine.ToString());
        }

        OffsetColumn.Text = offsetBuilder.ToString().TrimEnd();
        HexColumn.Text = hexBuilder.ToString().TrimEnd();
        AsciiColumn.Text = asciiBuilder.ToString().TrimEnd();
    }

    private void HexColumn_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        // Synchronize scroll positions
        var scrollViewer = GetScrollViewer(HexColumn);
        if (scrollViewer != null)
        {
            var offsetScrollViewer = GetScrollViewer(OffsetColumn);
            var asciiScrollViewer = GetScrollViewer(AsciiColumn);

            offsetScrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
            asciiScrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
        }
    }

    private ScrollViewer GetScrollViewer(TextBox textBox)
    {
        if (textBox.Template.FindName("PART_ContentHost", textBox) is ScrollViewer scrollViewer)
        {
            return scrollViewer;
        }
        return null;
    }
}
