using AUS_Cadastral_Tools.Extensions;
using AUS_Cadastral_Tools.Helpers;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientArrow : TransientDrawableBase
{
    private readonly Point3d _position;
    private readonly Angle _arrowDirection;
    private readonly int _size;
    private readonly bool _fill;

    public TransientArrow(Point3d position, Angle arrowDirection, int size, Color color, bool fill = true, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _position = position;
        _arrowDirection = arrowDirection;
        _size = size;
        _fill = fill;
        Color = color;
        Mode  = mode;

        CreateDrawableEntity();
    }

    protected override void CreateDrawableEntity()
    {
        var screenSize = ScreenSize(_size);

        var angle = _arrowDirection.Flip();

        var endPoint1 = PointHelpers.AngleAndDistanceToPoint(angle, screenSize * 1.5, _position.ToPoint());
        var endPoint2 = PointHelpers.AngleAndDistanceToPoint(angle + 90, screenSize * 0.5, endPoint1);
        var endPoint3 = PointHelpers.AngleAndDistanceToPoint(angle - 90, screenSize * 0.5, endPoint1);

        var polyline = new Polyline { Color = Color, Closed = true };
        polyline.AddVertexAt(0, _position.ToPoint2d(), 0, 0, 0);
        polyline.AddVertexAt(1, endPoint2.ToPoint2d(), 0, 0, 0);
        polyline.AddVertexAt(2, endPoint3.ToPoint2d(), 0, 0, 0);

        DrawableEntities.Add(polyline);

        if (_fill)
        {
            var solid = new Solid(_position, endPoint2.ToPoint3d(), endPoint3.ToPoint3d()) { Color = Color };
            DrawableEntities.Add(solid);
        }

        base.CreateDrawableEntity();
    }
}
