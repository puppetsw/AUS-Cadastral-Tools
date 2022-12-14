using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientPlus : TransientDrawableBase
{
    private readonly Point3d _position;
    private readonly int _size;

    public TransientPlus(Point3d position, int size, Color color, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _position = position;
        _size = size;
        Color = color;
        Mode = mode;

        CreateDrawableEntity();
    }

    protected override void CreateDrawableEntity()
    {
        double screenSize = ScreenSize(_size);

        var startPoint1 = new Point3d(_position.X - screenSize * 0.5, _position.Y, 0);
        var endPoint1 = new Point3d(_position.X + screenSize * 0.5, _position.Y, 0);

        var line1 = new Line
        {
            StartPoint = startPoint1,
            EndPoint = endPoint1,
            Color = Color
        };

        var startPoint2 = new Point3d(_position.X, _position.Y - screenSize * 0.5, 0);
        var endPoint2 = new Point3d(_position.X, _position.Y + screenSize * 0.5, 0);

        var line2 = new Line
        {
            StartPoint = startPoint2,
            EndPoint = endPoint2,
            Color = Color
        };

        DrawableEntities.Add(line1);
        DrawableEntities.Add(line2);

        base.CreateDrawableEntity();
    }
}
