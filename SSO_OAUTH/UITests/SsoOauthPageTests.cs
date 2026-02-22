using NUnit.Framework;
using OpenQA.Selenium;
using SSO_OAUTH.Helpers;
using SSO_OAUTH.Pages;
using SSO_OAUTH.Steps;

namespace SSO_OAUTH.Tests
{
    [TestFixture]
    public class SsoOauthPageTests
    {
        private WebDriverHelper _driverHelper;
        private StepsForSsoOauthPage _steps;

        [SetUp]
        public void Setup()
        {
            _driverHelper = new WebDriverHelper();
            _driverHelper.Init("chrome");
        }
        
        [TearDown]
        public void TearDown()
        {
            _driverHelper.Quit();
        }

        [Test]
        public void LoginWithoutPassword_Should_ShowRequiredError()
        {
            var baseUrl = FileManagerHelper.GetAppSettings().BaseUrl;
            _driverHelper.Driver.Navigate().GoToUrl(baseUrl);

            _steps = new StepsForSsoOauthPage(_driverHelper.Driver);
            
            var username = FileManagerHelper.GetEnvironmentCredentials("qa").username;
            var password = "";
            
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();

            Assert.That(_steps.IsPasswordRequiredErrorDisplayed(), Is.True);
        }
    }
}