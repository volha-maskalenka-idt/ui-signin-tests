using OpenQA.Selenium;

namespace SsoOAuth.BaseClasses
{
    public class UiElement
    {
        public IWebElement _element;

        public UiElement(IWebElement element)
        {
            _element = element;
        }
    }
}