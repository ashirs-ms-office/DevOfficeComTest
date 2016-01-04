using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    /// <summary>
    /// Test class for Code Sample Page
    /// </summary>
    [TestClass]
    public class CodeSamplePageTest
    {

        #region Additional test attributes
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Browser.Initialize();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Verify if choosing the filters can cause the displayed results updated 
        /// </summary>
        [TestMethod]
        public void Can_Filter_CheckedCorrectly()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Pages.Navigation.GetFilterCount();
            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Pages.Navigation.SelectFilter(i);
                Assert.IsTrue(Pages.Navigation.isFilterWorkable(i),
                    "The {0} filter should be valid to change displayed code samples",
                    filterName);
            }
        }
    }
}
