using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;
using TestFramework.DataStructure;

namespace Tests
{
    [TestClass]
    public class OfficeAddInPageTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.SetWaitTime(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Verify whether the navigation item style can be updated when it is chosen or rejected.
        /// </summary>
        [TestMethod]
        public void Can_OfficeAddInNavItem_Style_Updated_Accordingly()
        {
            #region Make all the nav items selectable
            //Randomly choose a product
            Product[] products = Enum.GetValues(typeof(Product)) as Product[];
            Random random = new Random();
            Product product = products[random.Next(0, products.Length)];
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
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
        public void Can_Choose_Product()
        {
            Product product = Product.PowerPoint;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingExampleOrVideo(product), "Failed to choose product {0}.", product.ToString());

        }

        [TestMethod]
        public void Can_Start_Building()
        {
            Product product = Product.Excel;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Pages.OfficeAddInPage.CardExcel.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardExcel.Build.IsShowingBuildPage(), "Failed to open build page");
        }

        [TestMethod]
        public void Excel_Product()
        {
            // Select app
            Product product = Product.Excel;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());

            Browser.SaveScreenShot(@"E:\\Excel.png");
            // Explore
            // Pages.OfficeAddInPage.CardExcel.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardExcel.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardExcel.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardExcel.MoreResouces.DownLoadStarterSample();
            Pages.OfficeAddInPage.CardExcel.MoreResouces.DesignYourAddIn();
            Pages.OfficeAddInPage.CardExcel.MoreResouces.MoreCodeSamples();
            Pages.OfficeAddInPage.CardExcel.MoreResouces.OfficeAddInTypes();
            Pages.OfficeAddInPage.CardExcel.MoreResouces.PublishYourAddIn();
            Pages.OfficeAddInPage.CardExcel.MoreResouces.ReadTheDocs();
        }

        [TestMethod]
        public void Outlook_Product()
        {
            // Select app
            Product product = Product.Outlook;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());

            // Explore
            // Pages.OfficeAddInPage.CardOutlook.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardOutlook.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardOutlook.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.OutlookDevCenter();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.DownLoadStarterSample();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.DesignYourAddIn();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.MoreCodeSamples();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.OfficeAddInTypes();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.PublishYourAddIn();
            Pages.OfficeAddInPage.CardOutlook.MoreResouces.ReadTheDocs();
        }
        [TestMethod]
        public void PowerPoint_Product()
        {
            // Select app
            Product product = Product.PowerPoint;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(Product.PowerPoint);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Browser.SaveScreenShot(@"E:\\PowerPoint.jpeg");
            // Explore
            // Pages.OfficeAddInPage.CardPowerPoint.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardPowerPoint.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardPowerPoint.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.DesignYourAddIn();
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.MoreCodeSamples();
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.OfficeAddInTypes();
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.PublishYourAddIn();
            Pages.OfficeAddInPage.CardPowerPoint.MoreResouces.ReadTheDocs();
        }
        [TestMethod]
        public void Word_Product()
        {
            // Select app
            Product product = Product.Word;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(Product.Word);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", product.ToString());
            Browser.SaveScreenShot(@"E:\\Word.jpeg");
            // Explore
            // Pages.OfficeAddInPage.CardWord.Explore.play();

            // Build
            Pages.OfficeAddInPage.CardWord.Build.StartBuilding();
            Assert.IsTrue(Pages.OfficeAddInPage.CardWord.Build.IsShowingBuildPage(), "Failed to open build page");

            // More Resource
            Pages.OfficeAddInPage.CardWord.MoreResouces.DesignYourAddIn();
            Pages.OfficeAddInPage.CardWord.MoreResouces.MoreCodeSamples();
            Pages.OfficeAddInPage.CardWord.MoreResouces.OfficeAddInTypes();
            Pages.OfficeAddInPage.CardWord.MoreResouces.PublishYourAddIn();
            Pages.OfficeAddInPage.CardWord.MoreResouces.ReadTheDocs();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close(); 
        }
    }
}
