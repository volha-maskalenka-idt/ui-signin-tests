using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.Helpers;

namespace SsoOAuth.Managers
{
    public class ApiManager
    {
        private static readonly string _baseUrl = ConfigurationHelper.GetSetting("baseApiUrl");
        private static readonly RestClient _apiClient = ApiHelper.InstanceClient(_baseUrl);
        private const string ContentType = "application/json";
        
        public static RestResponse GetRequest(string endPoint)
        {
            RestRequest request = new RestRequest(endPoint);
            request.AddHeader("Accept", ContentType);
            return _apiClient.Execute(request);
        }

        public static RestResponse GetRequest(string endpoint, List<(string key, string value)> queryParams = null)
        {
            var request = new RestRequest(endpoint);

            if (queryParams != null)
                foreach (var param in queryParams)
                    request.AddQueryParameter(param.key, param.value);

            return _apiClient.Execute(request);
        }

        public static RestResponse PostRequest(string endPoint, string body)
        {
            RestRequest request = new RestRequest(endPoint, Method.Post);
            request.AddParameter(ContentType, body, ParameterType.RequestBody);
            return _apiClient.Execute(request);
        }
    }
}