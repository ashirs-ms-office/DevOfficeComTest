using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class Navigation
    {
        [FindsBy(How = How.LinkText, Using = "Explore")]
        private IWebElement exploreLinkElement;

        [FindsBy(How = How.LinkText, Using = "Resources")]
        private IWebElement resourceLinkElement;

        [FindsBy(How = How.LinkText, Using = "Getting Started")]
        private IWebElement gettingstartedLinkElement;

        [FindsBy(How = How.LinkText, Using = "Code Samples")]
        private IWebElement codesamplesLinkElement;

        [FindsBy(How = How.LinkText, Using = "Documentation")]
        private IWebElement documentationLinkElement;
     
        public void Select(string menuName, string itemName)
        {
            switch (menuName)
            {
                case("Explore"):
                    exploreLinkElement.Click();
                    break;
                case("Resources"):
                    resourceLinkElement.Click();
                    break;
                case("Getting Started"):
                    gettingstartedLinkElement.Click();
                    break;
                case("Code Samples"):
                    codesamplesLinkElement.Click();
                    break;
                case("Documentaion"):
                    documentationLinkElement.Click();
                    break;
                default:
                    break;
            }
            
            var item = Browser.Driver.FindElement(By.LinkText(itemName));
            item.Click();
        }

        public bool IsAtProductPage(string productName)
        {
            var productPage = new ProductPage();
            PageFactory.InitElements(Browser.Driver, productPage);
            return productPage.ProductName == productName;
        }

        public bool IsAtResourcePage(string resourceName)
        {
            //To do
            return true;
        }
    }
}