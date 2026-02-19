using OpenQA.Selenium;

namespace UiTests.SignIn.Locators
{
    public static class SignInLocators
    {
        public static By UsernameInput = By.Id("Username");

        public static By PasswordInput = By.Id("Password");

        public static By LoginButton = By.CssSelector("button[value=login]");
        
        public static By CancleButton = By.CssSelector("button[value=cancel]");
        
        public static By GoogleLoginButton = By.CssSelector("img[alt=Google]");
    }
}