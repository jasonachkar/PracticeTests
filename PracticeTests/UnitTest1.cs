using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace PracticeTests
{
    [TestFixture]
    public class AuthTests
    {
        private IWebDriver _driver;
        private const string BaseUrl = "https://practice.expandtesting.com";

        // generate one test user to cover valid / duplicate registration + login
        private string _testUser;
        private const string TestPassword = "Abc123!";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // set up ChromeDriver  
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // make a unique username
            _testUser = "user" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }

        [SetUp]
        public void SetUp()
        {
            // clear cookies/session between tests
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        [Test, Order(1)]
        public void REG_01_Registration_Valid()
        {
            _driver.Navigate().GoToUrl($"{BaseUrl}/register");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys(TestPassword);
            _driver.FindElement(By.Id("confirmPassword")).SendKeys(TestPassword);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // should redirect to /login
            Assert.IsTrue(_driver.Url.Contains("/login"),
                "Expected to land on login page after successful registration");
        }

        [Test, Order(2)]
        public void REG_02_Registration_BlankFields()
        {
            _driver.Navigate().GoToUrl($"{BaseUrl}/register");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // still on /register
            Assert.IsTrue(_driver.Url.EndsWith("/register"),
                "Blank registration should not navigate away from /register");
        }

        [Test, Order(3)]
        public void REG_03_Registration_DuplicateUsername()
        {
            // first register
            _driver.Navigate().GoToUrl($"{BaseUrl}/register");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys(TestPassword);
            _driver.FindElement(By.Id("confirmPassword")).SendKeys(TestPassword);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Assert.IsTrue(_driver.Url.Contains("/login"));

            // try to register same user again
            _driver.Navigate().GoToUrl($"{BaseUrl}/register");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys(TestPassword);
            _driver.FindElement(By.Id("confirmPassword")).SendKeys(TestPassword);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // expect to remain on /register
            Assert.IsTrue(_driver.Url.EndsWith("/register"),
                "Duplicate registration should stay on /register");
        }

        [Test, Order(4)]
        public void LOG_01_Login_Valid()
        {
            _driver.Navigate().GoToUrl($"{BaseUrl}/login");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys(TestPassword);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // should land on secure
            Assert.IsTrue(_driver.Url.Contains("/secure"),
                "Valid login should redirect to /secure");

            // welcome message contains username
            StringAssert.Contains(_testUser, _driver.PageSource);
        }

        [Test, Order(5)]
        public void LOG_02_Login_WrongPassword()
        {
            _driver.Navigate().GoToUrl($"{BaseUrl}/login");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys("wrongpass");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // url stays on /login
            Assert.IsTrue(_driver.Url.EndsWith("/login"),
                "Wrong password should not navigate away from /login");

            // error contains "Invalid" keyword
            StringAssert.Contains("Invalid", _driver.PageSource);
        }

        [Test, Order(6)]
        public void DASH_01_Dashboard_AccessWithoutLogin()
        {
            // no login – go straight to secure
            _driver.Navigate().GoToUrl($"{BaseUrl}/secure");

            // should redirect to /login
            Assert.IsTrue(_driver.Url.EndsWith("/login"),
                "Accessing /secure without login should redirect to /login");
        }

        [Test, Order(7)]
        public void DASH_02_Dashboard_LogoutClearsSession()
        {
            // log in first
            _driver.Navigate().GoToUrl($"{BaseUrl}/login");
            _driver.FindElement(By.Id("username")).SendKeys(_testUser);
            _driver.FindElement(By.Id("password")).SendKeys(TestPassword);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Assert.IsTrue(_driver.Url.Contains("/secure"));

            // click Logout
            _driver.FindElement(By.LinkText("Logout")).Click();

            // then try to go back to secure
            _driver.Navigate().GoToUrl($"{BaseUrl}/secure");
            Assert.IsTrue(_driver.Url.EndsWith("/login"),
                "After logout, accessing /secure should redirect to /login");
        }
    }
}