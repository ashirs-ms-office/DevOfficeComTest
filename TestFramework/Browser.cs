using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace TestFramework
{
    public static class Browser
    {
        static IWebDriver webDriver = new ChromeDriver(@"d:\libraries");
        static string defaultTitle;
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

        public static IWebDriver Driver
        {
            get { return webDriver; }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            // need to replace with Framework wait methods: implicit wait, explicit wait
            Thread.Sleep((int) timeSpan.TotalSeconds*1000);
        }

        public static bool SwitchToWindow(string title)
        {
            defaultTitle = Title;
            webDriver.SwitchTo().DefaultContent();

            // get all window handles
            IList<string> handlers = webDriver.WindowHandles;
            foreach (var winHandler in handlers)
            {
                webDriver.SwitchTo().Window(winHandler);
                if (webDriver.Title.Contains(title))
                {
                    return true;
                }
                else
                {
                    webDriver.SwitchTo().DefaultContent();
                }
            }

            return false;
        }

        public static bool SwitchBack()
        {
            bool canSwitchBack = SwitchToWindow(defaultTitle);
            defaultTitle = Title;
            return canSwitchBack;
        }
    }
}