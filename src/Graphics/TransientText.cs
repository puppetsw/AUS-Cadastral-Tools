using AUS_Cadastral_Tools.Extensions;
using AUS_Cadastral_Tools.Helpers;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AUS_Cadastral_Tools.Graphics;

public sealed class TransientText : TransientDrawableBase
{
    private readonly Point3d _position;
    private readonly double _textSize;
    private readonly string _text;
    private readonly Angle _angle;
    private readonly double _offsetAmount;
    private readonly bool _planReadability;

    public TransientText(Point3d position, Color color, double textSize, string text, Angle angle, double offsetAmount = 0.5, bool planReadability = true)
    {
        _position = position;
        _textSize = textSize;
        _text = text;
        _angle = angle;
        _offsetAmount = offsetAmount;
        _planReadability = planReadability;
        Color = color;

        CreateDrawableEntity();
    }

    private const string TEXT_STYLE_NAME = "CSS_Transient_Text";

    private static ObjectId? GetTextStyleId()
    {
        using var tr = AcadApp.StartLockedTransaction();

        var tsTable = (TextStyleTable)tr.GetObject(AcadApp.ActiveDatabase.TextStyleTableId, OpenMode.ForRead);

        ObjectId textStyleId;

        if (!tsTable.Has(TEXT_STYLE_NAME))
        {
            tsTable.UpgradeOpen();
            var textStyle = new TextStyleTableRecord
            {
                FileName = "iso.shx",
                Name = TEXT_STYLE_NAME,
            };
            textStyleId = tsTable.Add(textStyle);
            tr.AddNewlyCreatedDBObject(textStyle, true);
        }
        else
        {
            textStyleId = tsTable[TEXT_STYLE_NAME];
        }

        tr.Commit();
        return textStyleId;
    }

    protected override void CreateDrawableEntity()
    {
        var mText = new MText();
        mText.SetDatabaseDefaults();

        TextSize = _textSize;

        mText.Rotation = _planReadability ?
            _angle.GetOrdinaryAngle().ToCounterClockwise().ToRadians() :
            _angle.ToCounterClockwise().ToRadians();

        Point insPoint = PointHelpers.AngleAndDistanceToPoint(_angle.GetOrdinaryAngle() - 90, ScreenTextOffset(_offsetAmount), _position.ToPoint());

        mText.Location = insPoint.ToPoint3d();
        mText.Color = Color;
        mText.Attachment = AttachmentPoint.BottomCenter;
        mText.TextHeight = TextSizeOnScreen;
        mText.Contents = _text;

        var textStyleId = GetTextStyleId();

        if (textStyleId != null)
            mText.TextStyleId = textStyleId.Value;

        DrawableEntities.Add(mText);

        base.CreateDrawableEntity();
    }
}
