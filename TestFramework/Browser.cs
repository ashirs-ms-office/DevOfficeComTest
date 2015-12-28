using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestFramework
{
    public static class Browser
    {
        static IWebDriver webDriver = new ChromeDriver(@"d:\libraries");
        public static string BaseAddress
        {
            get { return "http://officedevcentersite-orchard.azurewebsites.net"; }
        }

        public static void Initialize()
        {
            webDriver.Navigate().GoToUrl(BaseAddress);
        }
        
        public static void Goto(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static string Url
        {
            get { return webDriver.Url; }
        }

        public static void Close()
        {
            webDriver.Quit();
            //webDriver.Close();
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            // need to replace with Framework wait methods: implicit wait, explicit wait
            Thread.Sleep((int) timeSpan.TotalSeconds*1000);
        }
    }
}