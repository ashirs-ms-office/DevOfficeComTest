using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class HomePage
    {

        static string Url = "http://dev.office.com";
        private static string PageTitle = "Office Dev Center - Homepage";

        [FindsBy(How = How.LinkText, Using = "Explore")]
        private IWebElement exploreLink;

        public void Goto()
        {
            Browser.Goto(Url);
        }

        public bool IsAt()
        {
            return Browser.Title == PageTitle;
        }

        public void SelectProduct(string productName)
        {
            exploreLink.Click();
            var product = Browser.Driver.FindElement(By.LinkText(productName));
            product.Click();
        }

        public bool IsAtProductPage(string productName)
        {
            var productPage = new ProductPage();
            PageFactory.InitElements(Browser.Driver, productPage);
            return productPage.ProductName == productName;

        }
    }
}