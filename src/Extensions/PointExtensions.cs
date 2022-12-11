using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.Geometry;

namespace AUS_Cadastral_Tools.Extensions;

public static class PointExtensions
{
    public static Point ToPoint(this Point2d point) => new(point.X, point.Y);

    public static Point ToPoint(this Point3d point) => new(point.X, point.Y, point.Z);

    public static Point2d ToPoint2d(this Point point) => new(point.X, point.Y);

    public static Point3d ToPoint3d(this Point point) => new(point.X, point.Y, point.Z);

    public static Point2d ToPoint2d(this Point3d point) => new(point.X, point.Y);

    public static Point3d ToPoint3d(this Point2d point, double elevation = 0) =>
        new(point.X, point.Y, elevation);

    public static List<Point2d> ToListOfPoint2d(this IEnumerable<Point3d> points) =>
        points.Select(point => new Point2d(point.X, point.Y)).ToList();

    public static List<Point3d> ToListOfPoint3d(this IEnumerable<Point2d> points, double elevation = 0) =>
        points.Select(point => new Point3d(point.X, point.Y, elevation)).ToList();

    public static bool IsValid(this Point3d point)
    {
        if (Math.Abs(point.X) < 1E+20 && Math.Abs(point.Y) < 1E+20 && Math.Abs(point.Z) < 1E+20)
            return point.X + point.Y + point.Z != 0.0;
        return false;
    }
}
