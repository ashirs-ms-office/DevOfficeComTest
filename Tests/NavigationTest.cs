using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class NavigationTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.Initialize();
        }

        [TestMethod]
        public void Can_Go_To_AndroidPage()
        {
            Pages.Navigation.Select("Explore","Android");
            Assert.IsTrue(Pages.Navigation.IsAtProductPage("Android"));
        }

        [TestMethod]
        public void Can_Go_To_WordPage()
        {
            Pages.Navigation.Select("Explore","Word");
            Assert.IsTrue(Pages.Navigation.IsAtProductPage("Word"));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}