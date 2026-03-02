using OpenQA.Selenium;
using SsoOAuth.BaseClasses;
using SsoOAuth.Data;
using SsoOAuth.Helpers;
using SsoOAuth.Pages;

namespace SsoOAuth.Steps
{
    public class SsoOAuthPageSteps
    {
        private readonly IBasePage _page;
        private readonly SoftAssert _softAssert;

        public SsoOAuthPageSteps(SoftAssert softAssert)
        {
            _page = new SsoOAuthPage();
            _softAssert = softAssert;
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
            _softAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"PasswordRequiredError"),
                "Password required error was not displayed.");
        }

        public void VerifyUsernameRequiredError()
        {
            _softAssert.True(
                CommonActionsHelper.IsElementDisplayed(_page,"UsernameRequiredError" ),
                "Username required error was not displayed.");
        }
    }
}