using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
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
            WebDriverFactory.Init();
            _ssoOAuthPageSteps = new SsoOAuthPageSteps();
        }
        
        [TearDown]
        public void TearDown()
        {
            WebDriverFactory.Quit();
        }

        [Test]
        public void LoginWithoutPassword_Should_ShowRequiredError()
        {
            var username = Credentials.Load("qa").username;
            var password = "";
            
            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword(password);
            _ssoOAuthPageSteps.ClickLogin();
            _ssoOAuthPageSteps.VerifyPasswordRequiredError();
        }
    }
}