namespace SsoOAuth.BaseClasses.Entities
{
    public class Database
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Port { get; set; }
        public string DatabaseName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Service { get; set; }
        public string Host { get; set; }
    }
}