using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestFramework.Office365Page
{
    public class CardTryItOut
    {
        public void ChooseService(int serviceIndex)
        {
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

        public bool CanGetResponse(int serviceIndex)
        {
            var responseBody = Browser.Driver.FindElement(By.Id("responseBody"));
            switch (serviceIndex)
            {
                case (4):
                    return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#users/$entity");
                    
                default:
                    return false;
            }
        }
    }
}