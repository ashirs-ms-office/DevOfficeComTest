using System;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public class GraphHomePage : GraphBasePage
    {
        private static string PageTitle = "Microsoft Graph - Home";

        [FindsBy(How = How.LinkText, Using = "Explore")]
        private IWebElement exploreLink;

        public bool IsAt()
        {
            return GraphBrowser.Title == PageTitle;
        }
    }
}