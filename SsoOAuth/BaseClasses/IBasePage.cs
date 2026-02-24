using System.Reflection;
using OpenQA.Selenium;

namespace SsoOAuth.BaseClasses
    {
        public interface IBasePage
        {
            public By GetLocator(string uiName)
            {
                var properties = GetType()
                    .GetProperties(BindingFlags.NonPublic |
                                   BindingFlags.Static |
                                   BindingFlags.Instance);

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<UiNameAttribute>();

                    if (attribute != null && attribute.Name == uiName)
                    {
                        return (By)property.GetValue(null);
                    }
                }
                throw new Exception($"Locator with UiName '{uiName}' not found.");
            }

        }
    }