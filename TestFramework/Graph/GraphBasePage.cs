using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class GraphBasePage
    {
        public GraphBasePage()
        {
            PageFactory.InitElements(GraphBrowser.Driver, this);
        }
    }
}
