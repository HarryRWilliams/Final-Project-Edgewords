using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class MainPagePOM
    {
        IWebDriver driver;
        public MainPagePOM(IWebDriver driver)
        {
            this.driver = driver;
        }
        //Locators
        IWebElement addBeanieToCart => driver.FindElement(By.CssSelector(".post-27 > .button"));
        public void ClickOnItem() //make shop page and add this to it
        {
            //driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=27']")).Click();
            addBeanieToCart.Click(); //click on the add to cart option under the beanie
            WaitForElmStatic(driver, 10, By.LinkText("View cart")); //then wait for view cart to appear
        }
    }
}
