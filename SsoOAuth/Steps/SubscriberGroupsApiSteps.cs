using Newtonsoft.Json;
using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.Managers;
using SsoOAuth.BaseClasses.Entities.API;

namespace SsoOAuth.Steps
{
    public class SubscriberGroupsApiSteps
    {
        private RestResponse _response;
        private string _content;

        public void GetSubscriberGroupsFullListByPhone(Dictionary<string, string> queryParams)
        {
            _response = ApiManager.GetRequest("/api/fsa/GetSubscriberGroupsFull", queryParams);
            _content = _response.Content;
        }
        
        public void VerifyResponseStatusCode(string expectedStatus)
        {
            var expected = expectedStatus;
            var actual = _response.StatusDescription;
            SoftAssert.AreEqual(expected, actual);
        }

        public void VerifyGroupsCount(string expectedCount)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = expectedCount;
            var actual = actualResponse.GroupList.Count.ToString();
            SoftAssert.AreEqual(expected, actual, $"Expected groups count: {expected}, but actual: {actual}");
        }
    }
}