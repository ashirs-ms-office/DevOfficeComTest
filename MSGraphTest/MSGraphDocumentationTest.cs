using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;
using System.Drawing;

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
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            GraphPages.Navigation.Select("Documentation");

            Size windowSize;
            //Set as the screen size of IPad2
            double deviceScreenSize = double.Parse(Utility.GetConfigurationValue("IPad2Size"));
            GraphBrowser.TransferPhysicalSizeToPixelSize(
                deviceScreenSize,
                new Size
                {
                    Width = int.Parse(Utility.GetConfigurationValue("IPad2ScreenResolutionWidth")),
                    Height = int.Parse(Utility.GetConfigurationValue("IPad2ScreenResolutionHeight"))
                },
                out windowSize);
            GraphBrowser.SetWindowSize(windowSize.Width, windowSize.Height);

            Assert.IsTrue(
                GraphUtility.IsToggleArrowDisplayed(),
                "An IPad2 window size ({0} inches) can make table of content arrow appear.",
                deviceScreenSize);
            Assert.IsFalse(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists, table of content should be hidden.");

            GraphUtility.ToggleMenu();
            Assert.IsTrue(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists and table of content is hidden,clicking the arrow should show table of content.");

            GraphUtility.ToggleMenu();
            Assert.IsFalse(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists and table of content is shown,clicking the arrow should hide table of content.");

            //Set as the screen size of IPhone6 plus
            deviceScreenSize = double.Parse(Utility.GetConfigurationValue("IPhone6PlusSize"));
            //Since mobile phone width<Height, invert the output values
            GraphBrowser.TransferPhysicalSizeToPixelSize(
               deviceScreenSize,
               new Size
               {
                   Width = int.Parse(Utility.GetConfigurationValue("IPhone6PlusScreenResolutionWidth")),
                   Height = int.Parse(Utility.GetConfigurationValue("IPhone6PlusScreenResolutionHeight"))
               },
               out windowSize);
            //Since mobile phone widh<height, invert height and width
            GraphBrowser.SetWindowSize(windowSize.Height, windowSize.Width);

            Assert.IsTrue(
                GraphUtility.IsToggleArrowDisplayed(),
                "An IPhone6 Plus window size ({0} inches) can make table of content arrow appear.",
                deviceScreenSize);

            //Recover the window size
            GraphBrowser.SetWindowSize(currentWidth, currentHeight);
        }

        /// <summary>
        /// Verify whether toggle arrow hides when the window is large.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S04_TC02_CanToggleArrowHideInLargeDocumentaionPage()
        {
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            GraphPages.Navigation.Select("Documentation");

            int actualWidth = 0;
            int actualHeight = 0;
            //Maxsize the window to see if it is possible to hide the arrow
            GraphBrowser.SetWindowSize(actualWidth, actualHeight, true);
            GraphBrowser.GetWindowSize(out actualWidth, out actualHeight);
            if (GraphUtility.IsToggleArrowDisplayed())
            {
                Assert.Inconclusive(
                    "A window size ({0}*{1}) is not big enough to hide table of content arrow",
                    actualWidth,
                    actualHeight);
            }
            else
            {
                //Set a common laptop size: 17.3 and a common screen resolution:1024*768
                double deviceScreenSize = 17.3;
                Size windowSize;
                GraphBrowser.TransferPhysicalSizeToPixelSize(
                    deviceScreenSize,
                    new Size
                    {
                        Width = 1024,
                        Height = 768
                    },
                    out windowSize);
                GraphBrowser.SetWindowSize(windowSize.Width, windowSize.Height);

                Assert.IsFalse(
                    GraphUtility.IsToggleArrowDisplayed(),
                    "An large window size ({0} inches) can make table of content arrow hide.",
                    deviceScreenSize);
            }
            //Recover the window size
            GraphBrowser.SetWindowSize(currentWidth, currentHeight);
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
