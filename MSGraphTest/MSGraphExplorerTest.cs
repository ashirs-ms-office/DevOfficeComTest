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
        public void Acceptance_Graph_S05_TC01_CanLogin()
        {
            GraphUtility.ClearCookies();            
            GraphPages.Navigation.Select("Graph explorer");
            //Avoid logging in automatically
            GraphUtility.Click("Login");
            GraphUtility.Login(
                Utility.GetConfigurationValue("GraphExplorerUserName"),
                Utility.GetConfigurationValue("GraphExplorerPassword"));
            Assert.IsTrue(GraphUtility.IsLoggedIn(Utility.GetConfigurationValue("GraphExplorerUserName")), "");
        }

        /// <summary>
        /// Verify whether request GET me can succeed. 
        /// </summary>
        [TestMethod]
        public void Comps_Graph_S05_TC02_CanGetMe()
        {
            GraphUtility.ClearCookies();
            GraphPages.Navigation.Select("Graph explorer");
            string userName = Utility.GetConfigurationValue("GraphExplorerUserName");
            if (!GraphUtility.IsLoggedIn())
            {
            GraphUtility.Click("Login");
           
            GraphUtility.Login(
                userName,
                Utility.GetConfigurationValue("GraphExplorerPassword"));
           }
            GraphUtility.InputExplorerQueryString("https://graph.microsoft.com/v1.0/me" + "\n");
            GraphBrowser.Wait(TimeSpan.FromSeconds(10));
            string response = GraphUtility.GetExplorerResponse();

            Assert.IsTrue(
                response.Contains(@"""mail"":""" + userName + @""""),
                "");
        }
    }
}
