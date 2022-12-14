using System;
using System.Collections.Generic;
using AUS_Cadastral_Tools.Extensions;
using AUS_Cadastral_Tools.Graphics;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools;

/// <summary>
/// A helper class to display and manage TransientGraphics
/// within AutoCAD.
/// </summary>
/// <remarks>
/// Always wrap in a try/catch/finally block or use
/// a using statement to dispose of the graphics correctly.
/// </remarks>
/// <example>
/// <code>
/// var graphics = new TransientGraphics();
/// try
/// {
///   graphics.DrawLine(point1, point2);
///   graphics.Undo(); // If you need to undo last drawn.
/// }
/// finally
/// {
///   graphics.Dispose();
/// }
/// </code>
/// </example>
public sealed class TransientGraphics : IDisposable
{
    private readonly List<TransientDrawableBase> _drawables = new();

    private const string DASHED_LINE_TYPE = "DASHED";

    public Color Color { get; } = Color.FromColorIndex(ColorMethod.ByPen, Settings.TransientColorIndex);

    public void DrawLine(Point2d point1, Point2d point2, TransientDrawingMode mode = TransientDrawingMode.Main) => DrawLine(point1.ToPoint3d(), point2.ToPoint3d(), mode);

    public void DrawLine(Line line, TransientDrawingMode mode = TransientDrawingMode.Main) => DrawLine(line.StartPoint, line.EndPoint, mode);

    public void DrawLine(Point3d point1, Point3d point2, TransientDrawingMode mode = TransientDrawingMode.Main, bool useDashedLine = true)
    {
        var line = new TransientLine(point1, point2, Color, mode);
        line.Mode = mode;

        if (useDashedLine)
            if (LineTypeUtils.LoadLineType(DASHED_LINE_TYPE))
                line.LineType = DASHED_LINE_TYPE;

        _drawables.Add(line);
    }

    public void DrawLine(Curve curve, TransientDrawingMode mode = TransientDrawingMode.Main, bool useDashedLine = false)
    {
        var line = new TransientLine(curve, Color, mode);
        line.Mode = mode;

        if (useDashedLine)
            if (LineTypeUtils.LoadLineType(DASHED_LINE_TYPE))
                line.LineType = DASHED_LINE_TYPE;

        _drawables.Add(line);
    }

    public void DrawCircle(Point3d position, double circleSize = 0.5)
    {
        var circle = new TransientCircle(position, circleSize, Color);
        _drawables.Add(circle);
    }

    public void Redraw()
    {
        foreach (TransientDrawableBase transientDrawable in _drawables)
        {
            transientDrawable.Redraw();
        }
    }


    public void DrawX(Point3d position, int size)
    {
        var x = new TransientCross(position, size, Color);
        _drawables.Add(x);
    }

    public void DrawDot(Point3d point, int size)
    {
        var dot = new TransientDot(point, size, Color);
        _drawables.Add(dot);
    }

    public void DrawPlus(Point3d position, int size)
    {
        var plus = new TransientPlus(position, size, Color);
        _drawables.Add(plus);
    }

    public void DrawArrow(Point3d position, Angle arrowDirection, int size, bool fill = true)
    {
        var arrow = new TransientArrow(position, arrowDirection, size, Color, fill);
        _drawables.Add(arrow);
    }

    public void DrawText(Point3d position, string text, double textSize, Angle angle, double offsetAmount = 0.5, bool planReadability = true)
    {
        var ttext = new TransientText(position, Color, textSize, text, angle, offsetAmount);
        _drawables.Add(ttext);
    }

    public void ClearGraphics()
    {
        foreach (TransientDrawableBase drawable in _drawables)
            drawable.Clear();

        AcadApp.Editor.UpdateScreen();
    }
    public void Undo()
    {
        if (_drawables.Count <= 0)
            return;

        var lastIndex = _drawables.Count - 1;

        _drawables[lastIndex].Clear();
        _drawables.RemoveAt(lastIndex);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        ClearGraphics();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    /// Draws a chainage between two <see cref="Point3d"/> entities.
    /// </summary>
    /// <param name="firstPoint"></param>
    /// <param name="secondPoint"></param>
    /// <param name="interval"></param>
    /// <param name="textSize"></param>
    public void DrawChainage(Point3d firstPoint, Point3d secondPoint, int interval = 10, double textSize = 1.0)
    {
        var chainage = new TransientChainage(firstPoint, secondPoint, Color, interval, textSize);
        _drawables.Add(chainage);
    }
}
