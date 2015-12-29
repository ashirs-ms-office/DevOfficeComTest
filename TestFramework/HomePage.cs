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
            // should rewrite this case as use DOM element's URL to do the HTTP request, instead of pre-saved dictionary
            Dictionary<HomePageImages, string> homePageImagesPaths = new Dictionary<HomePageImages, string>();
            homePageImagesPaths.Add(HomePageImages.Banner, Browser.BaseAddress+"/Media/Default/Banners/Banners_300x1900/get-started-banner.png");

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(homePageImagesPaths[image]);
            request.Timeout = 15000;
            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
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