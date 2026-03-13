using Newtonsoft.Json;
using RestSharp;
using SsoOAuth.BaseClasses;
using SsoOAuth.Managers;
using SsoOAuth.BaseClasses.Entities.API;
using SsoOAuth.Helpers;

namespace SsoOAuth.Steps
{
    public class SubscriberGroupsApiSteps
    {
        private RestResponse _response;
        private string _content;

        public void GetSubscriberGroupsFullListWithFollowingValues(List<(string key, string value)> queryParams)
        {
            _response = ApiManager.GetRequest("/api/fsa/GetSubscriberGroupsFull", queryParams);
            _content = _response.Content;
        }
        
        public void VerifyResponseStatus(string expectedStatus)
        {
            var expected = expectedStatus;
            var actual = _response.StatusDescription;
            SoftAssert.AreEqual(expected, actual);
        }
        
        public void VerifyResponseContainsText(string expectedText)
        {
            var expected = expectedText;
            var actual = _content;
            SoftAssert.True(actual.Contains(expected));
        }

        public void VerifyGroupsCount(string expectedCount)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = expectedCount;
            var actual = actualResponse.GroupList.Count.ToString();
            SoftAssert.AreEqual(expected, actual, $"Expected groups count: {expected}, but actual: {actual}");
        }
        
        public void VerifyGroupsCountByFbGroupId(int fbGroupId)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = SubscriberGroupsTestDataHelper.CountGroupsByFbGroupId(fbGroupId).ToString();
            var actual = actualResponse.GroupList.Count(g => g.FbGroupId == fbGroupId).ToString();
            SoftAssert.AreEqual(expected, actual);
        }
        
        public void VerifyGroupSubscriberIdAndFbGroupId(int fbGroupId)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = SubscriberGroupsTestDataHelper.GetGroupByGroupId(fbGroupId);
            var actual = actualResponse.GroupList.FirstOrDefault(g => g.FbGroupId == fbGroupId);

            SoftAssert.AreEntitiesEqual(
                expected,
                actual,
                (e, a) =>
                    e.GroupSubscriberId == a.GroupSubscriberId &&
                    e.FbGroupId == a.FbGroupId
            );
        }
        
        public void VerifyGroupsCountByFbGroupIds(int expectedCount)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = expectedCount.ToString();
            var actual = actualResponse.GroupList.Count.ToString();
            SoftAssert.AreEqual(expected, actual);
        }

        public void VerifyOnlyRequestedGroupsReturned(List<int> fbGroupIds)
        {
            var actualResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(_content);
            var expected = true;
            var actual = actualResponse.GroupList.All(g => fbGroupIds.Contains(g.FbGroupId));
            SoftAssert.AreEqual(expected, actual);
        }

        public void VerifyGroupsCountAndOnlyRequestedGroupsReturned(int expectedCount, List<int> fbGroupIds)
        {
            VerifyGroupsCountByFbGroupIds(expectedCount);
            VerifyOnlyRequestedGroupsReturned(fbGroupIds);
        }
    }
}