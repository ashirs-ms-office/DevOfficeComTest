using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    public class OtherProductPage : BasePage
    {
        private OpenQA.Selenium.Remote.RemoteWebElement productTitle;

        public bool IsAt(string productName)
        {
            return productTitle.WrappedDriver.Title.Contains(productName);
        }

        public OtherProductPage()
        {
            Browser.Wait(By.CssSelector("head>title"));
            productTitle = (OpenQA.Selenium.Remote.RemoteWebElement)Browser.Driver.FindElement(By.CssSelector("head>title"));
        }
    }
}
