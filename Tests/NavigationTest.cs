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
        public void Can_Go_To_GettingStartedPage()
        {
            Pages.Navigation.Select("Getting Started");
            Assert.IsTrue(Pages.Navigation.IsAtOfficeGettingStartedPage("Getting Started"));
        }

        [TestMethod]
        public void Can_Go_To_CodeSamplesPage()
        {
            Pages.Navigation.Select("Code Samples");
            Assert.IsTrue(Pages.Navigation.IsAtOfficeGettingStartedPage("Code Samples"));
        }

        [TestMethod]
        public void Can_Go_To_ExploreSubPage()
        {
            foreach (MenuItemOfExplore item in Enum.GetValues(typeof(MenuItemOfExplore)))
            {
                Pages.Navigation.Select("Explore", item.ToString());
                Assert.IsTrue(Pages.Navigation.IsAtExplorePage(item), string.Format("The menu item {0} is not opened currectly.", item.ToString()));
            }
        }

        [TestMethod]
        public void Can_Go_To_ResourceSubPage()
        {
            foreach (MenuItemOfResource item in Enum.GetValues(typeof(MenuItemOfResource)))
            {
                Pages.Navigation.Select("Resources", item.ToString());
                Assert.IsTrue(Pages.Navigation.IsAtResourcePage(item), string.Format("The menu item {0} is not opened currectly.", item.ToString()));
            }
        }

        [TestMethod]
        public void Can_Go_To_DocumentationSubPage()
        {
            foreach (MenuItemOfDocumentation item in Enum.GetValues(typeof(MenuItemOfDocumentation)))
            {
                Pages.Navigation.Select("Documentation", item.ToString());
                Assert.IsTrue(Pages.Navigation.IsAtDocumentationPage(item), string.Format("The menu item {0} is not opened currectly.", item.ToString()));
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}