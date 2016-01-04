using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    public class OfficeAddInPage
    {
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
