using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class HomePageTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.Initialize();
        }

        [TestMethod]
        public void BVT_S03_TC01_CanGoToHomePage()
        {
            Assert.IsTrue(Pages.HomePage.IsAt());
        }

        [TestMethod]
        public void BVT_S03_TC02_CanLoadHomePageImages()
        {
            foreach (HomePageImages item in Enum.GetValues(typeof(HomePageImages)))
            {
                Assert.IsTrue(Pages.HomePage.CanLoadImages(item));
            }
        }
        
        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}