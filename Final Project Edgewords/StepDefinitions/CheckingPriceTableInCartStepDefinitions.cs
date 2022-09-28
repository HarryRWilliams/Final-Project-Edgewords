using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using System;
using System.Linq.Expressions;
using TechTalk.SpecFlow;
using NUnit.Framework;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

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
        private string couponWord; //a string to store the scenario word currently being used
        private string browser;
        //This POM page is used across multiple steps and thus is defined here
        CartPagePOM cartPage;

        //a constructor that sets up later needed variables
        public CheckingPriceTableInCartStepDefinitions(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
            Console.WriteLine(scenarioContext);
            this._driver = (IWebDriver)_scenarioContext["mydriver"];
            this._baseURL = (string)_scenarioContext["myurl"];
            cartPage = new CartPagePOM(_driver);
            this.browser = (string)_scenarioContext["myBrowser"];
        }             
        
        [Given(@"I have Logged into my Account")]
        public void GivenIHaveLoggedIntoMyAccount()
        {
            //The username and password are gotten from an external source
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");

            //The website is accessed and logged into 
           // _driver.Url = ("https://www.edgewordstraining.co.uk/demo-site//my-account/");
             _driver.Url = (_baseURL + "//my-account/");
            LoginPagePOM login = new LoginPagePOM(_driver);
            try
            {
                //If Login credentials returned true login was successful
                Assert.IsTrue(login.LoginWithValidCredentials(username, password), "We did not login");
            }
            catch
            {
                //Otherwise fail the test
                Assert.Fail("Login was unsucessful");
            }
        }
        //This step goes to the shop page and places an item into the cart before going to the cart page
        [When(@"I Add an Item to my Cart")]
        public void WhenIAddAnItemToMyCart()
        {
            ShopPagePOM shopPage = new ShopPagePOM(_driver);
            HeadingLinksPOM headers = new HeadingLinksPOM(_driver);
            headers.ClickShop();
            shopPage.ClickOnItem();
            headers.ClickCart();
        }

        //read in the word from the tested row
        [When(@"I enter the '([^']*)' Code")]
        public void WhenIEnterTheCode(string tableWord)
        {
            couponWord = tableWord; //asign the word from the feature file to a strong
            Console.WriteLine("Coupon read in is " + tableWord);
            try
            {
                Assert.IsTrue(cartPage.EnterCouponCode(tableWord)); //enter the feature file word into the coupon field and see if it's valid
            }
            catch
            {
                Assert.Fail("Coupon was invalid");
            }
        }

        [Then(@"the Total Price Takes (.*)% off of the Original Price")]
        public void ThenTheTotalPriceTakesOffOfTheOriginalPrice(int percentage)
        {
            Console.WriteLine("Browser from def is" + browser);
            //A screenshot is taken of the price table the browser decides if the picture needs to be setup the word is added to the file
            cartPage.TakePicOfPrice(browser, couponWord);

            //The text in the subtotal field is captured and converted into a useable number rather than a word
            string subTotalText = cartPage.CaptureSubTotal();
            Console.WriteLine("Sub Total found is " + subTotalText);
            decimal originalPriceNum = ConvertPriceToDec(subTotalText);

            //the discount amount is calculated by the original price / the percent given in the feature file * 100
            decimal discountAmount = originalPriceNum * percentage / 100;
            //This amount is then taken from the original price
            decimal priceWithDiscount = originalPriceNum - discountAmount;

            //wait for the coupon to be entered into the system
            WaitForElmStatic(_driver, 1, By.CssSelector(".cart-discount .woocommerce-Price-amount"));
            //when the coupon has been entered the new field added is captured and converted
            string siteCalculatedDiscount = cartPage.CaptureCouponDiscountField();
            Console.WriteLine("Site's calculated discount amount is : " + siteCalculatedDiscount);
            //Clear the coupon to enable a clean test next time it's run
            cartPage.RemoveCoupon();
            //Convert the discount number to a decimal
            decimal siteCalcDiscNum = ConvertPriceToDec(siteCalculatedDiscount);

            //the discount amount should be equal to the sum calculated above if it is not then the test fails
            try
            {
                Console.WriteLine("Original price is " + originalPriceNum + " Discount amount is " + discountAmount + " Site calculated discount is " + siteCalcDiscNum);
                //Check that the site's discount amount is equal to what it should be
                Assert.That(discountAmount, Is.EqualTo(siteCalcDiscNum));
            }
            catch
            {
                //If it wasn't this works out what discount percent the coupon did take off
                decimal actualDiscountPercent = siteCalcDiscNum / originalPriceNum * 100;
                Assert.Fail("Site does not give " + percentage + "% off instead it was " + actualDiscountPercent + "%");
            }
            //the shipping price is caputured converted and added to the sum
            string shippingPriceText = cartPage.CaptureShippingPrice();
            Console.WriteLine("Site shipping price is:" + shippingPriceText);
            decimal shippingPriceNum = ConvertPriceToDec(shippingPriceText);

            //the total price is calculated
            decimal TotalPrice = originalPriceNum - discountAmount + shippingPriceNum;
            //the site's calculated total is captured and converted
            string siteCalcTot = cartPage.CaptureTotalPrice();
            Console.WriteLine("Site calculated total is " + siteCalcTot);
            decimal siteCalcTotNum = ConvertPriceToDec(siteCalcTot);
            //Check that the total price is correct
            Assert.That(siteCalcTotNum, Is.EqualTo(TotalPrice));
        }

        [Then(@"I am given an order number which matches between the order and account page")]
        public void ThenIAmGivenAnOrderNumberWhichMatchesBetweenTheOrderAndAccountPage()
        {
            //Go to the checkout page
            CartPagePOM cartPage = new CartPagePOM(_driver);
            cartPage.ProceedToCheckout();
            CheckoutPage checkoutPage = new CheckoutPage(_driver);
            //the checkout details are filled in
            checkoutPage.FillCheckoutForm();
            Thread.Sleep(1000);
            //Check payments is selected
            checkoutPage.CheckPayments();
            //the order is placed and the order page is accessed
            checkoutPage.PlaceOrder();
            OrderPagePOM order = new OrderPagePOM(_driver);
            //Wait for the page to load
            WaitForElmStatic(_driver, 1, By.CssSelector(".order > strong"));
            //the order page will give an order number which is captured
            int recievedOrderNumber = order.CaptureOrderNumber(browser); 
            Console.WriteLine("Order number from Order Page is " + recievedOrderNumber);
            //The account page is accessed
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(_driver);
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcount = new MyAcountPOM(_driver);
            //The account orders are accessed
            myAcount.ClickOrders();
            //from the account page an order number will be displayed again and captured
            int accountOrderNumber = myAcount.GetOrderNumber(browser);
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
        //this small function takes the string given to it takes off the price symbol and returns it as a decimal
        public decimal ConvertPriceToDec(string inputString) 
        {
            decimal convertedDec = System.Convert.ToDecimal(inputString.Trim(new Char[] { '£' }));
            return convertedDec;
        }

    }
}
