using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class ShopPagePOM
    {
        IWebDriver driver;
        public ShopPagePOM(IWebDriver driver)
        {
            this.driver = driver;
        }
        //Locators
        IWebElement addBeanieToCart => driver.FindElement(By.CssSelector(".post-27 > .button"));
        public void ClickOnItem() //make shop page and add this to it
        {
            addBeanieToCart.Click(); //click on the add to cart option under the beanie
            WaitForElmStatic(driver, 10, By.LinkText("View cart")); //then wait for view cart to appear
        }
        string ef;
    }
}
