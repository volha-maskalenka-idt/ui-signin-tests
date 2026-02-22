using OpenQA.Selenium;
using SSO_OAUTH.BaseClasses;

namespace SSO_OAUTH.Helpers
{
    public class WebDriverHelper
    {
        private IWebDriver _driver;

        public IWebDriver Driver => _driver;

        public void Init(string browser)
        {
            _driver = WebDriverFactory.CreateDriver(browser);
            _driver.Manage().Window.Maximize();
        }

        public void Quit()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}