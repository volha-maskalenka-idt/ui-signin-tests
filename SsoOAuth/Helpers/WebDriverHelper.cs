using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        public static void CloseAndQuit()
        {
            WebDriverFactory.Close();
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
        
        public static void Refresh()
        {
            WebDriverFactory.Driver.Navigate().Refresh();
        }
        
        public static void Maximise()
        {
            WebDriverFactory.Driver.Manage().Window.Maximize();
        }
        
        public static void SwitchToFrame(int index)
        {
            WebDriverFactory.Driver.SwitchTo().Frame(index);
        }
        
        public static void SwitchToFrame(string frameNameOrId)
        {
            WebDriverFactory.Driver.SwitchTo().Frame(frameNameOrId);
        }
        
        public static void SwitchToFrame(By locator)
        {
            var frameElement = WebDriverFactory.Driver.FindElement(locator);
            WebDriverFactory.Driver.SwitchTo().Frame(frameElement);
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
            return WebDriverFactory.Driver.FindElement(locator);
        }
    }
}