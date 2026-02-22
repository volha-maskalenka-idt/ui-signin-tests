using System.IO;
using System.Text.Json;
using SSO_OAUTH.Data;

namespace SSO_OAUTH.Helpers
{
    public static class FileManagerHelper
    {
        public static AppSettings GetAppSettings()
        {
            var json = File.ReadAllText("AppSettings.json");

            return JsonSerializer.Deserialize<AppSettings>(json);
        }

        public static Credentials GetEnvironmentCredentials(string environment)
        {
            var json = File.ReadAllText("Environment.json");

            var data = JsonSerializer.Deserialize<EnvironmentData>(json);

            return data[environment];
        }

        public static TestData GetTestData()
        {
            var json = File.ReadAllText("Data/TestData.json");

            return JsonSerializer.Deserialize<TestData>(json);
        }
    }
}