using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools.Graphics;

public abstract class TransientDrawableBase : IDisposable
{
    protected const string DASHED_LINE_TYPE = "DASHED";

    protected readonly List<Drawable> DrawableEntities = new();

    protected double EntitySize { get; set; }

    protected double EntitySizeOnScreen => ScreenSize(EntitySize);

    protected double TextSize { get; set; }

    protected double TextSizeOnScreen => ScreenTextSize(TextSize);

    public TransientDrawingMode Mode { get; set; } = TransientDrawingMode.Main;

    protected Color? Color { get; set; }

    protected virtual void CreateDrawableEntity()
    {
        Draw();
    }

    public void Redraw()
    {
        Clear();
        CreateDrawableEntity();
    }

    public void Clear()
    {
        if (DrawableEntities.Count < 0)
            return;

        var tm = TransientManager.CurrentTransientManager;
        var intCol = new IntegerCollection();

        foreach (Drawable drawable in DrawableEntities)
        {
            tm.EraseTransient(drawable, intCol);
            drawable.Dispose();
        }

        DrawableEntities.Clear();
    }

    private void Draw()
    {
        var intCol = new IntegerCollection();

        foreach (Drawable drawable in DrawableEntities)
            TransientManager.CurrentTransientManager.AddTransient(drawable, Mode, 0, intCol);
    }

    protected static double ScreenSize(double numPix)
    {
        var viewSize = SystemVariables.VIEWSIZE;
        var screenSize = SystemVariables.SCREENSIZE;
        return viewSize / screenSize.Y * numPix;
    }

    protected static double ScreenTextSize(double textSize)
    {
        var viewSize = SystemVariables.VIEWSIZE;
        var text = viewSize / 100 * textSize;

        if (text <= 0) // don't allow value to be less than or equal to 0.
            return 1;

        return Math.Round(text, 2);
    }

    protected static double ScreenTextOffset(double offsetDist)
    {
        var viewSize = SystemVariables.VIEWSIZE;
        return viewSize / 100 * offsetDist;
    }

    public void Dispose()
    {
        if (DrawableEntities.Count < 0)
            return;

        var tm = TransientManager.CurrentTransientManager;
        var intCol = new IntegerCollection();

        foreach (Drawable drawable in DrawableEntities)
        {
            tm.EraseTransient(drawable, intCol);
            drawable.Dispose();
        }

        Color.Dispose();
    }
}
