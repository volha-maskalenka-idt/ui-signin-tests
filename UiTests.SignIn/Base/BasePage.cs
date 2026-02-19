using OpenQA.Selenium;

namespace UiTests.SignIn.Base
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected void Click(By locator)
        {
            Driver.FindElement(locator).Click();
        }

        protected void Type(By locator, string text)
        {
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return Driver.FindElement(locator).Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            return Driver.FindElement(locator).Displayed;
        }
    }
}