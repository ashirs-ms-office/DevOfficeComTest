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
    }
}
