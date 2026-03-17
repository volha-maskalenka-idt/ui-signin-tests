using System.Data;
using SsoOAuth.DB.Entities;

namespace SsoOAuth.DB.Tables
{
    public static class DbOperations
    {
        private const string EnvironmentName = "qa";
        private const string DbName = "Fireball";

        public static List<SubscriberGroupsDbEntity> GetGroupsByPhone(string phone)
        {
            var records = DbConnectionHelper.GetData(EnvironmentName, DbName,
                string.Format(Queries.GetGroupsDetailsByPhone, phone));
            return MapToGroupsList(records);
        }

        public static List<SubscriberGroupsDbEntity> GetGroupsByPhoneAndGroupId(string phone, int groupId)
        {
            var records = DbConnectionHelper.GetData(EnvironmentName, DbName,
                string.Format(Queries.GetGroupsDetailsByPhoneAndGroupId, phone, groupId));
            return MapToGroupsList(records);
        }

        public static SubscriberDbEntity GetSubscriberByPhone(string phone)
        {
            var records = DbConnectionHelper.GetData(EnvironmentName, DbName,
                string.Format(Queries.GetSubscriberByPhone, phone));
            return records.Rows.Count == 0 ? null : MapToSubscriber(records.Rows[0]);
        }

        private static List<SubscriberGroupsDbEntity> MapToGroupsList(DataTable records)
        {
            var result = new List<SubscriberGroupsDbEntity>();
            foreach (DataRow row in records.Rows)
                result.Add(MapToGroupsEntity(row));
            return result;
        }

        private static SubscriberGroupsDbEntity MapToGroupsEntity(DataRow row)
        {
            return new SubscriberGroupsDbEntity
            {
                GroupSubscriberId = Convert.ToDecimal(row["GROUP_SUBSCRIBER_ID"]),
                FbGroupId = Convert.ToDecimal(row["FB_GROUP_ID"]),
                SubscriberId = Convert.ToDecimal(row["SUBSCRIBER_ID"]),
                OptInDt = row["OPT_IN_DT"] == DBNull.Value ? null : Convert.ToDateTime(row["OPT_IN_DT"]),
                OptOutDt = row["OPT_OUT_DT"] == DBNull.Value ? null : Convert.ToDateTime(row["OPT_OUT_DT"])
            };
        }

        private static SubscriberDbEntity MapToSubscriber(DataRow row)
        {
            return new SubscriberDbEntity
            {
                SubscriberId = Convert.ToDecimal(row["SUBSCRIBER_ID"]),
                PhoneNo = row["PHONE_NO"].ToString(),
                Name = row["NAME"] == DBNull.Value ? null : row["NAME"].ToString(),
                Email = row["EMAIL"] == DBNull.Value ? null : row["EMAIL"].ToString(),
                City = row["CITY"] == DBNull.Value ? null : row["CITY"].ToString()
            };
        }
        
        public static List<SubscriberGroupsDbEntity> GetGroupsBySubscriberId(string subscriberId)
        {
            var records = DbConnectionHelper.GetData(EnvironmentName, DbName,
                string.Format(Queries.GetGroupsBySubscriberId, subscriberId));
            return MapToGroupsList(records);
        }
        
        public static List<SubscriberGroupsDbEntity> GetGroupsByPhoneAndGroupIds(string phone, List<int> groupIds)
        {
            var groupIdsString = string.Join(",", groupIds);
            var records = DbConnectionHelper.GetData(EnvironmentName, DbName,
                string.Format(Queries.GetGroupsByPhoneAndGroupIds, phone, groupIdsString));
            return MapToGroupsList(records);
        }
    }
}