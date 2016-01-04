using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    class CommonMoreResource : BasePage
    {
        public void OutlookDevCenter()
        {
            Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://dev.outlook.com/MailAppsGettingStarted/GetStarted');\"]")).Click();
            
        }
        public void OfficeAddInTypes()
        {
            Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/en-us/library/office/jj220082.aspx#StartBuildingApps_TypesofApps')\"]")).Click();
        }
        public void DownLoadStarterSample()
        {
            Browser.Driver.FindElement(By.Id("more-github")).Click();

        }
        public void MoreCodeSamples(Product product)
        {
            switch (product)
            {
                case Product.Excel:
                    Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=excel,office%20add-ins')\"]")).Click();
                    break;
                case Product.Outlook:
                    Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,outlook')\"]")).Click();
                    break;
                case Product.PowerPoint:
                    Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,powerpoint')\"]")).Click();
                    break;
                case Product.Word:
                    Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('http://dev.office.com/codesamples#?filters=office%20add-ins,word')\"]")).Click();
                    break;
                default:
                    break;
            }

        }
        public void ReadTheDocs()
        {
            Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/library/office/jj220082.aspx');\"]")).Click();
        }
        public void DesignYourAddIn()
        {
            Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/EN-US/library/office/mt484317.aspx');\"]")).Click();
        }
        public void PublishYourAddIn()
        {
            Browser.Driver.FindElement(By.XPath("//div[@onclick=\"window.open('https://msdn.microsoft.com/EN-US/library/office/fp123515.aspx');\"]")).Click();
        }
    }
}
