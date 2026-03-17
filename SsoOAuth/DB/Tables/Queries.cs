namespace SsoOAuth.DB.Tables

{
    public static class Queries
    {
        public static string GetSubscriberByPhone =
            @"SELECT * 
              FROM SUBSCRIBER
              WHERE PHONE_NO = '{0}'";

        public static string GetGroupsDetailsByPhone =
            @"SELECT gs.GROUP_SUBSCRIBER_ID,
                     gs.FB_GROUP_ID,
                     gs.SUBSCRIBER_ID,
                     gs.OPT_IN_DT,
                     gs.OPT_OUT_DT
              FROM GROUP_SUBSCRIBER gs
              JOIN SUBSCRIBER s ON s.SUBSCRIBER_ID = gs.SUBSCRIBER_ID
              WHERE s.PHONE_NO = '{0}'";

        public static string GetGroupsDetailsByPhoneAndGroupId =
            @"SELECT gs.GROUP_SUBSCRIBER_ID,
                     gs.FB_GROUP_ID,
                     gs.SUBSCRIBER_ID,
                     gs.OPT_IN_DT,
                     gs.OPT_OUT_DT
              FROM GROUP_SUBSCRIBER gs
              JOIN SUBSCRIBER s ON s.SUBSCRIBER_ID = gs.SUBSCRIBER_ID
              WHERE s.PHONE_NO = '{0}'
              AND gs.FB_GROUP_ID = {1}";
        
        public static string GetGroupsByPhoneAndGroupIds =
            @"SELECT gs.GROUP_SUBSCRIBER_ID,
                    gs.FB_GROUP_ID,
                    gs.SUBSCRIBER_ID,
                    gs.OPT_IN_DT,
                    gs.OPT_OUT_DT
              FROM GROUP_SUBSCRIBER gs
              JOIN SUBSCRIBER s ON s.SUBSCRIBER_ID = gs.SUBSCRIBER_ID
              WHERE s.PHONE_NO = '{0}'
              AND gs.FB_GROUP_ID IN ({1})";
        
        public static string GetGroupsBySubscriberId =
            @"SELECT gs.GROUP_SUBSCRIBER_ID,
                     gs.FB_GROUP_ID,
                     gs.SUBSCRIBER_ID,
                     gs.OPT_IN_DT,
                     gs.OPT_OUT_DT
              FROM GROUP_SUBSCRIBER gs
              WHERE gs.SUBSCRIBER_ID = {0}";
    }
}
