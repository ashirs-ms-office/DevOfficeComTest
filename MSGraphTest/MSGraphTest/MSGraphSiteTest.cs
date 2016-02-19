using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Site Test class for Microsoft Graph
    /// </summary>
    [TestClass]
    public class MSGraphSiteTest
    {
        #region Additional test attributes
        [ClassCleanup]
        public static void ClassCleanup()
        {
            GraphBrowser.Close();
        }
        #endregion

        /// <summary>
        /// Verify whether robots.txt specifies the site is accessible.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S06_TC01_CanAccessSiteRobots()
        {
            if (GraphBrowser.BaseAddress.Contains("graph.microsoft.io"))
            {
            Assert.Inconclusive("The test site should not be the production site");
            }
            string url = GraphBrowser.BaseAddress + "/robots.txt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string readResponse = readStream.ReadToEnd();
            bool disallowed = readResponse.Contains("Disallow:");
            Assert.IsTrue(disallowed, "The site should not be allowed to access");
        }

        // <summary>
        /// Verify whether robots.txt of production site specifies the site is accessible.
        /// </summary>
        [TestMethod]
        public void BVT_Graph_S06_TC02_CanAccessProductionSiteRobots()
        {
            string url = "http://graph.microsoft.io/robots.txt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string readResponse = readStream.ReadToEnd();
            bool disallowed = readResponse.Contains("Disallow:");
            bool allowed = readResponse.Contains("Allow:");
            Assert.IsTrue(!disallowed && allowed, "The site should be allowed to access");
        }
    }
}
