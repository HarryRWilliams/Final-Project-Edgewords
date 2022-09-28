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
        IWebDriver driver;
        public MyAcountPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //locators
        IWebElement orders => driver.FindElement(By.LinkText("Orders")); //Link to the orders page
        IWebElement AccountorderNumberCapture => driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number > a")); //The field that stores the order num
        IWebElement AccountorderNumberTable => driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number")); //An element so the screenshot can be taken further back
        IWebElement logout => driver.FindElement(By.LinkText("Logout")); 
        public void ClickOrders() //Go to orders page
        {
            orders.Click();
        }
        public int GetOrderNumber(string browser) //this goes to the order number section on the website trims it to just the number and converts the string to a number
        {
            if (browser != "firefox") //The screenshot needs to be set up if Firefox is not being used
            {
                SettingUpScreenhot(AccountorderNumberTable,driver);
            }
            TakeScreenshotElement(AccountorderNumberTable, "Acount Page Order Number"); //Takes a picture of the number
            string orderNumberText = AccountorderNumberCapture.Text.Trim(new Char[] { '#' }); //removes any formating to get just the number
            int orderNumber = Convert.ToInt16(orderNumberText); //convert the number from string to an int
            return orderNumber;
        }
        public void ClickLogout() //logout of account
        {
            logout.Click();
        }

    }
}
