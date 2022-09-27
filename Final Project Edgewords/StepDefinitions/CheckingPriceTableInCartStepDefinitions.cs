using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using System;
using System.Linq.Expressions;
using TechTalk.SpecFlow;
using NUnit.Framework;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace Final_Project_Edgewords.StepDefinitions
{
    [Binding]
    public class CheckingPriceTableInCartStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private string _baseURL;
        //This POM page is used across multiple steps and thus is defined here
        CartPagePOM cartPage;

        //use class or object for billing info
        //make top nav a seperate page

        public CheckingPriceTableInCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            Console.WriteLine(scenarioContext);
            this._driver = (IWebDriver)_scenarioContext["mydriver"];
            this._baseURL = (string)_scenarioContext["myurl"];
            cartPage = new CartPagePOM(_driver);
        }             
        
        [Given(@"I have Logged into my Account")]
        public void GivenIHaveLoggedIntoMyAccount()
        {
            //The username and password are gotten from an external source
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");
            //The website is accessed and logged into 
            _driver.Url = (_baseURL + "//my-account/");
            LoginPagePOM login = new LoginPagePOM(_driver);
            bool DidWeLogin = login.LoginWithValidCredentials(username, password); 
            try
            {
                Assert.IsTrue(DidWeLogin, "We did not login"); //If no warning message appears then login was successful otherwise the test has failed
            }
            catch
            {
                Assert.Fail("Login was unsucessful");
            }
        }

        [When(@"I Add an Item to my Cart")]
        public void WhenIAddAnItemToMyCart() //This step goes to the shop page and places an item into the cart before going to the cart page
        {
            ShopPagePOM shopPage = new ShopPagePOM(_driver);
            HeadingLinksPOM headers = new HeadingLinksPOM(_driver);
            headers.ClickShop();
            shopPage.ClickOnItem();
            headers.ClickCart();
        }

        [When(@"I enter the Coupon Code")] 
        public void WhenIEnterTheCouponCode()
        {
            cartPage.EnterCouponCode();
        }

        [Then(@"the Total Price Takes (.*)% off of the Original Price")]
        public void ThenTheTotalPriceTakesOffOfTheOriginalPrice(int percentage)
        {
            cartPage.TakePicOfPrice(); //A screenshot is taken of the price table element
            string subTotalText = cartPage.CaptureSubTotal(); //The text in the subtotal field is captured and converted into a useable number rather than a word
            Console.WriteLine("Sub Total found is " + subTotalText);
            decimal originalPriceNum = ConvertPriceToDec(subTotalText); 
            decimal discountAmount = originalPriceNum * percentage / 100; //the discount amount is calculated by the original price / the percent given in the feature file * 100
            decimal priceWithDiscount = originalPriceNum - discountAmount; //This amount is then taken from the original price
            WaitForElmStatic(_driver, 3, By.CssSelector(".cart-discount .woocommerce-Price-amount"));
            string siteCalculatedDiscount = cartPage.CaptureCouponDiscountField(); //when the coupon has been entered the new field added is captured and converted
            Console.WriteLine("Site's calculated discount amount is : " + siteCalculatedDiscount);
            decimal siteCalcDiscNum = ConvertPriceToDec(siteCalculatedDiscount); 
            try //the discount amount should be equal to the sum calculated above if it is not then the test fails
            {
                Console.WriteLine("Original price is " + originalPriceNum + " Discount amount is " + discountAmount + " Site calculated discount is " + siteCalcDiscNum);
                Assert.That(discountAmount, Is.EqualTo(siteCalcDiscNum));
            }
            catch
            {
               decimal actualDiscountPercent = siteCalcDiscNum / originalPriceNum * 100; //this works out what discount percent the coupon did take off
                Assert.Fail("Site does not give " + percentage + "% off instead it was " + actualDiscountPercent +"%");
            }
            //Assert.That(discountAmount, Is.EqualTo(siteCalcDiscNum));

            string shippingPriceText = cartPage.CaptureShippingPrice(); //the shipping price is then caputured converted and added to the sum
            Console.WriteLine("Site shipping price is:" + shippingPriceText);
            decimal shippingPriceNum = ConvertPriceToDec(shippingPriceText);
            //finding the total price and comparing it to the site
            decimal TotalPrice = originalPriceNum - discountAmount + shippingPriceNum; //the total price is calculated
            string siteCalcTot = cartPage.CaptureTotalPrice(); //the sites amount is captured and converted
            Console.WriteLine("Site calculated total is " + siteCalcTot);
            decimal siteCalcTotNum = ConvertPriceToDec(siteCalcTot);
              Assert.That(siteCalcTotNum, Is.EqualTo(TotalPrice)); //if these are not the same then the test will fail
        }
        [Then(@"Logout of My Account")]
        public void ThenLogoutOfMyAccount() //the relevent elements are captured and the website is logged out of
        {
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(_driver);
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcountPOM = new MyAcountPOM(_driver);
            myAcountPOM.ClickLogout();
            Assert.Pass("The site correctly calculated the costs");
        }

        [Then(@"I am given an order number which matches between the order and account page")]
        public void ThenIAmGivenAnOrderNumberWhichMatchesBetweenTheOrderAndAccountPage()
        {
            CartPagePOM cartPage = new CartPagePOM(_driver);
            cartPage.ProceedToCheckout();
            CheckoutPage checkoutPage = new CheckoutPage(_driver);
            checkoutPage.FillCheckoutForm(); //the checkout details are filled in
            WaitForElmStatic(_driver,3,By.)
            checkoutPage.ed();
            checkoutPage.CheckPayments();
            checkoutPage.PlaceOrder(); //the order is placed
            OrderPagePOM order = new OrderPagePOM(_driver);
            Thread.Sleep(1000);

            int recievedOrderNumber = order.CaptureOrderNumber(); //the order page will give an order number which is captured
            Console.WriteLine("Order number from Order Page is" + recievedOrderNumber);
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(_driver);
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcount = new MyAcountPOM(_driver);
            myAcount.ClickOrders();
            int accountOrderNumber = myAcount.GetOrderNumber(); //from the account page an order number will be displayed again and captured
            Console.WriteLine("Order number from My Account is " + accountOrderNumber);
            try //if the two order numbers are not equal the test will fail
            {
                Assert.That(recievedOrderNumber, Is.EqualTo(accountOrderNumber));
            }
            catch
            {
                Assert.Fail("Order numbers do not match on the order page it says " + recievedOrderNumber + " but on the account page it says" + accountOrderNumber);
            }
        }

        public decimal ConvertPriceToDec(string inputString) //this small function takes the string given to it takes off the price symbol and returns it as a decimal
        {
            decimal convertedDec = System.Convert.ToDecimal(inputString.Trim(new Char[] { '£' }));
            return convertedDec;
        }

    }
}
