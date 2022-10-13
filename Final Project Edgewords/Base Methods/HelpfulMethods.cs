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
            WebDriverWait _myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(Seconds));
            try
            {
                _myWait.Until(drv => drv.FindElement(locator).Displayed);
            } 
            catch (WebDriverTimeoutException Timeout)
            {
                Console.WriteLine("Timeout: Retrying");
                Console.WriteLine("Stacktrace:"+Timeout);
               _myWait.Until(drv => drv.FindElement(locator).Displayed);
            }
        }

        //This method takes a screenshot of a specific element so it can be attached in a report
        public static void TakeScreenshotElement (IWebElement _elm, string _filename) //This takes a screenshot of an element and attaches it to the current test report
        {
            ITakesScreenshot _sselm = _elm as ITakesScreenshot;
            Screenshot _file = _sselm.GetScreenshot();
            _file.SaveAsFile(@"C:\Screenshots\" + _filename + ".png", ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(@"C:\Screenshots\" + _filename + ".png");
        }
        public static void SettingUpScreenhot(IWebElement _elementToCapture,IWebDriver _driver) //This scrolls to the element for edge and chrome drivers
        {
            new Actions(_driver).ScrollToElement(_elementToCapture).Build().Perform();
            IJavaScriptExecutor js = _driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView();", _elementToCapture);
        }
    }
}
