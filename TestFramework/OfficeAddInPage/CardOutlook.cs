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
            CommonMoreResource resource = new CommonMoreResource();
            resource.OutlookDevCenter();
        }
        public void OfficeAddInTypes()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.OfficeAddInTypes();
        }
        public void DownLoadStarterSample()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.DownLoadStarterSample();
        }
        public void MoreCodeSamples()
        {
            CommonMoreResource resource = new CommonMoreResource();
            resource.MoreCodeSamples(Product.Outlook);
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
