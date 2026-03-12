using OpenQA.Selenium;
using SsoOAuth.BaseClasses;
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
        
        public void FillField(string fieldLocator,string text)
        {
            CommonActionsHelper.EnterText(_page, fieldLocator, text);
        }

        public void Click(string locatorName)
        {
            CommonActionsHelper.Click(_page,locatorName);
        }

        public void VerifyUrlChanged()
        {
            var actual = CommonActionsHelper.IsElementDisplayed(_page, "Username", 2);
            SoftAssert.False(actual);

            var expected = ConfigurationHelper.GetSetting("baseUrl");
            var actualUrl = WebDriverHelper.GetCurrentUrl();
            SoftAssert.AreNotEqual(expected, actualUrl);
        }
        
        public void VerifyCurrentUrlIsTheSame()
        {
            var expected = ConfigurationHelper.GetSetting("baseUrl");
            var actual = WebDriverHelper.GetCurrentUrl();
            SoftAssert.AreEqual(expected, actual);
        }
        
        public void VerifyElementTextIsCorrect(string locatorName, string textOfError)
        {
            var expected = textOfError;
            var actual = CommonActionsHelper.GetText(_page, locatorName);
            SoftAssert.AreEqual(expected, actual);
        }
        
        public void VerifyElementContainsText(string locatorName, string expectedText)
        {
            var expected = expectedText;
            var actual = CommonActionsHelper.GetText(_page, locatorName);
            SoftAssert.True(actual.Contains(expected));
        }
        
        public void VerifyElementIsDisplayed(string locatorName)
        {
            var actual = CommonActionsHelper.IsElementDisplayed(_page, locatorName);
            SoftAssert.True(actual);
        }

        public void VerifyElementIsNotDisplayed(string locatorName)
        {
            var actual = CommonActionsHelper.IsElementDisplayed(_page, locatorName);
            SoftAssert.False(actual);
        }
    }
}