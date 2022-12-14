using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientLine : TransientDrawableBase
{
    private readonly Point3d _startPoint;
    private readonly Point3d _endPoint;

    public string? LineType { get; set; }

    public TransientLine(Point3d startPoint, Point3d endPoint, Color color, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;

        Color = color;

        CreateDrawableEntity();
    }

    public TransientLine(Curve line, Color color, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _startPoint = line.StartPoint;
        _endPoint = line.EndPoint;

        Color = color;
        Mode = mode;

        CreateDrawableEntity();
    }

    protected override void CreateDrawableEntity()
    {
        var line = new Line(_startPoint, _endPoint) { Color = Color };

        if (LineType != null)
            line.Linetype = LineType;

        DrawableEntities.Add(line);

        base.CreateDrawableEntity();
    }
}
