using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Final_Project_Edgewords.Base_Methods
{
    internal static class HelpfulMethods
    {
        public static IWebDriver driver;
        //This method waits for the element it is searching for to become available
        public static void WaitForElmStatic(IWebDriver driver, int Seconds, By locator) //This method waits for an element to appear before continuing 
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(Seconds));
            try
            {
                myWait.Until(drv => drv.FindElement(locator).Displayed);
            } 
            catch (WebDriverTimeoutException Timeout)
            {
                Console.WriteLine("Timeout: Retrying");
                Console.WriteLine("Stacktrace:"+Timeout);
               myWait.Until(drv => drv.FindElement(locator).Displayed);
            }
        }

        //This method takes a screenshot of a specific element so it can be attached in a report
        public static void TakeScreenshotElement (IWebElement elm, string Filename) //This takes a screenshot of an element and attaches it to the current test report
        {
            ITakesScreenshot sselm = elm as ITakesScreenshot;
            Screenshot file = sselm.GetScreenshot();
            file.SaveAsFile(@"C:\Screenshots\" + Filename + ".png", ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(@"C:\Screenshots\" + Filename + ".png");
        }
        public static void SettingUpScreenhot(IWebElement elementToCapture,IWebDriver driver) //This scrolls to the element for edge and chrome drivers
        {
            new Actions(driver).ScrollToElement(elementToCapture).Build().Perform();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView();", elementToCapture);
        }
    }
}
