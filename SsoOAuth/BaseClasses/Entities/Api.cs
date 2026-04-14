namespace SsoOAuth.BaseClasses.Entities
{
    public class Api
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string? GrantType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Scope { get; set; }
        public string? TokenUrl { get; set; }
    }
}