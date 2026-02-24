using System.Text.Json;

namespace SsoOAuth.Data
{
    public class EnvironmentManager
    {
        public string username { get; set; }
        public string password { get; set; }

        public static EnvironmentManager Load(string environment)
        {
            var json = File.ReadAllText("Environment.json");

            var environments =
                JsonSerializer.Deserialize<Dictionary<string, EnvironmentManager>>(json);

            if (environments == null || !environments.ContainsKey(environment))
                throw new Exception($"Environment '{environment}' not found.");

            return environments[environment];
        }
    }
}