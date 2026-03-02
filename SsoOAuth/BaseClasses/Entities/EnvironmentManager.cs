using System.Linq;
using Microsoft.Extensions.Configuration;
using SsoOAuth.BaseClasses.Entities;

namespace SsoOAuth.BaseClasses
{
    public static class EnvironmentManager
    {
        private static IConfiguration EnvironmentConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Environment.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
        public static User GetUser(string environmentName)
        {
            var section = EnvironmentConfigurationBuilder().GetSection(environmentName);

            if (!section.Exists())
                throw new Exception($"Environment '{environmentName}' not found.");

            return section.Get<User>()
                   ?? throw new Exception($"Failed to bind environment '{environmentName}' to User.");
        }
        
        public static TestEnvironment GetEnvironment(string environmentName)
        {
            var environments = EnvironmentConfigurationBuilder()
                .GetSection("Environments")
                .Get<List<TestEnvironment>>();

            if (environments == null || !environments.Any())
                throw new Exception("No environments found in configuration.");

            return environments.First(e => e.Name == environmentName);
        }

        public static Portal GetPortal(string environmentName, string portalName)
        {
            return GetEnvironment(environmentName)
                .Portals
                .First(p => p.Name == portalName);
        }

        public static Api GetApi(string environmentName, string apiName)
        {
            return GetEnvironment(environmentName)
                .Apis
                .First(a => a.Name == apiName);
        }

        public static Database GetDatabase(string environmentName, string dbName)
        {
            return GetEnvironment(environmentName)
                .Databases
                .First(d => d.Name == dbName);
        }
    }
}