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
    internal class MyAcountPOM
    {
        IWebDriver _driver;
        public MyAcountPOM(IWebDriver _driver)
        {
            this._driver = _driver;
        }

        //locators
        IWebElement _orders => _driver.FindElement(By.LinkText("Orders")); //Link to the orders page
        IWebElement _accountorderNumberCapture => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number > a")); //The field that stores the order num
        IWebElement _accountorderNumberTable => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number")); //An element so the screenshot can be taken further back
        IWebElement _logout => _driver.FindElement(By.LinkText("Logout")); 
        public void ClickOrders() //Go to orders page
        {
            _orders.Click();
        }
        public int GetOrderNumber(string _browser) //this goes to the order number section on the website trims it to just the number and converts the string to a number
        {
            if (_browser != "firefox") //The screenshot needs to be set up if Firefox is not being used
            {
                SettingUpScreenhot(_accountorderNumberTable, _driver);
            }
            TakeScreenshotElement(_accountorderNumberTable, "Acount Page Order Number"); //Takes a picture of the number
            string _orderNumberText = _accountorderNumberCapture.Text.Trim(new Char[] { '#' }); //removes any formating to get just the number
            int _orderNumber = Convert.ToInt16(_orderNumberText); //convert the number from string to an int
            return _orderNumber;
        }
        public void ClickLogout() //logout of account
        {
            _logout.Click();
        }

    }
}
