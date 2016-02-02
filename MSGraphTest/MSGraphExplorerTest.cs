using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            GraphPages.Navigation.Select("Graph explorer");
            if (GraphUtility.IsLoggedIn())
            {
                GraphUtility.Click("Logout");
                GraphBrowser.Wait(TimeSpan.FromSeconds(5));
            }
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
            GraphPages.Navigation.Select("Graph explorer");
            string userName = Utility.GetConfigurationValue("GraphExplorerUserName");

            if (!GraphUtility.IsLoggedIn(userName))
            {
                if (GraphUtility.IsLoggedIn())
                {
                    GraphUtility.Click("Logout");
                    GraphBrowser.Wait(TimeSpan.FromSeconds(5));
                }
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
                @"GET ""me"" can obtain the correct response");
        }

        /// <summary>
        /// Verify Whether switching API version can get the correct response.
        /// </summary>
        [TestMethod]
        public void Comps_Graph_S05_TC03_CanSwitchAPIVersion()
        {
            GraphPages.Navigation.Select("Graph explorer");
            string userName = Utility.GetConfigurationValue("GraphExplorerUserName");

            if (!GraphUtility.IsLoggedIn())
            {
                GraphUtility.Click("Login");

                GraphUtility.Login(
                    userName,
                    Utility.GetConfigurationValue("GraphExplorerPassword"));
            }
            //v1.0
            GraphUtility.InputExplorerQueryString("https://graph.microsoft.com/v1.0/me" + "\n");
            GraphBrowser.Wait(TimeSpan.FromSeconds(10));
            string v10Response = GraphUtility.GetExplorerResponse();
            Assert.IsTrue(
                 v10Response.Contains(@"""@odata.context"":""https://graph.microsoft.com/v1.0"),
                 "Setting a v1.0 query string should get a v1.0 response.");

            //vBeta
            GraphUtility.InputExplorerQueryString("https://graph.microsoft.com/beta/me" + "\n");
            GraphBrowser.Wait(TimeSpan.FromSeconds(10));
            string betaResponse = GraphUtility.GetExplorerResponse();
            Assert.IsTrue(
                betaResponse.Contains(@"""@odata.context"":""https://graph.microsoft.com/beta"),
                "Setting a vBeta query string should get a vBeta response.");
        }

        /// <summary>
        /// Verify whether request PATCH can succeed.
        /// </summary>
        [TestMethod]
        public void Comps_Graph_S05_TC04_CanPatchMe()
        {
            GraphPages.Navigation.Select("Graph explorer");
            string userName = Utility.GetConfigurationValue("GraphExplorerUserName");

            if (!GraphUtility.IsLoggedIn())
            {
                GraphUtility.Click("Login");

                GraphUtility.Login(
                    userName,
                    Utility.GetConfigurationValue("GraphExplorerPassword"));
            }
            //Change the operation from GET to PATCH
            GraphUtility.ClickButton("GET");
            GraphUtility.Click("PATCH");
            string jobTitle = "JobTitle_" + DateTime.Now.ToString("M/d/yyyy/hh/mm/ss");
            GraphUtility.InputExplorerJSONBody(new KeyValuePair<string,string>("jobTitle",jobTitle));
            GraphBrowser.Wait(TimeSpan.FromSeconds(3));
            GraphUtility.InputExplorerQueryString("https://graph.microsoft.com/v1.0/me" + "\n");
            GraphBrowser.Wait(TimeSpan.FromSeconds(8));
            string patchResponse = GraphUtility.GetExplorerResponse();

            //Change the operation from PATCH to GET
            GraphUtility.ClickButton("PATCH");
            GraphUtility.Click("GET");
            GraphUtility.InputExplorerQueryString("https://graph.microsoft.com/v1.0/me" + "\n");
            GraphBrowser.Wait(TimeSpan.FromSeconds(8));
            string getResponse = GraphUtility.GetExplorerResponse();

            Dictionary<string, string> gottenProperties=GraphUtility.ParseJsonFormatProperties(getResponse);
            Assert.AreEqual(jobTitle, gottenProperties["jobTitle"], "The patched property should be updated accordingly");
        }

        
    }
}
