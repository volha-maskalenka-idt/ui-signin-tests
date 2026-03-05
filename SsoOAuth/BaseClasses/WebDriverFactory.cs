using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SsoOAuth.BaseClasses;
using System.Threading;

namespace SsoOAuth.BaseClasses
{
    public static class WebDriverFactory
    {
        private static readonly ThreadLocal<IWebDriver> _driver = new();
        private static readonly ThreadLocal<Application> _app = new();

        public static IWebDriver Driver =>
            _driver.Value ?? throw new InvalidOperationException("WebDriver is not initialized.");
        
        public static Application App =>
            _app.Value ?? throw new InvalidOperationException("Application is not initialized.");

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
            _app.Value = new Application(_driver.Value);
        }

        public static void Quit()
        {
            _driver.Value?.Quit();
            _driver.Value?.Dispose();
            _driver.Value = null;
            _app.Value = null;
        }
        
        public static void Close()
        {
            _driver.Value?.Close();
        }
        
        public static ITargetLocator SwitchTo()
        {
            return Driver.SwitchTo();
        }
        
        public static void Maximize()
        {
            Driver.Manage().Window.Maximize();
        }
        
        public static INavigation Navigate()
        {
            return Driver.Navigate();
        }
        
        
    }
}