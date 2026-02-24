using OpenQA.Selenium;  
using SsoOAuth.BaseClasses;

namespace SsoOAuth.Pages
{
    public class SsoOauthPage : IBasePage
    {

        [UiName("Username")] 
        private static By UsernameInputLocator => By.Id("Username");

        [UiName("Password")] 
        private static By PasswordInputLocator => By.Id("Password");

        [UiName("LoginButton")] 
        private static By LoginButtonLocator => By.CssSelector("button[value=login]");

        [UiName("CancelButton")] 
        private static By CancelButtonLocator => By.CssSelector("button[value=cancel]");

        [UiName("GoogleLoginButton")] 
        private static By GoogleLoginButtonLocator => By.CssSelector("img[alt=Google]");
        
        [UiName("UsernameRequiredError")]
        private static By UsernameRequiredErrorLocator =>
            By.XPath("//div[contains(@class,'alert-danger')]//li[contains(text(),'Username')]");

        [UiName("PasswordRequiredError")]
        private static By PasswordRequiredErrorLocator =>
            By.XPath("//div[contains(@class,'alert-danger')]//li[contains(text(),'Password')]");
    }
}