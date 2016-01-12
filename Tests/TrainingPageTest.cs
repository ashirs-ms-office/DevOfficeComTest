using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    /// <summary>
    /// Test class for Resources-->Training page 
    /// </summary>
    [TestClass]
    public class TrainingPageTest
    {
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

        /// <summary>
        /// Verify whether the filters in Training page can navigate to correct results
        /// </summary>
        [TestMethod]
        public void S14_TC01_Can_Filter_Trainings()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Utility.GetFilterCount();

            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Utility.SelectFilter(i);

                List<SearchedResult> resultList = Utility.GetFilterResults();
                Assert.AreNotEqual<int>(0,
                    resultList.Count,
                    "If select the filter {0}, there should be at least one training displayed",
                    filterName);
            }
        }

        /// <summary>
        /// Verify the search function in Resources->Training
        /// </summary>
        [TestMethod]
        public void S14_TC02_Can_Search_CorrectTrainings()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Utility.GetFilterCount();
            int randomIndex = new Random().Next(Utility.TypicalSearchText.Length);
            string searchString = Utility.TypicalSearchText[randomIndex];

            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Utility.SelectFilter(i);

                List<SearchedResult> resultList = Utility.GetFilterResults(searchString);
                foreach (SearchedResult resultInfo in resultList)
                {
                    bool isNameMatched = resultInfo.Name.ToLower().Contains(searchString.ToLower());
                    bool isDescriptionMatched = resultInfo.Description.ToLower().Contains(searchString.ToLower());
                    Assert.IsTrue(isNameMatched || isDescriptionMatched,
                        "Under {0} filter, the training:\n {1}:{2}\n should contain the search text: {3}",
                        filterName,
                        resultInfo.Name,
                        resultInfo.Description,
                        searchString);
                }
            }
        }

        /// <summary>
        /// Verify whether the trainings can be sorted by view count correctly
        /// </summary>
        [TestMethod]
        public void S14_TC03_Can_Sort_Trainings_By_ViewCount()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Utility.GetFilterCount();

            //Randomly choose two filters to check
            int randomIndex;
            int usedIndex = filterCount;
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    randomIndex = new Random().Next(filterCount);
                } while (randomIndex == usedIndex);
                string filterName = Utility.SelectFilter(randomIndex);

                // Set the sort order as descendent
                Utility.SetSortOrder(SortType.ViewCount, true);
                List<SearchedResult> resultList = Utility.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount >= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be larger than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                // Set the sort order as ascendent
                Utility.SetSortOrder(SortType.ViewCount, false);
                resultList = Utility.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].ViewCount <= resultList[j + 1].ViewCount,
                        @"The view count of ""{0}"" should be smaller than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }
                usedIndex = randomIndex;
            }
        }

        /// <summary>
        /// Verify whether the URL can be updated accordingly when a filter is chosen
        /// </summary>
        [TestMethod]
        public void S14_TC04_Can_URL_Updated_By_TrainingFilter()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Utility.GetFilterCount();
            string filterName;

            //Generate the index of filters to select
            int randomIndex = new Random().Next(filterCount);

            filterName = Utility.SelectFilter(randomIndex);

            List<string> unContainedFilters;
            Assert.IsTrue(Utility.AreFiltersInURL(new List<string> { filterName }, out unContainedFilters),
                "The filter {0} should be contained in URL!",
                filterName);
        }
    }
}
