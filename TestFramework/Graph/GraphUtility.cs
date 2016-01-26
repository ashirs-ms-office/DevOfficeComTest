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
    /// Static class for common functions of Graph site
    /// </summary>
    public static class GraphUtility
    {
        /// <summary>
        /// Verify if the toggle arrow is found on the page 
        /// </summary>
        /// <returns>Trye if yes, else no.</returns>
        public static bool IsToggleArrowDisplayed()
        {
            return GraphBrowser.FindElement(By.Id("toggleLeftPanelContainer")).Displayed;
        }

        /// <summary>
        /// Verify if the menu-content is found on the page
        /// </summary>
        /// <returns>Trye if yes, else no.</returns>
        public static bool IsMenuContentDisplayed()
        {
            return GraphBrowser.FindElement(By.CssSelector("#menu-content")).Displayed;
        }

        /// <summary>
        /// Execute the menu display toggle
        /// </summary>
        public static void ToggleMenu()
        {
            var element = GraphBrowser.FindElement(By.Id("toggleLeftPanelContainer"));
            GraphBrowser.Click(element);
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Click the branding image on the page
        /// </summary>
        public static void ClickBranding()
        {
            var element = GraphBrowser.FindElement(By.CssSelector("#branding>a"));
            GraphBrowser.Click(element);
        }

        /// <summary>
        /// Get the document title in the current doc page
        /// </summary>
        /// <returns>The title of document</returns>
        public static string GetDocTitle()
        {
            string docTitle;
            var title = GraphBrowser.FindElementInFrame("docframe", By.TagName("h1"), out docTitle);
            return docTitle;
        }

        /// <summary>
        /// Get the banner image url of MS Graph site
        /// </summary>
        /// <returns>The url of the banner image</returns>
        public static string GetGraphBannerImageUrl()
        {
            var element = GraphBrowser.FindElement(By.Id("banner-image"));
            if(element==null)
            {
                element = GraphBrowser.FindElement(By.CssSelector("div#layout-featured>div>article>div>div>div>div"));
            }
            string styleString = element.GetAttribute("style");
            string[] styles = styleString.Split(';');

            string url = string.Empty;
            foreach (string style in styles)
            {
                if (style.Contains("background-image"))
                {
                    int startIndex = style.IndexOf("/");
                    //2 is the length of ")
                    url = style.Substring(startIndex).Substring(0, style.Substring(startIndex).Length - 2);
                    break;
                }
            }
            return url;
        }

        /// <summary>
        /// Find an link or a button according to the specific text and click it
        /// </summary>
        /// <param name="text">The text of the element</param>
        public static void Click(string text)
        {
            var element = GraphBrowser.FindElement(By.LinkText(text));
            if (element != null)
            {
                GraphBrowser.Click(element);
            }
            else
            {
                IReadOnlyList<IWebElement> elements = GraphBrowser.webDriver.FindElements(By.TagName("button"));
                foreach (IWebElement elementToClick in elements)
                {
                    if (elementToClick.GetAttribute("innerHTML").Contains(text))
                    {
                        GraphBrowser.Click(elementToClick);
                        break;
                    }
                }
            }
        }
    }
}
