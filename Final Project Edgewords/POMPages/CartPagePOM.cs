using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.POMPages
{
    internal class CartPagePOM
    {
        IWebDriver driver;
        public CartPagePOM(IWebDriver driver)
        {
            this.driver = driver;
        }
        //Locators 
        IWebElement proceedToCheck => driver.FindElement(By.LinkText("Proceed to checkout")); 
        IWebElement priceTable => driver.FindElement(By.ClassName("cart-collaterals")); //this captures the entire price table
        IWebElement couponCodeField => driver.FindElement(By.Id("coupon_code")); 
        IWebElement submitCoupon => driver.FindElement(By.XPath("//button[.='Apply coupon']")); //this uses xpath because the button does not have a name tag and needs a readable identifier
        IWebElement subTotalField => driver.FindElement(By.CssSelector("td:nth-child(2) > .woocommerce-Price-amount > bdi")); //this locates the subtotal
        IWebElement couponDiscount => driver.FindElement(By.CssSelector(".cart-discount .woocommerce-Price-amount")); //this locates the coupon discount amount
        IWebElement shipingPrice => driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")); //this locates the shipping price
        IWebElement totalField => driver.FindElement(By.CssSelector(".> tr.order-total > td")); //this locates the total price
       
        public void ProceedToCheckout() //This goes to checkout page
        {
            proceedToCheck.Click();
        }
        public void TakePicOfPrice() //this takes a picture of the price table
        {
            new Actions(driver).ScrollToElement(priceTable).Build().Perform();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView();", priceTable);
            TakeScreenshotElement(priceTable, "cart");
        }
        public void EnterCouponCode() //this enters the coupon
        {
            couponCodeField.SendKeys("edgewords");
            submitCoupon.Click();
        }
        public string CaptureSubTotal() //This places the site's subtotal into a varaible 
        {
            return subTotalField.Text;
        }
        public string CaptureCouponDiscountField() //This places the site's discount amount into a varaible 
        {
            return couponDiscount.Text;
        }

        public string CaptureShippingPrice()//This places the site's shipping price into a varaible 
        {
            return shipingPrice.Text;
        }
        public string CaptureTotalPrice() //This places the site's total price into a varaible 
        {
            return totalField.Text;
        }
    }
}
