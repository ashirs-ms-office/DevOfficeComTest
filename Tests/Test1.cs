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
            Pages.CardSetupPlatform.ChoosePlatform("ios");
            Assert.IsTrue(Pages.CardSetupPlatform.IsShowingPlatformSetup("ios"));
        }

        [TestMethod]
        public void Can_SignIn()
        {
            Pages.CardRegisterApp.Goto();
            Pages.CardRegisterApp.SigninAs("JingyuShao@devexperience.onmicrosoft.com")
                .WithPassword("Sjy_10091=")
                .Signin();
            Assert.IsTrue(Pages.CardRegisterApp.IsSignedin("JingyuShao@devexperience.onmicrosoft.com"));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
