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
        private IWebDriver _driver;
        private ScenarioContext _scenarioContext;
        public Hooks(ScenarioContext _passedScenarioContext)
        {
            _scenarioContext = _passedScenarioContext;

        }
        [Before]
        public void Setup()
        {
            //The Browser is found from the secret file or console
            string _browser = Environment.GetEnvironmentVariable("BROWSER");
            //The options are created here so the same name can be used across cases
            ChromeOptions _options = new ChromeOptions();
            switch (_browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    _scenarioContext["myBrowser"] = _browser;
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    _options.AcceptInsecureCertificates = true;
                    _scenarioContext["myBrowser"] = _browser;
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    _scenarioContext["myBrowser"] = _browser;
                    break;
                default:
                    Console.WriteLine("No browser or unknown browser");
                    Console.WriteLine("Using Chrome");
                    _driver = new ChromeDriver();
                    _options.AcceptInsecureCertificates = true;
                    _scenarioContext["myBrowser"] = _browser;
                    break;
            }
            //This is stored in scenario context to be used later
            _scenarioContext["mydriver"] = _driver;
            //The URL is retrieved from the console or secret file
             string _baseUrl = Environment.GetEnvironmentVariable("BASEURL");
            //The driver is then taken to this url
            _driver.Url = _baseUrl;
            //The URL is then stored for later use
            _scenarioContext["myurl"] = _baseUrl;
            _driver.Manage().Window.Maximize();
            HeadingLinksPOM _header = new HeadingLinksPOM(_driver);
            //The pop up informing about the site being a test site is dismissed
            _header.ClickDismiss();
        }
        [After("@CheckPrice")]
        public void TearDownPrice()
        {
            //The cart is emptied 
            ClearCart();
            //The site is logged out of
            LogOut();
            _driver.Quit();
        }

        [After("@OrderItem")]
        public void TearDownOrder()
        {
            //The site is logged out of
            LogOut();
            _driver.Quit();
        }
        //This function ensures the cart can be cleared no matter which page you started from ready for the next clean test
        public void ClearCart()
        {
            HeadingLinksPOM _headingLinks = new HeadingLinksPOM(_driver);
            Console.WriteLine("Clearing Cart");
            _headingLinks.ClickCart();
            CartPagePOM _cartPagePOM = new CartPagePOM(_driver);
            _cartPagePOM.RemoveItem();
        }
        //This function ensures the account can be logged out no matter which page you started from ready for the next clean test
        public void LogOut()
        {
            HeadingLinksPOM _headingLinks = new HeadingLinksPOM(_driver);
            Console.WriteLine("Logging out");
            _headingLinks.ClickMyAccount();
            MyAcountPOM _myAcountPOM = new MyAcountPOM(_driver);
            _myAcountPOM.ClickLogout();
        }
    }
}
