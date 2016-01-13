using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class OfficeAddInPageTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.SetWaitTime(TimeSpan.FromSeconds(15));
        }

        /// <summary>
        /// Verify whether the navigation item style can be updated when it is chosen or rejected.
        /// </summary>
        //[TestMethod]
        //public void Can_OfficeAddInNavItem_Style_Updated_Accordingly()
        //{
        //    #region Make all the nav items selectable
        //    //Randomly choose a product
        //    Product[] products = Enum.GetValues(typeof(Product)) as Product[];
        //    Random random = new Random();
        //    Product product = products[random.Next(0, products.Length)];
        //    Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
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
        public void S10_TC01_CanChooseProduct_PowerPoint()
        {
            Product product = Product.PowerPoint;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());

        }

        [TestMethod]
        public void S12_TC01_CanStartBuilding_Excel()
        {
            Product product = Product.Excel;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Pages.OfficeAddInPage.CardExcel.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardExcel.Build.IsShowingBuildPage(), "Failed to open build page");
        }

        [TestMethod]
        public void S09_TC01_CanGoThroughAddinPage_Excel()
        {
            // Select app
            Product product = Product.Excel;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());

            //Browser.SaveScreenShot(@"E:\\Excel.png");
            // Explore
            // Pages.OfficeAddInPage.CardExcel.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardExcel.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardExcel.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardExcel.MoreResouces.DownLoadStarterSample();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open DownLoad Sample page.");
            Pages.OfficeAddInPage.CardExcel.MoreResouces.DesignYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Design guidelines page.");
            Pages.OfficeAddInPage.CardExcel.MoreResouces.MoreCodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Code Samples page.");
            Pages.OfficeAddInPage.CardExcel.MoreResouces.OfficeAddInTypes();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in Types page.");
            Pages.OfficeAddInPage.CardExcel.MoreResouces.PublishYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Publish AddIn page.");
            Pages.OfficeAddInPage.CardExcel.MoreResouces.ReadTheDocs();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in overview page.");
        }

        [TestMethod]
        public void BVT_S09_TC02_CanGoThroughAddinPage_Outlook()
        {
            // Select app
            Product product = Product.Outlook;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            //Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());

            // Explore
            // Pages.OfficeAddInPage.CardOutlook.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardOutlook.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardOutlook.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.OutlookDevCenter();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Outlook Dev Center page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.DownLoadStarterSample();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open DownLoad Sample page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.DesignYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Design guidelines page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.MoreCodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Code Samples page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.OfficeAddInTypes();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in Types page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.PublishYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Publish AddIn page.");
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.ReadTheDocs();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in overview page.");
        }
        [TestMethod]
        public void S09_TC03_CanGoThroughAddinPage_PowerPoint()
        {
            // Select app
            Product product = Product.PowerPoint;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(Product.PowerPoint);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            //Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());
            Browser.SaveScreenShot(@"E:\\PowerPoint.jpeg");
            // Explore
            // Pages.OfficeAddInPage.CardPowerPoint.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardPowerPoint.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardPowerPoint.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.DesignYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Design guidelines page.");
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.MoreCodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Code Samples page.");
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.OfficeAddInTypes();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in Types page.");
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.PublishYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Publish AddIn page.");
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.ReadTheDocs();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in overview page.");
        }
        [TestMethod]
        public void S09_TC04_CanGoThroughAddinPage_Word()
        {
            // Select app
            Product product = Product.Word;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(Product.Word);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            //Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());
            Browser.SaveScreenShot(@"E:\\Word.jpeg");
            // Explore
            // Pages.OfficeAddInPage.CardWord.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardWord.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardWord.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardWord.MoreResouces.DesignYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Design guidelines page.");
            Pages.OfficeAddInPage.CardWord.MoreResouces.MoreCodeSamples();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Code Samples page.");
            Pages.OfficeAddInPage.CardWord.MoreResouces.OfficeAddInTypes();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in Types page.");
            Pages.OfficeAddInPage.CardWord.MoreResouces.PublishYourAddIn();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Publish AddIn page.");
            Pages.OfficeAddInPage.CardWord.MoreResouces.ReadTheDocs();
            Assert.IsTrue(Pages.Office365Page.CardMoreResources.IsShowingMoreResourcePage(), "Failed to open Add-in overview page.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close(); 
        }
    }
}
