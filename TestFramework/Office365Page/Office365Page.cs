
using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class Office365Page
    {
        public CardSetupPlatform CardSetupPlatform
        {
            get
            {
                return new CardSetupPlatform();
            }
        }

        public CardTryItOut CardTryItOut
        {
            get
            {
                return new CardTryItOut();
            }
        }

        public CardRegisterApp CardRegisterApp
        {
            get
            {
                return new CardRegisterApp(); 
            }
        }

        public CardDownloadCode CardDownloadCode
        {
            get
            {
                return new CardDownloadCode();
            }
        }

        public CardMoreResources CardMoreResources
        {
            get
            {
                return new CardMoreResources(); 
            }
        }

        public bool IsAtOffice365Page()
        {
            return Browser.Title.Contains("Getting started with Office 365 REST APIs");
        }

        public bool OnlyDefaultCardsDisplayed()
        {
            var elements = Browser.Driver.FindElements(By.ClassName("card"));
            if (elements.Count > 0)
            {
                foreach (IWebElement item in elements)
                {
                    string itemId = item.GetAttribute("id");
                    if ((itemId == "intro" || itemId == "try-it-out" || itemId == "setup") && !item.Displayed)
                    {
                        return false;
                    }

                    if (itemId != "intro" && itemId != "try-it-out" && itemId != "setup" && item.Displayed)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
