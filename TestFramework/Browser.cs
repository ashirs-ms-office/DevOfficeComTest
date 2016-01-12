using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public static class Browser
    {
        //static IWebDriver webDriver = new InternetExplorerDriver();
        static IWebDriver webDriver = new ChromeDriver();
        static string defaultTitle;
        static string defaultHandle = webDriver.CurrentWindowHandle;
        
        /// <summary>
        /// Some typical search text
        /// </summary>
        public static readonly string[] typicalSearchText = new string[] { "Office 365", "API", "SharePoint", "Add-in", "Property Manager", "ios", "OneDrive" };
        
        public static string BaseAddress
        {
            get { return "http://officedevcenter-msprod-standby.azurewebsites.net"; }
            //get { return "http://officedevcentersite-orchard.azurewebsites.net"; }
            //get { return "http://localhost"; }
        }

        public static void Initialize()
        {
            SetWaitTime(TimeSpan.FromSeconds(15));
            webDriver.Navigate().GoToUrl(BaseAddress);
            defaultTitle = Title;
        }

        public static void Goto(string url)
        {
            webDriver.Navigate().GoToUrl(url);
            defaultTitle = Title;
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static string Url
        {
            get { return webDriver.Url; }
        }

        public static void Close()
        {
            webDriver.Quit();
        }

        public static SelectElement SelectElement(IWebElement webElement)
        {
            return new SelectElement(webElement);
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            // need to replace with Framework wait methods: implicit wait, explicit wait
            Thread.Sleep((int)timeSpan.TotalSeconds * 1000);
        }

        public static void Wait(By by)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementExists(by));
        }

        public static void SetWaitTime(TimeSpan timeSpan)
        {
            webDriver.Manage().Timeouts().ImplicitlyWait(timeSpan);
            webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(timeSpan.TotalSeconds * 2));
            webDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(timeSpan.TotalSeconds * 2));
        }

        public static bool SwitchToWindow(string title)
        {
            webDriver.SwitchTo().DefaultContent();

            // get all window handles
            IList<string> handlers = webDriver.WindowHandles;
            foreach (var winHandler in handlers)
            {
                webDriver.SwitchTo().Window(winHandler);
                if (webDriver.Title.Contains(title))
                {
                    return true;
                }
                else
                {
                    webDriver.SwitchTo().DefaultContent();
                }
            }

            return false;
        }

        public static bool SwitchToNewWindow()
        {
            // get all window handles
            IList<string> handlers = webDriver.WindowHandles;
            string newWindowHandle = string.Empty;
            foreach (var winHandler in handlers)
            {
                if (!winHandler.Equals(webDriver.CurrentWindowHandle))
                {
                    newWindowHandle = winHandler;
                    break;
                }
            }

            if (!newWindowHandle.Equals(string.Empty))
            {
                webDriver.SwitchTo().Window(newWindowHandle);
                return true;
            }
            else
            {
                webDriver.SwitchTo().DefaultContent();
                return false;
            }
        }

        public static bool SwitchBack()
        {
            //bool canSwitchBack = SwitchToWindow(defaultTitle);
            //defaultTitle = Title;
            //return canSwitchBack;
            if (!webDriver.CurrentWindowHandle.Equals(defaultHandle))
            {
                webDriver.Close();
                webDriver.SwitchTo().Window(defaultHandle);
                return true;
            }
            else
            {
                webDriver.SwitchTo().DefaultContent();
                return false;
            }
        }

        public static void GoBack()
        {
            if (!Title.Equals(defaultTitle))
            {
                webDriver.Navigate().Back();
            }
            else
            {
                webDriver.Navigate().Refresh();
            }
        }

        public static IWebElement FindElementInFrame(string frameIdOrName, By by)
        {
            IWebElement frame = FindFrame(frameIdOrName);
            if (frame != null)
            {
                webDriver.SwitchTo().Frame(frame);
                IWebElement element = webDriver.FindElement(by);
                webDriver.SwitchTo().DefaultContent();
                return element;
            }
            else
            {
                throw new NoSuchFrameException(string.Format("Cannot find frame with name {0}", frameIdOrName));
            }
        }

        public static IWebElement FindElement(By by, bool isRootElement = true)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                IList<IWebElement> frames = webDriver.FindElements(By.TagName("iframe"));
                IWebElement element = null;
                if (frames.Count > 0)
                {
                    for (int i = 0; i < frames.Count; i++)
                    {
                        webDriver.SwitchTo().Frame(frames[i]);
                        element = FindElement(by, false);
                        if (element != null)
                        {
                            if (isRootElement)
                            {
                                webDriver.SwitchTo().DefaultContent();
                            }

                            return element;
                        }
                    }
                }

                if (element == null)
                {
                    webDriver.SwitchTo().ParentFrame();
                }

                return element;
            }
        }

        public static void Click(IWebElement element)
        {
            (webDriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);
        }

        public static void SaveScreenShot(string PathAndFileName)
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)webDriver;
            Screenshot s = screenshot.GetScreenshot();
            s.SaveAsFile(PathAndFileName, System.Drawing.Imaging.ImageFormat.Png);

            //Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
            //string screenshot = ss.AsBase64EncodedString;
            //byte[] screenshotAsByteArray = ss.AsByteArray;

            // Save the screenshot
            //ss.SaveAsFile(PathAndFileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static bool ImageExist(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 15000;
            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static IWebElement FindFrame(string frameIdOrName)
        {
            IList<IWebElement> frames = webDriver.FindElements(By.TagName("iframe"));
            IWebElement frame = null;
            if (frames.Count > 0)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    if (frames[i].GetAttribute("id") == frameIdOrName || frames[i].GetAttribute("name") == frameIdOrName)
                    {
                        frame = frames[i];
                        return frame;
                    }
                    else
                    {
                        webDriver.SwitchTo().Frame(frames[i]);
                        frame = FindFrame(frameIdOrName);
                        if (frame != null)
                        {
                            return frame;
                        }
                    }
                }
            }

            if (frame == null)
            {
                webDriver.SwitchTo().ParentFrame();
            }

            return frame;
        }

        /// <summary>
        /// Verify if none of the filters is checked
        /// </summary>
        /// <param name="unclearedFilters">The name of the uncleared filters</param>
        /// <returns>True if yes, else no.</returns>
        public static bool areFiltersCleared(out List<string> unclearedFilters)
        {
            IReadOnlyList<IWebElement> elements = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"));
            unclearedFilters = new List<string>();
            foreach (IWebElement element in elements)
            {
                if (element.GetAttribute("type").Equals("checkbox") && element.GetAttribute("checked") != null && element.GetAttribute("checked").Equals("checked"))
                {
                    unclearedFilters.Add(element.GetAttribute("value"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verify whether the url contains the chosen filters
        /// </summary>
        /// <param name="filterNames">The chosen filters</param>
        /// <returns>True if yes, else no.</returns>
        public static bool AreFiltersInURL(List<string> filterNames, out List<string> unContainedFilters)
        {
            bool allContained = true;
            unContainedFilters = new List<string>();
            foreach (string filterName in filterNames)
            {
                if (!Browser.Url.Replace("%20", " ").ToLower().Contains(filterName.ToLower()))
                {
                    allContained = false;
                    unContainedFilters.Add(filterName);
                }
            }
            return allContained;
        }

        /// <summary>
        /// Get the count of filters
        /// </summary>
        public static int GetFilterCount()
        {
            return Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]")).Count;
        }

        /// <summary>
        /// Check whether a filter has an "ng-click" attribute with an updating seleted result function's name.
        /// </summary>
        /// <param name="index">The index of the filter type to select</param>
        /// <returns>True if yes, else no.</returns>
        public static bool isFilterWorkable(int index)
        {
            var element = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"))[index];
            return element.GetAttribute("ng-click").Contains("updateSelectedTypes(");
        }

        /// <summary>
        /// Set the displayed results' sort order
        /// </summary>
        /// <param name="sortType">Specifies which sort type to use</param>
        /// <param name="isDescendent">Specifies whether the results are sorted descendently. True means yes, False means no</param>
        public static void SetSortOrder(SortType sortType, bool isDescendent)
        {
            string typeString;
            if (sortType.Equals(SortType.ViewCount))
            {
                typeString = "orderByViews()";
            }
            else
            {
                typeString = "orderByDate()";
            }

            var sortElement = Browser.Driver.FindElement(By.XPath("//a[@ng-click='" + typeString + "']"));
            if (!sortElement.Selected)
            {
                sortElement.Click();
            }

            string orderString = isDescendent ? "sort_down" : "sort_up";
            if (!sortElement.FindElement(By.ClassName("sort-icon")).GetAttribute("src").Contains(orderString))
            {
                sortElement.Click();
            }
        }

        /// <summary>
        /// Clear the filters 
        /// </summary>
        public static void ExecuteClearFilters()
        {
            var element = Browser.FindElement(By.CssSelector(".clearfilters.filter-button"));
            Browser.Click(element);
        }

        /// <summary>
        /// Set a search text
        /// </summary>
        /// <param name="value">The value to set</param>
        public static void InputSearchString(string value)
        {
            var inputElement = Browser.Driver.FindElement(By.XPath(@"//input[@ng-model=""searchText""]"));
            inputElement.Clear();
            inputElement.SendKeys(value);
        }

        /// <summary>
        /// Returns the filtered traings
        /// </summary>
        /// <param name="searchString">The search text to use</param>
        /// <returns>The search result list. Each result contains the training title and description</returns>
        public static List<SearchedResult> GetFilterResults(string searchString = "")
        {
            InputSearchString(searchString);
            List<SearchedResult> resultList = new List<SearchedResult>();

            var nextElement = Browser.Driver.FindElement(By.ClassName("next-link"));
            bool shouldReadFirstPage = true;
            do
            {
                if (shouldReadFirstPage)
                {
                    shouldReadFirstPage = false;
                }
                else
                {
                    Browser.Click(nextElement);
                }
                var uList = Browser.Driver.FindElement(By.CssSelector(@"#OrderedResults+ul"));
                IReadOnlyList<IWebElement> listItems = uList.FindElements(By.XPath("li"));
                for (int i = 0; i < listItems.Count; i++)
                {
                    SearchedResult resultInfo = new SearchedResult();
                    resultInfo.Name = listItems[i].GetAttribute("aria-label");

                    var descriptionElement = listItems[i].FindElement(By.ClassName("description"));
                    resultInfo.Description = descriptionElement.Text;

                    resultInfo.ViewCount = Convert.ToInt64((listItems[i].FindElement(By.XPath("//span[contains(text(),' views')]")).GetAttribute("innerHTML").Split(' '))[0]);
                    resultList.Add(resultInfo);

                    IWebElement updatedDateElement;
                    try
                    {
                        updatedDateElement = listItems[i].FindElement(By.CssSelector(".date-updated"));
                    }
                    catch (NoSuchElementException)
                    {
                        updatedDateElement = null;
                    }
                    if (updatedDateElement != null)
                    {
                        resultInfo.UpdatedDate = DateTime.Parse(updatedDateElement.Text.Replace("Updated ", null));
                    }
                }
            } while (nextElement.Displayed);

            return resultList;
        }

        /// <summary>
        /// Choose a filter type
        /// </summary>
        /// <param name="index">The index of the filter type to select</param>
        /// <returns>The selected filter name</returns>
        public static string SelectFilter(int index)
        {
            var element = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"))[index];
            Browser.Click(element);

            return element.Text;
        }
    }
}