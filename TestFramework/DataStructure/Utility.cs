using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    /// <summary>
    /// Static class for common functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Some typical search text
        /// </summary>
        public static readonly string[] TypicalSearchText = new string[] { "Office 365", "API", "SharePoint", "Add-in", "Property Manager", "ios", "OneDrive" };

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

            var nextElement = Browser.FindElement(By.ClassName("next-link"));
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
                var uList = Browser.FindElement(By.CssSelector(@"#OrderedResults+ul"));
                IReadOnlyList<IWebElement> listItems = uList.FindElements(By.XPath("li"));
                for (int i = 0; i < listItems.Count; i++)
                {
                    SearchedResult resultInfo = new SearchedResult();
                    resultInfo.Name = listItems[i].GetAttribute("aria-label");

                    var descriptionElement = listItems[i].FindElement(By.ClassName("description"));
                    resultInfo.Description = descriptionElement.Text;

                    resultInfo.ViewCount = Convert.ToInt64((listItems[i].FindElement(By.XPath("//span[contains(text(),' views')]")).GetAttribute("innerHTML").Split(' '))[0]);

                    // Add if() here to reduce the time cost searching for non-existent element of class date-updated
                    if (!Browser.Url.Contains("dev.office.com/training"))
                    {
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

                    resultInfo.DetailLink = listItems[i].FindElement(By.XPath("//a[@role='link']")).GetAttribute("href");

                    resultList.Add(resultInfo);
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

        /// <summary>
        /// Choose a filter type
        /// </summary>
        /// <param name="filterName">The filter name to select</param>
        public static void SelectFilter(string filterName)
        {
            IReadOnlyList<IWebElement> elements = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"));
            foreach (IWebElement element in elements)
            {
                if (element.Text.Equals(filterName) || element.GetAttribute("value").Equals(filterName))
                {
                    Browser.Click(element);
                    break;
                }
            }
        }

        /// <summary>
        /// Verify whether a link of a specific source type can be found on the page 
        /// </summary>
        /// <param name="sourcePart">A string that contains the part of source link</param>
        /// <returns>True if yes, else no</returns>
        public static bool CanFindSourceLink(string sourcePart)
        {
            var element = Browser.FindElement(By.XPath("//a[contains(@href,'" + sourcePart + "')]"));
            if (element != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetConfigurationValue(string propertyName)
        {
            return ConfigurationManager.AppSettings[propertyName];
        }
    }
}
