using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class CardRegisterApp
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

            Browser.Wait(TimeSpan.FromSeconds(5));
           // var wait = new WebDriverWait(Browser.Driver as IWebDriver, TimeSpan.FromSeconds(5));
            //wait.Until(d => d.FindElement(By.Id("response-container")));
            //WebDriverWait wait = new WebDriverWait((Browser.Driver as IWebDriver), TimeSpan.FromSeconds(10));
            //IWebElement responseContainer = wait.Until(d =>
            //{
            //    return d.FindElement(By.Id("response-container"));
            //});


            //var responseContainer = Browser.Driver.FindElement(By.Id("response-container"));
            //action.MoveToElement(responseContainer);
            //action.Perform();
        }

        public SigninCommand SigninAs(string userName)
        {
            return new SigninCommand(userName);
        }

        public bool IsSignedin(string userName)
        {
            Browser.Wait(TimeSpan.FromSeconds(2));
            var registrationForm = Browser.Driver.FindElement(By.Id("registration-form"));
            return registrationForm.Displayed;
        }
    }
    public class SigninCommand
    {
        private readonly string userName;
        private string password;

        public SigninCommand(string userName)
        {
            this.userName = userName;
        }

        public SigninCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Signin()
        {
            var signinGoBtn = Browser.Driver.FindElement(By.Id("app-reg-signin"));
            signinGoBtn.Click();
            Browser.Wait(TimeSpan.FromSeconds(1));

            var signinInput = Browser.Driver.FindElement(By.Name("login"));
            signinInput.SendKeys(userName);
            var passwordInput = Browser.Driver.FindElement(By.Name("passwd"));
            passwordInput.SendKeys(password);
            var signinBtn = Browser.Driver.FindElement(By.Id("cred_sign_in_button"));
            Browser.Wait(TimeSpan.FromSeconds(1));

            signinBtn.Click();
        }
    }
}