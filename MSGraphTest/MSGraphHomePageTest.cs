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
            Browser.Goto(Utility.GetConfigurationValue("MSGraphBaseAddress"));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Browser.Goto(Utility.GetConfigurationValue("MSGraphBaseAddress"));
        }

        /// <summary>
        /// Verify whether Overview is shown when "See overview" is clicked.
        /// </summary>
        [TestMethod]
        public void CanSeeOverviewOnDocumentaionPage()
        {
            Utility.Click("See overview");
            string docTitle = Utility.GetDocTitle();
            Assert.AreEqual(
                "Overview of Microsoft Graph",
                docTitle,
                "The documentation should be Overview when clicking See overview on Home page");
        }

        /// <summary>
        /// Verify whether Overview is shown when "Try the API" is clicked.
        /// </summary>
        [TestMethod]
        public void CanTryAPIOnExplorerPage()
        {
            Utility.Click("Try the API");
            Assert.IsTrue(
                Browser.SwitchToWindow("Graph Explorer"),
                @"The opened page should be ""Graph explorer"" when clicking Try the API");
        }

        /// <summary>
        /// Verify whether Home page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToHomePage()
        {
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when go to the base url");

            Pages.Navigation.Select("Home");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when clicking it");

            //Currently ignore the Graph explorer, since this page desn't have Microsoft
            //Graph branding image
            string[] navOptions = new string[] { 
                "Get started", 
                "Documentation", 
                //"Graph explorer", 
                "App registration", 
                "Samples & SDKs", 
                "Changelog" };

            //Go to the other page to click Home
            string navPage = navOptions[new Random().Next(navOptions.Length)];
            Pages.Navigation.Select(navPage);

            Pages.Navigation.Select("Home");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Home"),
                @"The opened page should be ""Home"" when clicking it on the other page");
        }       
    }
}
