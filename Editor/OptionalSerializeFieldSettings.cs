using System.Collections.Generic;

namespace TanitakaTech.OptionalSerializeField
{
    internal static class OptionalSerializeFieldSettings
    {
        public static bool IsEnable => OptionalSerializeFieldSettingsManager.Log.IsEnableSetting.value;
        public static string LogFormat => OptionalSerializeFieldSettingsManager.Log.LogFormatSetting.value;
        public static List<string> IgnoreNamespaces => OptionalSerializeFieldSettingsManager.IgnoreNameSpaces.IgnoreNameSpacesSetting.value;
        public static List<string> IgnoreAssemblyNames => OptionalSerializeFieldSettingsManager.IgnoreAssemblies.IgnoreAssembliesSetting.value;
    }
}