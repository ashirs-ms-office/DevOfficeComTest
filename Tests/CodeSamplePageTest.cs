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

        /// <summary>
        /// Verify if all the checked filters can be cleared at one time
        /// </summary>
        [TestMethod]
        public void Can_Filter_Cleared()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Pages.Navigation.GetFilterCount();

            //Generate the count of filters to check
            int checkedRandomFilterCount = new Random().Next(filterCount);
            //Generate the indexes of filters to check
            List<int> indexList = new List<int>();
            while (indexList.Count < checkedRandomFilterCount)
            {
                int randomIndex = new Random().Next(filterCount);
                if (!indexList.Contains(randomIndex))
                {
                    indexList.Add(randomIndex);
                    Pages.Navigation.SelectFilter(randomIndex);
                }
            }

            Pages.Navigation.ExecuteClearFilters();
            List<string> unclearedFilters;
            if(!Pages.Navigation.areFiltersCleared(out unclearedFilters))
            {
                StringBuilder stringBuilder=new StringBuilder();
                foreach(string unClearedFilter in unclearedFilters)
                {
                stringBuilder.Append(unClearedFilter+";");
                }
                Assert.Fail("The following filters are not cleared: {0}",
                    stringBuilder.ToString());
            }
        }

        /// <summary>
        /// Verify whether the code samples can be sorted by view count correctly
        /// </summary>
        [TestMethod]
        public void Can_Sort_CodeSamples_By_ViewCount()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Pages.Navigation.GetFilterCount();

            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Pages.Navigation.SelectFilter(i);

                // Set the sort order as descendent
                Pages.Navigation.SetSortOrder(SortType.ViewCount, true);
                List<SearchedResult> resultList = Pages.Navigation.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount >= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be larger than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                // Set the sort order as ascendent
                Pages.Navigation.SetSortOrder(SortType.ViewCount, false);
                resultList = Pages.Navigation.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount <= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be smaller than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                Pages.Navigation.ExecuteClearFilters();
            }
        }

        /// <summary>
        /// Verify whether the URL can be updated accordingly when some filters are chosen
        /// </summary>
        [TestMethod]
        public void Can_URL__Updated_By_CodeSampleFilters()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Pages.Navigation.GetFilterCount();
            List<string> filterNames = new List<string>();

            //Generate the count of filters to select
            int checkedRandomFilterCount = new Random().Next(filterCount);
            //Generate the indexes of filters to select
            List<int> indexList = new List<int>();
            while (indexList.Count < checkedRandomFilterCount)
            {
                int randomIndex = new Random().Next(filterCount);
                if (!indexList.Contains(randomIndex))
                {
                    indexList.Add(randomIndex);
                    filterNames.Add(Pages.Navigation.SelectFilter(randomIndex));
                }
            }

            List<string> unContainedFilters;
            if (!Pages.Navigation.AreFiltersInURL(filterNames, out unContainedFilters))
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string unContainedFilter in unContainedFilters)
                {
                    stringBuilder.Append(unContainedFilter + ";");
                }
                Assert.Fail("The following filters are not contained: {0}",
                    stringBuilder.ToString());
            }
        }
    }
}
