using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestSelenium
{
    [TestClass]
    public class UserReservationTest
    {
        [TestMethod]
        public void UserLogin()
        {


            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(60000);
            try
            {
                driver.Navigate().GoToUrl("http://localhost:5153/");
                var loginNavButton = driver.FindElement(By.XPath("/html/body/header/nav[1]/div/div/ul[2]/li[2]/a"));
                loginNavButton.Click();

                var inputEmail = driver.FindElement(By.XPath("/html//input[@id='Input_Email']"));
                inputEmail.Click();
                inputEmail.SendKeys("user@user.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("User12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

            }
            finally { driver.Quit(); }
        }


        [TestMethod]
        public void UserReserveSeats()
        {


            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(60000);
            driver.Manage().Window.Maximize();
            try
            {
                

                driver.Navigate().GoToUrl("http://localhost:5153/");
                var loginNavButton = driver.FindElement(By.XPath("/html/body/header/nav[1]/div/div/ul[2]/li[2]/a"));
                loginNavButton.Click();

                var inputEmail = driver.FindElement(By.XPath("/html//input[@id='Input_Email']"));
                inputEmail.Click();
                inputEmail.SendKeys("user@user.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("User12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var screeningbutton = driver.FindElement(By.CssSelector("div:nth-of-type(2) > .col-md-8 .btn.btn-warning"));
                screeningbutton.Click();
                var reservebutton = driver.FindElement(By.CssSelector(".btn.btn-warning"));
                reservebutton.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[@class='container']/main[@role='main']/div[@class='container']//form[@action='/Reservation/ReserveSeats']/div[@class='form-group']/div[2]/div[1]/label[@id='seatcheck']")));

                var selectseat1 = driver.FindElement(By.XPath("/html/body/div[@class='container']/main[@role='main']/div[@class='container']//form[@action='/Reservation/ReserveSeats']/div[@class='form-group']/div[2]/div[1]/label[@id='seatcheck']"));
                selectseat1.Click();

                wait.Until(ExpectedConditions.ElementExists(By.LinkText("Reserve Seats")));
                var submit = driver.FindElement(By.LinkText("Reserve Seats"));
                submit.Click();
                submit.Click();




            }
            finally { driver.Quit(); }
        }
    }
}
