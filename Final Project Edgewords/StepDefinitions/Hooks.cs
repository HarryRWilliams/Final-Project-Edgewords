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
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }
        [Before]
        public void Setup()
        {
            //The Browser is found from the secret file or console
            string browser = Environment.GetEnvironmentVariable("BROWSER");
            //The options are created here so the same name can be used across cases
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
                    _scenarioContext["myBrowser"] = browser;
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    _scenarioContext["myBrowser"] = browser;
                    break;
                default:
                    Console.WriteLine("No browser or unknown browser");
                    Console.WriteLine("Using Chrome");
                    driver = new ChromeDriver();
                    Options.AcceptInsecureCertificates = true;
                    _scenarioContext["myBrowser"] = browser;
                    break;
            }
            //This is stored in scenario context to be used later
            _scenarioContext["mydriver"] = driver;
            //The URL is retrieved from the console or secret file
             string baseUrl = Environment.GetEnvironmentVariable("BASEURL");
            //The driver is then taken to this url
            driver.Url = baseUrl;
            //The URL is then stored for later use
            _scenarioContext["myurl"] = baseUrl;
            driver.Manage().Window.Maximize();
            HeadingLinksPOM header = new HeadingLinksPOM(driver);
            //The pop up informing about the site being a test site is dismissed
            header.ClickDismiss();
        }
        [After("@CheckPrice")]
        public void TearDownPrice()
        {
            //The cart is emptied 
            ClearCart();
            //The site is logged out of
            LogOut();
            driver.Quit();
        }

        [After("@OrderItem")]
        public void TearDownOrder()
        {
            //The site is logged out of
            LogOut();
            driver.Quit();
        }
        //This function ensures the cart can be cleared no matter which page you started from ready for the next clean test
        public void ClearCart()
        {
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            Console.WriteLine("Clearing Cart");
            headingLinks.ClickCart();
            CartPagePOM cartPagePOM = new CartPagePOM(driver);
            cartPagePOM.RemoveItem();
        }
        //This function ensures the account can be logged out no matter which page you started from ready for the next clean test
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
