using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.GraphicsInterface;

namespace AUS_Cadastral_Tools;

public sealed class TransientDrawable : List<Drawable>, IDisposable
{
    public void Dispose()
    {
        foreach (Drawable drawable in this)
            drawable.Dispose();
    }
}
