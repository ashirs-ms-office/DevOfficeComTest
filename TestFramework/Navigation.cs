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
                case ("Documentation"):
                    Browser.Click(documentationLinkElement);
                    break;
                default:
                    break;
            }

            if (!itemName.Equals(""))
            {
                MenuItemOfExplore exploreItem;
                MenuItemOfResource resourceItem;
                MenuItemOfDocumentation documentationItem;
                if (Enum.TryParse(itemName, out exploreItem))
                {
                    var item = Browser.Driver.FindElement(By.LinkText(EnumExtension.GetDescription(exploreItem)));
                    Browser.Click(item);
                }

                if (Enum.TryParse(itemName, out resourceItem))
                {
                    var item = Browser.Driver.FindElement(By.LinkText(EnumExtension.GetDescription(resourceItem)));
                    Browser.Click(item);
                }

                if (Enum.TryParse(itemName, out documentationItem))
                {
                    var item = Browser.Driver.FindElement(By.LinkText(EnumExtension.GetDescription(documentationItem)));
                    Browser.Click(item);
                }
            }
        }

        public bool IsAtProductPage(string productName)
        {
            switch (productName)
            {
                case ("Outlook"):
                    bool canSwitchWindow = Browser.SwitchToNewWindow();
                    bool isAtOutlookPage = false;
                    if (canSwitchWindow)
                    {
                        var outlookPage = new OtherProductPage();
                        isAtOutlookPage = outlookPage.IsAt(productName);
                        Browser.SwitchBack();
                    }

                    Browser.GoBack();
                    return isAtOutlookPage;
                case ("DotNET"):
                case ("Node"):
                    MenuItemOfExplore menuItemResult;
                    Enum.TryParse(productName, out menuItemResult);
                    var page = new ProductPage();
                    string pageName = EnumExtension.GetDescription(menuItemResult).Replace(" ", "");
                    return page.ProductName.Contains(pageName);
                case ("PHP"):
                case ("Python"):
                case ("Ruby"):
                    // These three have no pages and currently redirect to Office365 API getting-started page
                    Platform platformResult;
                    Enum.TryParse(productName, out platformResult);
                    var platform = new Office365Page.CardSetupPlatform();
                    bool isShownPlatformSetup = platform.IsShowingPlatformSetup(platformResult);
                    Browser.GoBack();
                    return isShownPlatformSetup;
                default:
                    var productPage = new ProductPage();
                    return productPage.ProductName == productName;
            }
        }

        public bool IsAtOpportunityPage()
        {
            var opportunityPage = new OpportunityPage();
            bool canLoadImage = opportunityPage.CanLoadImage();
            Browser.GoBack();
            return canLoadImage;
        }

        public bool IsAtFabricPage(string fabricTitle)
        {
            var fabricPage = new FabricPage();
            string title = fabricPage.FabricPageTitle;
            Browser.GoBack();
            return title == fabricTitle;
        }

        public bool IsAtFabricGettingStartedPage(string fabricTitle)
        {
            var fabricPage = new FabricGettingStartedPage();
            string title = fabricPage.FabricPageTitle;
            Browser.GoBack();
            return title == fabricTitle;
        }

        public bool IsAtChoosingAPIEndpointPage(string Title)
        {
            var endpointPage = new ChoosingAPIEndpointPage();
            string title = endpointPage.EndpointPageTitle;
            Browser.GoBack();
            return title == Title;
        }

        public bool IsAtGraphPage(string graphTitle)
        {
            var graphPage = new GraphPage();
            string title = graphPage.GraphTitle.Replace(" ", "");

            Browser.GoBack();
            return title.Contains(graphTitle);
        }

        public bool IsAtOfficeGettingStartedPage(string Title)
        {
            var gettingStartedPage = new OfficeGettingStartedPage();
            string pageTitle = gettingStartedPage.GettingStartedPageTitle;
            Browser.GoBack();
            return pageTitle.Contains(Title);
        }

        public bool IsAtCodeSamplesPage(string Title)
        {
            var codeSamplesPage = new CodeSamplesPage();
            string pageTitle = codeSamplesPage.CodeSamplesPageTitle;
            Browser.GoBack();
            return pageTitle.Contains(Title);
        }

        public bool IsAtResourcePage(MenuItemOfResource item)
        {
            var resourcePage = new ResourcePage();
            bool isAtResourcePage = false;
            switch (item)
            {
                case (MenuItemOfResource.MiniLabs):
                    string miniLabsName = EnumExtension.GetDescription(item).Replace("-", " ").ToLower();
                    isAtResourcePage = resourcePage.ResourceName.ToLower().Contains(miniLabsName);
                    break;
                case (MenuItemOfResource.SnackDemoVideos):
                    string snackVideosName = EnumExtension.GetDescription(item).Replace("Demo ", "").ToLower();
                    isAtResourcePage = resourcePage.ResourceName.ToLower().Contains(snackVideosName);
                    break;
                case (MenuItemOfResource.APISandbox):
                    bool canSwitchWindow = Browser.SwitchToNewWindow();
                    if (canSwitchWindow)
                    {
                        var sandboxPage = new OtherProductPage();
                        isAtResourcePage = sandboxPage.IsAt(EnumExtension.GetDescription(item));
                        Browser.SwitchBack();
                    }

                    Browser.GoBack();
                    break;
                default:
                    isAtResourcePage = resourcePage.ResourceName.ToLower().Contains(EnumExtension.GetDescription(item).ToLower());
                    break;
            }
            
            return isAtResourcePage;
        }

        public bool IsAtDocumentationPage(MenuItemOfDocumentation item)
        {
            switch (item)
            {
                case (MenuItemOfDocumentation.OfficeUIFabricGettingStarted):
                    return IsAtFabricGettingStartedPage(EnumExtension.GetDescription(item));
                case (MenuItemOfDocumentation.Office365RESTAPIs):
                    return IsAtChoosingAPIEndpointPage("Choosing your API endpoint");
                case (MenuItemOfDocumentation.MicrosoftGraphAPI):
                    return IsAtDocumentationPage("Microsoft Graph");
                case (MenuItemOfDocumentation.PreviousVersions):
                    return IsAtDocumentationPage("Office developer documentation");
                default:
                    return IsAtDocumentationPage(EnumExtension.GetDescription(item).ToString());
            }
        }

        public bool IsAtExplorePage(MenuItemOfExplore item)
        {
            Platform platformResult;
            Product productResult;
            OtherProduct otherProduct;
            if (Enum.TryParse(item.ToString(), out platformResult) || Enum.TryParse(item.ToString(), out productResult) ||
                item == MenuItemOfExplore.JavaScript)
            {
                return IsAtProductPage(item.ToString());
            }

            // These products have their own home page and will be navigated out of Dev.office.com
            if (Enum.TryParse(item.ToString(), out otherProduct))
            {
                bool canSwitchWindow = Browser.SwitchToNewWindow();
                bool isAtOtherProductPage = false;
                if (canSwitchWindow)
                {
                    var otherProductPage = new OtherProductPage();
                    isAtOtherProductPage = otherProductPage.IsAt(item.ToString());
                    Browser.SwitchBack();
                }

                Browser.GoBack();
                return isAtOtherProductPage;
            }

            switch (item)
            {
                case (MenuItemOfExplore.WhyOffice):
                    return IsAtOpportunityPage();
                case (MenuItemOfExplore.OfficeUIFabric):
                    return IsAtFabricPage(EnumExtension.GetDescription(item));
                case (MenuItemOfExplore.MicrosoftGraph):
                    return IsAtGraphPage(item.ToString());
            }

            return false;
        }

        private bool IsAtDocumentationPage(string pageTitle)
        {
            var documentationPage = new DocumentationPage();
            bool isAtDocumentationPage = false;
            bool canSwitchWindow = false;
            canSwitchWindow = Browser.SwitchToNewWindow();
            if (canSwitchWindow)
            {
                isAtDocumentationPage = documentationPage.DocumentationTitle.ToLower().Contains(pageTitle.ToLower());
                Browser.SwitchBack();
            }

            Browser.GoBack();
            return isAtDocumentationPage;
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
                    catch(NoSuchElementException)
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
            if (!sortElement.FindElement(By.ClassName("sort-icon")).GetAttribute("src").Contains(orderString))
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
        public bool AreFiltersInURL(List<string> filterNames, out List<string> unContainedFilters)
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