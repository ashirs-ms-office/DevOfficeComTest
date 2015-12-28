using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class CardSetupPlatform : BasePage
    {
        public void ChoosePlatform(Platform platformName)
        {
            if (!Browser.Url.Contains("/getting-started/office365apis"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started/office365apis#setup");
            }

            var platform = Browser.Driver.FindElement(By.Id("option-"+platformName.ToString().ToLower()));
            platform.Click();

            // Need refactor: Sometimes case failed for the platform setup text is not changed in time
            Browser.Wait(TimeSpan.FromSeconds(1));
        }

        public bool IsShowingPlatformSetup(Platform platformName)
        {
            //var setupPlatformDoc = Browser.Driver.FindElement(By.Id("ShowDocumentationDiv")).FindElement(By.TagName("h1"));
            //var setupPlatformDoc = Browser.Driver.FindElement(By.CssSelector("#SetupPlatform>div>h1"));
            var setupPlatformDoc = Browser.Driver.FindElement(By.CssSelector("#ShowDocumentationDiv>h1"));
            string platformDescription = EnumExtension.GetDescription(platformName).ToLower();
            return setupPlatformDoc.Text.ToLower().Contains(platformDescription);
        }
    }
}