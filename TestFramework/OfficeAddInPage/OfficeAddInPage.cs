using OpenQA.Selenium;

namespace TestFramework.OfficeAddInPage
{
    public class OfficeAddInPage
    {
        ///// <summary>
        ///// The nav bar of the page
        ///// </summary>
        //public static NavBar OfficeAddInNavBar;
    
        public CardChooseProduct CardChooseProduct
        {
            get
            {
                return new CardChooseProduct();
            }
        }
        public CardExcel CardExcel
        {
            get
            {
                return new CardExcel();
            }
        }
        public CardOutlook CardOutlook
        {
            get
            {
                return new CardOutlook();
            }
        }
        public CardPowerPoint CardPowerPoint
        {
            get
            {
                return new CardPowerPoint();
            }
        }
        public CardWord CardWord
        {
            get
            {
                return new CardWord();
            }
        }

        public bool IsAtAddinPage()
        {
            return Browser.Title.Contains("Getting Started with Office Add-ins");
        }

        public bool OnlyDefaultCardsDisplayed()
        {
            var elements = Browser.Driver.FindElements(By.ClassName("card"));
            if (elements.Count > 0)
            {
                foreach (IWebElement item in elements)
                {
                    string itemId = item.GetAttribute("id");
                    if ((itemId == "intro" || itemId == "selectapp") && !item.Displayed)
                    {
                        return false;
                    }

                    if (itemId != "intro" && itemId != "selectapp" && item.Displayed)
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
