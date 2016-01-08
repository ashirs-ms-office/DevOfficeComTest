using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    class CommonMoreResources : BasePage
    {
        public void OutlookDevCenter()
        {
            Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://dev.outlook.com/MailAppsGettingStarted/GetStarted');\"]")));
            
        }
        public void OfficeAddInTypes()
        {
            Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/en-us/library/office/jj220082.aspx#StartBuildingApps_TypesofApps')\"]")));
        }
        public void DownLoadStarterSample()
        {
            Browser.Click(Browser.Driver.FindElement(By.Id("more-github")));
        }
        public void MoreCodeSamples(Product product)
        {
            switch (product)
            {
                case Product.Excel:
                    Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=excel,office%20add-ins')\"]")));
                    break;
                case Product.Outlook:
                    Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,outlook')\"]")));
                    break;
                case Product.PowerPoint:
                    Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,powerpoint')\"]")));
                    break;
                case Product.Word:
                    Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,word')\"]")));
                    break;
                default:
                    break;
            }

        }
        public void ReadTheDocs()
        {
            Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/library/office/jj220082.aspx');\"]")));
        }
        public void DesignYourAddIn()
        {
            Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/EN-US/library/office/mt484317.aspx');\"]")));
        }
        public void PublishYourAddIn()
        {
            Browser.Click(Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/EN-US/library/office/fp123515.aspx');\"]")));
        }
        public bool IsShowingMoreResourcePage()
        {
            bool canSwitchWindow = Browser.SwitchToNewWindow();
            if (canSwitchWindow)
            {
                Browser.SwitchBack();
            }

            return canSwitchWindow;

        }
    }
}
