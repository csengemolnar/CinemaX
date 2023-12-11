using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MovieTicketReservation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using MovieTicketReservation.Models;

namespace TestSelenium
{
    [TestClass]
    public class MovieFunctionTest
    {
        


        [TestMethod]
        public void LoginAsAdmin()
        {

            
            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromMilliseconds(60000);
            try
            {
                driver.Navigate().GoToUrl("http://localhost:5153/");
                var loginNavButton = driver.FindElement(By.XPath("/html/body/header/nav[1]/div/div/ul[2]/li[2]/a"));
                loginNavButton.Click();

                var inputEmail = driver.FindElement(By.XPath("/html//input[@id='Input_Email']"));
                inputEmail.Click();
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var loggedInElement = driver.FindElement(By.CssSelector("a[href='/Identity/Account/Manage']"));

               
                var loggedInText = loggedInElement.Text;

               
                Assert.AreEqual("Hello admin@admin.com!", loggedInText, "The logged in user email does not match.");

            }
            finally
            {

                driver.Quit();
               
            }
        }

        [TestMethod]
        public void AdminEditMoviesPage()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("Edit Movies"));
                linkEditMovie.Click(); 

                var expectedUrl = "http://localhost:5153/Admin/EditMovies";
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");

               




            }
            finally
            {

                driver.Quit();

            }
        }

        [TestMethod]
        public void AdminEditMovieShowsPage()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("Edit Shows"));
                linkEditMovie.Click();

                var expectedUrl = "http://localhost:5153/Admin/EditMovieShows";
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");






            }
            finally
            {

                driver.Quit();

            }
        }


        [TestMethod]
        public void AdminEditMovie()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("Edit Movies"));
                linkEditMovie.Click();

                var expectedUrl = "http://localhost:5153/Admin/EditMovies";
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var editbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[class] div:nth-child(3) .btn-primary")));
                editbutton.Click();


                var inputNewLength = driver.FindElement(By.CssSelector("input#MovieLength"));
                inputNewLength.Click();
                inputNewLength.Clear();
                inputNewLength.SendKeys("180");


                
                var savebutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[value='Save']")));
                savebutton.Click();

                






            }
            finally
            {

                driver.Quit();

            }
        }




        [TestMethod]
        public void AdminEditMovieShows()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("Edit Shows"));
                linkEditMovie.Click();

                var expectedUrl = "http://localhost:5153/Admin/EditMovieShows"; 
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                var editbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[class] div:nth-child(3) .btn-primary")));
                editbutton.Click();


                var inputNewLength = driver.FindElement(By.CssSelector("input#MovieShows_HallId"));
                inputNewLength.Click();
                inputNewLength.Clear();
                inputNewLength.SendKeys("2");



                var savebutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[value='Save']")));
                savebutton.Click();








            }
            finally
            {

                driver.Quit();

            }
        }

        [TestMethod]
        public void AdminFilterReservation()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("My Reservations"));
                linkEditMovie.Click();

                var expectedUrl = "http://localhost:5153/Reservation/ListReservation";
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");

             


                var searchId = driver.FindElement(By.CssSelector("[class='col-xs-2']"));
                searchId.Click();
                searchId.SendKeys("20");

                var exceptedData = driver.FindElement(By.CssSelector("tr > td:nth-of-type(1)"));
                string id= exceptedData.Text;




                Assert.IsNotNull(id, "Filtered element was not found.");



            }
            finally
            {

                driver.Quit();

            }
        }

        [TestMethod]
        public void AdminEditReservation()
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
                inputEmail.SendKeys("admin@admin.com");

                var inputpassword = driver.FindElement(By.XPath("/html//input[@id='Input_Password']"));
                inputpassword.Click();
                inputpassword.SendKeys("Admin12345!");

                var loginButton = driver.FindElement(By.Id("login-submit"));
                loginButton.Click();

                var linkEditMovie = driver.FindElement(By.LinkText("Edit Reservations"));
                linkEditMovie.Click();

                var expectedUrl = "http://localhost:5153/Admin/EditReservations";
                Assert.AreEqual(expectedUrl, driver.Url, "The URLs do not match.");

                var dropdown = driver.FindElement(By.CssSelector("select#currentstatus"));

                SelectElement selectElement = new SelectElement(dropdown);
                selectElement.SelectByText("EXPIRED");
                var filterbutton = driver.FindElement(By.CssSelector("input[value='Filter']"));
                filterbutton.Click();


                var exceptedData = driver.FindElement(By.CssSelector(".bg-warning.mb-2.p-3.status-display.text-center.text-white"));
                string id = exceptedData.Text;



                
                Assert.IsNotNull(id, "Filtered element was not found.");



            }
            finally
            {

                driver.Quit();

            }
        }

    }


}