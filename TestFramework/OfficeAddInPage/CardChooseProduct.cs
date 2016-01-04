using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
            Browser.Click(product);

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

        public bool IsShowingExampleOrVideo(Product productName)
        {
            switch (productName)
            {
                case Product.Excel:
                    {
                        string frameId = "{C30CDC67-9AD9-4AD4-A608-9B8BCA4D04DA}";
                        var scenarioSelector = Browser.FindElementInFrame(frameId, By.Id("scenarioList"));
                        return scenarioSelector != null;
                    }
                case Product.Outlook:
                case Product.PowerPoint:
                case Product.Word:
                    {
                        //string frameId = "embedOfficeFrame";
                        //string frameId = "__omexExtensionAnonymousProxy";
                        //var proxyPoster = Browser.FindElementInFrame(frameId, By.Id("m_ewaEmbedForm"));
                        //var proxyPoster = Browser.FindElementInFrame(frameId, By.Id("PageHtml"));
                        var proxyPoster = Browser.FindElement(By.Id("PageHtml"));

                        return proxyPoster != null;
                    }
            }

            return false;
        }
    }
}
