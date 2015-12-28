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
                return new HomePage();
            }
        }

        public static Navigation Navigation
        {
            get
            {
                return new Navigation();
            }
        }
        public static CardSetupPlatform CardSetupPlatform
        {
            get
            {
                return new CardSetupPlatform();
            }
        }

        public static CardTryItOut CardTryItOut
        {
            get
            {
                return new CardTryItOut();
            }
        }
        public static CardRegisterApp CardRegisterApp
        {
            get
            {
                return new CardRegisterApp(); ;
            }
        }
    }
}
