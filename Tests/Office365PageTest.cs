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
            Browser.SetWaitTime(TimeSpan.FromSeconds(15));
        }

        // Design changed. No NavBar for now. So disable this case for the moment.
        /// <summary>
        /// Verify whether the navigation item style can be updated when it is chosen or rejected.
        /// </summary>
        //[TestMethod]
        //public void Can_Office365NavItem_Style_Updated_Accordingly()
        //{
        //    #region Make all the nav items selectable
        //    //Randomly choose a platform
        //    Platform[] platforms = Enum.GetValues(typeof(Platform)) as Platform[];
        //    Random random = new Random();
        //    Platform platform = platforms[random.Next(0, platforms.Length)];
        //    Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            
        //    //Choose to sign in later
        //    Pages.Office365Page.CardRegisterApp.SigninLater();
        //    #endregion Make all the nav items selectable

        //    try
        //    {
        //        for (int i = 0; i < NavBar.NavItemCount; i++)
        //        {
        //            NavBar.SelectNavItem(i);
        //            NavBar.VerifyItemStyleCorrect(i);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail(e.Message);
        //    }
        //}

        [TestMethod]
        public void S04_TC01_CanGoThroughO365API()
        {
            Platform platform = Platform.Node;
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}.", platform.ToString());

            Pages.Office365Page.CardRegisterApp.SigninLater();
            Pages.Office365Page.CardDownloadCode.DownloadCode();
            Assert.IsTrue(Pages.Office365Page.CardDownloadCode.IsCodeDownloaded(), "Failed to download code.");

            Pages.Office365Page.CardMoreResources.OutlookDevCenter();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Outlook Dev Center page.");
            Pages.Office365Page.CardMoreResources.Training();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Training page.");
            Pages.Office365Page.CardMoreResources.APIReferences();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open API References page.");
            Pages.Office365Page.CardMoreResources.CodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Code Samples page.");
            Pages.Office365Page.CardMoreResources.AzureAppAndPermissions();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Azure app & Permissions page.");
            Pages.Office365Page.CardMoreResources.AddToO365AppLauncher();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open O365 App Launcher page.");
            Pages.Office365Page.CardMoreResources.SubmitToOfficeStore();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Submit to Office Store page.");
        }

        [TestMethod]
        public void S05_TC01_CanTryO365API_GetUsers()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetUsers);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetUsers));
        }
		
        [TestMethod]
        public void S05_TC02_Can_Try_O365API_GetGroups_DriverRootChildren()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetGroups);
            Pages.Office365Page.CardTryItOut.ChooseServiceValue(ServiceToTry.GetGroups, GetGroupValue.drive_root_children);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetGroups));
        }

        [TestMethod]
        public void S06_TC01_CanChoosePlatform()
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
        public void S08_TC01_CanDownloadCode_Node()
        {
            Platform platform = Platform.Node;
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}.", platform.ToString());

            Pages.Office365Page.CardRegisterApp.SigninLater();
            //Pages.Office365Page.CardRegisterApp.Register().WithAppName("Test_App");
            //Assert.IsTrue(Pages.Office365Page.CardRegisterApp.IsRegistered(), "Failed to register app.");

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
