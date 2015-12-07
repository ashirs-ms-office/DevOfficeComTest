using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using TestFramework.Office365Page;

namespace TestFramework
{
    public static class Pages
    {
        public static HomePage HomePage
        {
            get
            {
                var homePage = new HomePage();
                PageFactory.InitElements(Browser.Driver, homePage);
                return homePage;
            }
        }

        public static Navigation Navigation
        {
            get
            {
                var navigation = new Navigation();
                PageFactory.InitElements(Browser.Driver, navigation);
                return navigation;
            }
        }
        public static CardSetupPlatform CardSetupPlatform
        {
            get
            {
                var cardSetupPlatform = new CardSetupPlatform();
                PageFactory.InitElements(Browser.Driver, cardSetupPlatform);
                return cardSetupPlatform;
            }
        }

        public static CardTryItOut CardTryItOut
        {
            get
            {
                var cardTryItOut = new CardTryItOut();
                PageFactory.InitElements(Browser.Driver, cardTryItOut);
                return cardTryItOut;
            }
        }
        public static CardRegisterApp CardRegisterApp
        {
            get
            {
                var cardRegisterApp = new CardRegisterApp();
                PageFactory.InitElements(Browser.Driver, cardRegisterApp);
                return cardRegisterApp;
            }
        }
    }
}
