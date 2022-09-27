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
        IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        //Locator
        IWebElement firstNameField => driver.FindElement(By.Id("billing_first_name"));
        IWebElement lastNameField => driver.FindElement(By.Id("billing_last_name"));
        IWebElement streetAddress => driver.FindElement(By.Id("billing_address_1"));
        IWebElement townName => driver.FindElement(By.Id("billing_city"));
        IWebElement postcode => driver.FindElement(By.Id("billing_postcode"));
        IWebElement number => driver.FindElement(By.Id("billing_phone"));

        IWebElement checkPayment => driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque"));
        IWebElement tempOrderOptCheck => driver.FindElement(By.XPath("//*[@id=\"payment\"]/ul/li[2]"));
        IWebElement placeOrder => driver.FindElement(By.Id("place_order"));//*[@id="payment_method_cheque"]


        public void FillCheckoutForm() //this clears the already entered information and enters the correct details
        {
            firstNameField.Clear();
            firstNameField.SendKeys("Ben");
            lastNameField.Clear();
            lastNameField.SendKeys("Bank");
            streetAddress.Clear();
            streetAddress.SendKeys("11 In the Middle of our Street");
            townName.Clear();
            townName.SendKeys("Also not real town");
            postcode.Clear();
            postcode.SendKeys("SW1W 0NY");
            number.Clear();
            number.SendKeys("12345678910");
        }
        public void CheckPayments()
        {
            checkPayment.Click();
        }
        public void ed()
        {
            tempOrderOptCheck.Click();
        }
        public void PlaceOrder() //this redirects to the order page
        {
            placeOrder.Click();
        }

    }
}
