using System.Text.Json;

namespace SsoOAuth.Data
{
    public class AppSettings
    {
        public string baseUrl { get; set; }
        public string browser { get; set; }

        public static AppSettings Load()
        {
            var json = File.ReadAllText("appsettings.json");
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
    }
}