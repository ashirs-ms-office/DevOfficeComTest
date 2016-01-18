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

        [TestMethod]
        public void CanGoToGetstartedPage()
        {
            // Browser.SetWindowSize(200,300);
            Pages.Navigation.Select("Get started");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Get started"),
                @"The opened page should be ""Get started""");
        }

        [TestMethod]
        public void CanGoToDocumentationPage()
        {
            Pages.Navigation.Select("Documentation");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Documentation"),
                @"The opened page should be ""Documentation""");
        }

        [TestMethod]
        public void CanGoToGraphExplorerPage()
        {
            Pages.Navigation.Select("Graph explorer");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Graph Explorer"),
                @"The opened page should be ""Graph explorer""");
        }

        [TestMethod]
        public void CanGoToAppRegistrationPage()
        {
            Pages.Navigation.Select("App registration");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("App registration"),
                @"The opened page should be ""App registration""");
        }

        [TestMethod]
        public void CanGoToSamplesAndSDKsPage()
        {
            Pages.Navigation.Select("Samples & SDKs");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Samples & SDKs"),
                @"The opened page should be ""Samples & SDKs""");
        }
    }
}
