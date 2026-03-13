using NUnit.Framework;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SubscriberApiTests
    {
        private SubscriberGroupsApiSteps _steps;
        private Dictionary<string, string> _testData;

        [SetUp]
        public void Setup()
        {
            _steps = new SubscriberGroupsApiSteps();
            _testData = FileManagerHelper.ReadJson("TestData.json");
        }

        [Test]
        public void CheckSubscriberGroups_WithValidPhone_ShouldReturnOkStatusAndCorrectGroupCount()
        {
            var expectedCount = SubscriberGroupsTestDataHelper.GetTotalGroupsCount().ToString();
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"])
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCount(expectedCount);
        }
        
        [Test]
        public void CheckSubscriberGroups_WithoutPhoneParameter_ShouldReturnBadRequestAndPhoneRequiredMessage()
        {
            var queryParams = new List<(string , string )>() ;

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyResponseContainsText("Phone number is required");
        }
        
        [Test]
        public void CheckSubscriberGroups_WithEmptyPhone_ShouldReturnBadRequestAndPhoneRequiredMessage()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", "")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyResponseContainsText("Phone number is required");
        }

        [Test]
        public void CheckSubscriberGroups_WithInvalidPhone_ShouldReturnOkStatusAndEmptyGroupList()
        {
            var invalidPhone = "0000000000";
            var queryParams = new List<(string key, string value)>
            {
                ("phone", invalidPhone)
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCount("0");
        }

        [Test]
        public void CheckSubscriberGroups_WithExistingFbGroupId_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> {18312};
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
        }

        [Test]
        public void CheckSubscriberGroups_WithNonExistingGroupId_ShouldReturnOkStatusAndEmptyList()
        {
            var invalidId = "1234";
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", invalidId),
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCount("0");
        }

        [Test]
        public void CheckSubscriberGroups_WithExistingAndNonExistingGroupId_ShouldReturnOkStatusAndOneGroup()
        {
            var groupsIds = new List<int> {18312, 29075};
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
        }
        
        [Test]
        public void CheckSubscriberGroups_WithGetAllTrue_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> {18312, 29075};
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
                ("getAll", "true")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
        }

        [Test]
        public void CheckSubscriberGroups_WithGetAllFalse_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> {18312, 29075};
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
                ("getAll", "false")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
        }

        [Test]
        public void CheckSubscriberGroups_WithEmptyGetAll_ShouldReturnBadRequestAndGetAllNotValidMessage()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("getAll", "")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyResponseContainsText("'getAll' is not valid");
        }

        [Test]
        public void CheckSubscriberGroups_WithInvalidGetAll_ShouldReturnBadRequestAndGetAllNotValidMessage()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "1234"),
                ("getAll", "invalid")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyResponseContainsText("'getAll' is not valid");
        }
        
        public void CheckSubscriberGroups_WithValidSourceId_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> {18312, 29075};
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
                ("sourceId", "test")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
        }
        
        [Test]
        public void CheckSubscriberGroups_WithInvalidSourceId_ShouldReturnBadRequestAndSourceIdNotAllowedMessage()
        {
            var invalidSourceId = "invalid";
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("getAll", "true"),
                ("sourceId", invalidSourceId)
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyResponseContainsText("Source id is not allowed");
        }

        [Test]
        public void CheckSubscriberGroups_WithEmptySourceId_ShouldReturnOkStatusAndFullGroupList()
        {
            var groupsCount = SubscriberGroupsTestDataHelper.GetTotalGroupsCount().ToString();
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("getAll", "true"),
                ("sourceId", "")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCount(groupsCount);
        }
    }
}