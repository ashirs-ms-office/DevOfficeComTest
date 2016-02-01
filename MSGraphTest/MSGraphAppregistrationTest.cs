using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph App registration page
    /// </summary>
    [TestClass]
    public class MSGraphAppregistrationTest
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
        /// Verify whether Office 365 App Registration Tool can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S07_TC01_CanGoToDevOfficeCom()
        {
            GraphPages.Navigation.Select("App registration");
            GraphUtility.Click("Office 365 App Registration Tool");
            Assert.IsTrue(
                GraphBrowser.SwitchToWindow("Office Dev Center - Office 365 App Registration Tool"),
                @"Clicking ""Office 365 App Registration Tool"" on App registration page can navigate to devofficecom, App Registration Tool page");
        }

        /// <summary>
        /// Verify whether apps.dev.microsoft.com can be navigated to.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S07_TC02_CanGoToAppRegistrationPortal()
        {
            GraphPages.Navigation.Select("App registration");
            GraphUtility.Click("New App Registration Portal (preview)");
            Assert.IsTrue(
                GraphBrowser.SwitchToWindow("apps.dev.microsoft.com"),
                @"Clicking ""New App Registration Portal (preview)"" on App registration page can navigate to apps.dev.microsoft.com page");
        }
    }
}
