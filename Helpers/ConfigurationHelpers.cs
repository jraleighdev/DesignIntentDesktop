using Microsoft.Extensions.Configuration;

namespace DesignIntentDesktop.Helpers
{
    public static class ConfigurationHelpers
    {
        public static T GetSetting<T>(this IConfiguration configuration, string name, string section = "AppSettings")
        {
            return configuration.GetSection($"{section}:{name}").Get<T>();
        }
    }
}
