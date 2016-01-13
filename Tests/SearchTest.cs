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
    public class SearchTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

        }

        [TestMethod]
        public void BVT_S16_TC01_SearchGoogle()
        {
            Browser.InitializeGoogle();
            foreach (KeyWord keyword in Enum.GetValues(typeof(KeyWord)))
            {
                SearchKeyWordPage.SearchGoogleKeyWord(keyword);
                Assert.IsTrue(SearchKeyWordPage.IsTopFiveInGoogleSearchResult(), string.Format("The Graph home page is not in top 5 when searching keyword \"{0}\" with Google.", keyword.ToString()));
            }
        }
        [TestMethod]
        public void BVT_S16_TC02_SearchBing()
        {
            Browser.InitializeBing();
            foreach (KeyWord keyword in Enum.GetValues(typeof(KeyWord)))
            {
                SearchKeyWordPage.SearchBingKeyWord(keyword);
                Assert.IsTrue(SearchKeyWordPage.IsTopFiveInBingSearchResult(), string.Format("The Graph home page is not in top 5 when searching keyword \"{0}\" with Bing.", keyword.ToString()));
            }
        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            Browser.Close();
        }
    }
}