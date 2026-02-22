using OpenQA.Selenium;

namespace SSO_OAUTH.BaseClasses
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}