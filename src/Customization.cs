using System;
using System.IO;
using System.Reflection;
using AUS_Cadastral_Tools.Helpers;
using Autodesk.AutoCAD.Customization;

namespace AUS_Cadastral_Tools;

public static class Customization
{
    private const string CUIX_FILE_EXTENSION = ".cuix";

    /// <summary>
    /// Loads a partial cui file.
    /// </summary>
    /// <param name="fileName">Path to cui file.</param>
    public static void LoadCuiFile(string fileName)
    {
        // Is Cui file already loaded?
        // If it is, we will unload so it can be reloaded.
        if (IsCuiFileLoaded(fileName))
            UnloadCuiFile(fileName);

        // Load the CUI file.
        var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + fileName;

        if (!File.Exists(filePath))
        {
            AcadApp.Editor.WriteMessage($"\n{ResourceHelpers.GetLocalizedString("ErrorCUIFile")}: {filePath}");
            return;
        }

        AcadApp.Editor.WriteMessage("\n");
        Autodesk.AutoCAD.ApplicationServices.Application.LoadPartialMenu(filePath);
    }

    /// <summary>
    /// Unloads a partial cui file.
    /// </summary>
    /// <param name="fileName">Path to cui file.</param>
    public static void UnloadCuiFile(string fileName)
    {
        // Unload the CUI file.
        var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + fileName;
        Autodesk.AutoCAD.ApplicationServices.Application.UnloadPartialMenu(filePath);
    }

    private static bool IsCuiFileLoaded(string fileName)
    {
        var mainCuiFile = SystemVariables.MENUNAME;
        mainCuiFile += CUIX_FILE_EXTENSION;

        var cs = new CustomizationSection(mainCuiFile);

        foreach (var file in cs.PartialCuiFiles)
            if (string.Equals(Path.GetFileName(file), fileName, StringComparison.InvariantCultureIgnoreCase))
                return true;

        return false;
    }
}
