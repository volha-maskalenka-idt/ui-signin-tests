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

        public static string GetAllLanguages =
            @"SELECT LANGUAGE_DMN_ID, NAME, VALID_SMS_CHAR_SET
              FROM FIREBALL.LANGUAGE_DMN";

        public static string GetLanguageById =
            @"SELECT LANGUAGE_DMN_ID, NAME, VALID_SMS_CHAR_SET
              FROM FIREBALL.LANGUAGE_DMN
              WHERE LANGUAGE_DMN_ID = {0}";

        public static string GetAllMarketingChannels =
            @"SELECT MARKETING_CHANNEL_DMN_ID, NAME, COUNTRY_ABBREVIATIONS
              FROM FIREBALL.MARKETING_CHANNEL_DMN";

        public static string GetMarketingChannelById =
            @"SELECT MARKETING_CHANNEL_DMN_ID, NAME, COUNTRY_ABBREVIATIONS
              FROM FIREBALL.MARKETING_CHANNEL_DMN
              WHERE MARKETING_CHANNEL_DMN_ID = {0}";
    }
}
