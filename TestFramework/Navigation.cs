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

        public void Select(string menuName, string itemName)
        {
            switch (menuName)
            {
                case ("Explore"):
                    exploreLinkElement.Click();
                    break;
                case ("Resources"):
                    resourceLinkElement.Click();
                    break;
                case ("Getting Started"):
                    gettingstartedLinkElement.Click();
                    break;
                case ("Code Samples"):
                    codesamplesLinkElement.Click();
                    break;
                case ("Documentaion"):
                    documentationLinkElement.Click();
                    break;
                default:
                    break;
            }

            var item = Browser.Driver.FindElement(By.LinkText(itemName));
            item.Click();
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
            return Browser.Driver.FindElements(By.XPath(@"//a[@ng-model=""selectedTypes""]")).Count;
        }

        /// <summary>
        /// Returns the filtered traings
        /// </summary>
        /// <param name="searchString">The search text to use</param>
        /// <returns>The search result list. Each result contains the training title and description</returns>
        public List<Training> GetFilterResults(string searchString = "")
        {
            this.InputSearchString(searchString);
            var trainingList = Browser.Driver.FindElement(By.XPath(@"//div[@id=""training-page""]/div[@class=""row""]/div/ul"));
            List<Training> resultList = new List<Training>();
            IReadOnlyList<IWebElement> listTrainingItems = trainingList.FindElements(By.XPath("li"));

            for (int i = 0; i < listTrainingItems.Count; i++)
            {
                Training trainingInfo = new Training();
                trainingInfo.Name = listTrainingItems[i].GetAttribute("aria-label");
                trainingInfo.Description = listTrainingItems[i].FindElement(By.ClassName("description")).FindElement(By.TagName("span")).Text;
                trainingInfo.ViewCount = Convert.ToInt64((listTrainingItems[i].FindElement(By.ClassName("description")).FindElement(By.XPath("./div/span")).GetAttribute("innerHTML").Split(' '))[0]);
                resultList.Add(trainingInfo);
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
            var element = Browser.Driver.FindElements(By.XPath(@"//a[@ng-model=""selectedTypes""]"))[index];
            //Use Javascript click() to avoid the known issue of chrome driver Click()
            (Browser.webDriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);

            return element.Text;
        }

        /// <summary>
        /// Set the displayed trainings' sort order
        /// </summary>
        /// <param name="trainingSortType">Specifies which sort type to use</param>
        /// <param name="isDescendent">Specifies whether the trainings are sorted descendently. True means yes, False means no</param>
        public void SetSortOrder(TrainingSortType trainingSortType, bool isDescendent)
        {
            string typeString;
            if (trainingSortType.Equals(TrainingSortType.ViewCount))
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
    }
}