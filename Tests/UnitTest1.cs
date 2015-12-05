using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            Pages.HomePage.Goto();
        }
        
        [TestMethod]
        public void Can_Go_To_HomePage()
        {
            Assert.IsTrue(Pages.HomePage.IsAt());
        }

        [TestMethod]
        public void Can_Go_To_AndroidPage()
        {
            Pages.HomePage.SelectProduct("Android");
            Assert.IsTrue(Pages.HomePage.IsAtProductPage("Android"));
        }
        [TestMethod]
        public void Can_Go_To_iOSPage()
        {
            Pages.HomePage.SelectProduct("iOS");
            Assert.IsTrue(Pages.HomePage.IsAtProductPage("iOS"));
        }
        
        [TestCleanup]
        public void Cleanup()
        {
             Pages.HomePage.Goto();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
