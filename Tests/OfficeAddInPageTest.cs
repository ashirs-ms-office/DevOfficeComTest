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
            Pages.OfficeAddInPage.CardChooseProduct.ChooseProduct(Product.PowerPoint);
            Assert.IsTrue(Pages.OfficeAddInPage.CardChooseProduct.IsShowingProductExplore(Product.PowerPoint), "Failed to choose product {0}.", Product.PowerPoint.ToString());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}
