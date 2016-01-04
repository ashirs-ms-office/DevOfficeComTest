using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class Office365PageTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.SetWaitTime(TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void Try_It_Out()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetUsers);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetUsers));
        }

        [TestMethod]
        public void Can_Choose_Platform()
        {
            foreach (Platform item in Enum.GetValues(typeof(Platform)))
            {
                Pages.Office365Page.CardSetupPlatform.ChoosePlatform(item);
                Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(item), "Failed to choose platform {0}.", item.ToString());
            }
        }

        [TestMethod]
        public void Can_SignIn_OfficeDevAccount()
        {
            Pages.Office365Page.CardRegisterApp.SigninAs("Tester@devexperience.onmicrosoft.com")
                .WithPassword("Password02@")
                .Signin();
            Assert.IsTrue(Pages.Office365Page.CardRegisterApp.IsSignedin("Tester@devexperience.onmicrosoft.com"), "Failed to sign in.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
