using NUnit.Framework;
using OpenQA.Selenium;
using UiTests.SignIn.Driver;
using UiTests.SignIn.Pages;
using UiTests.SignIn.Configuration;

namespace UiTests.SignIn.Tests
{
    [TestFixture]
    public class SignInTests
    {
        private IWebDriver _driver;
        private SignInPage _signInPage;

        [SetUp]
        public void Setup()
        {
            _driver = WebDriverFactory.CreateDriver();
            _driver.Navigate().GoToUrl(TestProperties.BaseUrl);

            _signInPage = new SignInPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [Test]
        public void Login_WithInvalidPassword  ()
        {
            _signInPage.EnterUsername(TestProperties.ValidUsername);
            _signInPage.EnterPassword(TestProperties.InvalidPassword);
            _signInPage.ClickLogin();
        }
    }
}