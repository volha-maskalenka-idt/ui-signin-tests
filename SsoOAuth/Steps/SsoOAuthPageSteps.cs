using OpenQA.Selenium;
using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
using SsoOAuth.Helpers;
using SsoOAuth.Pages;

namespace SsoOAuth.Steps
{
    public class SsoOAuthPageSteps
    {
        private readonly SsoOAuthPage _page;

        public SsoOAuthPageSteps()
        {
            _page = new SsoOAuthPage();
        }
        public void NavigateToBaseUrl()
        {
            var baseUrl = ConfigurationHelper.GetSetting("baseUrl");
            WebDriverHelper.NavigateTo(baseUrl);
        }
        
        public void EnterUsername(string username)
        {
            CommonActionsHelper.EnterText(_page, "Username", username);
        }
        
        public void EnterPassword(string password)
        {
            CommonActionsHelper.EnterText(_page, "Password", password);
        }

        public void ClickLogin()
        {
            CommonActionsHelper.Click(_page,"LoginButton");
        }

        public void ClickCancel()
        {
            CommonActionsHelper.Click(_page,"CancelButton");
        }

        public void ClickGoogleLogin()
        {
            CommonActionsHelper.Click(_page,"GoogleLoginButton");
        }

        public void VerifyPasswordRequiredError()
        {
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"PasswordRequiredError"),
                "Password required error was not displayed.");
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"PasswordRequiredErrorUnderField" ),
                "Username required error under field was not displayed.");
        }
        
        public void VerifyPasswordRequiredErrorIsNotDisplayed()
        {
            SoftAssert.False(
                CommonActionsHelper.IsElementDisplayed(_page,"PasswordRequiredError"),
                "Password required error was not displayed.");
            SoftAssert.False(
                CommonActionsHelper.IsElementDisplayed(_page,"PasswordRequiredErrorUnderField" ),
                "Username required error under field was not displayed.");
        }

        public void VerifyUsernameRequiredError()
        {
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"UsernameRequiredError" ),
                "Username required error was not displayed.");
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"UsernameRequiredErrorUnderField" ),
                "Username required error under field was not displayed.");
        }
        
        public void VerifyUsernameRequiredErrorIsNotDisplayed()
        {
            SoftAssert.False(
                CommonActionsHelper.IsElementDisplayed(_page,"UsernameRequiredError" ),
                "Username required error was not displayed.");
            SoftAssert.False(
                CommonActionsHelper.IsElementDisplayed(_page,"UsernameRequiredErrorUnderField" ),
                "Username required error under field was not displayed.");
        }

        public void VerifySinginDropdownIsDisplayed()
        {
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page, "SingInDropdown"),
                "SingIn dropdown was not displayed.");
        }
        
        public void VerifySinginDropdownIsNotDisplayed()
        {
            SoftAssert.False(
                CommonActionsHelper.IsElementDisplayed(_page, "SingInDropdown"),
                "SingIn dropdown is displayed.");
        }

        public void VerifyUrlIsTheSame()
        {
            SoftAssert.AreEqual(
                WebDriverHelper.GetCurrentUrl(), ConfigurationHelper.GetSetting("baseUrl"),
                "Invalid Url");
        }
        
        public void VerifyUrlChanched()
        {
            SoftAssert.False(CommonActionsHelper.IsElementDisplayed(_page, "Username"),
                "Url still visible");
            SoftAssert.AreNotEqual(
                WebDriverHelper.GetCurrentUrl(), ConfigurationHelper.GetSetting("baseUrl"),
                "Url didn't change");
        }
        

        public void VerifyAuthenticationFailedError()
        {
            SoftAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"AuthenticationFailedError" ),
                "Authentication failed error was not displayed.");
        }
    }
}