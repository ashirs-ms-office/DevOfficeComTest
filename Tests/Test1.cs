using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class Test1
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            //Pages.HomePage.Goto();
            Browser.Initialize();
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

        [TestMethod]
        public void Try_It_Out()
        {
            Pages.CardTryItOut.Goto();
            Pages.CardTryItOut.ChooseService(4);
            Pages.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.CardTryItOut.CanGetResponse(4));
        }
        [TestMethod]
        public void Can_Choose_Platform()
        {
            Pages.CardSetupPlatform.Goto();
            Pages.CardSetupPlatform.ChoosePlatform("android");
            Assert.IsTrue(Pages.CardSetupPlatform.IsShowingPlatformSetup("android"));
        }

        



        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
