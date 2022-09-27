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
        protected string baseUrl;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Before]
        public void Setup()
        {
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");
            string browser = Environment.GetEnvironmentVariable("BROWSER");
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
            string baseUrl = Environment.GetEnvironmentVariable("BASEURL");
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
