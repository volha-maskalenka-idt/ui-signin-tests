using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Helpers
{
    public static class CommonActionsHelper
    {
        public static void WaitElementIsDisplayed(IBasePage page, string locatorName, int timeoutInSec = 5)
        {
            var app = WebDriverFactory.App;
            var locator = page.GetLocator(locatorName);
            app.Waiter(timeoutInSec).Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void EnterText(IBasePage page, string locatorName, string text, int timeoutInSec = 5)
        {
            var app = WebDriverFactory.App;
            WaitElementIsDisplayed(page, locatorName, timeoutInSec);
            var element = app.UiElement(page, locatorName)._element;
            element.Clear();
            element.SendKeys(text);
        }

        public static void Click(IBasePage page, string locatorName, int timeoutInSec = 5)
        {
            var app = WebDriverFactory.App;
            var locator = page.GetLocator(locatorName);
            WaitElementIsDisplayed(page, locatorName, timeoutInSec);
            app.UiElement(page, locatorName)._element.Click();
        }
        
        public static string GetText(IBasePage page, string locatorName, int timeoutInSec = 5)
        {
            var app = WebDriverFactory.App;
            WaitElementIsDisplayed(page, locatorName, timeoutInSec);
            return app.UiElement(page, locatorName)._element.Text;
        }

        public static bool IsElementDisplayed(IBasePage page, string locatorName, int timeoutInSec = 5)
        {
            try
            {
                WaitElementIsDisplayed(page, locatorName, timeoutInSec);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string? GetElementAttributeValue(IBasePage page, string locatorName, string attribute, int timeoutInSec = 5)
        {
            var app = WebDriverFactory.App;
            WaitElementIsDisplayed(page, locatorName, timeoutInSec);
            return app.UiElement(page, locatorName)._element.GetAttribute(attribute);
        }
        
        
    }
}