using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph explorer page
    /// </summary>
    [TestClass]
    public class MSGraphExplorerTest
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
        /// Verify whether login on Graph explorer page can succeed.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S05_TC01_CanLogin()
        {
            GraphPages.Navigation.Select("Graph explorer");
            
            //Avoid logging in automatically
            GraphUtility.ClearCookies();
            GraphUtility.Click("Login");
            GraphUtility.Login(
                Utility.GetConfigurationValue("GraphExplorerUserName"), 
                Utility.GetConfigurationValue("GraphExplorerPassword"));
            Assert.IsTrue(GraphUtility.IsLoggedIn(Utility.GetConfigurationValue("GraphExplorerUserName")), "");
        }
    }
}
