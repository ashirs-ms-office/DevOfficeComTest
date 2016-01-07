using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;
using TestFramework.DataStructure;

namespace Tests
{
    [TestClass]
    public class Office365PageTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.SetWaitTime(TimeSpan.FromSeconds(15));
        }

        /// <summary>
        /// Verify whether the navigation item style can be updated when it is chosen or rejected.
        /// </summary>
        [TestMethod]
        public void Can_NavItem_Style_Updated_Accordingly()
        {
            #region Make all the nav items selectable
            //Randomly choose a platform
            Platform[] platforms = Enum.GetValues(typeof(Platform)) as Platform[];
            Random random = new Random();
            Platform platform = platforms[random.Next(0, platforms.Length)];
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            
            //Choose to sign in later
            Pages.Office365Page.CardRegisterApp.SigninLater();
            #endregion Make all the nav items selectable

            try
            {
                for (int i = 0; i < NavBar.NavItemCount; i++)
                {
                    NavBar.SelectNavItem(i);
                    NavBar.VerifyItemStyleCorrect(i);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        
        [TestMethod]
        public void Try_It_Out()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetUsers);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetUsers));
        }
		
        [TestMethod]
        public void Try_It_Out_Value()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetGroups);
            Pages.Office365Page.CardTryItOut.ChooseServiceValue(ServiceToTry.GetGroups, GetGroupValue.drive_root_children);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetGroups));
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

        //[TestMethod]
        //public void Can_SignIn_OfficeDevAccount()
        //{
        //    Pages.Office365Page.CardRegisterApp.SigninAs("Tester@devexperience.onmicrosoft.com")
        //        .WithPassword("Password02@")
        //        .Signin();
        //    Assert.IsTrue(Pages.Office365Page.CardRegisterApp.IsSignedin("Tester@devexperience.onmicrosoft.com"), "Failed to sign in.");
        //}

        [TestMethod]
        public void Can_DownloadCode()
        {
            Platform platform = Platform.Node;
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}.", platform.ToString());

            Pages.Office365Page.CardRegisterApp.SigninLater();
            Pages.Office365Page.CardRegisterApp.Register().WithAppName("Test_App");
            Assert.IsTrue(Pages.Office365Page.CardRegisterApp.IsRegistered(), "Failed to register app.");

            Pages.Office365Page.CardDownloadCode.DownloadCode();
            Assert.IsTrue(Pages.Office365Page.CardDownloadCode.IsCodeDownloaded(), "Failed to download code.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
