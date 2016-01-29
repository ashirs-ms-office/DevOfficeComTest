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

        public bool CanLoadImage(HomePageImages image)
        {
            switch (image)
            {
                case (HomePageImages.Banner):
                    IWebElement element = Browser.Driver.FindElement(By.CssSelector("#carousel>div>div"));
                    string Url = element.GetAttribute("style");
                    Url = Browser.BaseAddress + Url.Substring(Url.IndexOf('/'), Url.LastIndexOf('"') - Url.IndexOf('/'));
                    return Utility.ImageExist(Url);
                case (HomePageImages.AppAwards):
                case (HomePageImages.Hackathons):
                    // Todo
                    return false;
                default:
                    return false;
            }
        }
    }

    public enum HomePageImages
    {
        Banner,
        Hackathons,
        AppAwards
    }
}