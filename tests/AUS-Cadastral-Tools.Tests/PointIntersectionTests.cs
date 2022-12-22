using AUS_Cadastral_Tools.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AUS_Cadastral_Tools.Tests;

[TestClass]
public class PointIntersectionTests
{
    private Point _interAngleAnglePoint1;
    private Point _interAngleAnglePoint2;
    private Angle _interAngleAngleAngle1;
    private Angle _interAngleAngleAngle2_135;
    private Angle _interAngleAngleAngle2_315;
    private Point _interAngleAngleExpectedPoint;

    [TestInitialize]
    public void Test_Initialize()
    {
        _interAngleAnglePoint1 = new Point(0, 0, 0);
        _interAngleAnglePoint2 = new Point(130, 90, 0);
        _interAngleAngleAngle1 = new Angle(45);
        _interAngleAngleAngle2_135 = new Angle(135);
        _interAngleAngleAngle2_315 = new Angle(315);
        _interAngleAngleExpectedPoint = new Point(110, 110);
    }

    [TestMethod]
    public void Test_AngleAngleIntersection_Should_Intersect()
    {
        var canIntersect = PointHelpers.AngleAngleIntersection(_interAngleAnglePoint1, _interAngleAngleAngle1, _interAngleAnglePoint2, _interAngleAngleAngle2_315, out var intersectionPoint);

        Assert.IsTrue(canIntersect);
        Assert.IsNotNull(intersectionPoint);
        Assert.AreEqual(_interAngleAngleExpectedPoint.X, intersectionPoint.X, 0.001);
        Assert.AreEqual(_interAngleAngleExpectedPoint.Y, intersectionPoint.Y, 0.001);
    }

    [TestMethod]
    public void Test_AngleAngleIntersection_Should_Intersect_With_Opposite_Angle_Direction()
    {
        var canIntersect = PointHelpers.AngleAngleIntersection(_interAngleAnglePoint1, _interAngleAngleAngle1, _interAngleAnglePoint2, _interAngleAngleAngle2_135, out var intersectionPoint);

        Assert.IsTrue(canIntersect);
        Assert.IsNotNull(intersectionPoint);
        Assert.AreEqual(_interAngleAngleExpectedPoint.X, intersectionPoint.X, 0.001);
        Assert.AreEqual(_interAngleAngleExpectedPoint.Y, intersectionPoint.Y, 0.001);
    }
}
