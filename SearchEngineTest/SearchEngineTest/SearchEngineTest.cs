using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SearchEngineTest
{
    [TestClass]
    public class SearchEngineTest
    {
        static string searchEngine = ConfigurationManager.AppSettings["SearchEngine"];
        public static IWebDriver WebDriver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            switch (ConfigurationManager.AppSettings["Browser"].ToLower())
            {
                case ("chrome"):
                    WebDriver = new ChromeDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/");
                    break;
                case ("ie32"):
                    WebDriver = new InternetExplorerDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/IE32/");
                    break;
                case ("ie64"):
                    WebDriver = new InternetExplorerDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/IE64/");
                    break;
                case ("firefox"):
                default:
                    WebDriver = new FirefoxDriver();
                    break;
            }
            int waitTime = Int32.Parse(ConfigurationManager.AppSettings["WaitTime"]);
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(waitTime));
            WebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(waitTime));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            WebDriver.Quit();
        }
        /// <summary>
        /// Verify whether Graph site can be found on search engine
        /// </summary>
        [TestMethod]
        public void BVT_S01_TC01_CanFindGraphSite()
        {
            int lowestRanking = Int32.Parse(ConfigurationManager.AppSettings["LowestRanking"]);
            int ranking = 0;
            bool isFounded = false;
            
            WebDriver.Navigate().GoToUrl("http://" + searchEngine + ".com");
            List<SearchedResult> searchedResults = Search("Microsoft Graph");
            foreach (SearchedResult result in searchedResults)
            {
                if (ranking >= lowestRanking) break;
                if (result.Name.Contains("Microsoft Graph"))
                {
                    isFounded = true;
                    WebDriver.Navigate().GoToUrl(result.DetailLink);
                    break;
                }
                ranking++;
            }
            if (!isFounded)
            {
                Assert.Fail("Cannot find the production site in top {0}", lowestRanking);
            }
            else
            {
                Assert.IsTrue(WebDriver.Title.Contains("Microsoft Graph"), "The result at position {0} should be Microsoft Graph production site", ranking + 1);
            }
        }

        /// <summary>
        /// Verify whether dev.office.com site can be found on search engine
        /// </summary>
        [TestMethod]
        public void BVT_S01_TC02_CanFindGraphSite()
        {
            int lowestRanking = Int32.Parse(ConfigurationManager.AppSettings["LowestRanking"]);
            int ranking = 0;
            bool isFounded = false;

            WebDriver.Navigate().GoToUrl("http://" + searchEngine + ".com");
            List<SearchedResult> searchedResults = Search("Office Dev Center");
            foreach (SearchedResult result in searchedResults)
            {
                if (ranking >= lowestRanking) break;
                if (result.Name.Contains("Office Dev Center"))
                {
                    isFounded = true;
                    WebDriver.Navigate().GoToUrl(result.DetailLink);
                    break;
                }
                ranking++;
            }
            if (!isFounded)
            {
                Assert.Fail("Cannot find Office Dev Center production site in top {0}", lowestRanking);
            }
            else
            {
                Assert.IsTrue(WebDriver.Title.Contains("Office Dev Center"), "The result at position {0} should be Office Dev Center production site", ranking + 1);
            }
        }

        /// <summary>
        /// Search production site
        /// </summary>
        /// <param name="searchSite">Production Site to search</param>
        /// <returns>Searched results</returns>
        public static List<SearchedResult> Search(string searchSite)
        {
            IReadOnlyList<IWebElement> elements = WebDriver.FindElements(By.TagName("input"));
            foreach (IWebElement element in elements)
            {
                if (element.Enabled && element.Displayed)
                {
                    element.SendKeys(searchSite + "\n");
                    break;
                }
            }

            List<SearchedResult> searchedResults = new List<SearchedResult>();
            IReadOnlyList<IWebElement> searchedElementResults;
            int waitTime = Int32.Parse(ConfigurationManager.AppSettings["WaitTime"]);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));

            switch (searchEngine)
            {
                case "google":
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h3>a")));
                    searchedElementResults = WebDriver.FindElements(By.CssSelector("h3>a"));
                    break;
                case "bing":
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("li>h2>a")));
                    searchedElementResults = WebDriver.FindElements(By.CssSelector("li>h2>a"));
                    break;
                default:
                    searchedElementResults = null;
                    break;
            }
            if (searchedElementResults != null)
            {
                for (int i = 0; i < searchedElementResults.Count; i++)
                {
                    SearchedResult result = new SearchedResult();
                    result.Name = searchedElementResults[i].Text;
                    result.DetailLink = searchedElementResults[i].GetAttribute("href");
                    searchedResults.Add(result);
                }
            }

            return searchedResults;
        }
    }
}
