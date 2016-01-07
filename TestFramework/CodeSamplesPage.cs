using OpenQA.Selenium;

namespace TestFramework
{
    public class CodeSamplesPage : BasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement codeSamplesTitle;
        public string CodeSamplesPageTitle
        {
            get { return codeSamplesTitle.WrappedDriver.Title; }
        }
        public bool CanLoadImage()
        {
            IWebElement element = Browser.Driver.FindElement(By.Id("banner-image"));
            string Url = element.GetAttribute("style");
            Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
            return Browser.ImageExist(Url);
        }

        public CodeSamplesPage()
        {
            Browser.Wait(By.CssSelector("head>title"));
            codeSamplesTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
        }
    }
}