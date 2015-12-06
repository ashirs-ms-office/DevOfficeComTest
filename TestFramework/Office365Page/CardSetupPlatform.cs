using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace TestFramework.Office365Page
{
    public class CardSetupPlatform
    {
        public void ChoosePlatform(string platformName)
        {
            var platform = Browser.Driver.FindElement(By.Id("option-"+platformName));
            platform.Click();
            Thread.Sleep(1000);
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