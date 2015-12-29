using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    public class CardChooseProduct : BasePage
    {
        public void ChooseProduct(Product productName)
        {
            if (!Browser.Url.Contains("/getting-started/addins"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started/addins#selectapp");
            }

            var product = Browser.Driver.FindElement(By.Id(productName.ToString()));
            product.Click();

            // Need refactor: Sometimes case failed for the product explore text is not changed in time
            Browser.Wait(TimeSpan.FromSeconds(2));
        }

        public bool IsShowingProductExplore(Product productName)
        {
            string product = productName.ToString().ToLower();
            try
            {
                var exploreInfoOfProduct = Browser.Driver.FindElement(By.CssSelector("#embedContents>div>h1"));
                return exploreInfoOfProduct.Text.ToLower().Contains(product);
            }
            catch (NoSuchElementException)
            {
                var exploreInfoOfProduct = Browser.Driver.FindElement(By.CssSelector("#embedContents>h1"));
                return exploreInfoOfProduct.Text.ToLower().Contains(product);
            }
        }
    }
}
