using System.Collections.Generic;

namespace SsoOAuth.BaseClasses.Entities
{
    public class Portal
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<User> Users { get; set; } = new();
    }
}