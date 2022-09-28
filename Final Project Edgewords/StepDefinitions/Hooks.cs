using Final_Project_Edgewords.Base_Methods;
using Final_Project_Edgewords.POMPages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static Final_Project_Edgewords.Base_Methods.HelpfulMethods;

namespace Final_Project_Edgewords.StepDefinitions
{
    [Binding]
    internal class Hooks
    {
        private IWebDriver driver;
        private ScenarioContext _scenarioContext;
        string browser;
        HeadingLinksPOM headingLinks;

        CartPagePOM cartPagePOM;    
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }
        [Before]
        public void Setup()
        {
             string username = Environment.GetEnvironmentVariable("USERNAME");
            //string username = Environment.GetEnvironmentVariable("harry.williams @nfocus.co.uk");
             string password = Environment.GetEnvironmentVariable("PASSWORD");
          //  string password = Environment.GetEnvironmentVariable("Passforex1");
             browser = Environment.GetEnvironmentVariable("BROWSER");
           // string browser = Environment.GetEnvironmentVariable("chrome");
            ChromeOptions Options = new ChromeOptions();
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    _scenarioContext["myBrowser"] = browser;
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    Options.AcceptInsecureCertificates = true;
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine("No browser or unknown browser");
                    Console.WriteLine("Using Chrome");
                    driver = new ChromeDriver();
                    Options.AcceptInsecureCertificates = true;
                    break;
            }
            _scenarioContext["mydriver"] = driver;
        //    Console.WriteLine("In start browser is " + browser);
             string baseUrl = Environment.GetEnvironmentVariable("BASEURL");
           // string baseUrl = "https://www.edgewordstraining.co.uk/demo-site";
            driver.Url = baseUrl;
            _scenarioContext["myurl"] = baseUrl;
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("body > p > a")).Click();
        }
        [After("@CheckPrice")]
        public void TearDownPrice()
        {
            ClearCart();
            LogOut();
            driver.Quit();
            //Add clear cart to this
        }

        [After("@OrderItem")]
        public void TearDownOrder()
        {
            LogOut();
            driver.Quit();
        }

        public void ClearCart()
        {
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            Console.WriteLine("Clearing Cart");
            headingLinks.ClickCart();
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.RemoveItem();
        }

        public void LogOut()
        {
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            Console.WriteLine("Logging out");
            headingLinks.ClickMyAccount();
            MyAcountPOM myAcountPOM = new MyAcountPOM(driver);
            myAcountPOM.ClickLogout();
        }
    }
}
