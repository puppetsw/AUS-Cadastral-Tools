using System;
using System.Diagnostics;
using AUS_Cadastral_Tools;
using AUS_Cadastral_Tools.Commands;
using Autodesk.AutoCAD.Runtime;
using Exception = System.Exception;

[assembly: CommandClass(typeof(AcadCommands))]
namespace AUS_Cadastral_Tools;

public static class AcadCommands
{
    [CommandMethod("WMS", "_TRAVERSE", CommandFlags.Modal)]
    public static void Traverse()
    {
        ExecuteCommand<TraverseCommand>();
    }

    [CommandMethod("WMS", "PTPRODOFLINEDIST", CommandFlags.Modal)]
    public static void PtProdDist()
    {
        PointUtils.Create_At_Production_Of_Line_And_Distance(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTOFFSETLINE", CommandFlags.Modal)]
    public static void PtOffsetLn()
    {
        PointUtils.Create_At_Offset_Two_Lines(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTANGLEDISTANCE", CommandFlags.Modal)]
    public static void PtBrgDist()
    {
        PointUtils.Create_At_Angle_And_Distance(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTINTANGLES", CommandFlags.Modal)]
    public static void PtIntBrg()
    {
        PointUtils.Create_At_Intersection_Two_Bearings(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTINTDISTANCES", CommandFlags.Modal)]
    public static void PtIntDist()
    {
        PointUtils.Create_At_Intersection_Two_Distances(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTINTANGLEDISTANCE", CommandFlags.Modal)]
    public static void PtIntBearingDist()
    {
        PointUtils.Create_At_Intersection_Of_Angle_And_Distance(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTBETWEENPTS", CommandFlags.Modal)]
    public static void PtBetweenPts()
    {
        PointUtils.Create_Between_Points(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTOFFSETBETWEENPTS", CommandFlags.Modal)]
    public static void PtOffsetBetweenPts()
    {
        PointUtils.Create_At_Offset_Between_Points(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTINTFOURPTS", CommandFlags.Modal)]
    public static void PtIntFour()
    {
        PointUtils.Create_At_Intersection_Of_Four_Points(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "PTDISTANCEONLINE", CommandFlags.Modal)]
    public static void PtLine()
    {
        PointUtils.Create_At_Distance_Between_Points(PointUtils.CreatePoint);
    }

    [CommandMethod("WMS", "INVERSECL", CommandFlags.Modal)]
    public static void Inverse()
    {
        PointUtils.Inverse_Pick();
    }

    [CommandMethod("WMS", "INVERSEOS", CommandFlags.Modal)]
    public static void InverseDisplay()
    {
        PointUtils.Inverse_Pick_Display();
    }

    [CommandMethod("WMS", "INVERSECHOS", CommandFlags.Modal)]
    public static void InverseChainageOffset()
    {
        PointUtils.Inverse_Pick_Perpendicular();
    }

    private static void ExecuteCommand<T>() where T : IAcadCommand
{
    try
    {
        var cmd = Activator.CreateInstance<T>();
        cmd.Execute();
    }
    catch (Exception ex)
    {
        Debug.WriteLine(ex.Message);
    }
}
}
