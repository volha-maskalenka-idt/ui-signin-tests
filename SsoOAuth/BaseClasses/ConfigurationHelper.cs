using Microsoft.Extensions.Configuration;

namespace SsoOAuth.Data
{
    public static class ConfigurationHelper
    {
        private static IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }

        public static string GetSetting(string key)
        {
            return Configure()[key]
                   ?? throw new Exception($"Configuration key '{key}' not found.");
        }
    }
}
