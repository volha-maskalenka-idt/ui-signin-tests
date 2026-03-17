using Newtonsoft.Json;
using SsoOAuth.BaseClasses;
using SsoOAuth.BaseClasses.Entities.API;
using SsoOAuth.DB.Entities;
using SsoOAuth.DB.Tables;

namespace SsoOAuth.DB.Steps
{
    public class DbSteps
    {
        public string GetSubscriberIdByPhone(string phone)
        {
            var subscriber = DbOperations.GetSubscriberByPhone(phone);
            return subscriber?.SubscriberId.ToString();
        }
        
        

        public string GetGroupsCountBySubscriberId(string subscriberId)
        {
            var groups = DbOperations.GetGroupsBySubscriberId(subscriberId);
            return groups.Count.ToString();
        }

        public (List<int> GroupIds, List<SubscriberGroupsDbEntity> GroupDetails) GetExpectedGroups(string phone)
        {
            var groups = DbOperations.GetGroupsByPhone(phone);

            if (groups == null || !groups.Any())
                return (new List<int>(), new List<SubscriberGroupsDbEntity>());

            var groupIds = groups.Select(g => (int)g.FbGroupId).ToList();
            return (groupIds, groups);
        }
        
        public (List<int> GroupIds, List<SubscriberGroupsDbEntity> GroupDetails) GetExpectedGroups(string phone, List<int> groupIds = null)
        {
            var groups = groupIds != null && groupIds.Any()
                ? DbOperations.GetGroupsByPhoneAndGroupIds(phone, groupIds)
                : DbOperations.GetGroupsByPhone(phone);

            var ids = groups.Select(g => (int)g.FbGroupId).ToList();
            return (ids, groups);
        }

        public void VerifySubscriberGroupsResponseMatchesDb(
            string apiContent,
            string expectedCount,
            List<int> expectedGroupIds,
            List<SubscriberGroupsDbEntity> expectedGroupDetails)
        {
            VerifyGroupsCount(apiContent, expectedCount);
            VerifyGroupIds(apiContent, expectedGroupIds);
            VerifyGroupDetails(apiContent, expectedGroupDetails);
        }

        private void VerifyGroupsCount(string apiContent, string expectedCount)
        {
            var apiResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(apiContent);
            var actual = apiResponse.GroupList.Count.ToString();
            SoftAssert.AreEqual(expectedCount, actual,
                $"Groups count mismatch. Expected: {expectedCount}, Actual: {actual}");
        }

        private void VerifyGroupIds(string apiContent, List<int> expectedGroupIds)
        {
            var apiResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(apiContent);
            var actualGroupIds = apiResponse.GroupList.Select(g => g.FbGroupId).ToList();

            foreach (var expectedId in expectedGroupIds)
            {
                var expected = true;
                var actual = actualGroupIds.Contains(expectedId);
                SoftAssert.AreEqual(expected, actual,
                    $"Group with FbGroupId {expectedId} not found in response");
            }
        }

        private void VerifyGroupDetails(string apiContent, List<SubscriberGroupsDbEntity> expectedGroupDetails)
        {
            var apiResponse = JsonConvert.DeserializeObject<SubscriberGroupsList>(apiContent);

            foreach (var dbGroup in expectedGroupDetails)
            {
                var apiGroup = apiResponse.GroupList
                    .FirstOrDefault(g => g.FbGroupId == (int)dbGroup.FbGroupId);

                var mappedApiGroup = MapToDbEntity(apiGroup);

                SoftAssert.AreEntitiesEqual(
                    dbGroup,
                    mappedApiGroup,
                    (e, a) =>
                        e.GroupSubscriberId == a.GroupSubscriberId &&
                        e.FbGroupId == a.FbGroupId &&
                        e.SubscriberId == a.SubscriberId &&
                        e.OptInDt == a.OptInDt &&
                        e.OptOutDt == a.OptOutDt,
                    $"Group details mismatch for FbGroupId {dbGroup.FbGroupId}"
                );
            }
        }

        private SubscriberGroupsDbEntity MapToDbEntity(SubscriberGroupsEntity apiEntity)
        {
            return new SubscriberGroupsDbEntity
            {
                GroupSubscriberId = apiEntity.GroupSubscriberId,
                FbGroupId = apiEntity.FbGroupId,
                SubscriberId = apiEntity.SubscriberId,
                OptInDt = string.IsNullOrEmpty(apiEntity.OptInDt)
                    ? null
                    : DateTime.Parse(apiEntity.OptInDt),
                OptOutDt = string.IsNullOrEmpty(apiEntity.OptOutDt)
                    ? null
                    : DateTime.Parse(apiEntity.OptOutDt)
            };
        }
    }
}