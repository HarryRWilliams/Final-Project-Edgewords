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
    internal class OrderingWithPOM:SetupAndClosure
    {
        [Test]
        public void CheckOrderNumbers()
        {
            MainPagePOM main = new MainPagePOM(driver);
            HeadingLinksPOM header = new HeadingLinksPOM(driver);
            header.ClickShop();
            //Thread.Sleep(1000);
            main.ClickOnItem(); //needs to be buy an item
            header.ClickCart();
            CartPagePOM cartPage = new CartPagePOM(driver);
            cartPage.ProceedToCheckout();
            CheckoutPage checkoutPage = new CheckoutPage(driver);
            checkoutPage.FillCheckoutForm();
            Thread.Sleep(1000);
            checkoutPage.PlaceOrder();
            OrderPagePOM order = new OrderPagePOM(driver);
            Thread.Sleep(1000);
            int recievedOrderNumber = order.CaptureOrderNumber();
            Console.WriteLine("Order number from Order Page is" + recievedOrderNumber);
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcount = new MyAcountPOM(driver);
            myAcount.ClickOrders();
            int accountOrderNumber = myAcount.GetOrderNumber();
            Console.WriteLine("Order number from My Account is "+ accountOrderNumber);
            Assert.That(recievedOrderNumber, Is.EqualTo(accountOrderNumber));
            headingLinks.ClickMyAccount();
            myAcount.ClickLogout();
        }
    }
}
