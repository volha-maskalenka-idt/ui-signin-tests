using Microsoft.Extensions.Configuration;

namespace SsoOAuth.BaseClasses
{
    public static class ConfigurationHelper
    {
        private static IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string GetSetting(string key)
        {
            return Configure()[key]
                   ?? throw new Exception($"Configuration key '{key}' not found.");
        }
    }
}
