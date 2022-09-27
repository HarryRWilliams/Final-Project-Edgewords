using Final_Project_Edgewords.Base_Methods;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;
using static OpenQA.Selenium.RelativeBy;

namespace Final_Project_Edgewords
{
    internal class Tests: SetupAndClosure
    {

        [Test]
        public void PurchasingAndDiscounts()
        {
            //TO DO FOR THIS TEST
            //CONVERT CSS SELECTOR TO SOMETHING CLEANER
            //COMMENT CODE
            //CONVERT TO POM
           // Console.WriteLine("Starting Test Case 1");
           // LoginToAccount(driver);
          
            //finding item to add to cart
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#menu-item-43 > a")).Click();
            WaitForElmStatic(driver, 10, By.CssSelector("#main > ul > li.product.type-product.post-27.status-publish.first.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart"));
         
            driver.FindElement(By.CssSelector("#main > ul > li.product.type-product.post-27.status-publish.first.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart")).Click();
           //viewing cart and comparing prices
            driver.FindElement(By.LinkText("Cart")).Click();


            //    TakeScreenshot(driver, "CartPage");
            //Thread.Sleep(2000);
            IWebElement priceTable = driver.FindElement(By.ClassName("cart-collaterals"));
            new Actions(driver).ScrollToElement(priceTable).Build().Perform();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView();", priceTable);
            TakeScreenshotElement(priceTable, "cart");


            //entering the coupon code 
            driver.FindElement(By.Id("coupon_code")).SendKeys("edgewords");
            driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr:nth-child(2) > td > div > button")).Click();
            Thread.Sleep(1000);
            //get the original price from the site
            string originalPriceText = driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi")).Text;
            Console.WriteLine("Original Price found from site: " + originalPriceText);
           //taking the string, passing it to a function to conver it and then finding 15% of it
            decimal originalPriceNum = ConvertPriceToDec(originalPriceText);
            decimal discountAmount = originalPriceNum * 15 / 100;
            decimal priceWithDiscount = originalPriceNum - discountAmount;
            //taking the amount the site lists the discound amount as and checking it against the result found
            string siteCalcDiscText = driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span")).Text;
            Console.WriteLine("Site's calculated discount amount is : " + siteCalcDiscText);
            decimal siteCalcDiscNum = ConvertPriceToDec(siteCalcDiscText);          
            Assert.That(discountAmount, Is.EqualTo(siteCalcDiscNum));
            //finding the shipping price
            string shippingPriceText = driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")).Text;
            Console.WriteLine("Site shipping price is:" + shippingPriceText);
            decimal shippingPriceNum = ConvertPriceToDec(shippingPriceText);
           //finding the total price and comparing it to the site
            decimal TotalPrice = originalPriceNum - discountAmount + shippingPriceNum;
            string siteCalcTot = driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span > bdi")).Text;
            Console.WriteLine("Site calculated total is " + siteCalcTot);
            decimal siteCalcTotNum = ConvertPriceToDec(siteCalcTot);
            Assert.That(siteCalcTotNum, Is.EqualTo(TotalPrice));
            //logging out of the site
            driver.FindElement(By.LinkText("My account")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            Assert.Pass("The site correctly calculated the costs");

        }
        public decimal ConvertPriceToDec(string inputString)
        {
            decimal convertedDec = System.Convert.ToDecimal(inputString.Trim(new Char[] { '£' }));
            return convertedDec;
        }

        public void LoginToAccount(IWebDriver driver)
        {
            driver.Url = baseUrl + "/my-account/";
            driver.FindElement(By.Id("username")).SendKeys("harry.williams@nfocus.co.uk");
            driver.FindElement(By.CssSelector("password")).SendKeys("Passforex1");
            driver.FindElement(By.CssSelector("#customer_login > div.u-column1.col-1 > form > p:nth-child(3) > button")).Click();
        }
    }
}