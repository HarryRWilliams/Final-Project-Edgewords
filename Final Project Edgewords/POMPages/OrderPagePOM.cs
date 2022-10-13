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
        IWebDriver _driver;
        public OrderPagePOM(IWebDriver _driver)
        {
            this._driver = _driver;
        }
        IWebElement _orderNumber => _driver.FindElement(By.CssSelector(".order > strong")); //fetch the field that contains the order number
        IWebElement _orderPageTable => _driver.FindElement(By.CssSelector(".order_details.woocommerce-thankyou-order-details")); //Fetches the entire table to see full details


        public int CaptureOrderNumber(string _browser) //capture the order string from the page and convert it to a number
        {
            if (_browser != "firefox")
            {
                SettingUpScreenhot(_orderPageTable, _driver);
            }
            TakeScreenshotElement(_orderPageTable, "Order Page Number"); //Takes a picture that gives details about the order
            string _CapordernumberText = _orderNumber.Text; //retrieves the order number
            int _capOrderNumber = Convert.ToInt16(_CapordernumberText); //converts the number from a string to an int
            return _capOrderNumber;
        }
    }
}
