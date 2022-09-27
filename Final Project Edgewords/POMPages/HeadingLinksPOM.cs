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
        IWebDriver driver;
        public HeadingLinksPOM(IWebDriver driver)
        {
            this.driver = driver;
        }
        IWebElement accountLink => driver.FindElement(By.LinkText("My account")); 

        
        IWebElement dismissBox => driver.FindElement(By.LinkText("Dismiss"));
        IWebElement shopTab => driver.FindElement(By.LinkText("Shop"));
        IWebElement cartTab => driver.FindElement(By.LinkText("Cart"));

        public void ClickShop() //go to the shop page
        {
            shopTab.Click();
        }

        public void ClickCart() //go to the cart page
        {
            cartTab.Click();
        }

        public void ClickDismiss() //This closes an alert that can cause problems with the test
        {
            dismissBox.Click();
        }
        public void ClickMyAccount() //This takes the user to the account page
        {
            accountLink.Click();
        }
    }
}
