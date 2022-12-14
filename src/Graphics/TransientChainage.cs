using System;
using System.Globalization;
using AUS_Cadastral_Tools.Extensions;
using AUS_Cadastral_Tools.Helpers;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientChainage : TransientDrawableBase
{
    private readonly Point3d _startPoint;
    private readonly Point3d _endPoint;
    private readonly int _interval;
    private readonly double _textSize;

    public TransientChainage(Point3d startPoint, Point3d endPoint, Color color, int interval = 10, double textSize = 1.0)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        _interval = interval;
        _textSize = textSize;
        Color = color;

        CreateDrawableEntity();
    }

    protected override void CreateDrawableEntity()
    {
        var distance = PointHelpers.GetDistanceBetweenPoints(_startPoint.ToPoint(), _endPoint.ToPoint());
        var angle = AngleHelpers.GetAngleBetweenPoints(_startPoint.ToPoint(), _endPoint.ToPoint());


        var intervalStep = 0;
        while (intervalStep < distance)
        {
            var point = PointHelpers.AngleAndDistanceToPoint(angle, intervalStep, _startPoint.ToPoint());

            DrawableEntities.Add(DrawTick(point.ToPoint3d(), angle));
            DrawableEntities.Add(DrawChainageText(point.ToPoint3d(), intervalStep.ToString(), _textSize, angle, planReadability: false));
            intervalStep += _interval;
        }
        //draw last
        DrawableEntities.Add(DrawTick(_endPoint, angle));
        DrawableEntities.Add(DrawChainageText(_endPoint, Math.Round(distance, 3).ToString(CultureInfo.InvariantCulture), _textSize, angle, planReadability: false));

        base.CreateDrawableEntity();
    }


    private Drawable DrawChainageText(Point3d position, string text, double textSize, Angle angle, double offsetAmount = 0.5, bool planReadability = true)
    {
        var mText = new MText();
        mText.SetDatabaseDefaults();

        mText.Rotation = planReadability ?
            angle.GetOrdinaryAngle().ToCounterClockwise().ToRadians() :
            angle.ToCounterClockwise().ToRadians();

        Point insPoint = PointHelpers.AngleAndDistanceToPoint(angle.GetOrdinaryAngle() - 90, ScreenTextOffset(offsetAmount), position.ToPoint());

        mText.Location = insPoint.ToPoint3d();
        mText.Color = Color;
        mText.Attachment = AttachmentPoint.BottomCenter;
        mText.TextHeight = ScreenTextSize(textSize);
        mText.Contents = text;

        return mText;
    }

    private Drawable DrawTick(Point3d tickPoint, Angle drawAngle, double tickLength = 1.0)
    {
        var point1 = PointHelpers.AngleAndDistanceToPoint(drawAngle + 90, tickLength, tickPoint.ToPoint());
        var point2 = PointHelpers.AngleAndDistanceToPoint(drawAngle - 90, tickLength, tickPoint.ToPoint());
        return new Line(point1.ToPoint3d(), point2.ToPoint3d());
    }

}