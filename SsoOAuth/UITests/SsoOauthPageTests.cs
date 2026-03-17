using SsoOAuth.BaseClasses;
using SsoOAuth.Helpers;
using SsoOAuth.Steps;

namespace SsoOAuth.Tests
{
    [TestFixture]
    public class SsoOauthPageTests
    {
        private SsoOAuthPageSteps steps;
        
        [SetUp]
        public void Setup()
        {
            WebDriverHelper.Init(); 
            steps = new SsoOAuthPageSteps();
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

            steps.NavigateToBaseUrl();
            steps.FillField("Username",username);
            steps.FillField("Password",username);
            steps.Click("LoginButton");

            steps.VerifyUrlChanged();
        }

        [Test]
        public void LoginWithEmptyFields_GlobalErrorShouldContainUsernameAndPasswordErrors()
        {
            steps.NavigateToBaseUrl();
            steps.FillField("Username","");
            steps.FillField("Password","");
            steps.Click("LoginButton");
            
            steps.VerifyElementTextIsCorrect("UsernameRequiredGlobalError",
                "The Username field is required.");
            steps.VerifyElementTextIsCorrect("PasswordRequiredGlobalError",
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyFields_ErrorsShouldPresentUnderUsernameAndPasswordFields()
        {
            steps.NavigateToBaseUrl();
            steps.FillField("Username","");
            steps.FillField("Password","");
            steps.Click("LoginButton");
            
            steps.VerifyElementTextIsCorrect("UsernameRequiredErrorUnderField",
                "The Username field is required.");
            steps.VerifyElementTextIsCorrect("PasswordRequiredErrorUnderField",
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyUsername_GlobalErrorShouldPresent()
        {
            var password = EnvironmentManager.GetUser("qa").Password;

            steps.NavigateToBaseUrl();
            steps.FillField("Username","");
            steps.FillField("Password",password);
            steps.Click("LoginButton");
            
            steps.VerifyElementTextIsCorrect("UsernameRequiredGlobalError", 
                "The Username field is required.");
        }
        
        [Test]
        public void LoginWithEmptyUsername_ErrorUnderFieldShouldPresent()
        {
            var password = EnvironmentManager.GetUser("qa").Password;

            steps.NavigateToBaseUrl();
            steps.FillField("Username","");
            steps.FillField("Password",password);
            steps.Click("LoginButton");

            steps.VerifyElementTextIsCorrect("UsernameRequiredErrorUnderField", 
                "The Username field is required.");
        }
        
        [Test]
        public void LoginWithEmptyPassword_GlobalErrorShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            steps.NavigateToBaseUrl();
            steps.FillField("Username",username);
            steps.FillField("Password","");
            steps.Click("LoginButton");
            
            steps.VerifyElementTextIsCorrect("PasswordRequiredGlobalError", 
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithEmptyPassword_ErrorUnderFieldShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            steps.NavigateToBaseUrl();
            steps.FillField("Username",username);
            steps.FillField("Password","");
            steps.Click("LoginButton");

            steps.VerifyElementTextIsCorrect("PasswordRequiredErrorUnderField", 
                "The Password field is required.");
        }
        
        [Test]
        public void LoginWithInvalidPassword_AuthenticationFailedErrorShouldPresent()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            steps.NavigateToBaseUrl();
            steps.FillField("Username",username);
            steps.FillField("Password","invalid");
            steps.Click("LoginButton");
            
            steps.VerifyElementTextIsCorrect("AuthenticationFailedError", 
                "Authentication failed. Invalid username or password.");
        }
        
        [Test]
        public void LoginWithInvalidPassword_UrlShouldRemainTheSame()
        {
            var username = EnvironmentManager.GetUser("qa").Username;

            steps.NavigateToBaseUrl();
            steps.FillField("Username",username);
            steps.FillField("Password","invalid");
            steps.Click("LoginButton");

            steps.VerifyCurrentUrlIsTheSame();
        }
        
        [Test]
        public void ClickCancel_ShouldNavigateToAnotherPage()
        {
            steps.NavigateToBaseUrl();
            steps.Click("CancelButton");

            steps.VerifyUrlChanged();
        }
    }
}