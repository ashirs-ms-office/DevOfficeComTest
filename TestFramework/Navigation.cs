using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class Navigation : BasePage
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
                    Browser.Click(exploreLinkElement);
                    break;
                case("Resources"):
                    Browser.Click(resourceLinkElement);
                    break;
                case("Getting Started"):
                    Browser.Click(gettingstartedLinkElement);
                    break;
                case("Code Samples"):
                    Browser.Click(codesamplesLinkElement);
                    break;
                case("Documentaion"):
                    Browser.Click(documentationLinkElement);
                    break;
                default:
                    break;
            }
            
            var item = Browser.Driver.FindElement(By.LinkText(itemName));
            Browser.Click(item);
        }

        public bool IsAtProductPage(string productName)
        {
            var productPage = new ProductPage();
            return productPage.ProductName == productName;
        }

        public bool IsAtResourcePage(string resourceName)
        {
            var resourcePage = new ResourcePage();
            return resourcePage.ResourceName == resourceName;
        }
    }
}