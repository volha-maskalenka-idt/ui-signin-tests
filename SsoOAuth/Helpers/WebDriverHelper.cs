using OpenQA.Selenium;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class WebDriverHelper
    {
        private static IWebDriver Driver => WebDriverFactory.Driver;

        public static void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public static void CloseAndQuit()
        {
            Driver.Close();
            Driver.Quit();
        }

        public static string GetCurrentUrl()
        {
            return Driver.Url;
        }

        public static string GetTitle()
        {
            return Driver.Title;
        }
    }
}