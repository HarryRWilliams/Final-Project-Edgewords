using OpenQA.Selenium;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class ShopPagePOM
    {
        IWebDriver _driver;
        public ShopPagePOM(IWebDriver _driver)
        {
            this._driver = _driver;
        }
        //Locators
        IWebElement addBeanieToCart => _driver.FindElement(By.CssSelector(".post-27 > .button"));
        public void ClickOnItem(string _item) //make shop page and add this to it
        {
            Console.WriteLine("In switch method");
            switch (_item)
            {
                case "Beanie":
                    Console.WriteLine("Beanie found");
                    addBeanieToCart.Click();
                    break;
                default:
                    Console.WriteLine("No item found");
                    break;
            }
            WaitForElmStatic(_driver, 10, By.LinkText("View cart")); //then wait for view cart to appear
        }
    }
}
