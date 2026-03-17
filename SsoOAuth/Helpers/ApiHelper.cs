using RestSharp;

namespace SsoOAuth.Helpers
{
    public class ApiHelper
    {
        private static RestClient _instanceClient;

        public static RestClient InstanceClient(string baseUrl)
        {
            _instanceClient ??= new RestClient(baseUrl);
            return _instanceClient;
        }
    }
}