﻿using OpenQA.Selenium;
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
        IWebElement orders => driver.FindElement(By.LinkText("Orders"));
        IWebElement AccountorderNumberCapture => driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number > a"));
        IWebElement AccountorderNumberTable => driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
        IWebElement logout => driver.FindElement(By.LinkText("Logout")); 
        public void ClickOrders()
        {
            orders.Click();
        }
        public int GetOrderNumber(string browser) //this goes to the order number section on the website trims it to just the number and converts the string to a number
        {
            if (browser != "firefox")
            {
                SettingUpScreenhot(AccountorderNumberTable);
            }
            TakeScreenshotElement(AccountorderNumberTable, "Acount Page Order Number");
            string orderNumberText = AccountorderNumberCapture.Text.Trim(new Char[] { '#' });
            //Console.WriteLine(orderNumberText);
            int orderNumber = Convert.ToInt16(orderNumberText);
            return orderNumber;
        }
        public void ClickLogout() //logout of account
        {
            logout.Click();
        }

    }
}
