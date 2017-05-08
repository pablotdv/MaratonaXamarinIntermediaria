// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SocialLoginMaratona.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;
    }
}