using OpenQA.Selenium;
using SSO_OAUTH.BaseClasses;

namespace SSO_OAUTH.Pages
{
    public class SsoOauthPage : BasePage
    {
        public SsoOauthPage(IWebDriver driver) : base(driver)
        {
        }

        [UiName("Username")] private static By UsernameInputLocator => By.Id("Username");

        [UiName("Password")] private static By PasswordInputLocator => By.Id("Password");

        [UiName("Login Button")] private static By LoginButtonLocator => By.CssSelector("button[value=login]");

        [UiName("Cancel Button")] private static By CancelButtonLocator => By.CssSelector("button[value=cancel]");

        [UiName("Google Login Button")] private static By GoogleLoginButtonLocator => By.CssSelector("img[alt=Google]");
        
        [UiName("Username Required Error")]
        private static By UsernameRequiredErrorLocator =>
            By.XPath("//div[contains(@class,'alert-danger')]//li[contains(text(),'Username')]");

        [UiName("Password Required Error")]
        private static By PasswordRequiredErrorLocator =>
            By.XPath("//div[contains(@class,'alert-danger')]//li[contains(text(),'Password')]");

        protected By UsernameInput => UsernameInputLocator;
        protected By PasswordInput => PasswordInputLocator;
        protected By LoginButton => LoginButtonLocator;
        protected By CancelButton => CancelButtonLocator;
        protected By GoogleLoginButton => GoogleLoginButtonLocator;
        protected By UsernameRequiredError => UsernameRequiredErrorLocator;
        protected By PasswordRequiredError => PasswordRequiredErrorLocator;
    }
}