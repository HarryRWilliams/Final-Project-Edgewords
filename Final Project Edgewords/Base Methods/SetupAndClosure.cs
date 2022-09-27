using Final_Project_Edgewords.POMPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.Base_Methods
{
    internal class SetupAndClosure  //This class is used to setup and close tests
    {
        protected IWebDriver driver; 
        protected string baseUrl = "https://www.edgewordstraining.co.uk/demo-site"; //This fetches the url that the test must start on
        [SetUp]
        public void Setup()
        {//These lines of code store information from the secrets file and enters them into the relevant variable
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");
            string browser = Environment.GetEnvironmentVariable("BROWSER");
                                
            switch (browser) //This uses the variable entered above and opens the correct browser
            {

                case "firefox":
                    Console.WriteLine("Using Firefox");
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    Console.WriteLine("Using Chrome");
                    SetupWithChrome();
                    break;
                case "edge":
                    Console.WriteLine("Using Edge");
                    driver = new EdgeDriver();
                    break;
                default: //if no browser is specified it will use chrome
                    Console.WriteLine("No browser or unknown browser entered");
                    Console.WriteLine("Using Chrome");
                    SetupWithChrome();
                    break;
            }
            driver.Url = baseUrl; //the browser will go to the base url
            driver.Manage().Window.Maximize();
            HeadingLinksPOM headingLinks = new HeadingLinksPOM(driver);
            headingLinks.ClickDismiss(); //This is in the setup function to allow tests to be run in any order
            headingLinks.ClickMyAccount(); //This goes to the login page needed for both tests
            LoginPagePOM login = new LoginPagePOM(driver); 
            bool SuccessfulLogin = login.LoginWithValidCredentials(username, password);
            Assert.IsTrue(SuccessfulLogin, "We did not login");
        }

        [TearDown]
        public void TearDown() //close down the open driver
        {
           Thread.Sleep(1000);
            driver.Quit();
        }
        public void SetupWithChrome() //As both the chrome option and default option use chrome this method keeps the code concise
        {
            driver = new ChromeDriver();
            ChromeOptions Options = new ChromeOptions();
            Options.AcceptInsecureCertificates = true; //this makes sure the site can be run even if it isn't secure
        }
    }
}
