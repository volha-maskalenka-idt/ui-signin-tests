using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SsoOauthPageTests
    {
        private SsoOAuthPageSteps _ssoOAuthPageSteps;

        [SetUp]
        public void Setup()
        {
            WebDriverHelper.Init();
            _ssoOAuthPageSteps = new SsoOAuthPageSteps();
        }
        
        [TearDown]
        public void TearDown()
        {
            WebDriverHelper.Quit();
        }

        [Test]
        public void LoginWithoutPassword_Should_ShowRequiredError()
        {
            var username = EnvironmentManager.Load("qa").username;
            var password = "";
            
            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword(password);
            _ssoOAuthPageSteps.ClickLogin();
            _ssoOAuthPageSteps.VerifyPasswordRequiredError();
        }
    }
}