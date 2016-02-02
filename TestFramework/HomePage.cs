using System;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class HomePage : BasePage
    {
        private static string PageTitle = "Office Dev Center - Homepage";

        [FindsBy(How = How.LinkText, Using = "Explore")] private IWebElement exploreLink;
      
        public bool IsAt()
        {
            return Browser.Title == PageTitle;
        }

        public bool CanLoadImages(HomePageImages image)
        {
            switch (image)
            {
                case (HomePageImages.Banner):
                    var elements = Browser.Driver.FindElements(By.CssSelector("#carousel>div>div.item"));
                    foreach (IWebElement item in elements)
                    {
                        string Url = item.GetAttribute("style");
                        Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
                        if (!Utility.ImageExist(Url))
                        {
                            return false;
                        }
                    }

                    return true;
                case (HomePageImages.Icons):
                    elements = Browser.Driver.FindElements(By.CssSelector("#quarter1 > div > article > div > div.quicklinks-hidden-xs > div > ol > li"));
                    foreach (IWebElement item in elements)
                    {
                        IWebElement subItem = item.FindElement(By.CssSelector("a>div>div>div"));
                        string Url = subItem.GetAttribute("style");
                        Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
                        if (!Utility.ImageExist(Url))
                        {
                            return false;
                        }
                    }

                    return true;
                default:
                    return false;
            }
        }

        public bool CanDisplayCorrectTradeMark()
        {
            IWebElement element = Browser.Driver.FindElement(By.CssSelector("#layout-footer > div > div > div > div.clearfix > div > div > div.col-xs-6.col-md-12.col-lg-12.visible-md.visible-lg.privacy-links > ul > li"));
            return element.Text.Contains(DateTime.Now.Year.ToString());
        }
    }

    public enum HomePageImages
    {
        Banner,
        Icons
    }
}