using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class GraphBasePage
    {
        /// <summary>
        /// The constructor method
        /// </summary>
        /// <param name="atGraphSite">Indicates whether it is during the testing of ms graph or dev.office.com</param>
        public GraphBasePage(bool atGraphSite)
        {
            if (atGraphSite)
            {
                PageFactory.InitElements(GraphBrowser.Driver, this);
            }
            else
            {
                PageFactory.InitElements(Browser.Driver, this);
            }
        }
    }
}
