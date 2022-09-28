using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class OrderPagePOM
    {
        IWebDriver driver;
        public OrderPagePOM(IWebDriver driver)
        {
            this.driver = driver;
        }
        IWebElement orderNumber => driver.FindElement(By.CssSelector(".order > strong")); //fetch the field that contains the order number
        IWebElement accountLink => driver.FindElement(By.LinkText("My account"));


        public int CaptureOrderNumber(string browser) //capture the order string from the page and convert it to a number
        {
            if (browser != "firefox")
            {
                new Actions(driver).ScrollToElement(orderNumber).Build().Perform();
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView();", orderNumber);
            }
            TakeScreenshotElement(orderNumber, "Order Page Number");
            string CapordernumberText = orderNumber.Text;
          //  Console.WriteLine(CapordernumberText);
            int capOrderNumber = Convert.ToInt16(CapordernumberText);
            return capOrderNumber;
        }
    }
}
