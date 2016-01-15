using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public static class Browser
    {
        static IWebDriver webDriver = new ChromeDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/");

        //static IWebDriver webDriver = new InternetExplorerDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/IE32/");
        //static IWebDriver webDriver = new InternetExplorerDriver(System.IO.Directory.GetCurrentDirectory() + @"/Drivers/IE64/");
        //static IWebDriver webDriver = new FirefoxDriver();


        static string defaultTitle;
        static string defaultHandle = webDriver.CurrentWindowHandle;

        public static string BaseAddress
        {
            get { return Utility.GetConfigurationValue("BaseAddress"); }
            //get { return "http://officedevcenter-msprod-standby.azurewebsites.net"; }
            //get { return "http://officedevcentersite-orchard.azurewebsites.net"; }
            //get { return "http://localhost"; }
        }

        public static void Initialize()
        {
            SetWaitTime(TimeSpan.FromSeconds(30));
            webDriver.Navigate().GoToUrl(BaseAddress);
            defaultTitle = Title;
        }

        public static void InitializeGoogle()
        {
            webDriver.Navigate().GoToUrl("https://www.google.com");
        }

        public static void InitializeBing()
        {
            webDriver.Navigate().GoToUrl("https://www.bing.com/");
        }
        public static void Goto(string url)
        {
            webDriver.Navigate().GoToUrl(url);
            defaultTitle = Title;
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
        }

        public static SelectElement SelectElement(IWebElement webElement)
        {
            return new SelectElement(webElement);
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void Wait(TimeSpan timeSpan)
        {
            // need to replace with Framework wait methods: implicit wait, explicit wait
            Thread.Sleep((int)timeSpan.TotalSeconds * 1000);
        }

        public static void Wait(By by)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementExists(by));
        }

        public static void SetWaitTime(TimeSpan timeSpan)
        {
            webDriver.Manage().Timeouts().ImplicitlyWait(timeSpan);
            webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(timeSpan.TotalSeconds * 2));
            webDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(timeSpan.TotalSeconds * 2));
        }

        public static bool SwitchToWindow(string title)
        {
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

        public static bool SwitchToNewWindow()
        {
            // get all window handles
            IList<string> handlers = webDriver.WindowHandles;
            string newWindowHandle = string.Empty;
            foreach (var winHandler in handlers)
            {
                if (!winHandler.Equals(webDriver.CurrentWindowHandle))
                {
                    newWindowHandle = winHandler;
                    break;
                }
            }

            if (!newWindowHandle.Equals(string.Empty))
            {
                webDriver.SwitchTo().Window(newWindowHandle);
                return true;
            }
            else
            {
                webDriver.SwitchTo().DefaultContent();
                return false;
            }
        }

        public static bool SwitchBack()
        {
            //bool canSwitchBack = SwitchToWindow(defaultTitle);
            //defaultTitle = Title;
            //return canSwitchBack;
            if (!webDriver.CurrentWindowHandle.Equals(defaultHandle))
            {
                webDriver.Close();
                webDriver.SwitchTo().Window(defaultHandle);
                return true;
            }
            else
            {
                webDriver.SwitchTo().DefaultContent();
                return false;
            }
        }

        public static void GoBack()
        {
            if (!Title.Equals(defaultTitle))
            {
                webDriver.Navigate().Back();
            }
            else
            {
                webDriver.Navigate().Refresh();
            }
        }

        public static IWebElement FindElementInFrame(string frameIdOrName, By by)
        {
            IWebElement frame = FindFrame(frameIdOrName);
            if (frame != null)
            {
                webDriver.SwitchTo().Frame(frame);
                IWebElement element = webDriver.FindElement(by);
                webDriver.SwitchTo().DefaultContent();
                return element;
            }
            else
            {
                throw new NoSuchFrameException(string.Format("Cannot find frame with name {0}", frameIdOrName));
            }
        }

        public static IWebElement FindElement(By by, bool isRootElement = true)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                IList<IWebElement> frames = webDriver.FindElements(By.TagName("iframe"));
                IWebElement element = null;
                if (frames.Count > 0)
                {
                    for (int i = 0; i < frames.Count; i++)
                    {
                        webDriver.SwitchTo().Frame(frames[i]);
                        element = FindElement(by, false);
                        if (element != null)
                        {
                            if (isRootElement)
                            {
                                webDriver.SwitchTo().DefaultContent();
                            }

                            return element;
                        }
                    }
                }

                if (element == null)
                {
                    webDriver.SwitchTo().ParentFrame();
                }

                return element;
            }
        }

        public static void Click(IWebElement element)
        {
            (webDriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);
        }

        public static void SaveScreenShot(string PathAndFileName)
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)webDriver;
            Screenshot s = screenshot.GetScreenshot();
            s.SaveAsFile(PathAndFileName, System.Drawing.Imaging.ImageFormat.Png);

            //Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
            //string screenshot = ss.AsBase64EncodedString;
            //byte[] screenshotAsByteArray = ss.AsByteArray;

            // Save the screenshot
            //ss.SaveAsFile(PathAndFileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static bool ImageExist(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 15000;
            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static IWebElement FindFrame(string frameIdOrName)
        {
            IList<IWebElement> frames = webDriver.FindElements(By.TagName("iframe"));
            IWebElement frame = null;
            if (frames.Count > 0)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    if (frames[i].GetAttribute("id") == frameIdOrName || frames[i].GetAttribute("name") == frameIdOrName)
                    {
                        frame = frames[i];
                        return frame;
                    }
                    else
                    {
                        webDriver.SwitchTo().Frame(frames[i]);
                        frame = FindFrame(frameIdOrName);
                        if (frame != null)
                        {
                            return frame;
                        }
                    }
                }
            }

            if (frame == null)
            {
                webDriver.SwitchTo().ParentFrame();
            }

            return frame;
        }
    }
}