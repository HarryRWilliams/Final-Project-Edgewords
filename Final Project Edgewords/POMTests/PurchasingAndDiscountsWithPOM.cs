using Final_Project_Edgewords.Base_Methods;
using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.POMTests
{
    internal class PurchasingAndDiscountsWithPOM : SetupAndClosure
    {
        [Test]
        public void PurchasingAndDiscountsWithPom()
        {
            Console.WriteLine("Starting Test Case 1");
            // HelpfulMethods.LoginToAccount(driver,baseUrl);
            MainPagePOM mainPage = new MainPagePOM(driver);
            HeadingLinksPOM headingLinksPOM = new HeadingLinksPOM(driver);
            headingLinksPOM.ClickShop();
            mainPage.ClickOnItem();
            headingLinksPOM.ClickCart();
            CartPagePOM cartPage = new CartPagePOM(driver);
            cartPage.TakePicOfPrice();
            cartPage.EnterCouponCode();
            string subTotalText = cartPage.CaptureSubTotal();
            Console.WriteLine("Sub Total found is " + subTotalText);
            decimal originalPriceNum = ConvertPriceToDec(subTotalText);
            decimal discountAmount = originalPriceNum * 10 / 100;
            decimal priceWithDiscount = originalPriceNum - discountAmount;
            string siteCalculatedDiscount = cartPage.CaptureCouponDiscountField();
            Console.WriteLine("Site's calculated discount amount is : " + siteCalculatedDiscount);
            decimal siteCalcDiscNum = ConvertPriceToDec(siteCalculatedDiscount);
            Assert.That(discountAmount, Is.EqualTo(siteCalcDiscNum));
            string shippingPriceText = cartPage.CaptureShippingPrice();
            Console.WriteLine("Site shipping price is:" + shippingPriceText);
            decimal shippingPriceNum = ConvertPriceToDec(shippingPriceText);
            //finding the total price and comparing it to the site
            decimal TotalPrice = originalPriceNum - discountAmount + shippingPriceNum;
            string siteCalcTot = cartPage.CaptureTotalPrice();
            Console.WriteLine("Site calculated total is " + siteCalcTot);
            decimal siteCalcTotNum = ConvertPriceToDec(siteCalcTot);
            Assert.That(siteCalcTotNum, Is.EqualTo(TotalPrice));
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcountPOM = new MyAcountPOM(driver);
            myAcountPOM.ClickLogout();
            Assert.Pass("The site correctly calculated the costs");

        }


        public decimal ConvertPriceToDec(string inputString)
        {
            decimal convertedDec = System.Convert.ToDecimal(inputString.Trim(new Char[] { '£' }));
            return convertedDec;
        }
    }
}
