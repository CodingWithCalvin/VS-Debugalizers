using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodingWithCalvin.Debugalizers.Core;
using CodingWithCalvin.Debugalizers.UI;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace CodingWithCalvin.Debugalizers.Visualizers;

/// <summary>
/// Abstract base class for all debug visualizers.
/// </summary>
public abstract class BaseVisualizer : DialogDebuggerVisualizer
{
    /// <summary>
    /// Gets the title displayed in the visualizer window.
    /// </summary>
    protected abstract string Title { get; }

    /// <summary>
    /// Gets the type of visualizer (determines parsing/formatting behavior).
    /// </summary>
    protected abstract VisualizerType Type { get; }

    /// <summary>
    /// Gets the supported view types for this visualizer.
    /// </summary>
    protected abstract IEnumerable<ViewType> SupportedViews { get; }

    /// <summary>
    /// Gets the default view type to show when the visualizer opens.
    /// </summary>
    protected virtual ViewType DefaultView => ViewType.Formatted;

    /// <summary>
    /// Shows the visualizer window with the debugged value.
    /// </summary>
    /// <param name="windowService">The dialog visualizer service.</param>
    /// <param name="objectProvider">Provides access to the object being visualized.</param>
    protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
    {
        if (objectProvider == null)
        {
            throw new ArgumentNullException(nameof(objectProvider));
        }

        try
        {
            string content;
            if (objectProvider is IVisualizerObjectProvider3 provider3)
            {
                content = provider3.GetObject<string>() ?? string.Empty;
            }
            else
            {
                // Fallback for older VS versions - use obsolete method
                content = (objectProvider.GetObject() as string) ?? string.Empty;
            }

            var window = new VisualizerWindow(Title, content, Type, SupportedViews, DefaultView);
            window.ShowDialog();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in {GetType().Name}: {ex.Message}");
            throw;
        }
    }
}
