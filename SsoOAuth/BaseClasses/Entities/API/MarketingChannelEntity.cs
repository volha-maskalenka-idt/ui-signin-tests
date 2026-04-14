namespace SsoOAuth.BaseClasses.Entities.API
{
    public class MarketingChannelEntity
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string? CountryAbbreviations { get; set; }
    }

    public class SearchMarketingChannelsResponse
    {
        public List<MarketingChannelEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
