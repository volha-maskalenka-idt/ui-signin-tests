using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class CommonActionsHelper
    {
        private static WebDriverWait Wait =>
            new WebDriverWait(WebDriverFactory.Driver, TimeSpan.FromSeconds(5));

        public static void EnterText(By locator, string text)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));

            var element = WebDriverFactory.Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public static void Click(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            WebDriverFactory.Driver.FindElement(locator).Click();
        }

        public static bool IsElementDisplayed(By locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return WebDriverFactory.Driver.FindElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static string GetText(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return WebDriverFactory.Driver.FindElement(locator).Text;
        }
    }
}