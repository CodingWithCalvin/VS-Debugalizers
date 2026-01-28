using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace CodingWithCalvin.Debugalizers;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[Guid(PackageGuidString)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
public sealed class DebugalizersPackage : AsyncPackage
{
    public const string PackageGuidString = "5e8f7c9a-3b2d-4a1e-9c6f-8d5b0e4a3f2c";

    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

        try
        {
            DeployVisualizersToDocumentsFolder();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Debugalizers: Failed to deploy visualizers: {ex}");
        }
    }

    private void DeployVisualizersToDocumentsFolder()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        // Get the source folder - visualizers are in a Visualizers subfolder of the extension
        var extensionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var sourceFolder = Path.Combine(extensionFolder, "Visualizers");

        if (!Directory.Exists(sourceFolder))
        {
            System.Diagnostics.Debug.WriteLine($"Debugalizers: Source folder not found: {sourceFolder}");
            return;
        }

        // Get the destination folder using IVsShell
        var shell = GetService(typeof(SVsShell)) as IVsShell;
        if (shell == null)
        {
            System.Diagnostics.Debug.WriteLine("Debugalizers: Could not get IVsShell service");
            return;
        }

        shell.GetProperty((int)__VSSPROPID2.VSSPROPID_VisualStudioDir, out var documentsFolderObj);
        if (documentsFolderObj == null)
        {
            System.Diagnostics.Debug.WriteLine("Debugalizers: Could not get VisualStudioDir property");
            return;
        }

        var visualizersFolder = Path.Combine(documentsFolderObj.ToString(), "Visualizers");

        // Create the Visualizers folder if it doesn't exist
        if (!Directory.Exists(visualizersFolder))
        {
            Directory.CreateDirectory(visualizersFolder);
        }

        // Copy all DLL files from the source Visualizers folder
        foreach (var sourceFile in Directory.GetFiles(sourceFolder, "*.dll"))
        {
            var fileName = Path.GetFileName(sourceFile);
            var destFile = Path.Combine(visualizersFolder, fileName);
            CopyFileIfNewerVersion(sourceFile, destFile);
        }

        System.Diagnostics.Debug.WriteLine($"Debugalizers: Visualizers deployed to {visualizersFolder}");
    }

    private static void CopyFileIfNewerVersion(string sourceFile, string destFile)
    {
        try
        {
            if (!File.Exists(destFile))
            {
                File.Copy(sourceFile, destFile, overwrite: false);
                return;
            }

            // Compare file timestamps
            var sourceInfo = new FileInfo(sourceFile);
            var destInfo = new FileInfo(destFile);

            if (sourceInfo.LastWriteTimeUtc > destInfo.LastWriteTimeUtc)
            {
                File.Copy(sourceFile, destFile, overwrite: true);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Debugalizers: Failed to copy {Path.GetFileName(sourceFile)}: {ex.Message}");
        }
    }
}
