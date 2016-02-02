using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    /// <summary>
    /// Header Test class for dev.office.com web site
    /// </summary>
    [TestClass]
    public class HeaderTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.Initialize();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }

        /// <summary>
        /// Verify whether clicking the branding can navigate to home page
        /// </summary>
        [TestMethod]
        public void Acceptance_S01_TC01_CanBrandingNavToHomePage()
        {
            string[] navOptions = new string[] { 
                "Explore",
                "Getting Started", 
                "Code Samples", 
                "Resources", 
                "Documentation" };

            string navItem = navOptions[new Random().Next(navOptions.Length)];

            Pages.Navigation.Select(navItem);
            string[] navSubOptions = Utility.GetNavSubItems(navItem);
            if (navSubOptions == null)
            {
                Utility.ClickBranding();

                Assert.IsTrue(
                   Pages.HomePage.IsAt(),
                    @"Clicking the branding image at {0}should navigate to Dev center home Page",
                    navItem);
            }
            else
            {
                string subNavItem = string.Empty;

                //Find a page that contains the branding
                do
                {
                    Browser.GoBack();
                    int randomIndex = new Random().Next(navSubOptions.Length);
                    subNavItem = navSubOptions[randomIndex];
                    Pages.Navigation.Select(navItem);
                    Utility.Click(subNavItem);
                    Browser.SwitchToNewWindow();
                } while (!Utility.BrandingExists());

                Utility.ClickBranding();

                Assert.IsTrue(
                   Pages.HomePage.IsAt(),
                    @"Clicking the branding image at {0}->{1} should navigate to Dev center home Page",
                    navItem,
                    subNavItem);
            }
        }

        /// <summary>
        /// Verify whether clicking the branding can navigate to home page
        /// </summary>
        [TestMethod]
        public void Comps_S01_TC02_CanSearchWidget()
        {
            int randomIndex = new Random().Next(Utility.TypicalSearchText.Length);
            string searchString = Utility.TypicalSearchText[randomIndex];
            string[] results = Utility.SearchWidget(searchString);
            bool isFound = false;
            string foundResult=string.Empty;
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].ToLower().Contains(searchString.ToLower()))
                {
                    isFound = true;
                    foundResult=results[i];
                    break;
                }
            }
            Assert.IsTrue(isFound,
                "Search {0} should find the result:\n{1}",
                searchString,
                foundResult);
        }
    }
}
