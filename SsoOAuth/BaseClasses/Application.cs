using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SsoOAuth.BaseClasses
{
    public class Application
    {
        private readonly IWebDriver _driver;

        public Application(IWebDriver driver)
        {
            _driver = driver;
        }

        public WebDriverWait Waiter(int timeoutInSec = 5)
        {
            return new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSec));
        }

        public UiElement UiElement(IBasePage page, string locatorName)
        {
            var locator = page.GetLocator(locatorName);
            return new UiElement(_driver.FindElement(locator));
        }
    }
}