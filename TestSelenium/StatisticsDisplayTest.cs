using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium
{
    [TestClass]
    public class StatisticsDisplayTest
    {
        [TestMethod]
        public void DisplayPieChart()
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

                var loggedInElement = driver.FindElement(By.CssSelector("a[href='/Identity/Account/Manage']"));


                var loggedInText = loggedInElement.Text;


                Assert.AreEqual("Hello admin@admin.com!", loggedInText, "The logged in user email does not match.");

                var linkEditMovie = driver.FindElement(By.LinkText("Screening Statistics"));
                linkEditMovie.Click();

                IWebElement selectStartDate = driver.FindElement(By.CssSelector("[name = 'StartDate']"));
                selectStartDate.Click();
                selectStartDate.SendKeys("2023"+Keys.Left+"10"+ Keys.Left + Keys.Left + "12");



                
                

                IWebElement selectEndDate = driver.FindElement(By.CssSelector("[name='EndDate']"));
                selectEndDate.Click();
                selectEndDate.SendKeys("2023"+Keys.Left+"28"+Keys.Left+ Keys.Left+"12");
                var submitbutton = driver.FindElement(By.CssSelector("[class='pb-3'] [type='submit']"));
                submitbutton.Click();

                var element = driver.FindElement(By.CssSelector(".highcharts-background"));

                Assert.IsTrue(element.Displayed, "The element is not displayed on the page.");


            }
            finally
            {

                driver.Quit();

            }
        }
        [TestMethod]
        public void DisplayLineChart()
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

                var loggedInElement = driver.FindElement(By.CssSelector("a[href='/Identity/Account/Manage']"));


                var loggedInText = loggedInElement.Text;


                Assert.AreEqual("Hello admin@admin.com!", loggedInText, "The logged in user email does not match.");

                var linkEditMovie = driver.FindElement(By.LinkText("Trends"));
                linkEditMovie.Click();

                var selectMovieId = driver.FindElement(By.CssSelector("select#movieId"));
                selectMovieId.Click();

                SelectElement selectElement = new SelectElement(selectMovieId);
                selectElement.SelectByText("Forrest Gump");

                IWebElement selectStartDate = driver.FindElement(By.CssSelector("[name = 'StartDate']"));
                selectStartDate.Click();
                selectStartDate.SendKeys("2023" + Keys.Left + "07" + Keys.Left + Keys.Left + "12");






                IWebElement selectEndDate = driver.FindElement(By.CssSelector("[name='EndDate']"));
                selectEndDate.Click();
                selectEndDate.SendKeys("2023" + Keys.Left + "28" + Keys.Left + Keys.Left + "12");
                var submitbutton = driver.FindElement(By.CssSelector("[class='pb-3'] [type='submit']"));
                submitbutton.Click();

                var element = driver.FindElement(By.CssSelector(".highcharts-background"));

                Assert.IsTrue(element.Displayed, "The element is not displayed on the page.");


            }
            finally
            {

                driver.Quit();

            }
        }
    }
}
