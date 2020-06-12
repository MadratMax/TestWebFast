using System;

namespace TestWebFast
{
    class Settings
    {
        public static int Columns = Int32.Parse(SettingsManager.GetSettingsValue("settings", "ui", "columns"));

        public static int SpacingPerLine = Int32.Parse(SettingsManager.GetSettingsValue("settings", "ui", "spacingPerLine"));

        public static string LoginName = SettingsManager.GetSettingsValue("settings", "account", "username");

        public static string Password = SettingsManager.GetSettingsValue("settings", "account", "password");

        public static string TestsPath = SettingsManager.GetSettingsValue("settings", "data", "tests");

        public static string TestsExtension = SettingsManager.GetSettingsValue("settings", "data", "testFilesExtension");

        public static string PageObjectsPath = SettingsManager.GetSettingsValue("settings", "data", "pages");

        public static string PagesExtension = SettingsManager.GetSettingsValue("settings", "data", "pageFilesExtension");

        public static string ProtocolPath = SettingsManager.GetSettingsValue("settings", "data", "protocol");
    }
}
