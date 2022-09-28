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
        IWebElement orderPageTable => driver.FindElement(By.CssSelector(".order_details.woocommerce-thankyou-order-details")); //Fetches the entire table to see full details


        public int CaptureOrderNumber(string browser) //capture the order string from the page and convert it to a number
        {
            if (browser != "firefox")
            {
                SettingUpScreenhot(orderPageTable,driver);
            }
            TakeScreenshotElement(orderPageTable, "Order Page Number"); //Takes a picture that gives details about the order
            string CapordernumberText = orderNumber.Text; //retrieves the order number
            int capOrderNumber = Convert.ToInt16(CapordernumberText); //converts the number from a string to an int
            return capOrderNumber;
        }
    }
}
