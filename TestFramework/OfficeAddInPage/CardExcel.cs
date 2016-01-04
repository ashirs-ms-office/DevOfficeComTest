using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TestFramework.OfficeAddInPage
{
    public class CardExcel : BasePage
    {
        public ExcelExplore Explore
        {
            get
            {
                return new ExcelExplore();
            }
        }
        public CardBuild Build
        {
            get
            {
                return new CardBuild(Product.Excel);
            }
        }
        public ExcelMoreResources MoreResouces
        {
            get
            {
                return new ExcelMoreResources();
            }
        }
    }
    public class ExcelExplore
    {

    }
    public class ExcelMoreResources
    {      
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
            resource.MoreCodeSamples(Product.Excel);
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
