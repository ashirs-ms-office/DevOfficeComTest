using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace TestFramework
{
    public class Navigation : BasePage
    {
        [FindsBy(How = How.LinkText, Using = "Explore")]
        private IWebElement exploreLinkElement;

        [FindsBy(How = How.LinkText, Using = "Resources")]
        private IWebElement resourceLinkElement;

        [FindsBy(How = How.LinkText, Using = "Getting Started")]
        private IWebElement gettingstartedLinkElement;

        [FindsBy(How = How.LinkText, Using = "Code Samples")]
        private IWebElement codesamplesLinkElement;

        [FindsBy(How = How.LinkText, Using = "Documentation")]
        private IWebElement documentationLinkElement;

        public void Select(string menuName, string itemName = "")
        {
            switch (menuName)
            {
                case ("Explore"):
                    Browser.Click(exploreLinkElement);
                    break;
                case ("Resources"):
                    Browser.Click(resourceLinkElement);
                    break;
                case ("Getting Started"):
                    Browser.Click(gettingstartedLinkElement);
                    break;
                case ("Code Samples"):
                    Browser.Click(codesamplesLinkElement);
                    break;
                case ("Documentaion"):
                    Browser.Click(documentationLinkElement);
                    break;
                default:
                    break;
            }

            if (!itemName.Equals(""))
            {
                var item = Browser.Driver.FindElement(By.LinkText(itemName));
                Browser.Click(item);
            }
        }

        public bool IsAtProductPage(string productName)
        {
            var productPage = new ProductPage();
            return productPage.ProductName == productName;
        }

        public bool IsAtResourcePage(string resourceName)
        {
            var resourcePage = new ResourcePage();
            return resourcePage.ResourceName == resourceName;
        }

        /// <summary>
        /// Set a search text
        /// </summary>
        /// <param name="value">The value to set</param>
        public void InputSearchString(string value)
        {
            var inputElement = Browser.Driver.FindElement(By.XPath(@"//input[@ng-model=""searchText""]"));
            inputElement.Clear();
            inputElement.SendKeys(value);
        }

        /// <summary>
        /// Get the count of filters
        /// </summary>
        public int GetFilterCount()
        {
            return Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]")).Count;
        }

        /// <summary>
        /// Returns the filtered traings
        /// </summary>
        /// <param name="searchString">The search text to use</param>
        /// <returns>The search result list. Each result contains the training title and description</returns>
        public List<SearchedResult> GetFilterResults(string searchString = "")
        {
            this.InputSearchString(searchString);
            var uList = Browser.Driver.FindElement(By.CssSelector(@"#OrderedResults+ul"));
            List<SearchedResult> resultList = new List<SearchedResult>();
            IReadOnlyList<IWebElement> listItems = uList.FindElements(By.XPath("li"));

            for (int i = 0; i < listItems.Count; i++)
            {
                SearchedResult resultInfo = new SearchedResult();
                resultInfo.Name = listItems[i].GetAttribute("aria-label");

                var descriptionElement = listItems[i].FindElement(By.ClassName("description"));
                resultInfo.Description = descriptionElement.Text;

                resultInfo.ViewCount = Convert.ToInt64((listItems[i].FindElement(By.XPath("//span[contains(text(),' views')]")).GetAttribute("innerHTML").Split(' '))[0]);
                resultList.Add(resultInfo);
            }
            return resultList;
        }

        /// <summary>
        /// Choose a filter type
        /// </summary>
        /// <param name="index">The index of the filter type to select</param>
        /// <returns>The selected filter name</returns>
        public string SelectFilter(int index)
        {
            var element = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"))[index];
            Browser.Click(element);

            return element.Text;
        }

        /// <summary>
        /// Check whether a filter has an "ng-click" attribute with an updating seleted result function's name.
        /// </summary>
        /// <param name="index">The index of the filter type to select</param>
        /// <returns>True if yes, else no.</returns>
        public bool isFilterWorkable(int index)
        {
            var element = Browser.Driver.FindElements(By.XPath(@"//*[@ng-model=""selectedTypes""]"))[index];
            return element.GetAttribute("ng-click").Contains("updateSelectedTypes(");
        }

        /// <summary>
        /// Set the displayed results' sort order
        /// </summary>
        /// <param name="sortType">Specifies which sort type to use</param>
        /// <param name="isDescendent">Specifies whether the results are sorted descendently. True means yes, False means no</param>
        public void SetSortOrder(SortType sortType, bool isDescendent)
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
            if (!sortElement.FindElement(By.Id("mostPopularIcon")).GetAttribute("src").Contains(orderString))
            {
                sortElement.Click();
            }
        }

        /// <summary>
        /// Verify if none of the filters is checked
        /// </summary>
        /// <param name="unclearedFilters">The name of the uncleared filters</param>
        /// <returns>True if yes, else no.</returns>
        public bool areFiltersCleared(out List<string> unclearedFilters)
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
        /// Clear the filters 
        /// </summary>
        public void ExecuteClearFilters()
        {
            var element = Browser.FindElement(By.CssSelector(".clearfilters.filter-button"));
            Browser.Click(element);
        }

        /// <summary>
        /// Verify whether the url contains the chosen filters
        /// </summary>
        /// <param name="filterNames">The chosen filters</param>
        /// <returns>True if yes, else no.</returns>
        public bool AreFiltersInURL(List<string> filterNames,out List<string> unContainedFilters)
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
    }
}