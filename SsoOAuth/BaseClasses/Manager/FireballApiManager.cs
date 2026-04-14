using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.Helpers;

namespace SsoOAuth.Managers
{
    public static class FireballApiManager
    {
        private static readonly string _baseUrl = ConfigurationHelper.GetSetting("baseApiUrl");
        private static readonly RestClient _apiClient = ApiHelper.InstanceClient(_baseUrl);
        private const string ContentType = "application/json";

        public static RestResponse PostRequestWithAuth(string endpoint, string body)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Authorization", $"Bearer {OAuthHelper.GetToken("qa", "FireballSubscriber")}");
            request.AddParameter(ContentType, body, ParameterType.RequestBody);
            return _apiClient.Execute(request);
        }

        public static RestResponse PostRequestWithoutAuth(string endpoint, string body)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddParameter(ContentType, body, ParameterType.RequestBody);
            return _apiClient.Execute(request);
        }
    }
}
