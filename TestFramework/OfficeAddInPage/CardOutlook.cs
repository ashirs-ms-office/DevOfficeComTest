using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.OfficeAddInPage
{
    public class CardOutlook : BasePage
    {
        public OutlookExplore Explore
        {
            get
            {
                return new OutlookExplore();
            }
        }
        public CardBuild Build
        {
            get
            {
                return new CardBuild(Product.Outlook);
            }
        }
        public OutlookMoreResources MoreResouces
        {
            get
            {
                return new OutlookMoreResources();
            }
        }
    }
    public class OutlookExplore
    {
        public void play()
        {
            Browser.Driver.FindElement(By.CssSelector("button.ytp-large-play-button.ytp-button")).Click();
        }
    }
    public class OutlookBuild
    {
        public void StartBuilding()
        {
            Browser.Driver.FindElement(By.Id("more-playground")).Click();
        }

    }
    public class OutlookMoreResources
    {
        public void OutlookDevCenter()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.OutlookDevCenter();
        }
        public void OfficeAddInTypes()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.OfficeAddInTypes();
        }
        public void DownLoadStarterSample()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.DownLoadStarterSample();
        }
        public void MoreCodeSamples()
        {
            CommonMoreResources resource = new CommonMoreResources();
            resource.MoreCodeSamples(Product.Outlook);
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
