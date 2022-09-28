using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Edgewords.POMPages
{
    internal class LoginPagePOM
    {
        IWebDriver driver;

        public LoginPagePOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        IWebElement usernameField => driver.FindElement(By.Id("username"));
        IWebElement passwordField => driver.FindElement(By.Id("password"));
        IWebElement loginButton => driver.FindElement(By.CssSelector("button[name='login']"));


        //Service Methods
        public LoginPagePOM SetUsername(string username) //Enter the username
        {
            usernameField.SendKeys(username);
            return this;
        }
        public LoginPagePOM SetPassword(string password)//Enter the password
        {
            passwordField.SendKeys(password);
            return this;
        }

        public void ClickLogin() //Click login
        {
            loginButton.Click();
        }

        //Helpers

        public Boolean LoginWithValidCredentials(string username, string password) //this tries to enter the username and password and checks if the login was successful
        {
            SetUsername(username);
            SetPassword(password);
            ClickLogin();

            try
            {
                //driver.SwitchTo().Alert(); //if a login failure alert is created then login was not successful
                IWebElement failurePopUp = driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch (Exception)
            {
                //No alert so catch error
                //We must have logged in 
                Console.WriteLine("Returning true - did log in");
                return true;
            }
            Console.WriteLine("Returning false - didn't log in");
            return false; //If there was an alert we didn't login
        }
    }
}
