using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class CommonActionsHelper
    {
        private static IWebDriver Driver => WebDriverFactory.Driver;

        private static WebDriverWait Wait =>
            new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

        public static void EnterText(By locator, string text)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));

            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public static void Click(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            Driver.FindElement(locator).Click();
        }

        public static bool IsElementDisplayed(By locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Driver.FindElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static string GetText(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return Driver.FindElement(locator).Text;
        }
    }
}