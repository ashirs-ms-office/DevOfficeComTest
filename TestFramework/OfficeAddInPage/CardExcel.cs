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
            resource.MoreCodeSamples(Product.Excel);
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
