using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SSO_OAUTH.Helpers
{
    public class CommonActionsHelper
    {
        protected IWebDriver Driver;

        public CommonActionsHelper(IWebDriver driver)
        {
            Driver = driver;
        }

        public void EnterText(By element, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(element));

            Driver.FindElement(element).Clear();
            Driver.FindElement(element).SendKeys(text);
        }

        public void Click(By element)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            Driver.FindElement(element).Click();
        }
        
        public bool IsElementDisplayed(By element)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            return Driver.FindElement(element).Displayed;
        }
    }
}