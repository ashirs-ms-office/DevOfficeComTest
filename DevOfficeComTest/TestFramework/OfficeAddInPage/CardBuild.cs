using OpenQA.Selenium;
using System;

namespace TestFramework.OfficeAddInPage
{
    public class CardBuild : BasePage
    {
        private Product product;
        public void StartBuilding()
        {
            if (!Browser.Url.Contains("/getting-started/addins"))
            {
                Browser.Goto(Browser.BaseAddress + "/getting-started/addins#build");
            }

            switch (product)
            {
                case Product.Excel:
                case Product.Outlook:
                    {
                        var buildBtn = Browser.Driver.FindElement(By.Id("more-playground"));
                        Browser.Click(buildBtn);
                        break;
                    }
                case Product.PowerPoint:
                case Product.Word:
                    {
                        var buildBtn = Browser.Driver.FindElement(By.Id("build-downloadFromGithub"));
                        Browser.Click(buildBtn);
                        break;
                    }
            }

            // Need refactor: The waiting time is not stable
            Browser.Wait(TimeSpan.FromSeconds(10));

        }
        public bool IsShowingBuildPage()
        {
            bool canSwitchWindow = Browser.SwitchToNewWindow();
            bool isCorrectBuildPage = false;
            int retryCount = Int32.Parse(Utility.GetConfigurationValue("RetryCount"));
            int waitTime = Int32.Parse(Utility.GetConfigurationValue("WaitTime"));

            if (canSwitchWindow)
            {
                int i = 0;
                switch (product)
                {
                    case Product.Excel:
                        {
                            do
                            {
                                Browser.Wait(TimeSpan.FromSeconds(waitTime));
                                i++;
                                isCorrectBuildPage = Browser.Title.Contains("Task Pane Add-in Sample - Excel - Napa");
                            } while (i < retryCount && !isCorrectBuildPage);
                            break;
                        }
                    case Product.Outlook:
                        {
                            do
                            {
                                Browser.Wait(TimeSpan.FromSeconds(waitTime));
                                i++;
                                isCorrectBuildPage = Browser.Title.Contains("Mail Read Sample - Napa");
                            } while (i < retryCount && !isCorrectBuildPage);
                            break;
                        }
                    case Product.PowerPoint:
                    case Product.Word:
                        {
                            do
                            {
                                Browser.Wait(TimeSpan.FromSeconds(waitTime));
                                i++;
                                isCorrectBuildPage = Browser.Title.Contains("OfficeDev/Add-in-TaskPane-Sample");
                            } while (i < retryCount && !isCorrectBuildPage);
                            break;
                        }
                }
                Browser.SwitchBack();
            }
            return isCorrectBuildPage;
        }

        public CardBuild(Product product)
        {
            this.product = product;
        }
    }
}
