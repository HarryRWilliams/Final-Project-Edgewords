using Final_Project_Edgewords.POMPages;
using NUnit.Framework;
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
            try
            {
                myWait.Until(drv => drv.FindElement(locator).Displayed);
            } 
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("Timeout: Retrying");
                Console.WriteLine("Stacktrace:"+e);
               myWait.Until(drv => drv.FindElement(locator).Displayed);
            }
        }
      /*  public static void WaitForPOMElementPresent(IWebDriver driver,IWebElement theElement,int timeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {
                wait.Until(drv.)
            }
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque");
            wait.Until(drv => drv.FindElement(theElement));
        }*/

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
