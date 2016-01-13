using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace TestFramework
{
    public class SearchKeyWordPage : BasePage
    {
        public static void SearchGoogleKeyWord(KeyWord keyWord)
        {
            Browser.Driver.FindElement(By.Id("lst-ib")).Clear();
            string key = EnumExtension.GetDescription(keyWord);
            Browser.Driver.FindElement(By.Id("lst-ib")).SendKeys(key);
            Browser.Click(Browser.Driver.FindElement(By.Name("btnG")));
        }

        public static void SearchBingKeyWord(KeyWord keyWord)
        {
            Browser.Driver.FindElement(By.Id("sb_form_q")).Clear();
            string key = EnumExtension.GetDescription(keyWord); ;
            Browser.Driver.FindElement(By.Id("sb_form_q")).SendKeys(key);
            Browser.Click(Browser.Driver.FindElement(By.Id("sb_form_go")));
        }

        public static bool IsTopFiveInBingSearchResult()
        {
            bool isContain = false;
            try
            {
                var results = Browser.Driver.FindElements(By.CssSelector("ol#b_results>li.b_algo"));
                for (int i=0; i<5; i++)
                {
                    IWebElement result = results[i].FindElement(By.CssSelector("h2>a"));
                    if (result.Text.Equals("Microsoft Graph - Home") && result.GetAttribute("href").Contains("graph.microsoft.io"))
                    {
                        isContain = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }

            return isContain;
        }

        public static bool IsTopFiveInGoogleSearchResult()
        {
            bool isContain = false;
            try
            {
                var results = Browser.Driver.FindElements(By.CssSelector("ol#rso>div.srg>div.g"));
                for (int i = 0; i < 5; i++)
                {
                    IWebElement result = results[i].FindElement(By.CssSelector("div>h3>a"));
                    if (i==0 && result.Text.Equals("Microsoft Graph - Home") && result.GetAttribute("data-href").Contains("graph.microsoft.io"))
                    {
                        isContain = true;
                        break;
                    }
                    else if (result.Text.Equals("Microsoft Graph - Home") && result.GetAttribute("href").Contains("graph.microsoft.io"))
                    {
                        isContain = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }

            return isContain;
        }
    }
}