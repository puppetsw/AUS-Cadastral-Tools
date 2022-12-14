using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

namespace AUS_Cadastral_Tools.Graphics;

public class TransientDot : TransientDrawableBase
{
    private readonly Point3d _position;
    private readonly int _size;

    public TransientDot(Point3d position, int size, Color color, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _position = position;
        _size = size;
        Color = color;
        Mode = mode;
    }

    protected override void CreateDrawableEntity()
    {
        double screenSize = ScreenSize(_size);
        double circleSize = screenSize * 0.5;

        var polyline = new Polyline { Color = Color, Elevation = _position.Z, Closed = true };

        polyline.AddVertexAt(0, new Point2d(_position.X - screenSize * 0.25, _position.Y), 1.0, circleSize, circleSize);
        polyline.AddVertexAt(1, new Point2d(_position.X + screenSize * 0.25, _position.Y), 1.0, circleSize, circleSize);

        var vector3d = new Vector3d(0, 0, 1);
        var circle = new Circle(_position, vector3d, circleSize) { Color = Color };

        DrawableEntities.Add(polyline);
        DrawableEntities.Add(circle);

        base.CreateDrawableEntity();
    }
}
