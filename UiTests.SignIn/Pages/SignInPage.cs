using OpenQA.Selenium;
using UiTests.SignIn.Base;
using UiTests.SignIn.Locators;

namespace UiTests.SignIn.Pages
{
    public class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public void EnterUsername(string username)
        {
            Type(SignInLocators.UsernameInput, username);
        }

        public void EnterPassword(string password)
        {
            Type(SignInLocators.PasswordInput, password);
        }

        public void ClickLogin()
        {
            Click(SignInLocators.LoginButton);
        }
        
        public void ClickCancle () {
            Click(SignInLocators.CancleButton);
        }

        public void ClickGoogleLogin()
        {
            Click(SignInLocators.GoogleLoginButton);
        }
    }
}