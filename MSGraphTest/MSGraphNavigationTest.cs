using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph site
    /// </summary>
    [TestClass]
    public class MSGraphNavigationTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.Goto(Utility.GetConfigurationValue("MSGraphBaseAddress"));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }

        /// <summary>
        /// Verify whether Home page can be navigated to by clicking the branding image.
        /// </summary>
        [TestMethod]
        public void CanBrandingNavToHomePage()
        {
            //Currently ignore the Graph explorer, since this page desn't have Microsoft
            //Graph branding image
            string[] navOptions = new string[] { 
                "Home",
                "Get started", 
                "Documentation", 
                //"Graph explorer", 
                "App registration", 
                "Samples & SDKs", 
                "Changelog" };

            string navPage = navOptions[new Random().Next(navOptions.Length)];
            Pages.Navigation.Select(navPage);

            Utility.ClickBranding();

            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Home"),
                @"Clicking the branding image should navigate to Graph Home Page");
        }

        /// <summary>
        /// Verify whether Get started page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToGetstartedPage()
        {
            Pages.Navigation.Select("Get started");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Get started"),
                @"The opened page should be ""Get started""");
        }

        /// <summary>
        /// Verify whether Documentation page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToDocumentationPage()
        {
            Pages.Navigation.Select("Documentation");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Documentation"),
                @"The opened page should be ""Documentation""");
        }

        /// <summary>
        /// Verify whether Graph explorer page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToGraphExplorerPage()
        {
            Pages.Navigation.Select("Graph explorer");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Graph Explorer"),
                @"The opened page should be ""Graph explorer""");
        }

        /// <summary>
        /// Verify whether App Registration page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void CanGoToAppRegistrationPage()
        {
            Pages.Navigation.Select("App registration");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("App registration"),
                @"The opened page should be ""App registration""");
        }

        /// <summary>
        /// Verify whether Samples and SDKs page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToSamplesAndSDKsPage()
        {
            Pages.Navigation.Select("Samples & SDKs");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Samples & SDKs"),
                @"The opened page should be ""Samples & SDKs""");
        }

        /// <summary>
        /// Verify whether Changelog page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void CanGoToChangelogPage()
        {
            Pages.Navigation.Select("Changelog");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Changelog"),
                @"The opened page should be ""Changelog""");
        }
    }
}
