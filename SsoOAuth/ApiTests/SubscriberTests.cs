using NUnit.Framework;
using SsoOAuth.DB.Steps;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SubscriberApiTests
    {
        private SubscriberGroupsApiSteps _steps;
        private DbSteps _dbSteps;
        private Dictionary<string, string> _context;
        private Dictionary<string, string> _testData;

        [SetUp]
        public void Setup()
        {
            _steps = new SubscriberGroupsApiSteps();
            _dbSteps = new DbSteps();
            _context = new Dictionary<string, string>();
            _testData = FileManagerHelper.ReadJson("TestData.json");
        }

        [Test]
        public void CheckSubscriberGroups_WithValidPhone_ShouldReturnOkStatusAndCorrectGroupCount()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"])
            };
            
            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"]);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }
        
        [Test]
        public void CheckSubscriberGroups_WithoutPhoneParameter_ShouldReturnBadRequestAndPhoneRequiredMessage()
        {
            var expectedErrorBody = SubscriberGroupsTestDataHelper.GetErrorResponse("phoneRequired");
            var queryParams = new List<(string , string )>() ;

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyErrorResponse(expectedErrorBody);
        }
        
        [Test]
        public void CheckSubscriberGroups_WithEmptyPhone_ShouldReturnBadRequestAndPhoneRequiredMessage()
        {
            var expectedErrorBody = SubscriberGroupsTestDataHelper.GetErrorResponse("phoneRequired");
            var queryParams = new List<(string key, string value)>
            {
                ("phone", "")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyErrorResponse(expectedErrorBody);
        }

        [Test]
        public void CheckSubscriberGroups_WithInvalidPhone_ShouldReturnOkStatusAndEmptyGroupList()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", "Invalid phone")
            };
            
            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone("Invalid phone");
            var expectedGroups = _dbSteps.GetExpectedGroups("Invalid phone");
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
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
            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }

        [Test]
        public void CheckSubscriberGroups_WithNonExistingGroupId_ShouldReturnOkStatusAndEmptyList()
        {
            var groupsIds = new List<int> {1111};
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "1111")
            };
            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(0, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
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
                ("groupIds", "29075")
            };
            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
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

            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }

        [Test]
        public void CheckSubscriberGroups_WithGetAllFalse_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> { 18312, 29075 };
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
                ("getAll", "false")
            };

            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }

        [Test]
        public void CheckSubscriberGroups_WithEmptyGetAll_ShouldReturnBadRequestAndGetAllNotValidMessage()
        {
            var expectedErrorBody = SubscriberGroupsTestDataHelper.GetErrorResponse("getAllNotValid");
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("getAll", "")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyErrorResponse(expectedErrorBody);
        }

        
        [Test]
        public void CheckSubscriberGroups_WithInvalidGetAll_ShouldReturnBadRequestAndGetAllNotValidMessage()
        {
            var expectedErrorBody = SubscriberGroupsTestDataHelper.GetErrorResponse("getAllNotValid");
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "1234"),
                ("getAll", "invalid")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyErrorResponse(expectedErrorBody);
        }

        [Test]
        public void CheckSubscriberGroups_WithValidSourceId_ShouldReturnOkStatusAndCorrectGroupData()
        {
            var groupsIds = new List<int> { 18312, 29075 };
            var expectedCount = SubscriberGroupsTestDataHelper.CountGroupsByGroupIds(groupsIds);
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("groupIds", "18312"),
                ("groupIds", "29075"),
                ("sourceId", "test")
            };

            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"], groupsIds);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCountAndOnlyRequestedGroupsReturned(expectedCount, groupsIds);
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }

        [Test]
        public void CheckSubscriberGroups_WithInvalidSourceId_ShouldReturnBadRequestAndSourceIdNotAllowedMessage()
        {
            var expectedErrorBody = SubscriberGroupsTestDataHelper.GetErrorResponse("sourceIdNotAllowed");
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("getAll", "true"),
                ("sourceId", "invalid")
            };

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("Bad Request");
            _steps.VerifyErrorResponse(expectedErrorBody);
        }

        [Test]
        public void CheckSubscriberGroups_WithEmptySourceId_ShouldReturnOkStatusAndFullGroupList()
        {
            var queryParams = new List<(string key, string value)>
            {
                ("phone", _testData["userPhoneNumber"]),
                ("getAll", "true"),
                ("sourceId", "")
            };

            _context["subscriberId"] = _dbSteps.GetSubscriberIdByPhone(_testData["userPhoneNumber"]);
            var expectedGroups = _dbSteps.GetExpectedGroups(_testData["userPhoneNumber"]);
            _context["groupsCount"] = expectedGroups.GroupIds.Count.ToString();

            _steps.GetSubscriberGroupsFullListWithFollowingValues(queryParams);
            _steps.VerifyResponseStatus("OK");
            _steps.VerifyGroupsCount(SubscriberGroupsTestDataHelper.GetTotalGroupsCount().ToString());
            _dbSteps.VerifySubscriberGroupsResponseMatchesDb(
                _steps.GetContent(),
                _context["groupsCount"],
                expectedGroups.GroupIds,
                expectedGroups.GroupDetails);
        }
    }
}