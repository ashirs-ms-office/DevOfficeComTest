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
            Browser.Click(service);
        }
		
        public void ChooseServiceValue(ServiceToTry service, object value)
        {
            string serviceValue = null;
            switch (service)
            {
                case ServiceToTry.GetMessages:
                    {
                        switch ((GetMessagesValue)value)
                        {
                            case GetMessagesValue.Inbox:
                            case GetMessagesValue.Drafts:
                            case GetMessagesValue.DeletedItems:
                            case GetMessagesValue.SentItems:
                                serviceValue = value.ToString();
                                break;
                            default:
                                serviceValue = null;
                                break;
                        }
                        break;
                    }
                case ServiceToTry.GetFiles:
                    {
                        switch ((GetFilesValue)value)
                        {
                            case GetFilesValue.drive_root_children:
                                serviceValue = "drive/root/children";
                                break;
                            case GetFilesValue.me_drive:
                                serviceValue = "me/drive";
                                break;
                            default:
                                serviceValue = null;
                                break;
                        }
                        break;
                    }
                case ServiceToTry.GetUsers:
                    {
                        switch ((GetUsersValue)value)
                        {
                            case GetUsersValue.me:
                                serviceValue = "me";
                                break;
                            case GetUsersValue.me_manager:
                                serviceValue = "me/manager";
                                break;
                            case GetUsersValue.me_select_skills:
                                serviceValue = "me?$select=skills";
                                break;
                            case GetUsersValue.myOrganization_users:
                                serviceValue = "myOrganization/users";
                                break;
                            default:
                                serviceValue = null;
                                break;

                        }
                        break;
                    }
                case ServiceToTry.GetGroups:
                    {
                        switch ((GetGroupValue)value)
                        {
                            case GetGroupValue.me_memberOf:
                                serviceValue = "me/memberOf";
                                break;
                            case GetGroupValue.members:
                                serviceValue = "groups/41525360-8eca-49ce-bcee-b205cd0aa747/members";
                                break;
                            case GetGroupValue.drive_root_children:
                                serviceValue = "groups/41525360-8eca-49ce-bcee-b205cd0aa747/drive/root/children";
                                break;
                            case GetGroupValue.conversations:
                                serviceValue = "groups/41525360-8eca-49ce-bcee-b205cd0aa747/conversations";
                                break;
                            default:
                                serviceValue = null;
                                break;
                        }
                        break;
                    }
                case ServiceToTry.GetContacts:
                case ServiceToTry.GetEvents:
                default:
                    serviceValue = null;
                    break;
            }

            //Browser.SelectElement(Browser.Driver.FindElement(By.Id("valueSelection"))).SelectByValue(serviceValue);
            Browser.SelectElement(Browser.Driver.FindElement(By.Id("valueSelection"))).SelectByText(serviceValue);
            bool isValue = Browser.Driver.FindElement(By.Id("urlValue")).Text.Contains(serviceValue);
        }

        public void ClickTry()
        {
            var tryBtn = Browser.Driver.FindElement(By.Id("invokeurlBtn"));
            Browser.Click(tryBtn);

            if (Browser.Driver.FindElement(By.Id("responseBody")) != null)
            {
                Browser.Wait(TimeSpan.FromSeconds(3));
            }
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

        public bool CanGetResponse(ServiceToTry serviceToTry, object value)
        {
            var responseBody = Browser.Driver.FindElement(By.Id("responseBody"));
            int serviceIndex = (int)serviceToTry;
            switch (serviceIndex)
            {
                // To do: finish all services and parameters
                case (0):
                    switch ((GetMessagesValue)value)
                    {
                        case GetMessagesValue.Inbox:
                            string responseText = responseBody.Text;
                            return responseText.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/mailFolders('Inbox')/messages");
                        case GetMessagesValue.Drafts:
                            responseText = responseBody.Text;
                            return responseText.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/mailFolders('Drafts')/messages");
                        case GetMessagesValue.DeletedItems:
                            responseText = responseBody.Text;
                            return responseText.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/mailFolders('DeletedItems')/messages");
                        case GetMessagesValue.SentItems:
                            responseText = responseBody.Text;
                            return responseText.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/mailFolders('SentItems')/messages");
                        default:
                            return false;
                    }
                case (1):
                    return responseBody.Text.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/events");
                case (2):
                    return responseBody.Text.Contains(@"https://graph.microsoft.com/v1.0/$metadata#users('alexd%40a830edad9050849NDA1.onmicrosoft.com')/contacts");
                case (3):
                    switch ((GetFilesValue)value)
                    {
                        case GetFilesValue.drive_root_children:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#drive/root/children");
                        case GetFilesValue.me_drive:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#drives/$entity");
                        default:
                            return false;
                    }
                case (4):
                    switch ((GetUsersValue)value)
                    {
                        case GetUsersValue.me:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#users/$entity");
                        case GetUsersValue.me_manager:
                            return responseBody.Text.Contains(@"https://graph.microsoft.com/v1.0/$metadata#directoryObjects/$entity"); 
                        case GetUsersValue.me_select_skills:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#users(skills)/$entity");
                        case GetUsersValue.myOrganization_users:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#users");
                        default:
                            return false;
                    }
                case (5):
                    switch ((GetGroupValue)value)
                    {
                        case GetGroupValue.me_memberOf:
                            return responseBody.Text.Contains(@"https://graph.microsoft.com/v1.0/$metadata#directoryObjects");
                        case GetGroupValue.members:
                            return responseBody.Text.Contains(@"https://graph.microsoft.com/v1.0/$metadata#directoryObjects");
                        case GetGroupValue.drive_root_children:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#groups('41525360-8eca-49ce-bcee-b205cd0aa747')/drive/root/children");
                        case GetGroupValue.conversations:
                            return responseBody.Text.ToLower().Contains(@"https://graph.microsoft.com/v1.0/$metadata#groups('41525360-8eca-49ce-bcee-b205cd0aa747')/conversations");
                        default:
                            return false;
                    }

                default:
                    return false; 
            }
        }
    }
}