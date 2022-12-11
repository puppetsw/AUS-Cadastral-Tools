﻿using System;
using System.Diagnostics;

namespace AUS_Cadastral_Tools;

[DebuggerDisplay("{X}"+ "," + "{Y}" + "," + "{Z}")]
public sealed class Point
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public static Point Origin => new(0, 0, 0);

    public Point() { }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
        Z = 0;
    }

    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public bool IsValid()
    {
        if (Math.Abs(X) < 1E+20 && Math.Abs(Y) < 1E+20 && Math.Abs(Z) < 1E+20)
            return X + Y + Z != 0.0;
        return false;
    }

    public override string ToString()
    {
        return $"X:{X},Y:{Y},Z:{Z}";
    }
}
