using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Summary description for DocumentationTest
    /// </summary>
    [TestClass]
    public class MSGraphDocumentationTest
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
        /// Verify whether there is a toggle arrow which work correctly when the window is small.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S04_TC01_CanToggleArrowWorkInSmallDocumentaionPage()
        {
            GraphPages.Navigation.Select("Documentation");
            
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);

            if (GraphUtility.IsToggleArrowDisplayed())
            {
                //Assert a value that must be true, just to log the window size,
                Assert.IsTrue(GraphUtility.IsToggleArrowDisplayed(),
                    "{0}*{1} is small enough for browser window to show the arrow on Documentation page",
                    currentWidth,
                    currentHeight);
                VerifyArrowAvailability();
            }
            else
            {
                //The arrow doesn't exist means currently the window is big. 
                //Try setting a smaller window size to make it appear.
                int randomWidth;
                int randomHeight;
                long retryTime = currentWidth * currentHeight - 1;
                do
                {
                    retryTime--;
                    randomWidth = new Random().Next(1, currentWidth + 1);
                    randomHeight = new Random().Next(1, currentHeight + 1);
                    GraphBrowser.SetWindowSize(randomWidth, randomHeight);
                } while (retryTime > 0 && !GraphUtility.IsToggleArrowDisplayed());
                Assert.IsTrue(GraphUtility.IsToggleArrowDisplayed(),
                    "{0}*{1} is small enough for browser window to show the arrow on Documentation page",
                    randomWidth,
                    randomHeight);
                VerifyArrowAvailability();
            }
            GraphBrowser.SetWindowSize(currentWidth, currentHeight);
        }

        /// <summary>
        /// Verify whether toggle arrow hides when the window is large.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S04_TC02_CanToggleArrowHideInLargeDocumentaionPage()
        {
            GraphPages.Navigation.Select("Documentation");

            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);

            if (GraphUtility.IsToggleArrowDisplayed())
            {
                //The arrow exists means currently the window is small. 
                //Try setting a bigger window size to make it appear.
                int maxWidth = 0;
                int maxHeight = 0;
                GraphBrowser.GetWindowSize(out maxWidth, out maxHeight, true);
                int retryTime = (maxWidth * maxHeight) - (currentWidth*currentHeight);
                int randomWidth=0;
                int randomHeight = 0;
                do
                {
                    retryTime--;
                    randomWidth = new Random().Next(randomWidth, maxWidth + 1);
                    randomHeight = new Random().Next(currentHeight, maxHeight + 1);
                    GraphBrowser.SetWindowSize(randomWidth, randomHeight);
                } while (retryTime >=0 && GraphUtility.IsToggleArrowDisplayed());
                Assert.IsFalse(GraphUtility.IsToggleArrowDisplayed(),
                    "{0}*{1} is big enough for browser window to hide the arrow on Documentation page",
                    currentWidth,
                    currentHeight);
            }
            else
            {
                //Assert a value that must be false, just to log the window size,
                Assert.IsFalse(GraphUtility.IsToggleArrowDisplayed(),
                    "{0}*{1} is big enough for browser window to hide the arrow on Documentation page",
                    currentWidth,
                    currentHeight);
            }
        }

        /// <summary>
        /// Verify whether clicking different subject on Documentation page's
        /// table of content will show the correct duc content.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S04_TC03_CanDisplayAppropriateContentOnDocumentaionPage()
        {
            GraphPages.Navigation.Select("Documentation");
            //If the table of content is replaced by the toggle arrow, click the arrow to display table of content
            if (GraphUtility.IsToggleArrowDisplayed())
            {
                GraphUtility.ToggleMenu();
            }

            //Level 1 layer
            GraphUtility.Click("WALKTHROUGHS");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            string docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Platform specific walkthroughs",
                docTitle,
                @"Platform specific walkthroughs content should be shown when ""WALKTHROUGHS"" is chosen in the table of content on Documentation page");

            //Level 2 layer
            GraphUtility.Click("OVERVIEW");
            GraphUtility.Click("Paging");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Paging Microsoft Graph data in your app",
                docTitle,
                @"Paging content should be shown when ""OVERVIEW""->""Paging"" is chosen in the table of content on Documentation page");

            //Level 2 layer
            GraphUtility.Click("AUTHORIZATION");
            GraphUtility.Click("Associate Office 365 account");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Associate your Office 365 account with Azure AD to create and manage apps",
                docTitle,
                @"Associate Office 365 account content should be shown when ""AUTHORIZATION""->""Associate Office 365 account"" is chosen in the table of content on Documentation page");

            //Level 3 layer
            GraphUtility.Click("/V1.0 REFERENCE");
            GraphUtility.Click("OUTLOOK CALENDAR");
            GraphUtility.Click("EVENT");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "event resource type",
                docTitle,
                @"Event resource type content should be shown when ""/V1.0 REFERENCE""->""OUTLOOK CALENDAR""->""EVENT"" is chosen in the table of content on Documentation page");

            //Level 3 layer
            GraphUtility.Click("GROUPS");
            GraphUtility.Click("POST");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "post resource type",
                docTitle,
                @"post resource type content should be shown when ""/V1.0 REFERENCE""->""GROUPS""->""POST"" is chosen in the table of content on Documentation page");

            //Level 3 layer
            GraphUtility.Click("/BETA REFERENCE");
            GraphUtility.Click("PEOPLE");
            GraphUtility.Click("PERSON");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "person resource type",
                docTitle,
                @"person resource type content should be shown when ""/BETA REFERENCE""->""PEOPLE""->""PERSON"" is chosen in the table of content on Documentation page");

            //Level 4 layer
            GraphUtility.Click("/V1.0 REFERENCE");
            GraphUtility.Click("USERS");
            GraphUtility.Click("PHOTO");
            GraphUtility.Click("Update photo");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Update profilephoto",
                docTitle,
                @"Update profilephoto content should be shown when ""/V1.0 REFERENCE""->""USERS""->""PHOTO""->""Update photo"" is chosen in the table of content on Documentation page");

            //Level 4 layer
            GraphUtility.Click("/BETA REFERENCE");
            GraphUtility.Click("OUTLOOK EXTENSIONS");
            GraphUtility.Click("OPENTYPEEXTENSION");
            GraphUtility.Click("Delete openTypeExtension");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Delete extension",
                docTitle,
                @"Delete extension content should be shown when ""/BETA REFERENCE""->""OUTLOOK EXTENSIONS""->""OPENTYPEEXTENSION""->""Delete openTypeExtension"" is chosen in the table of content on Documentation page");

            //Level 4 layer
            GraphUtility.Click("/V1.0 REFERENCE");
            GraphUtility.Click("DIRECTORY");
            GraphUtility.Click("DEVICE");
            GraphUtility.Click("List devices");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "List devices",
                docTitle,
                @"List devices content should be shown when ""/V1.0 REFERENCE""->""DIRECTORY""->""DEVICE""->""List devices"" is chosen in the table of content on Documentation page");

            //Level 4 layer
            GraphUtility.Click("/BETA REFERENCE");
            GraphUtility.Click("ONEDRIVE");
            GraphUtility.Click("ITEM");
            GraphUtility.Click("Create item");
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
            docTitle = GraphUtility.GetDocTitle();

            Assert.AreEqual(
                "Create an item in a collection",
                docTitle,
                @"Create an item in a collection content should be shown when ""/BETA REFERENCE""->""ONEDRIVE""->""ITEM""->""Create item"" is chosen in the table of content on Documentation page");
        }

        /// <summary>
        /// Verify whether the arrow hide table of content, 
        /// and whether clicking it can hide/show table of content alternatively
        /// </summary>
        private void VerifyArrowAvailability()
        {
            Assert.IsFalse(
                 GraphUtility.IsMenuContentDisplayed(),
                 "Without clicking the toogle arrow, the menu content should not appear.");
            //Click the arrow to show the menu content
            GraphUtility.ToggleMenu();
            Assert.IsTrue(
                 GraphUtility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow, the menu content should appear.");

            //Click the arrow again to hide the menu content
            GraphUtility.ToggleMenu();
            Assert.IsFalse(
                 GraphUtility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow for the second time, the menu content should disappear.");
        }
    }
}
