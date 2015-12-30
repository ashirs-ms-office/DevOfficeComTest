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
        /// <summary>
        /// Test for the search function in Resources->Training
        /// </summary>
        [TestMethod]
        public void Can_Search_CorrectTrainings()
        {
            Pages.Navigation.Select("Resources", "Training");
            int trainingTypeCount = Pages.Navigation.GetSelectableTypeCount();
            string searchString = "a";
            for (int i = 0; i < trainingTypeCount; i++)
            {
                string trainingType = Pages.Navigation.SelectTrainingType(i);

                List<KeyValuePair<string, string>> resultList = Pages.Navigation.GetSearchResults(searchString);
                foreach (KeyValuePair<string, string> resultInfo in resultList)
                {
                    bool isNameMatched = resultInfo.Key.ToLower().Contains(searchString.ToLower());
                    bool isDescriptionMatched = resultInfo.Value.ToLower().Contains(searchString.ToLower());
                    Assert.IsTrue(isNameMatched || isDescriptionMatched, 
                        "Under {0} category, the training:\n {1}:{2}\n should contain the search text: {3}",
                        trainingType,
                        resultInfo.Key, 
                        resultInfo.Value,
                        searchString);
                }
            }
        }
    }
}
