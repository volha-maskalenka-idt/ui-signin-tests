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
        public SsoOAuthPageSteps()
        {
            _page = new SsoOauthPage();
        }
        public void NavigateToBaseUrl()
        {
            var baseUrl = ConfigurationHelper.GetSetting("baseUrl");
            WebDriverHelper.NavigateTo(baseUrl);
        }
        
        public void EnterUsername(string username)
        {
            CommonActionsHelper.EnterText(_page.GetLocator("Username"),
                username);
        }
        
        public void EnterPassword(string password)
        {
            CommonActionsHelper.EnterText(
                _page.GetLocator("Password"), password);
        }

        public void ClickLogin()
        {
            CommonActionsHelper.Click(
                _page.GetLocator("LoginButton"));
        }

        public void ClickCancel()
        {
            CommonActionsHelper.Click(
                _page.GetLocator("CancelButton"));
        }

        public void ClickGoogleLogin()
        {
            CommonActionsHelper.Click(
                _page.GetLocator("GoogleLoginButton"));
        }

        public void VerifyPasswordRequiredError()
        {
            Assert.That(
                CommonActionsHelper.IsElementDisplayed(
                    _page.GetLocator("PasswordRequiredError")),
                Is.True,"Password required error was not displayed.");
        }

        public void VerifyUsernameRequiredError()
        {
            Assert.That(
                CommonActionsHelper.IsElementDisplayed(
                    _page.GetLocator("UsernameRequiredError")),
                Is.True,"Username required error was not displayed.");
        }
    }
}