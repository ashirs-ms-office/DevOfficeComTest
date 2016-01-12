using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
            int filterCount = Utility.GetFilterCount();
            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Utility.SelectFilter(i);
                Assert.IsTrue(Utility.isFilterWorkable(i),
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
            int filterCount = Utility.GetFilterCount();

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
                    Utility.SelectFilter(randomIndex);
                }
            }

            Utility.ExecuteClearFilters();
            List<string> unclearedFilters;
            if (!Utility.areFiltersCleared(out unclearedFilters))
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string unClearedFilter in unclearedFilters)
                {
                    stringBuilder.Append(unClearedFilter + ";");
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
            int filterCount = Utility.GetFilterCount();
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
                    filterNames.Add(Utility.SelectFilter(randomIndex));
                }
            }

            List<string> unContainedFilters;
            if (!Utility.AreFiltersInURL(filterNames, out unContainedFilters))
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
                Utility.ExecuteClearFilters();
            }
        }

        /// <summary>
        /// Verify whether the code samples can be sorted by the updated date correctly
        /// </summary>
        [TestMethod]
        public void S13_TC05_CanSortCodeSamplesByUpdatedDate()
        {
            Pages.Navigation.Select("Code Samples");
            int filterCount = Utility.GetFilterCount();

            for (int i = 0; i < filterCount; i++)
            {
                string filterName = Utility.SelectFilter(i);

                // Set the sort order as descendent
                Utility.SetSortOrder(SortType.Date, true);
                List<SearchedResult> resultList = Utility.GetFilterResults();

                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].UpdatedDate >= resultList[j + 1].UpdatedDate,
                        @"The updated date of ""{0}"" should be later than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                // Set the sort order as ascendent
                Utility.SetSortOrder(SortType.Date, false);
                resultList = Utility.GetFilterResults();
                for (int j = 0; j < resultList.Count - 1; j++)
                {
                    Assert.IsTrue(resultList[j].UpdatedDate <= resultList[j + 1].UpdatedDate,
                        @"The updated date of ""{0}"" should be earlier than or equal to its sibling ""{1}""'s",
                        resultList[j].Name,
                        resultList[j + 1].Name);
                }

                Utility.ExecuteClearFilters();
            }
        }

        /// <summary>
        /// Verify if choosing the filter SharePoint Add-ins can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC06_CanFindSharePointAddInSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("SharePoint Add-ins");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("sharepoint add-ins");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("sharepoint add-ins");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter SharePoint Add-ins");
        }

        /// <summary>
        /// Verify if choosing the filter Office Add-ins can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC07_CanFindOfficeAddInSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("Office Add-ins");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("office add-in");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("office add-in");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter Office Add-in");
        }

        /// <summary>
        /// Verify if choosing the filter Office 365 App can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC08_CanFindOffice365AppSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("Office 365 App");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("office 365");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("office 365");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter Office 365 App");
        }

        /// <summary>
        /// Verify if choosing the filter AngularJS can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC09_CanFindAngularJSSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("AngularJS");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("angularjs");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("angularjs");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter AngularJS");
        }

        /// <summary>
        /// Verify if choosing the filter C# can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC10_CanFindCSharpSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("C#");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains(".net");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains(".net");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter C#");
        }

        /// <summary>
        /// Verify if choosing the filter Java can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC11_CanFindJavaSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("Java");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("android");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("android");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter Java");
        }

        /// <summary>
        /// Verify if choosing the filter node.js can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC12_CanFindNodejsSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("node.js");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("node.js");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("node.js");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter node.js");
        }

        /// <summary>
        /// Verify if choosing the filter Objective C can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC13_CanFindObjectiveCSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("Objective C");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("ios");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("ios");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter Objective C");
        }

        /// <summary>
        /// Verify if choosing the filter PHP can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC14_CanFindPHPSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("PHP");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("php");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("php");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter PHP");
        }

        /// <summary>
        /// Verify if choosing the filter Python can get any correct sample. 
        /// </summary>
        [TestMethod]
        public void S13_TC15_CanFindPHPSamples()
        {
            Pages.Navigation.Select("Code Samples");
            Utility.SelectFilter("Python");
            bool hasFounded = false;

            List<SearchedResult> resultList = Utility.GetFilterResults();
            foreach (SearchedResult resultInfo in resultList)
            {
                bool isNameMatched = resultInfo.Name.ToLower().Contains("python");
                bool isDescriptionMatched = resultInfo.Description.ToLower().Contains("python");
                if (isNameMatched || isDescriptionMatched)
                {
                    hasFounded = true;
                    break;
                }
            }
            Assert.IsTrue(hasFounded, "There should be at least one sample which meets the filter Python");
        }
    }
}
