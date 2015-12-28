using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class Office365PageTest
    {      
        [TestMethod]
        public void Try_It_Out()
        {
            Pages.CardTryItOut.ChooseService(4);
            Pages.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.CardTryItOut.CanGetResponse(4));
        }

        [TestMethod]
        public void Can_Choose_Platform()
        {
            foreach (Platform item in Enum.GetValues(typeof(Platform)))
            {
                Pages.CardSetupPlatform.ChoosePlatform(item);
                Assert.IsTrue(Pages.CardSetupPlatform.IsShowingPlatformSetup(item), "Failed to choose platform {0}.", item.ToString());
            }
        }

        [TestMethod]
        public void Can_SignIn_OfficeDevAccount()
        {
            Pages.CardRegisterApp.SigninAs("Tester@devexperience.onmicrosoft.com")
                .WithPassword("Password02@")
                .Signin();
            Assert.IsTrue(Pages.CardRegisterApp.IsSignedin("Tester@devexperience.onmicrosoft.com"), "Failed to sign in.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
