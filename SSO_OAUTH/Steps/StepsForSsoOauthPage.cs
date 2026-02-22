using OpenQA.Selenium;
using SSO_OAUTH.Helpers;
using SSO_OAUTH.Pages;

namespace SSO_OAUTH.Steps
{
    public class StepsForSsoOauthPage : SsoOauthPage
    {
        private readonly CommonActionsHelper _actions;

        public StepsForSsoOauthPage(IWebDriver driver)
            : base(driver)
        {
            _actions = new CommonActionsHelper(driver);
        }

        public void EnterUsername(string username)
        {
            _actions.EnterText(UsernameInput, username);
        }

        public void EnterPassword(string password)
        {
            _actions.EnterText(PasswordInput, password);
        }

        public void ClickLogin()
        {
            _actions.Click(LoginButton);
        }
        
        public void ClickCancel()
        {
            _actions.Click(CancelButton);
        }
        
        public void ClickGoogleLogin()
        {
            _actions.Click(GoogleLoginButton);
        }
        
        public bool IsPasswordRequiredErrorDisplayed()
        {
            return _actions.IsElementDisplayed(PasswordRequiredError);
        }
        
        public bool IsUsernameRequiredErrorDisplayed()
        {
            return _actions.IsElementDisplayed(UsernameRequiredError);
        }
    }
}