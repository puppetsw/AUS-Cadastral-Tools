using AUS_Cadastral_Tools.Strings;

namespace AUS_Cadastral_Tools.Helpers;

public static class ResourceHelpers
{
    public static string GetLocalisedString(string name)
    {
        var resourceMgr = ResourceStrings.ResourceManager;
        var str = resourceMgr.GetString(name);
        return str;
    }
}
