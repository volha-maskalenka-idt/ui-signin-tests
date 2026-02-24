using OpenQA.Selenium;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class WebDriverHelper
    {
        public static void NavigateTo(string url)
        {
            WebDriverFactory.Driver.Navigate().GoToUrl(url);
        }
        
        public static void Init()
        {
            WebDriverFactory.Init();
        }
        
        public static void Quit()
        {
            WebDriverFactory.Quit();
        }
        
        public static string GetCurrentUrl()
        {
            return WebDriverFactory.Driver.Url;
        }

        public static string GetTitle()
        {
            return WebDriverFactory.Driver.Title;
        }
    }
}