using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class WebDriverHelper
    {
        public static Application App => WebDriverFactory.App;
        public static void Init()
        {
            WebDriverFactory.Init();
        }
        public static void CloseAndQuit()
        {
            WebDriverFactory.Close();
            WebDriverFactory.Quit();
        }
        
        public static void NavigateTo(string url)
        {
            WebDriverFactory.Navigate().GoToUrl(url);
        }

        public static void NavigateBack()
        {
            WebDriverFactory.Navigate().Back();
        }
        
        public static void Refresh()
        {
            WebDriverFactory.Navigate().Refresh();
        }
        
        public static string GetCurrentUrl()
        {
            return WebDriverFactory.Driver.Url;
        }

        public static string GetTitle()
        {
            return WebDriverFactory.Driver.Title;
        }
        
        public static void Maximize()
        {
            WebDriverFactory.Maximize();
        }
        
        public static void SwitchToFrame(int index)
        {
            WebDriverFactory.SwitchTo().Frame(index);
        }

        public static void SwitchToFrame(string frameNameOrId)
        {
            WebDriverFactory.SwitchTo().Frame(frameNameOrId);
        }

        public static void SwitchToFrame(By locator)
        {
            WebDriverFactory.SwitchTo().Frame(WebDriverFactory.Driver.FindElement(locator));
        }

        public static void SwitchToDefaultContent()
        {
            WebDriverFactory.SwitchTo().DefaultContent();
        }

        public static void SwitchToWindow(string windowHandle)
        {
            WebDriverFactory.SwitchTo().Window(windowHandle);
        }

        public static void SwitchToAlert()
        {
            WebDriverFactory.SwitchTo().Alert();
        }
    }
}