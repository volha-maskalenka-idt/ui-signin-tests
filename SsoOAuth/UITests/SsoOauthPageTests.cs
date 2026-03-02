using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;
using EnvironmentManager = SsoOAuth.BaseClasses.EnvironmentManager;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SsoOauthPageTests
    {
        private SsoOAuthPageSteps _steps;
        private SoftAssert _softAssert;

        [SetUp]
        public void Setup()
        {
            WebDriverHelper.Init();
            _softAssert = new SoftAssert();
            _steps = new SsoOAuthPageSteps(_softAssert);
        }
        
        [TearDown]
        public void TearDown()
        {
            WebDriverHelper.CloseAndQuit();
        }

        [Test]
        public void LoginWithoutPassword_Should_ShowRequiredError()
        {
            var username = EnvironmentManager.GetUser("qa").Username;
            var password = "";
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();
            
            _steps.VerifyPasswordRequiredError();
            
            _softAssert.AssertAll();
        }
    }
}