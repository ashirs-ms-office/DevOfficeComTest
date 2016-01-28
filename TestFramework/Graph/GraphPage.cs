using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    /// <summary>
    /// A page on MS Graph site 
    /// </summary>
    public class GraphPage : GraphBasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement graphTitle;
        public string GraphTitle
        {
            get { return graphTitle.WrappedDriver.Title; }
        }
        public bool CanLoadImage()
        {
            IWebElement element = GraphBrowser.Driver.FindElement(By.CssSelector("article>div>div>div>div"));
            string Url = element.GetAttribute("style");
            Url = GraphBrowser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
            return GraphBrowser.ImageExist(Url);
        }

        /// <summary>
        /// The constructor method
        /// </summary>
        /// <param name="atGraphSite">Indicates whether it is during the testing of ms graph or dev.office.com</param>
        public GraphPage(bool atGraphSite)
            : base(atGraphSite)
        {
            if (atGraphSite)
            {
                GraphBrowser.Wait(By.CssSelector("head>title"));
                graphTitle = (OpenQA.Selenium.Remote.RemoteWebElement)GraphBrowser.Driver.FindElement(By.CssSelector("head>title"));
            }
            else
            {
                Browser.Wait(By.CssSelector("head>title"));
                graphTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
            }
            }
    }
}