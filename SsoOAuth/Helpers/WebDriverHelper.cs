using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        
        public static void Init()
        {
            WebDriverFactory.Init();
        }
        public static void CloseAndQuit()
        {
            WebDriverFactory.Close();
            WebDriverFactory.Quit();
        }
        
        public static string GetCurrentUrl()
        {
            return Driver.Url;
        }

        public static string GetTitle()
        {
            return Driver.Title;
        }
        
        public static void Refresh()
        {
            Driver.Navigate().Refresh();
        }
        
        public static void Maximise()
        {
            Driver.Manage().Window.Maximize();
        }
        
        public static void SwitchToFrame(int index)
        {
            Driver.SwitchTo().Frame(index);
        }
        
        public static void SwitchToFrame(string frameNameOrId)
        {
            Driver.SwitchTo().Frame(frameNameOrId);
        }
        
        public static void SwitchToFrame(By locator)
        {
            var frameElement = Driver.FindElement(locator);
            Driver.SwitchTo().Frame(frameElement);
        }
        
        public static void SwitchToDefaultContent()
        {
            WebDriverFactory.Driver.SwitchTo().DefaultContent();
        }
        
        public static WebDriverWait CreateWait(int timeout)
        {
            return new WebDriverWait(
                WebDriverFactory.Driver,
                TimeSpan.FromSeconds(timeout));
        }
        
        public static IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }
    }
}