using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestFramework.Office365Page
{
    public class CardTryItOut : BasePage
    {
        public void ChooseService(ServiceToTry serviceToTry)
        {
            if (!Browser.Url.Contains("/getting-started/office365apis"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started/office365apis#try-it-out");
            }

            int serviceIndex = (int)serviceToTry;
            var service = Browser.Driver.FindElement(By.Id("serviceOption"+serviceIndex));
            service.Click();
        }

        public void ClickTry()
        {
            var tryBtn = Browser.Driver.FindElement(By.Id("invokeurlBtn"));
            tryBtn.Click();

            Browser.Wait(TimeSpan.FromSeconds(3));
           // var wait = new WebDriverWait(Browser.Driver as IWebDriver, TimeSpan.FromSeconds(5));
            //wait.Until(d => d.FindElement(By.Id("response-container")));
            //WebDriverWait wait = new WebDriverWait((Browser.Driver as IWebDriver), TimeSpan.FromSeconds(10));
            //IWebElement responseContainer = wait.Until(d =>
            //{
            //    return d.FindElement(By.Id("response-container"));
            //});

            try
            {
                var action = new Actions(Browser.Driver as IWebDriver);
                var responseContainer = Browser.Driver.FindElement(By.Id("response-container"));
                action.MoveToElement(responseContainer);
                action.Perform();
            }
            catch (Exception)
            {
                { }
                throw;
            }
        }

        public bool CanGetResponse(ServiceToTry serviceToTry)
        {
            var responseBody = Browser.Driver.FindElement(By.Id("responseBody"));
            int serviceIndex = (int)serviceToTry;
            switch (serviceIndex)
            {
                // To do
                case (0):
                case (1):
                case (2):
                case (3):
                    return true;
                case (4):
                    return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#users/$entity");
                case (5):
                    return true;

                default:
                    return false;
            }
        }
    }
}