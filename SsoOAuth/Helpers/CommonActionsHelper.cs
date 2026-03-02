using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class CommonActionsHelper
    {

        public static void EnterText(IBasePage page, string locatorName, string text, int timeout = 5)
        {
            var locator = page.GetLocator(locatorName);

            WebDriverHelper.CreateWait(timeout).Until(
                ExpectedConditions.ElementIsVisible(locator));

            var element = WebDriverHelper.FindElement(locator);

            element.Clear();
            element.SendKeys(text);
        }

        public static void Click(IBasePage page, string locatorName, int timeout = 5)
        {
            var locator = page.GetLocator(locatorName);

            WebDriverHelper.CreateWait(timeout).Until(
                ExpectedConditions.ElementToBeClickable(locator));

            WebDriverHelper.FindElement(locator).Click();
        }

        public static bool IsElementDisplayed(IBasePage page, string locatorName, int timeout = 5)
        {
            try
            {
                var locator = page.GetLocator(locatorName);

                WebDriverHelper.CreateWait(timeout).Until(
                    ExpectedConditions.ElementIsVisible(locator));

                return WebDriverHelper
                    .FindElement(locator)
                    .Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static string GetText(IBasePage page, string locatorName, int timeout = 5)
        {
            var locator = page.GetLocator(locatorName);

            WebDriverHelper.CreateWait(timeout).Until(
                ExpectedConditions.ElementIsVisible(locator));

            return WebDriverHelper.FindElement(locator).Text;
        }
    }
}