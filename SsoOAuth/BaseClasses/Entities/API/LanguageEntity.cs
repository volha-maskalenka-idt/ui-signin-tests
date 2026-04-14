namespace SsoOAuth.BaseClasses.Entities.API
{
    public class LanguageEntity
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string? ValidSmsCharSet { get; set; }
    }

    public class SearchLanguagesResponse
    {
        public List<LanguageEntity> Languages { get; set; }
        public int TotalCount { get; set; }
    }
}
