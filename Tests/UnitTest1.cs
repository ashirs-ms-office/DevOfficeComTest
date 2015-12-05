using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Go_To_HomePage()
        {
            Pages.HomePage.Goto();
            Assert.IsTrue(Pages.HomePage.IsAt());
        }

        [TestMethod]
        public void Can_Go_To_AndroidPage()
        {
            Pages.HomePage.Goto();
            Pages.HomePage.SelectProduct("Android");
            Assert.IsTrue(Pages.HomePage.IsAtProductPage("Android"));
        }
        [TestMethod]
        public void Can_Go_To_iOSPage()
        {
            Pages.HomePage.Goto();
            Pages.HomePage.SelectProduct("iOS");
            Assert.IsTrue(Pages.HomePage.IsAtProductPage("iOS"));
        }

        //[TestMethod]
        //public void Can_Try_It_Out()
        //{
        //    Pages.Office365APIPage.Goto();
        //    Assert.IsTrue(Pages.Office365APIPage.IsAt());
        //    Pages.Office365APIPage.SelectService(3); // 3 is service number for Get files
        //    Pages.Office365APIPage.ClickTry();
        //    Assert.IsTrue(Pages.Office365APIPage.ContainsResponseBody());
        //}

        [TestCleanup]
        public void Cleanup()
        {
             Pages.HomePage.Goto();
             //Browser.Close();
        }
        
    }
}
