using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    /// <summary>
    /// Test class for pages which are navigated to by the links under Resources
    /// </summary>
    [TestClass]
    public class ResourcePageTest
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
        public void Can_Filter_Trainings()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Pages.Navigation.GetFilterCount();
            
            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Pages.Navigation.SelectFilter(i);

                List<KeyValuePair<string, string>> resultList = Pages.Navigation.GetFilterResults();
                Assert.AreNotEqual<int>(0,
                    resultList.Count,
                    "If select the filter {0}, there should be at least one training displayed",
                    filterName);
            }
        }

        /// <summary>
        /// Test for the search function in Resources->Training
        /// </summary>
        [TestMethod]
        public void Can_Search_CorrectTrainings()
        {
            Pages.Navigation.Select("Resources", "Training");
            int filterCount = Pages.Navigation.GetFilterCount();
            string searchString = "a";
            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Pages.Navigation.SelectFilter(i);

                List<KeyValuePair<string, string>> resultList = Pages.Navigation.GetFilterResults(searchString);
                foreach (KeyValuePair<string, string> resultInfo in resultList)
                {
                    bool isNameMatched = resultInfo.Key.ToLower().Contains(searchString.ToLower());
                    bool isDescriptionMatched = resultInfo.Value.ToLower().Contains(searchString.ToLower());
                    Assert.IsTrue(isNameMatched || isDescriptionMatched, 
                        "Under {0} filter, the training:\n {1}:{2}\n should contain the search text: {3}",
                        filterName,
                        resultInfo.Key, 
                        resultInfo.Value,
                        searchString);
                }
            }
        }
    }
}
