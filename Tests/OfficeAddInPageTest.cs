using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class OfficeAddInPageTest
    {
        [TestMethod]
        public void Can_Choose_Product()
        {
            Product product = Product.PowerPoint;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", Product.PowerPoint.ToString());
        }

        [TestMethod]
        public void Can_Start_Building()
        {
            Product product = Product.Excel;
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(product), "Failed to choose product {0}.", Product.Outlook.ToString());
            Pages.OfficeAddInPage.CardBuild.StartBuilding(product);
            Assert.IsTrue(Pages.OfficeAddInPage.CardBuild.IsShowingBuildPage(product), "Failed to open build page");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
