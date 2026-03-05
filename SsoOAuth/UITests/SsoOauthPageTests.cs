using SsoOAuth.BaseClasses;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SsoOauthPageTests
    {
        private SsoOAuthPageSteps _ssoOAuthPageSteps;
        
        [SetUp]
        public void Setup()
        {
            WebDriverHelper.Init(); 
            _ssoOAuthPageSteps = WebDriverHelper.App.SsoOAuthPageSteps;
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

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword(password);
            _ssoOAuthPageSteps.Click("LoginButton");

            _ssoOAuthPageSteps.VerifyUrlChanged();
        }

        [Test]
        public void LoginWithEmptyFields_GlobalErrorShouldContainUsernameAndPasswordErrors()
        {
            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername("");
            _ssoOAuthPageSteps.EnterPassword("");
            _ssoOAuthPageSteps.Click("LoginButton");
            
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("UsernameRequiredGlobalError",
                "The Username field is required.");
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("PasswordRequiredGlobalError",
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyFields_ErrorsShouldPresentUnderUsernameAndPasswordFields()
        {
            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername("");
            _ssoOAuthPageSteps.EnterPassword("");
            _ssoOAuthPageSteps.Click("LoginButton");
            
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("UsernameRequiredErrorUnderField",
                "The Username field is required.");
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("PasswordRequiredErrorUnderField",
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyUsername_GlobalErrorShouldPresent()
        {
            var password = EnvironmentManager.GetUser("qa").Password;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername("");
            _ssoOAuthPageSteps.EnterPassword(password);
            _ssoOAuthPageSteps.Click("LoginButton");
            
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("UsernameRequiredGlobalError", 
                "The Username field is required.");
        }
        
        [Test]
        public void LoginWithEmptyUsername_ErrorUnderFieldShouldPresent()
        {
            var password = EnvironmentManager.GetUser("qa").Password;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername("");
            _ssoOAuthPageSteps.EnterPassword(password);
            _ssoOAuthPageSteps.Click("LoginButton");

            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("UsernameRequiredErrorUnderField", 
                "The Username field is required.");
        }
        
        [Test]
        public void LoginWithEmptyPassword_GlobalErrorShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword("");
            _ssoOAuthPageSteps.Click("LoginButton");
            
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("PasswordRequiredGlobalError", 
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyPassword_ErrorUnderFieldShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword("");
            _ssoOAuthPageSteps.Click("LoginButton");

            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("PasswordRequiredErrorUnderField", 
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithInvalidPassword_AuthenticationFailedErrorShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword("invalid");
            _ssoOAuthPageSteps.Click("LoginButton");
            
            _ssoOAuthPageSteps.VerifyElementTextIsCorrect("AuthenticationFailedError", 
                "Authentication failed. Invalid username or password.");
        }
        
        [Test]
        public void LoginWithInvalidPassword_UrlShouldRemainTheSame()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.EnterUsername(username);
            _ssoOAuthPageSteps.EnterPassword("invalid");
            _ssoOAuthPageSteps.Click("LoginButton");

            _ssoOAuthPageSteps.VerifyCurrentUrlIsTheSame();
        }
        
        [Test]
        public void ClickCancel_ShouldNavigateToAnotherPage()
        {
            _ssoOAuthPageSteps.NavigateToBaseUrl();
            _ssoOAuthPageSteps.Click("CancelButton");

            _ssoOAuthPageSteps.VerifyUrlChanged();
        }
    }
}