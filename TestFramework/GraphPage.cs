using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class GraphPage : BasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement graphTitle;
        public string GraphTitle
        {
            get { return graphTitle.WrappedDriver.Title; }
        }
        public bool CanLoadImage()
        {
            IWebElement element = Browser.Driver.FindElement(By.CssSelector("article>div>div>div>div"));
            string Url = element.GetAttribute("style");
            Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
            return Browser.ImageExist(Url);
        }

        public GraphPage()
        {
            graphTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
        }
    }
}