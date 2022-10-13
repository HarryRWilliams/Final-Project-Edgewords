using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using System;
using System.Linq.Expressions;
using TechTalk.SpecFlow;
using NUnit.Framework;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

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
        private string _couponWord; //a string to store the scenario word currently being used
        private string _browser;
        //This POM page is used across multiple steps and thus is defined here
        CartPagePOM _cartPage;

        //a constructor that sets up later needed variables
        public CheckingPriceTableInCartStepDefinitions(ScenarioContext _scenarioContext) 
        {
            this._scenarioContext = _scenarioContext;
            Console.WriteLine(_scenarioContext);
            this._driver = (IWebDriver)this._scenarioContext["mydriver"];
            this._baseURL = (string)this._scenarioContext["myurl"];
            _cartPage = new CartPagePOM(_driver);
            this._browser = (string)this._scenarioContext["myBrowser"];
        }             
        
        [Given(@"I have Logged into my Account")]
        public void GivenIHaveLoggedIntoMyAccount()
        {
            //The username and password are gotten from an external source
            string _username = Environment.GetEnvironmentVariable("USERNAME");
            string _password = Environment.GetEnvironmentVariable("PASSWORD");

            //The website is accessed and logged into 
             _driver.Url = (_baseURL + "//my-account/");
            LoginPagePOM _login = new LoginPagePOM(_driver);
            try
            {
                //If Login credentials returned true login was successful
                Assert.IsTrue(_login.LoginWithValidCredentials(_username, _password), "We did not login");
            }
            catch
            {
                //Otherwise fail the test
                Assert.Fail("Login was unsucessful");
            }
        }
        //This step goes to the shop page and places an item into the cart before going to the cart page
        [When(@"I Add an item to my Cart followed by going to the cart")]
        public void WhenIAddAnItemToMyCartFollowedByGoingToTheCart(Table _table)
        {
            Console.WriteLine("Table item is " + _table);
        ShopPagePOM _shopPage = new ShopPagePOM(_driver);
            HeadingLinksPOM _headers = new HeadingLinksPOM(_driver);
            _headers.ClickShop();
            foreach(TableRow row in _table.Rows)
            {
                Console.WriteLine("Row is " + row[0]);
                _shopPage.ClickOnItem(row[0]);
            }
            _headers.ClickCart();
        }





        //read in the word from the tested row
        [When(@"I enter the '([^']*)' Code")]
        public void WhenIEnterTheCode(string _tableWord)
        {
            _couponWord = _tableWord; //asign the word from the feature file to a strong
            Console.WriteLine("Coupon read in is " + _tableWord);
            try
            {
                Assert.IsTrue(_cartPage.EnterCouponCode(_tableWord)); //enter the feature file word into the coupon field and see if it's valid
            }
            catch
            {
                Assert.Fail("Coupon was invalid");
            }
        }

        [Then(@"the test should have failed")]
        public void ThenTheTestShouldHaveFailed()
        {
            Assert.Fail("Coupon was allowed");
        }




        [Then(@"the Total Price Takes '([^']*)' off of the Original Price")]
        public void ThenTheTotalPriceTakesOffOfTheOriginalPrice(int _percentage)
        {

        Console.WriteLine("Browser from def is" + _browser);
            //A screenshot is taken of the price table the _browser decides if the picture needs to be setup the word is added to the file
            _cartPage.TakePicOfPrice(_browser, _couponWord);

            //The text in the subtotal field is captured and converted into a useable number rather than a word
            string _subTotalText = _cartPage.CaptureSubTotal();
            Console.WriteLine("Sub Total found is " + _subTotalText);
            decimal _originalPriceNum = ConvertPriceToDec(_subTotalText);

            //the discount amount is calculated by the original price / the percent given in the feature file * 100
            decimal _discountAmount = _originalPriceNum * _percentage / 100;
            //This amount is then taken from the original price
            decimal _priceWithDiscount = _originalPriceNum - _discountAmount;

            //wait for the coupon to be entered into the system
            WaitForElmStatic(_driver, 1, By.CssSelector(".cart-discount .woocommerce-Price-amount"));
            //when the coupon has been entered the new field added is captured and converted
            string _siteCalculatedDiscount = _cartPage.CaptureCouponDiscountField();
            Console.WriteLine("Site's calculated discount amount is : " + _siteCalculatedDiscount);
            //Clear the coupon to enable a clean test next time it's run
            _cartPage.RemoveCoupon();
            //Convert the discount number to a decimal
            decimal _siteCalcDiscNum = ConvertPriceToDec(_siteCalculatedDiscount);

            //the discount amount should be equal to the sum calculated above if it is not then the test fails
            try
            {
                Console.WriteLine("Original price is " + _originalPriceNum + " Discount amount is " + _discountAmount + " Site calculated discount is " + _siteCalcDiscNum);
                //Check that the site's discount amount is equal to what it should be
                Assert.That(_discountAmount, Is.EqualTo(_siteCalcDiscNum));
            }
            catch
            {
                //If it wasn't this works out what discount percent the coupon did take off
                decimal _actualDiscountPercent = _siteCalcDiscNum / _originalPriceNum * 100;
                Assert.Fail("Site does not give " + _percentage + "% off instead it was " + _actualDiscountPercent + "%");
            }
            //the shipping price is caputured converted and added to the sum
            string _shippingPriceText = _cartPage.CaptureShippingPrice();
            Console.WriteLine("Site shipping price is:" + _shippingPriceText);
            decimal _shippingPriceNum = ConvertPriceToDec(_shippingPriceText);

            //the total price is calculated
            decimal _TotalPrice = _originalPriceNum - _discountAmount + _shippingPriceNum;
            //the site's calculated total is captured and converted
            string _siteCalcTot = _cartPage.CaptureTotalPrice();
            Console.WriteLine("Site calculated total is " + _siteCalcTot);
            decimal _siteCalcTotNum = ConvertPriceToDec(_siteCalcTot);
            //Check that the total price is correct
            Assert.That(_siteCalcTotNum, Is.EqualTo(_TotalPrice));
        }

        [Then(@"I am given an order number which matches between the order and account page")]
        public void ThenIAmGivenAnOrderNumberWhichMatchesBetweenTheOrderAndAccountPage()
        {
            //Go to the checkout page
            CartPagePOM _cartPage = new CartPagePOM(_driver);
            _cartPage.ProceedToCheckout();
            CheckoutPage _checkoutPage = new CheckoutPage(_driver);
            //the checkout details are filled in
            _checkoutPage.FillCheckoutForm();
            Thread.Sleep(1000);
            //Check payments is selected
            _checkoutPage.CheckPayments();
            //the order is placed and the order page is accessed
            _checkoutPage.PlaceOrder();
            OrderPagePOM _order = new OrderPagePOM(_driver);
            //Wait for the page to load
            WaitForElmStatic(_driver, 1, By.CssSelector(".order > strong"));
            //the order page will give an order number which is captured
            int _recievedOrderNumber = _order.CaptureOrderNumber(_browser); 
            Console.WriteLine("Order number from Order Page is " + _recievedOrderNumber);
            //The account page is accessed
            HeadingLinksPOM _headingLinks = new HeadingLinksPOM(_driver);
            _headingLinks.ClickMyAccount();
            MyAcountPOM _myAcount = new MyAcountPOM(_driver);
            //The account orders are accessed
            _myAcount.ClickOrders();
            //from the account page an order number will be displayed again and captured
            int _accountOrderNumber = _myAcount.GetOrderNumber(_browser);
            Console.WriteLine("Order number from My Account is " + _accountOrderNumber);

            try //if the two order numbers are not equal the test will fail
            {
                Assert.That(_recievedOrderNumber, Is.EqualTo(_accountOrderNumber));
            }
            catch
            {
                Assert.Fail("Order numbers do not match on the order page it says " + _recievedOrderNumber + " but on the account page it says" + _accountOrderNumber);
            }
        }
        //this small function takes the string given to it takes off the price symbol and returns it as a decimal
        public decimal ConvertPriceToDec(string _inputString) 
        {
            decimal _convertedDec = System.Convert.ToDecimal(_inputString.Trim(new Char[] { '£' }));
            return _convertedDec;
        }

    }
}
