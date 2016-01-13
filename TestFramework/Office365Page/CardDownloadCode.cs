using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Office365Page
{
    public class CardDownloadCode : BasePage
    {
        public void DownloadCode()
        {
            var downloadBtn = Browser.Driver.FindElement(By.Id("downloadCodeSampleButton"));
            Browser.Click(downloadBtn);

            Browser.Wait(TimeSpan.FromSeconds(2));
        }

        public bool IsCodeDownloaded()
        {
            var downloadResult = Browser.Driver.FindElement(By.Id("post-download-instructions"));
            return downloadResult.Displayed;
        }
    }
}
