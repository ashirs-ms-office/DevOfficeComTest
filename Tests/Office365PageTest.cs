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
            Browser.SetWaitTime(TimeSpan.FromSeconds(Utility.DefaultWaitTime));
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
        public void Acceptance_S04_TC01_CanGoThroughO365API()
        {
            Pages.OfficeGettingStartedPage.Office365APIGetStarted();
            Assert.IsTrue(Pages.Office365Page.IsAtOffice365Page(), "Failed to open Office 365 APIs Getting started page.");

            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetMessages);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetMessages, GetMessagesValue.Inbox), "Failed to get the response for the serivce to try.");

            Platform platform = Platform.Node;
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}.", platform.ToString());

            Pages.Office365Page.CardRegisterApp.SigninLater();
            Pages.Office365Page.CardDownloadCode.DownloadCode();
            Assert.IsTrue(Pages.Office365Page.CardDownloadCode.IsCodeDownloaded(), "Failed to download code.");

            Pages.Office365Page.CardMoreResources.OutlookDevCenter();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open Outlook Dev Center page.");
            Pages.Office365Page.CardMoreResources.Training();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open Training page.");
            Pages.Office365Page.CardMoreResources.APIReferences();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open API References page.");
            Pages.Office365Page.CardMoreResources.CodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open Code Samples page.");
            Pages.Office365Page.CardMoreResources.AzureAppAndPermissions();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open Azure app & Permissions page.");
            Pages.Office365Page.CardMoreResources.AddToO365AppLauncher();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open O365 App Launcher page.");
            Pages.Office365Page.CardMoreResources.SubmitToOfficeStore();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingCorrectResourcePage(), "Failed to open Submit to Office Store page.");
        }

        [TestMethod]
        public void BVT_S04_TC02_ShowThreeCardsByDefault()
        {
            Pages.OfficeGettingStartedPage.Office365APIGetStarted();
            Assert.IsTrue(Pages.Office365Page.IsAtOffice365Page(), "Failed to open Office 365 APIs Getting started page.");

            // should show and only show first 3 cards
            Assert.IsTrue(Pages.Office365Page.OnlyDefaultCardsDisplayed(), "Cards in Office 365 page are not displayed correctly.");
        }

        [TestMethod]
        public void Acceptance_S05_TC01_CanTryO365API_GetUsers()
        {
            ServiceToTry service = ServiceToTry.GetUsers;
            Pages.Office365Page.CardTryItOut.ChooseService(service);
            foreach (GetUsersValue item in Enum.GetValues(typeof(GetUsersValue)))
            {
                Pages.Office365Page.CardTryItOut.ChooseServiceValue(service, item);
                Pages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(service, item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
            }
        }

        [TestMethod]
        public void Comps_S05_TC02_CanTryO365API_GetGroups()
        {
            ServiceToTry service = ServiceToTry.GetGroups;
            Pages.Office365Page.CardTryItOut.ChooseService(service);
            foreach (GetGroupValue item in Enum.GetValues(typeof(GetGroupValue)))
            {
                Pages.Office365Page.CardTryItOut.ChooseServiceValue(service, item);
                Pages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(service, item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
            }
        }

        [TestMethod]
        public void Comps_S05_TC03_CanTryO365API_GetMessages()
        {
            ServiceToTry service = ServiceToTry.GetMessages;
            Pages.Office365Page.CardTryItOut.ChooseService(service);
            foreach (GetMessagesValue item in Enum.GetValues(typeof(GetMessagesValue)))
            {
                Pages.Office365Page.CardTryItOut.ChooseServiceValue(service, item);
                Pages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(service, item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
            }
        }

        [TestMethod]
        public void Comps_S05_TC04_CanTryO365API_GetFiles()
        {
            ServiceToTry service = ServiceToTry.GetFiles;
            Pages.Office365Page.CardTryItOut.ChooseService(service);
            foreach (GetFilesValue item in Enum.GetValues(typeof(GetFilesValue)))
            {
                Pages.Office365Page.CardTryItOut.ChooseServiceValue(service, item);
                Pages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(service, item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
            }
        }

        [TestMethod]
        public void Comps_S05_TC05_CanTryO365API_GetEvents()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetEvents);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetEvents, null));
        }

        [TestMethod]
        public void Comps_S05_TC06_CanTryO365API_GetContacts()
        {
            Pages.Office365Page.CardTryItOut.ChooseService(ServiceToTry.GetContacts);
            Pages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(Pages.Office365Page.CardTryItOut.CanGetResponse(ServiceToTry.GetContacts, null));
        }

        [TestMethod]
        public void BVT_S06_TC01_CanChoosePlatform()
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
        public void Acceptance_S08_TC01_CanDownloadCode_Node()
        {
            Platform platform = Platform.Node;
            Pages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            Assert.IsTrue(Pages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}.", platform.ToString());

            Pages.Office365Page.CardRegisterApp.SigninLater();            

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
