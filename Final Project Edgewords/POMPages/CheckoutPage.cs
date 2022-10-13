using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.POMPages
{
    internal class CheckoutPage
    {
        IWebDriver _driver;
        public CheckoutPage(IWebDriver _driver)
        {
            this._driver = _driver;
        }
        //These locators find the fields of the forms
        //Locator
        IWebElement _firstNameField => _driver.FindElement(By.Id("billing_first_name"));
        IWebElement _lastNameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement _streetAddress => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement _townName => _driver.FindElement(By.Id("billing_city"));
        IWebElement _postcode => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement _number => _driver.FindElement(By.Id("billing_phone"));
        //These locators click on the buttons
        IWebElement _checkPayment => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque")); //This clicks the checkPayment options
        IWebElement _placeOrder => _driver.FindElement(By.Id("place_order"));


        public void FillCheckoutForm() //this clears the already entered information and enters the correct details
        {
            _firstNameField.Clear();
            _firstNameField.SendKeys("Ben");
            _lastNameField.Clear();
            _lastNameField.SendKeys("Bank");
            _streetAddress.Clear();
            _streetAddress.SendKeys("11 In the Middle of our Street");
            _townName.Clear();
            _townName.SendKeys("Also not real town");
            _postcode.Clear();
            _postcode.SendKeys("SW1W 0NY");
            _number.Clear();
            _number.SendKeys("12345678910");
        }
        public void CheckPayments() //This clicks the check payments option
        {
            _checkPayment.Click();
        }
        public void PlaceOrder() //this redirects to the order page
        {
            _placeOrder.Click();
        }

    }
}
