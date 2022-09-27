// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class AddtocartTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  [SetUp]
  public void SetUp() {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  protected void TearDown() {
    driver.Quit();
  }
  //[Test]
  //public void AddToCart() {
  //  // Test name: Add to cart
  //  // Step # | name | target | value
  //  // 1 | open | /demo-site/my-account/ | 
  //  driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/demo-site/my-account/");
  //  // 3 | click | id=username | 
  //  driver.FindElement(By.Id("username")).Click();
  //  // 4 | click | id=username | 
  //  driver.FindElement(By.Id("username")).Click();
  //  // 5 | type | id=username | harry.williams@nfocus.co.uk
  //  driver.FindElement(By.Id("username")).SendKeys("harry.williams@nfocus.co.uk");
  //  // 6 | click | id=password | 
  //  driver.FindElement(By.Id("password")).Click();
  //  // 7 | type | id=password | Passforex1
  //  driver.FindElement(By.Id("password")).SendKeys("Passforex1");
  //  // 8 | click | name=login | 
  //  driver.FindElement(By.Name("login")).Click();
  //  // 9 | click | linkText=Add to cart | 
  //  driver.FindElement(By.LinkText("Add to cart")).Click();
  //  // 10 | click | linkText=Cart | 
  //  driver.FindElement(By.LinkText("Cart")).Click();
  //  // 11 | click | id=coupon_code | 
  //  driver.FindElement(By.Id("coupon_code")).Click();
  //  // 12 | type | id=coupon_code | edgewords
  //  driver.FindElement(By.Id("coupon_code")).SendKeys("edgewords");
  //  // 13 | click | name=apply_coupon | 
  //  driver.FindElement(By.Name("apply_coupon")).Click();
  //  // 14 | click | css=.woocommerce-breadcrumb > a | 
  //  driver.FindElement(By.CssSelector(".woocommerce-breadcrumb > a")).Click();
  //  // 15 | click | linkText=My account | 
  //  driver.FindElement(By.LinkText("My account")).Click();
  //  // 16 | click | linkText=Logout | 
  //  driver.FindElement(By.LinkText("Logout")).Click();
  //  // 17 | click | linkText=Shop | 
  //  driver.FindElement(By.LinkText("Shop")).Click();
  }