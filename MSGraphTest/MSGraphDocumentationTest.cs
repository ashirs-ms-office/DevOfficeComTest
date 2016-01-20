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
        /// Verify whether there is a toggle arrow which work correctly when the window is small.
        /// </summary>
        [TestMethod]
        public void CanToggleArrowWorkInSmallDocumentaionPage()
        {
            Pages.Navigation.Select("Documentation");
            int currentWidth = 0;
            int currentHeight = 0;
            Browser.GetWindowSize(out currentWidth, out currentHeight);

            int threshholdWidth = int.Parse(Utility.GetConfigurationValue("ThreshholdWindowWidth")) + 1;
            int threshholdHeight = int.Parse(Utility.GetConfigurationValue("ThreshholdWindowHeight")) + 1;

            int randomWidth = new Random().Next(threshholdWidth);
            int randomHeight = new Random().Next(threshholdHeight);

            // Change the width
            Browser.SetWindowSize(randomWidth, currentHeight);
            Assert.IsTrue(
                Utility.IsToggleArrowDisplayed(),
                "The toogle arrow should appear when the width is smaller than or equal to {0}",
                threshholdWidth);

            //Recover the width, and change the height 
            Browser.SetWindowSize(currentWidth, randomHeight);
            Assert.IsTrue(
                Utility.IsToggleArrowDisplayed(),
                "The toogle arrow should appear when the height is smaller than or equal to {0}",
                threshholdHeight);

            Assert.IsFalse(
                 Utility.IsMenuContentDisplayed(),
                 "Without clicking the toogle arrow, the menu content should not appear.");

            //Click the arrow to show the menu content
            Utility.ToggleMenu();
            Assert.IsTrue(
                 Utility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow, the menu content should appear.");

            //Click the arrow again to hide the menu content
            Utility.ToggleMenu();
            Assert.IsFalse(
                 Utility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow for the second time, the menu content should disappear.");

            //Recover the the whole window size
            Browser.SetWindowSize(currentWidth, currentHeight);
        }

        /// <summary>
        /// Verify whether toggle arrow hides when the window is large.
        /// </summary>
        [TestMethod]
        public void CanToggleArrowHideInLargeDocumentaionPage()
        {
            Pages.Navigation.Select("Documentation");
            int currentWidth = 0;
            int currentHeight = 0;
            Browser.GetWindowSize(out currentWidth, out currentHeight);

            int threshholdWidth = int.Parse(Utility.GetConfigurationValue("ThreshholdWindowWidth"));
            int threshholdHeight = int.Parse(Utility.GetConfigurationValue("ThreshholdWindowHeight"));

            //change the window size to the threshhold size 
            Browser.SetWindowSize(threshholdWidth, threshholdHeight);
            Assert.IsTrue(
                Utility.IsToggleArrowDisplayed(),
                "The toogle arrow should appear when the width is equal to {0} and the height is equal to {1}",
                threshholdWidth,
                threshholdHeight);

            int maxWidth = 0;
            int maxHeight = 0;
            Browser.GetWindowSize(out maxWidth, out maxHeight, true);

            Browser.SetWindowSize(threshholdWidth + new Random().Next(0, maxWidth - threshholdWidth + 1), threshholdHeight + new Random().Next(0, maxHeight - threshholdHeight + 1));
            Assert.IsFalse(
                 Utility.IsToggleArrowDisplayed(),
                 "The toogle arrow should disappear when the width is larger than {0} and the height is larger than {1}",
                threshholdWidth,
                threshholdHeight);

            //Recover the the whole window size
            Browser.SetWindowSize(currentWidth, currentHeight);
        }
    }
}
