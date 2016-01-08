using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    public class CardWord : BasePage
    {
        public WordExplore Explore
        {
            get
            {
                return new WordExplore();
            }
        }
        public CardBuild Build
        {
            get
            {
                return new CardBuild(Product.Word);
            }
        }
        public WordMoreResources MoreResouces
        {
            get
            {
                return new WordMoreResources();
            }
        }
    }
    public class WordExplore
    {
        public void play()
        {
            Browser.Driver.FindElement(By.CssSelector("button.ytp-play-button.ytp-button")).Click();
        }
    }
    public class WordMoreResources
    {
        public void OfficeAddInTypes()
        {
            CommonMoreResources resource= new CommonMoreResources ();
            resource.OfficeAddInTypes();
        }
        public void MoreCodeSamples()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.MoreCodeSamples(Product.Word);
        }
        public void ReadTheDocs()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.ReadTheDocs();
        }
        public void DesignYourAddIn()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.DesignYourAddIn();
        }
        public void PublishYourAddIn()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.PublishYourAddIn();
        }
    }
}
