using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class CardSetupPlatform
    {
        public void ChoosePlatform(string platformName)
        {
            var platform = Browser.Driver.FindElement(By.Id("option-"+platformName));
            platform.Click();

            Browser.Wait(TimeSpan.FromSeconds(1));
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