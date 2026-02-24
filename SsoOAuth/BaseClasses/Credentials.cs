using System.Text.Json;

namespace SsoOAuth.Data
{
    public class Credentials
    {
        public string username { get; set; }
        public string password { get; set; }

        public static Credentials Load(string environment)
        {
            var json = File.ReadAllText("environment.json");

            var environments =
                JsonSerializer.Deserialize<Dictionary<string, Credentials>>(json);

            if (environments == null || !environments.ContainsKey(environment))
                throw new Exception($"Environment '{environment}' not found.");

            return environments[environment];
        }
    }
}