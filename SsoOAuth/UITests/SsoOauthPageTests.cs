using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SsoOauthPageTests
    {
        private SsoOAuthPageSteps _steps;

        [SetUp]
        public void Setup()
        {
            WebDriverHelper.Init();
            _steps = new SsoOAuthPageSteps();
        }
        
        [TearDown]
        public void TearDown()
        {
            WebDriverHelper.CloseAndQuit();
        }

        [Test]
        public void ValidLogin_ShouldNavigateToAnotherPage()
        {
            var username = EnvironmentManager.GetUser("qa").Username;
            var password = EnvironmentManager.GetUser("qa").Password;
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();

            _steps.VerifyUrlChanched();
            
            SoftAssert.AssertAll();
        }

        [Test]
        public void LoginWithEmptyFields_ShouldDisplayBothRequiredErrors()
        {
            var username = "";
            var password = "";
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();

            _steps.VerifyUsernameRequiredError();
            _steps.VerifyPasswordRequiredError();
            
            SoftAssert.AssertAll();
        }
        
        [Test]
        public void LoginWithoutUsername_ShouldDisplayUsernameRequiredErrorOnly()
        {
            var username = "";
            var password = EnvironmentManager.GetUser("qa").Password;
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();
            
            _steps.VerifyUsernameRequiredError();
            _steps.VerifyPasswordRequiredErrorIsNotDisplayed();
            
            SoftAssert.AssertAll();
        }
        
        [Test]
        public void LoginWithoutPassword_ShouldDisplayPasswordRequiredErrorOnly()
        {
            var username = EnvironmentManager.GetUser("qa").Username;
            var password = "";
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();
            
            _steps.VerifyPasswordRequiredError();
            _steps.VerifyUsernameRequiredErrorIsNotDisplayed();
            
            SoftAssert.AssertAll();
        }

        [Test]
        public void LoginWithInvalidPassword_ShouldShowAuthenticationFailedError()
        {
            var username = EnvironmentManager.GetUser("qa").Username;
            var password = "1";
            
            _steps.NavigateToBaseUrl();
            _steps.EnterUsername(username);
            _steps.EnterPassword(password);
            _steps.ClickLogin();
            
            _steps.VerifyAuthenticationFailedError();
            _steps.VerifyUrlIsTheSame();
            
            SoftAssert.AssertAll();
        }

        [Test]
        public void ClickCancle_ShouldNavigateToAnotherPage()
        {
            _steps.NavigateToBaseUrl();
            _steps.ClickCancel();
            
            _steps.VerifyUrlChanched();
            
            SoftAssert.AssertAll();
        }
    }
}