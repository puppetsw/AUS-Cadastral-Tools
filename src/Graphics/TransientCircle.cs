using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientCircle : TransientDrawableBase
{
    private readonly Point3d _position;
    private readonly double _size;

    public TransientCircle(Point3d position, double size, Color color, TransientDrawingMode mode = TransientDrawingMode.Main)
    {
        _position = position;
        _size = size;
        Color = color;
        Mode = mode;

        CreateDrawableEntity();
    }

    protected override void CreateDrawableEntity()
    {
        var circle = new Circle(_position, Vector3d.ZAxis, _size) { Color = Color };

        if (LineTypeUtils.LoadLineType(DASHED_LINE_TYPE))
            circle.Linetype = DASHED_LINE_TYPE;

        DrawableEntities.Add(circle);

        base.CreateDrawableEntity();
    }
}
