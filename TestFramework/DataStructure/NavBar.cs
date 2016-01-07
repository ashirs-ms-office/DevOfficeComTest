using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TestFramework.DataStructure
{  
    public class NavBar
    {
        private static IReadOnlyList<IWebElement> navItems = Browser.Driver.FindElement(By.Id("navBarList")).FindElements(By.TagName("li"));

        /// <summary>
        /// the number of nav items in this nav bar
        /// </summary>
        public static int NavItemCount
        {
            get
            {
                return navItems.Count;
            }
        }

        /// <summary>
        /// Select a nav item
        /// </summary>
        /// <param name="index">The index of the nav item to select</param>
        public static void SelectNavItem(int index)
        {
            var element = navItems[index].FindElement(By.TagName("a"));
            Browser.Click(element);         
        }

        /// <summary>
        /// Verify whether the styles of navItems are appropriate
        /// </summary>
        /// <param name="index">The index of the nav item activating</param>
        public static void VerifyItemStyleCorrect(int index)
        {
            string itemClass;

            //All the nav items before the activating item(inclusively) should have "card-done" class 
            for (int i = 0; i <= index; i++)
            {
                //Temp: add delay time to make case pass
                System.Threading.Thread.Sleep(3000);
                itemClass = navItems[i].GetAttribute("class");
                if (!itemClass.Contains("card-done"))
                {
                    throw new Exception("The nav item "+navItems[i].Text+ @" should display the style of ""card-done"" class!");
                }
            }

            //Temp: add delay time to make case pass
            System.Threading.Thread.Sleep(3000);
            // The activating item should have "activating" class
            itemClass = navItems[index].GetAttribute("class");
            if (!itemClass.Contains("activating")) 
            {
                throw new Exception("The nav item " + navItems[index].Text + @" should display the style of ""activating"" class!");
            }

            //All the nav items after the activated one should have no classes 
            for (int i = index+1; i < navItems.Count; i++)
            {
                itemClass = navItems[i].GetAttribute("class");
                if (itemClass != string.Empty) 
                {
                    throw new Exception("The nav item " + navItems[i].Text + @" should not display the style of ""card-done"" class or ""activating"" class!");
                }
            }

            
        }
    }
}
