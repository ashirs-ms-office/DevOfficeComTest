using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class CardRegisterApp : BasePage
    {
        public SigninCommand SigninAs(string userName)
        {
            if (!Browser.Url.Contains("/getting-started/office365apis"))
            {
                Browser.Goto(Browser.BaseAddress + "/Getting-Started/office365Apis#register-app");
            }

            return new SigninCommand(userName);
        }

        public bool IsSignedin(string userName)
        {
            Browser.Wait(TimeSpan.FromSeconds(2));
            var registrationForm = Browser.Driver.FindElement(By.Id("registration-form"));
            return registrationForm.Displayed;
        }

        /// <summary>
        /// Choose to sign in later in the register app card
        /// </summary>
        public void SigninLater()
        {
            var signedinLater = Browser.Driver.FindElement(By.Id("app-reg-signin-later"));
            Browser.Click(signedinLater);
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
            Browser.Click(signinGoBtn);
            Browser.Wait(TimeSpan.FromSeconds(1));

            var signinInput = Browser.Driver.FindElement(By.Name("login"));
            signinInput.SendKeys(userName);
            var passwordInput = Browser.Driver.FindElement(By.Name("passwd"));
            passwordInput.SendKeys(password);
            var signinBtn = Browser.Driver.FindElement(By.Id("cred_sign_in_button"));
            Browser.Wait(TimeSpan.FromSeconds(1));

            Browser.Click(signinBtn);
        }
    }
}