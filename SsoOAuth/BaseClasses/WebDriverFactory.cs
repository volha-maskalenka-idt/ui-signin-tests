using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SsoOAuth.Data;
using System.Threading;

namespace SsoOAuth.BaseClasses
{
    public static class WebDriverFactory
    {
        private static ThreadLocal<IWebDriver> _driver = new();

        public static IWebDriver Driver =>
            _driver.Value ?? throw new InvalidOperationException("WebDriver is not initialized.");

        public static void Init()
        {
            var browser = ConfigurationHelper.GetSetting("browser");

            switch (browser.ToLower())
            {
                case "chrome":
                    _driver.Value = new ChromeDriver();
                    break;

                case "firefox":
                    _driver.Value = new FirefoxDriver();
                    break;

                case "edge":
                    _driver.Value = new EdgeDriver();
                    break;

                default:
                    throw new ArgumentException($"Browser '{browser}' is not supported.");
            }

            _driver.Value.Manage().Window.Maximize();
        }

        public static void Quit()
        {
            _driver.Value?.Quit();
            _driver.Value?.Dispose();
            _driver.Value = null;
        }
    }
}