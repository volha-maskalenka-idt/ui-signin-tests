using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.Helpers;

namespace SsoOAuth.Managers
{
    public class ApiManager
    {
        private static readonly string _baseUrl = ConfigurationHelper.GetSetting("baseApiUrl");
        private static readonly RestClient _apiClient = ApiHelper.InstanceClient(_baseUrl);

        public static RestResponse GetRequest(string endpoint, Dictionary<string, string> queryParams = null)
        {
            var request = new RestRequest(endpoint);
    
            if (queryParams != null)
                foreach (var param in queryParams)
                    request.AddQueryParameter(param.Key, param.Value);

            return _apiClient.Execute(request);
        }

        public static RestResponse Post(string endpoint, object body)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            return _apiClient.Execute(request);
        }
    }
}