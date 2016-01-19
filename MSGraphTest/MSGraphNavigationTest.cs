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

        /// <summary>
        /// Verify whether Get started page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToGetstartedPage()
        {
            // Browser.SetWindowSize(200,300);
            Pages.Navigation.Select("Get started");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Get started"),
                @"The opened page should be ""Get started""");
        }

        /// <summary>
        /// Verify whether Documentation page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToDocumentationPage()
        {
            Pages.Navigation.Select("Documentation");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Documentation"),
                @"The opened page should be ""Documentation""");
        }

        /// <summary>
        /// Verify whether Graph explorer page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToGraphExplorerPage()
        {
            Pages.Navigation.Select("Graph explorer");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Graph Explorer"),
                @"The opened page should be ""Graph explorer""");
        }

        /// <summary>
        /// Verify whether App Registration page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void CanGoToAppRegistrationPage()
        {
            Pages.Navigation.Select("App registration");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("App registration"),
                @"The opened page should be ""App registration""");
        }

        /// <summary>
        /// Verify whether Samples and SDKs page can be navigated to.
        /// </summary>
        [TestMethod]
        public void CanGoToSamplesAndSDKsPage()
        {
            Pages.Navigation.Select("Samples & SDKs");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Samples & SDKs"),
                @"The opened page should be ""Samples & SDKs""");
        }

        /// <summary>
        /// Verify whether Changelog page can be navigated to.
        /// </summary>        
        [TestMethod]
        public void CanGoToChangelogPage()
        {
            Pages.Navigation.Select("Changelog");
            Assert.IsTrue(
                Pages.Navigation.IsAtGraphPage("Changelog"),
                @"The opened page should be ""Changelog""");
        }

        /// <summary>
        /// Verify whether there is a toggle arrow when the window is small.
        /// </summary>
        [TestMethod]
        public void CanFindArrowInSmallDocumentaionPage()
        {
             Pages.Navigation.Select("Documentation");
             int currentWidth = 0;
             int currentHeight = 0;
             Browser.GetWindowSize(out currentWidth, out currentHeight);

             int threshholdWidth=int.Parse(Utility.GetConfigurationValue("ThreshholdWindowWidth"))+1;
             int threshholdHeight = int.Parse(Utility.GetConfigurationValue("ThreshholdWindowHeight")) + 1;

             int randomWidth = new Random().Next(threshholdWidth);
             int randomHeight = new Random().Next(threshholdHeight);

             // Change the width of the window
             Browser.SetWindowSize(randomWidth, currentHeight);
             bool isDisplayed = Utility.IsToggledDisplayed();
             Assert.IsTrue(
                 isDisplayed, 
                 "The toogle arrow should appear when the width is smaller than or equal to {0}", 
                 threshholdWidth);

             //Recover the orignal width, then change the height 
             Browser.SetWindowSize(currentWidth, randomHeight);
             isDisplayed = Utility.IsToggledDisplayed();
             Assert.IsTrue(
                 isDisplayed,
                 "The toogle arrow should appear when the height is smaller than or equal to {0}",
                 threshholdHeight);

            //Recover the the whole window size
             Browser.SetWindowSize(currentWidth, currentHeight);
        }
    }
}
