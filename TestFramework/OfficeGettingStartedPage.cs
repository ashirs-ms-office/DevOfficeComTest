using OpenQA.Selenium;
using System;

namespace TestFramework
{
    public class OfficeGettingStartedPage : BasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement gettingStartedTitle;
        public string GettingStartedPageTitle
        {
            get { return gettingStartedTitle.WrappedDriver.Title; }
        }

        public void O365GetStarted()
        {
            if (!Browser.Url.Contains("/getting-started"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started");
            }

            var o365GetStarted = Browser.Driver.FindElement(By.CssSelector("a title=\"Getting Started with Office 365 APIs\""));
            Browser.Click(o365GetStarted);

            // When the card to choose platform is displayed, the click event can be considered as finished.
            Browser.Wait(By.Id("setup"));
        }

        public void OfficeAddInGetStarted()
        {
            if (!Browser.Url.Contains("/getting-started"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started");
            }

            var o365GetStarted = Browser.Driver.FindElement(By.CssSelector("a title=\"Getting Started with Office Add-ins\""));
            Browser.Click(o365GetStarted);

            // When the card to choose product is displayed, the click event can be considered as finished.
            Browser.Wait(By.Id("selectapp"));
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
            gettingStartedTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
        }
    }
}