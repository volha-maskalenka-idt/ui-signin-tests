namespace SsoOAuth.DB.Entities
{
    public class SubscriberGroupsDbEntity
    { 
        public decimal GroupSubscriberId { get; set; }
        public decimal FbGroupId { get; set; }
        public decimal SubscriberId { get; set; }
        public DateTime? OptInDt { get; set; }
        public DateTime? OptOutDt { get; set; }
        public decimal? OptInRuleId { get; set; }
        public decimal? OptOutRuleId { get; set; }
        public DateTime CreateTs { get; set; }
        public decimal CreateByUId { get; set; }
        public DateTime UpdateTs { get; set; }
        public decimal UpdateByUId { get; set; }
        public string? OptInSourceTxt { get; set; }
        public string? OptOutSourceTxt { get; set; }
        public string? ExternalSubscriberCode { get; set; }
        public decimal? Disconnected { get; set; }
    }
}


