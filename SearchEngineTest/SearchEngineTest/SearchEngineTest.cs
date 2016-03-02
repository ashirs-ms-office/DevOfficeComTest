using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

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
                    WebDriver = new InternetExplorerDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/IE64/", new InternetExplorerOptions() { RequireWindowFocus=true});
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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int lowestRanking = Int32.Parse(ConfigurationManager.AppSettings["LowestRanking"]);
            int ranking = 0;
            bool isFounded = false;

            WebDriver.Navigate().GoToUrl("http://" + searchEngine + ".com");
            List<SearchedResult> searchedResults = Search(SearchSite.MSGraph);
            foreach (SearchedResult result in searchedResults)
            {
                if (ranking >= lowestRanking) break;
                if (result.DetailLink.Contains("graph.microsoft.io"))
                {
                    isFounded = true;
                    break;
                }
                ranking++;
            }
            sw.Stop();
            Assert.IsTrue(isFounded,
                "The result{0} Microsoft Graph production site on {1}. Time elapsed: {2}",
                isFounded?String.Format(" at position {0} is",ranking + 1):"s in top 5 don't contain",
                searchEngine,
                sw.Elapsed);
        }

        /// <summary>
        /// Verify whether dev.office.com site can be found on search engine
        /// </summary>
        [TestMethod]
        public void BVT_S01_TC02_CanFindOfficeDevCenterSite()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int lowestRanking = Int32.Parse(ConfigurationManager.AppSettings["LowestRanking"]);
            int ranking = 0;
            bool isFounded = false;

            WebDriver.Navigate().GoToUrl("http://" + searchEngine + ".com");
            List<SearchedResult> searchedResults = Search(SearchSite.OfficeDevCenter);
            foreach (SearchedResult result in searchedResults)
            {
                if (ranking >= lowestRanking) break;
                if (result.DetailLink.Contains("dev.office.com"))
                {
                    isFounded = true;
                    break;
                }
                ranking++;
            }
            sw.Stop();
            Assert.IsTrue(isFounded,
                "The result {0} Office Dev Center production site on {1}. Time elapsed: {2}",
                isFounded ? String.Format(" at position {0} is", ranking + 1) : "s in top 5 don't contain",
                searchEngine,
                sw.Elapsed);

        }

        /// <summary>
        /// Search production site
        /// </summary>
        /// <param name="searchSite">Production Site to search</param>
        /// <returns>Searched results</returns>
        public static List<SearchedResult> Search(SearchSite searchSite)
        {
            IReadOnlyList<IWebElement> elements = WebDriver.FindElements(By.TagName("input"));
            foreach (IWebElement element in elements)
            {
                if (element.Enabled && element.Displayed)
                {
                    string keyWord = GetDescription(searchSite);
                    element.SendKeys(keyWord + "\n");
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

        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            System.ComponentModel.DescriptionAttribute[] attributes =
                  (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(System.ComponentModel.DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
