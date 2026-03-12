using Newtonsoft.Json;

namespace SsoOAuth.BaseClasses.Entities.API
{
    public class UserApiEntity
    {
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}