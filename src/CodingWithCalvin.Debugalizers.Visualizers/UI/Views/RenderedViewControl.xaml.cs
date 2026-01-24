using System.Windows.Controls;
using CodingWithCalvin.Debugalizers.Core;
using Markdig;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that renders HTML or Markdown content.
/// </summary>
public partial class RenderedViewControl : UserControl
{
    private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    /// <summary>
    /// Initializes a new instance of the RenderedViewControl.
    /// </summary>
    /// <param name="content">The content to render.</param>
    /// <param name="type">The visualizer type.</param>
    public RenderedViewControl(string content, VisualizerType type)
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        string html;
        if (type == VisualizerType.Markdown)
        {
            html = ConvertMarkdownToHtml(content);
        }
        else
        {
            html = content;
        }

        // Wrap in basic HTML structure with styling
        var fullHtml = WrapInHtmlDocument(html);
        WebContent.NavigateToString(fullHtml);
    }

    private string ConvertMarkdownToHtml(string markdown)
    {
        return Markdown.ToHtml(markdown, MarkdownPipeline);
    }

    private string WrapInHtmlDocument(string bodyContent)
    {
        return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
            font-size: 14px;
            line-height: 1.6;
            color: #333;
            padding: 16px;
            max-width: 100%;
            overflow-wrap: break-word;
        }}
        pre {{
            background: #f4f4f4;
            padding: 12px;
            border-radius: 4px;
            overflow-x: auto;
            font-family: Consolas, 'Courier New', monospace;
            font-size: 13px;
        }}
        code {{
            background: #f4f4f4;
            padding: 2px 6px;
            border-radius: 3px;
            font-family: Consolas, 'Courier New', monospace;
            font-size: 13px;
        }}
        pre code {{
            background: none;
            padding: 0;
        }}
        table {{
            border-collapse: collapse;
            width: 100%;
            margin: 16px 0;
        }}
        th, td {{
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }}
        th {{
            background: #f4f4f4;
        }}
        blockquote {{
            border-left: 4px solid #ddd;
            margin: 16px 0;
            padding: 0 16px;
            color: #666;
        }}
        img {{
            max-width: 100%;
            height: auto;
        }}
        a {{
            color: #0066cc;
        }}
        h1, h2, h3, h4, h5, h6 {{
            margin-top: 24px;
            margin-bottom: 16px;
        }}
    </style>
</head>
<body>
{bodyContent}
</body>
</html>";
    }
}
