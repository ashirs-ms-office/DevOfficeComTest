using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    public class ResourcePage : BasePage
    {
        private IWebElement resourceName;
        public string ResourceName
        {
            get { return resourceName.Text; }
        }

        public ResourcePage()
        {
            try
            {
                var bannerImage = Browser.Driver.FindElement(By.Id("banner-image"));
                if (bannerImage.FindElement(By.CssSelector("div")).GetAttribute("class") == "no-description-banner-contents")
                {
                    resourceName = bannerImage.FindElement(By.CssSelector("div>div>h2"));
                }
                else
                {
                    resourceName = bannerImage.FindElement(By.CssSelector("div>div>div>h2"));
                }
            }
            catch (NoSuchElementException)
            {
                var container = Browser.Driver.FindElement(By.CssSelector("div.zone.zone-content"));
                foreach (IWebElement article in container.FindElements(By.TagName("article")))
                {
                    if (article.GetAttribute("class") == "widget-HighlightsWidget widget-content widget-highlights-widget widget")
                    {
                        resourceName = article.FindElement(By.CssSelector("div>div>div>h1"));
                        break;
                    }

                    if (article.GetAttribute("class") == "widget-AppRegistrationWidget widget-content widget-app-registration-widget widget")
                    {
                        resourceName = article.FindElement(By.CssSelector("div#register-app.card>h1"));
                        break;
                    }
                }
            }
        }
    }
}
