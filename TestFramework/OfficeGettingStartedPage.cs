using OpenQA.Selenium;

namespace TestFramework
{
    public class OfficeGettingStartedPage : BasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement gettingStartedTitle;
        public string GettingStartedPageTitle
        {
            get { return gettingStartedTitle.WrappedDriver.Title; }
        }
        public bool CanLoadImage()
        {
            IWebElement element = Browser.Driver.FindElement(By.Id("banner-image"));
            string Url = element.GetAttribute("style");
            Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
            return Browser.ImageExist(Url);
        }

        public OfficeGettingStartedPage()
        {
            Browser.Wait(By.CssSelector("head>title"));
            gettingStartedTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
        }
    }
}