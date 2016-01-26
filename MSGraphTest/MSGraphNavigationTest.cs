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
            GraphBrowser.Initialize();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GraphBrowser.Close();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GraphBrowser.Goto(Utility.GetConfigurationValue("MSGraphBaseAddress"));
        }

        /// <summary>
        /// Verify whether Home page can be navigated to by clicking the branding image.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC01_CanBrandingNavToHomePage()
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
            GraphPages.Navigation.Select(navPage);

            GraphUtility.ClickBranding();

            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Home"),
                @"Clicking the branding image should navigate to Graph Home Page");
        }

        /// <summary>
        /// Verify whether Get started page can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC02_CanGoToGetstartedPage()
        {
            GraphPages.Navigation.Select("Get started");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Get started"),
                @"The opened page should be ""Get started""");
        }

        /// <summary>
        /// Verify whether Documentation page can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC03_CanGoToDocumentationPage()
        {
            GraphPages.Navigation.Select("Documentation");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Documentation"),
                @"The opened page should be ""Documentation""");
        }

        /// <summary>
        /// Verify whether Graph explorer page can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC04_CanGoToGraphExplorerPage()
        {
            GraphPages.Navigation.Select("Graph explorer");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Graph Explorer"),
                @"The opened page should be ""Graph explorer""");
        }

        /// <summary>
        /// Verify whether App Registration page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void BVT_Graph_S01_TC05_CanGoToAppRegistrationPage()
        {
            GraphPages.Navigation.Select("App registration");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("App registration"),
                @"The opened page should be ""App registration""");
        }

        /// <summary>
        /// Verify whether Samples and SDKs page can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC06_CanGoToSamplesAndSDKsPage()
        {
            GraphPages.Navigation.Select("Samples & SDKs");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Samples & SDKs"),
                @"The opened page should be ""Samples & SDKs""");
        }

        /// <summary>
        /// Verify whether Changelog page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void BVT_Graph_S01_TC07_CanGoToChangelogPage()
        {
            GraphPages.Navigation.Select("Changelog");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Changelog"),
                @"The opened page should be ""Changelog""");
        }

        /// <summary>
        /// Verify whether the default banner image can be loaded.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S01_TC08_CanLoadBannerImage()
        {
            //Currently ignore Graph explorer and Documentation, since these pages don't have banner image
            //Graph branding image
            string[] navOptions = new string[] { 
                "Home",
                "Get started", 
                //"Documentation", 
                //"Graph explorer", 
                "App registration", 
                "Samples & SDKs", 
                "Changelog" };

            string navPage = navOptions[new Random().Next(navOptions.Length)];
            GraphPages.Navigation.Select(navPage);

            string imageUrl = GraphUtility.GetGraphBannerImageUrl();
            Assert.IsTrue(GraphBrowser.ImageExist(Utility.GetConfigurationValue("MSGraphBaseAddress") + imageUrl), "The banner image should be valid to load");
        }
    }
}
