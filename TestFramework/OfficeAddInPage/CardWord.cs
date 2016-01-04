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
            CommonMoreResource resource= new CommonMoreResource ();
            resource.OfficeAddInTypes();
        }
        public void MoreCodeSamples()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.MoreCodeSamples(Product.Word);
        }
        public void ReadTheDocs()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.ReadTheDocs();
        }
        public void DesignYourAddIn()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.DesignYourAddIn();
        }
        public void PublishYourAddIn()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.PublishYourAddIn();
        }
    }
}
