using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace TestFramework.Office365Page
{
    public class CardSetupPlatform
    {
        static string Url = "http://dev.office.com/getting-started/office365apis#setup";
        private static string PageTitle = "Office Dev Center - Getting started with Office 365 REST APIs";
        
        public void Goto()
        {
            Browser.Goto(Url);
        }

        public bool IsAt()
        {
            return Browser.Title == PageTitle;
        }

        public void ChoosePlatform(string platformName)
        {
            var platform = Browser.Driver.FindElement(By.Id("option-"+platformName));
            platform.Click();
            
            Thread.Sleep(1000);
            var setupPlatform = Browser.Driver.FindElement(By.Id("SetupPlatform"));
            Actions action = new Actions(Browser.Driver as IWebDriver);
            action.MoveToElement(setupPlatform).Perform();
            
        }

        public bool IsShowingPlatformSetup(string platformName)
        {
            var setupPlatformDoc = Browser.Driver.FindElement(By.Id("get-setup"));

            switch (platformName)
            {
                case ("android"):
                case("ios"):
                    return setupPlatformDoc.Text.ToLower().Contains(platformName);
                default:
                    return false;
            }
        }
    }
}