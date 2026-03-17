namespace SsoOAuth.DB.Entities
{
    public class SubscriberDbEntity
    {
        public decimal SubscriberId { get; set; }
        public decimal? CountryId { get; set; }
        public decimal LanguageDmnId { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AddressTxt { get; set; }
        public string? StateCode { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreateTs { get; set; }
        public decimal CreateByUid { get; set; }
        public DateTime UpdateTs { get; set; }
        public decimal UpdateByUid { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public decimal? EmailVerifiedFlg { get; set; }
        public decimal? TzNameId { get; set; }
        public string? TzName { get; set; }
        public decimal? StdUtcBias { get; set; }
        public string? CountryAbbreviation { get; set; }
        public string? CarrierCode { get; set; }
        public string? TzNameIana { get; set; }
        public decimal? Disconnected { get; set; }
        public decimal? LastOptTransactionId { get; set; }
    }
}


