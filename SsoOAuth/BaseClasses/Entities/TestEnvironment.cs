using System.Collections.Generic;

namespace SsoOAuth.BaseClasses.Entities
{
    public class TestEnvironment
    {
        public string Name { get; set; }

        public List<Api> Apis { get; set; } = new();
        public List<Database> Databases { get; set; } = new();
        public List<Portal> Portals { get; set; } = new();
    }
}