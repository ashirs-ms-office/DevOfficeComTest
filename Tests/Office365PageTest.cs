using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class Office365PageTest
    {      
        [TestMethod]
        public void Try_It_Out()
        {
            Browser.Goto(Browser.BaseAddress + "/getting-started/office365apis#try-it-out");
            Pages.CardTryItOut.ChooseService(4);
            Pages.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.CardTryItOut.CanGetResponse(4));
        }

        [TestMethod]
        public void Can_Choose_Platform()
        {
            Browser.Goto(Browser.BaseAddress + "/getting-started/office365apis#setup");
            Pages.CardSetupPlatform.ChoosePlatform("ios");
            Assert.IsTrue(Pages.CardSetupPlatform.IsShowingPlatformSetup("ios"));
        }

        [TestMethod]
        public void Can_SignIn()
        {
            Browser.Goto(Browser.BaseAddress + "/Getting-Started/office365Apis#register-app");
            Pages.CardRegisterApp.SigninAs("Tester@devexperience.onmicrosoft.com")
                .WithPassword("Password02@")
                .Signin();
            Assert.IsTrue(Pages.CardRegisterApp.IsSignedin("JingyuShao@devexperience.onmicrosoft.com"),"Failed to sign in.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
