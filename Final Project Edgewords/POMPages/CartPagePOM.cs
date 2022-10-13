using Final_Project_Edgewords.StepDefinitions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class CartPagePOM
    {
        IWebDriver _driver;
        public CartPagePOM(IWebDriver _driver)
        {
            this._driver = _driver;
        }
        //Locators 
        IWebElement _proceedToCheck => _driver.FindElement(By.LinkText("Proceed to checkout")); 
        IWebElement _priceTable => _driver.FindElement(By.ClassName("cart-collaterals")); //this captures the entire price table
        IWebElement _couponCodeField => _driver.FindElement(By.Id("coupon_code")); 
        IWebElement _submitCoupon => _driver.FindElement(By.XPath("//button[.='Apply coupon']")); //this uses xpath because the button does not have a name tag and needs a readable identifier
        IWebElement _subTotalField => _driver.FindElement(By.CssSelector("td:nth-child(2) > .woocommerce-Price-amount > bdi")); //this locates the subtotal
        IWebElement _couponDiscount => _driver.FindElement(By.CssSelector(".cart-discount .woocommerce-Price-amount")); //this locates the coupon discount amount
        IWebElement _shipingPrice => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")); //this locates the shipping price
        IWebElement _totalField => _driver.FindElement(By.CssSelector("tr.order-total > td > strong > span"));
        IWebElement _removeCouponLink => _driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
        IWebElement _removeItem => _driver.FindElement(By.CssSelector(".remove")); //This selects the x icon next to a item



        public void ProceedToCheckout() //This goes to checkout page
        {
            _proceedToCheck.Click();
        }
        public void TakePicOfPrice(string _browser,string _coupon) //this takes a picture of the price table
        {
            if (_browser != "firefox") //if the driver is firefox there is no 
            {
                SettingUpScreenhot(_priceTable,_driver);
            }
            TakeScreenshotElement(_priceTable, "Cart"+_coupon);
        }
        public bool EnterCouponCode(string _coupon) //this enters the coupon
        {
           // couponCodeField.SendKeys("edgewords");
            _couponCodeField.SendKeys(_coupon);
            _submitCoupon.Click();
            try
            {
                WaitForElmStatic(_driver, 3, By.ClassName("woocommerce-error"));
                //if a login failure alert is created then login was not successful
                IWebElement failurePopUp = _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch (Exception)
            {
                //No alert so catch error
                //We must have logged in 
                Console.WriteLine("Returning true - did find coup");
                return true;
            }
            Console.WriteLine("Returning false - didn't find coup in");
            return false; //If there was an alert we didn't login
        }
        public string CaptureSubTotal() //This places the site's subtotal into a varaible 
        {
            return _subTotalField.Text;
        }
        public string CaptureCouponDiscountField() //This places the site's discount amount into a varaible 
        {
            return _couponDiscount.Text;
        }

        public string CaptureShippingPrice()//This places the site's shipping price into a varaible 
        {
            return _shipingPrice.Text;
        }
        public string CaptureTotalPrice() //This places the site's total price into a varaible 
        {
            return _totalField.Text;
        }

        public void RemoveCoupon()
        {
            _removeCouponLink.Click();
        }

        public void RemoveItem()
        {
            _removeItem.Click();
        }
    }
}
