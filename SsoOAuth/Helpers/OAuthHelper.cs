using Newtonsoft.Json;
using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.BaseClasses.Entities.API;

namespace SsoOAuth.Helpers
{
    public static class OAuthHelper
    {
        private static string? _cachedToken;
        private static DateTime _tokenExpiry = DateTime.MinValue;

        public static string GetToken(string environmentName, string apiName)
        {
            if (_cachedToken != null && DateTime.UtcNow < _tokenExpiry)
                return _cachedToken;

            var api = EnvironmentManager.GetApi(environmentName, apiName);
            var client = new RestClient(api.TokenUrl);

            var request = new RestRequest("/connect/token", Method.Post);
            request.AddParameter("grant_type", api.GrantType);
            request.AddParameter("client_id", api.ClientId);
            request.AddParameter("client_secret", api.ClientSecret);
            request.AddParameter("scope", api.Scope);

            var response = client.Execute(request);
            var token = JsonConvert.DeserializeObject<TokenEntity>(response.Content)
                        ?? throw new Exception("Failed to deserialize OAuth token response.");

            _cachedToken = token.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(token.ExpiresIn - 30);

            return _cachedToken;
        }
    }
}
