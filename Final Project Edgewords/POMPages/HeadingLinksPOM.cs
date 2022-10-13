using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.POMPages
{
    internal class HeadingLinksPOM
    {
        IWebDriver _driver;
        public HeadingLinksPOM(IWebDriver _driver) //This POM page exists to remove awkwardness in accessing random POM pages
        {
            this._driver = _driver;
        }
        //Unless specified these links go to the heading links at the top page
        IWebElement _accountLink => _driver.FindElement(By.LinkText("My account")); 
        IWebElement _dismissBox => _driver.FindElement(By.LinkText("Dismiss")); //This is the pop up telling the user the site is a test site
        IWebElement _shopTab => _driver.FindElement(By.LinkText("Shop"));
        IWebElement _cartTab => _driver.FindElement(By.LinkText("Cart"));

        public void ClickShop() //go to the shop page
        {
            _shopTab.Click();
        }

        public void ClickCart() //go to the cart page
        {
            _cartTab.Click();
        }

        public void ClickDismiss() //This closes an alert that can cause problems with the test
        {
            _dismissBox.Click();
        }
        public void ClickMyAccount() //This takes the user to the account page
        {
            _accountLink.Click();
        }
    }
}
