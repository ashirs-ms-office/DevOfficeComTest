using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph Home page
    /// </summary>
    [TestClass]
    public class MSGraphHomePageTest
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
        /// Verify whether Home page can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S02_TC01_CanGoToHomePage()
        {
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when go to the base url");

            GraphPages.Navigation.Select("Home");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when clicking it");

            //Currently ignore the Graph explorer, since this page desn't have Microsoft
            //MS Graph nav bar
            string[] navOptions = new string[] { 
                "Get started", 
                "Documentation", 
                //"Graph explorer", 
                "App registration", 
                "Samples & SDKs", 
                "Changelog" };

            //Go to the other page to click "Home" on nav bar
            string navPage = navOptions[new Random().Next(navOptions.Length)];
            GraphPages.Navigation.Select(navPage);

            GraphPages.Navigation.Select("Home");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when clicking it {0} page's nav bar", 
                navPage);
        }

        /// <summary>
        /// Verify whether Overview is shown when "See overview" is clicked.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S02_TC02_ClickSeeOverviewCanShowDocumentaionPage()
        {
            GraphUtility.Click("See overview");
            string docTitle = GraphUtility.GetDocTitle();
            Assert.AreEqual(
                "Overview of Microsoft Graph",
                docTitle,
                "The documentation should be Overview when clicking See overview on Home page");
        }

        /// <summary>
        /// Verify whether Overview is shown when "Try the API" is clicked.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S02_TC03_ClickTryAPIOnExplorerCanShowPage()
        {
            GraphUtility.Click("Try the API");
            Assert.IsTrue(
                GraphBrowser.SwitchToWindow("Graph Explorer"),
                @"The opened page should be ""Graph explorer"" when clicking Try the API");
        }
    }
}
