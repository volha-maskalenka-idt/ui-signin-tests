using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SSO_OAUTH.BaseClasses
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    return new ChromeDriver();

                case "firefox":
                    return new FirefoxDriver();
                
                case "edge":
                    return new EdgeDriver();

                default:
                    throw new ArgumentException($"Browser '{browser}' is not supported.");
            }
        }
    }
}