namespace SsoOAuth.BaseClasses.Entities.API
{
    public class ErrorResponseEntity
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
    }
}