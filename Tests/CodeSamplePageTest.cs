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
        #endregion

        /// <summary>
        /// Verify if choosing the filters can cause the displayed results updated 
        /// </summary>
        [TestMethod]
        public void S13_TC01_FilterCheckedWithCorrectEvent()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Browser.GetFilterCount();
            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Browser.SelectFilter(i);
                Assert.IsTrue(Browser.isFilterWorkable(i),
                    "The {0} filter should be valid to change displayed code samples",
                    filterName);
            }
        }

        /// <summary>
        /// Verify if all the checked filters can be cleared at one time
        /// </summary>
        [TestMethod]
        public void S13_TC02_CanFilterCleared()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Browser.GetFilterCount();

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
                    Browser.SelectFilter(randomIndex);
                }
            }

            Browser.ExecuteClearFilters();
            List<string> unclearedFilters;
            if (!Browser.areFiltersCleared(out unclearedFilters))
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
        /// Verify whether the URL can be updated accordingly when some filters are chosen
        /// </summary>
        [TestMethod]
        public void S13_TC03_CanURLUpdatedByCodeSampleFilters()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Browser.GetFilterCount();
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
                    filterNames.Add(Browser.SelectFilter(randomIndex));
                }
            }

            List<string> unContainedFilters;
            if (!Browser.AreFiltersInURL(filterNames, out unContainedFilters))
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

        /// <summary>
        /// Verify whether the code samples can be sorted by view count correctly
        /// </summary>
        [TestMethod]
        public void S13_TC04_CanSortCodeSamplesByViewCount()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Browser.GetFilterCount();
            
            //Randomly choose two filters to check
            int randomIndex;
            int usedIndex=filterCount;
            for (int i = 0; i < 2; i++)
            {
                do{
                randomIndex = new Random().Next(filterCount);
                } while (randomIndex==usedIndex);
                string filterName = Browser.SelectFilter(randomIndex);

                // Set the sort order as descendent
                Browser.SetSortOrder(SortType.ViewCount, true);
                List<SearchedResult> resultList = Browser.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount >= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be larger than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                // Set the sort order as ascendent
                Browser.SetSortOrder(SortType.ViewCount, false);
                resultList = Browser.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount <= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be smaller than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                usedIndex = randomIndex;
                Browser.ExecuteClearFilters();
            }
        }

        /// <summary>
        /// Verify whether the code samples can be sorted by the updated date correctly
        /// </summary>
        [TestMethod]
        public void S13_TC05_CanSortCodeSamplesByUpdatedDate()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Browser.GetFilterCount();

            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Browser.SelectFilter(i);

                // Set the sort order as descendent
                Browser.SetSortOrder(SortType.Date, true);
                List<SearchedResult> resultList = Browser.GetFilterResults();
                
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].UpdatedDate >= resultList[j + 1].UpdatedDate,
                        @"The updated date of ""{0}"" should be later than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                // Set the sort order as ascendent
                Browser.SetSortOrder(SortType.Date, false);
                resultList = Browser.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].UpdatedDate <= resultList[j + 1].UpdatedDate,
                        @"The updated date of ""{0}"" should be earlier than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                Browser.ExecuteClearFilters();
            }
        }     
    }
}
