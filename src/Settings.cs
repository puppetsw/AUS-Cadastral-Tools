namespace AUS_Cadastral_Tools;

public static class Settings
{
    public static int GraphicsSize
    {
        get => Properties.Settings.Default.Graphics_Size;
        set => Properties.Settings.Default.Graphics_Size = value;
    }

    public static int GraphicsTextSize
    {
        get => Properties.Settings.Default.Graphics_Text_Size;
        set => Properties.Settings.Default.Graphics_Text_Size = value;
    }

    public static short TransientColorIndex
    {
        get => Properties.Settings.Default.Transient_ColorIndex;
        set => Properties.Settings.Default.Transient_ColorIndex = value;
    }
}