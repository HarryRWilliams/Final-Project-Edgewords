using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.Base_Methods
{
    internal static class HelpfulMethods
    {
        //This method waits for the element it is searching for to become available
        public static void WaitForElmStatic(IWebDriver driver, int Seconds, By locator) //This method waits for an element to appear before continuing 
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(Seconds));
            myWait.Until(drv => drv.FindElement(locator).Displayed);
        }

        /**
        public static void TakeScreenshot(IWebDriver driver, string FileName)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot file = ssdriver.GetScreenshot();
            file.SaveAsFile(@"C:\Screenshots\" + FileName + ".png", ScreenshotImageFormat.Png);
        }**/

        //This method takes a screenshot of a specific element so it can be attached in a report
        public static void TakeScreenshotElement (IWebElement elm, string Filename) //This takes a screenshot of an element and attaches it to the current test report
        {
            ITakesScreenshot sselm = elm as ITakesScreenshot;
            Screenshot file = sselm.GetScreenshot();
            file.SaveAsFile(@"C:\Screenshots\" + Filename + ".png", ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(@"C:\Screenshots\" + Filename + ".png");
        }
    }
}
