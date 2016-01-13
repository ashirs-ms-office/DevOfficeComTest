using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class CheckAllLinks
    {
        [TestMethod]
        public void S15_TC01_CheckAllLinks()
        {
            string linkCheckerPath = Utility.GetConfigurationValue("linkCheckerPath");
            System.Diagnostics.Process.Start(linkCheckerPath, "-r1 -t200 -Fhtml/result.html -v -q " + "http://dev.office.com" + " --check-extern --ignore-url=.*\\.js");
        }
    }
}
