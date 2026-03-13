using Newtonsoft.Json;
using SsoOAuth.BaseClasses.Entities.API;

namespace SsoOAuth.Helpers
{
    public static class SubscriberGroupsTestDataHelper
    {
        private static SubscriberGroupsList? _testData;

        private static SubscriberGroupsList GetTestData()
        {
            if (_testData != null) return _testData;

            var json = FileManagerHelper.ReadRawJson("SubscriberGroupsTestData.json");
            _testData = JsonConvert.DeserializeObject<SubscriberGroupsList>(json);
            return _testData;
        }

        public static int GetTotalGroupsCount()
        {
            return GetTestData().GroupList.Count;
        }

        public static SubscriberGroupsEntity GetGroupByGroupId(int fbGroupId)
        {
            return GetTestData().GroupList
                .FirstOrDefault(g => g.FbGroupId == fbGroupId);
        }
        
        public static int CountGroupsByGroupIds(List<int> fbGroupIds)
        {
            return GetTestData().GroupList
                .Count(g => fbGroupIds.Contains(g.FbGroupId));
        }

        public static int CountGroupsByFbGroupId(int fbGroupId)
        {
            return GetTestData().GroupList
                .Count(g => g.FbGroupId == fbGroupId);
        }

        public static bool GroupExistsByName(string groupName)
        {
            return GetTestData().GroupList
                .Any(g => g.GroupName == groupName);
        }
    }
}