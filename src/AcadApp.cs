using System;
using System.Reflection;
using AUS_Cadastral_Tools.Extensions;
using AUS_Cadastral_Tools.Helpers;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AUS_Cadastral_Tools;

/// <summary>
/// Provides access to several "active" objects and helper methods
/// in the AutoCAD runtime environment.
/// </summary>
public sealed class AcadApp : IExtensionApplication
{
    /// <summary>
    /// Gets the <see cref="DocumentManager"/>.
    /// </summary>
    public static DocumentCollection DocumentManager => Application.DocumentManager;

    /// <summary>
    /// Gets the active <see cref="Document"/> object.
    /// </summary>
    public static Document ActiveDocument => DocumentManager.MdiActiveDocument;

    /// <summary>
    /// Gets the active <see cref="Database"/> object.
    /// </summary>
    public static Database ActiveDatabase => ActiveDocument.Database;

    /// <summary>
    /// Gets the active <see cref="Editor"/> object.
    /// </summary>
    public static Editor Editor => ActiveDocument.Editor;

    public void Initialize()
    {
        try
        {
            Editor.WriteMessage($"\n{ResourceHelpers.GetLocalizedString("ACAD_Loading")} {Assembly.GetExecutingAssembly().GetName().Name}");
        }
        catch (InvalidOperationException e)
        {
            Editor.WriteMessage($"\n{ResourceHelpers.GetLocalizedString("ACAD_LoadingError")} {e.Message}");
        }
    }

    public void Terminate()
    {
    }

    /// <summary>
    /// Starts a transaction.
    /// </summary>
    /// <returns>Transaction.</returns>
    public static Transaction StartTransaction()
    {
        return ActiveDocument.TransactionManager.StartTransaction();
    }

    /// <summary>
    /// Starts a locked transaction.
    /// </summary>
    /// <returns>Transaction.</returns>
    public static Transaction StartLockedTransaction()
    {
        return ActiveDocument.TransactionManager.StartLockedTransaction();
    }

    public static void WriteMessage(string message)
    {
        Editor.WriteMessage($"\n{message}");
    }

    public static void WriteErrorMessage(Exception e)
    {
        Editor.WriteMessage($"\nError: {e.ErrorStatus}");
        Editor.WriteMessage($"\nException: {e.Message}");
    }
}
