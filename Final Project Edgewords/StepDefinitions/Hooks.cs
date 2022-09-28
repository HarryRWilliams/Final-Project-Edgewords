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
             string username = Environment.GetEnvironmentVariable("USERNAME");
            //string username = Environment.GetEnvironmentVariable("harry.williams @nfocus.co.uk");
             string password = Environment.GetEnvironmentVariable("PASSWORD");
          //  string password = Environment.GetEnvironmentVariable("Passforex1");
             string browser = Environment.GetEnvironmentVariable("BROWSER");
           // string browser = Environment.GetEnvironmentVariable("chrome");
            ChromeOptions Options = new ChromeOptions();
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
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
            // string baseUrl = Environment.GetEnvironmentVariable("BASEURL");
            string baseUrl = "https://www.edgewordstraining.co.uk/demo-site";
            driver.Url = baseUrl;
            _scenarioContext["myurl"] = baseUrl;
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("body > p > a")).Click();
        }

        [After]
        public void TearDown()
        {
            Thread.Sleep(1000);
            driver.Quit();
            //Add clear cart to this
        }
    }
}
