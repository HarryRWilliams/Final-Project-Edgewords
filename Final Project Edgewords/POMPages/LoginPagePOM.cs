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
        IWebDriver _driver;

        public LoginPagePOM(IWebDriver _driver)
        {
            this._driver = _driver;
        }

        //Locators
        IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        IWebElement _loginButton => _driver.FindElement(By.CssSelector("button[name='login']"));


        //Service Methods
        public LoginPagePOM SetUsername(string _username) //Enter the username
        {
            _usernameField.SendKeys(_username);
            return this;
        }
        public LoginPagePOM SetPassword(string _password)//Enter the password
        {
            _passwordField.SendKeys(_password);
            return this;
        }

        public void ClickLogin() //Click login
        {
            _loginButton.Click();
        }

        //Helpers

        public Boolean LoginWithValidCredentials(string _username, string _password) //this tries to enter the username and password and checks if the login was successful
        {
            SetUsername(_username);
            SetPassword(_password);
            ClickLogin();

            try
            {
                //driver.SwitchTo().Alert(); //if a login failure alert is created then login was not successful
                IWebElement _failurePopUp = _driver.FindElement(By.ClassName("woocommerce-error"));
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
