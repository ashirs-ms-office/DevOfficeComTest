using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace TestFramework
{
    public class GraphNavigation : GraphBasePage
    {
        public GraphNavigation()
            : base(true)
        { }
        [FindsBy(How = How.LinkText, Using = "Home")]
        private IWebElement homeLinkElement;

        [FindsBy(How = How.LinkText, Using = "Get started")]
        private IWebElement getstartedLinkElement;

        [FindsBy(How = How.LinkText, Using = "Documentation")]
        private IWebElement documentationLinkElement;

        [FindsBy(How = How.XPath, Using = "//ul[@class='nav navbar-nav']/li/a[contains(@href,'graphexplorer2.azurewebsites.net')]")]
        private IWebElement exploreLinkElement;

        [FindsBy(How = How.XPath, Using = "//ul[@class='nav navbar-nav']/li/a[contains(@href,'/app-registration')]")]
        private IWebElement appregistrationLinkElement;

        [FindsBy(How = How.LinkText, Using = "Samples & SDKs")]
        private IWebElement samplesandsdksLinkElement;

        [FindsBy(How = How.LinkText, Using = "Changelog")]
        private IWebElement changelogLinkElement;
        
        public void Select(string menuName)
        {
            switch (menuName)
            {
                case ("Home"):
                    GraphBrowser.Click(homeLinkElement);
                    break;
                case ("Get started"):
                    GraphBrowser.Click(getstartedLinkElement);
                    break;
                case ("Documentation"):
                    GraphBrowser.Click(documentationLinkElement);
                    break;
                case ("Graph explorer"):
                    GraphBrowser.Click(exploreLinkElement);
                    break;
                case ("App registration"):
                    GraphBrowser.Click(appregistrationLinkElement);
                    break;
                case ("Samples & SDKs"):
                    GraphBrowser.Click(samplesandsdksLinkElement);
                    break;
                case ("Changelog"):
                    GraphBrowser.Click(changelogLinkElement);
                    break;
               default:
                    break;
            }
        }

        /// <summary>
        /// Verify whether the current graph page has the specific title
        /// </summary>
        /// <param name="graphTitle">The expected page title</param>
        /// <returns>True if yes, else no.</returns>
        public bool IsAtGraphPage(string graphTitle)
        {
            var graphPage = new GraphPage(true);
            string title = graphPage.GraphTitle.Replace(" ", "");

            GraphBrowser.GoBack();
            return title.Contains(graphTitle.Replace(" ", ""));
        }
    }
}