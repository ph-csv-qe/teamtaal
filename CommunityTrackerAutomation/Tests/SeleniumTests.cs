using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OtpNet;
using System.Security.Cryptography;
using System.Numerics;
using OpenQA.Selenium.DevTools.V85.Animation;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    /// <summary>
    /// Composite Selenium test class
    /// </summary>
    [TestClass]
    public class SeleniumTests : BaseSeleniumTest
    {
        /// <summary>
        /// Do database setup for test run
        /// </summary>
        // [ClassInitialize] - Disabled because this step will fail as the template does not include access to a test database
        public static void TestSetup(TestContext context)
        {
            // Do database setup
            using (DatabaseDriver wrapper = new DatabaseDriver(DatabaseConfig.GetProviderTypeString(), DatabaseConfig.GetConnectionString()))
            {
                var result = wrapper.Query("getStateAbbrevMatch", new { StateAbbreviation = "MN" }, commandType: CommandType.StoredProcedure);
                Assert.AreEqual(1, result.Count(), "Expected 1 state abbreviation to be returned.");
            }
        }

        /// <summary>
        /// Do post test run web service cleanup
        /// </summary>
        // [ClassCleanup] - Disabled because this step will fail against the current base service
        public static void TestCleanup()
        {
            //// Do web service post run cleanup
            //WebServiceDriver client = new WebServiceDriver(new Uri(WebServiceConfig.GetWebServiceUri()));
            //string result = client.Delete("/api/String/Delete/1", "text/plain", true);
            //Assert.AreEqual(string.Empty, result);
        }


        /// <summary>
        /// Enter credentials test
        /// </summary>
        [TestMethod]
        public void EnterCredentialsTest()
        {
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);
            HomePageModel homepage = page.ByPass2FactorAuthentication();
            Assert.IsTrue(homepage.IsPageLoaded());
        }


        [TestMethod]
        public void AutomatedLogin()
        {
            //Web App Setup 
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("http://localhost:3000/login");

            var bytesecret = Base32Encoding.ToBytes("wsm7j6cilxhrchm2dadpdxtje7xei7vo");
            var totp = new Totp(bytesecret);
            var generatedOtp = totp.ComputeTotp(DateTime.UtcNow);

            //Select Sign In Button
            Thread.Sleep(1000);
            driver.SwitchTo().Frame(0);
            IWebElement LoginButton = driver.FindElement(By.XPath("//body[@class='qJTHM']"));

            LoginButton.Click();

            //Automated Email Input
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            IWebElement InputEmail = driver.FindElement(By.Id("identifierId"));
            InputEmail.SendKeys("Jasper.Liwanag@softvision.com");

            IWebElement NextButton = driver.FindElement(By.XPath("//body/div[1]/div[1]/div[2]/div[1]/c-wiz[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/button[1]"));
            NextButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Automated Password Input

            IWebElement InputPass = driver.FindElement(By.Name("Passwd"));
            InputPass.SendKeys("District-Analysis2121");

            Thread.Sleep(1000);

            IWebElement NextButton2 = driver.FindElement(By.Id("passwordNext"));
            NextButton2.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            // Workaround Code when "Too Many Attempts" alert is present 
            //IWebElement TryAnotherWay;
            //IWebElement tooManyFailedAttempts = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/span/section"));
            //bool isDisplayed = tooManyFailedAttempts.Displayed;

            //if (isDisplayed)
            //{
            //    TryAnotherWay = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            //}
            //else 
            //{
            //    TryAnotherWay = driver.FindElement(By.XPath("//button[@jsaction='click:cOuCgd; mousedown:UX7yZ; mouseup:lbsD7e; mouseenter:tfO1Yc; mouseleave:JywGue; touchstart:p6p2H; touchmove:FwuNnf; touchend:yfqBxc; touchcancel:JMtRjd; focus:AHmuwe; blur:O22p3e; contextmenu:mg9Pef;mlnRJb:fLiPzd;']//span[text()='Try another way']"));
            //}

            //TryAnotherWay.Click();


            //Automated Select Try Another Way option
            IWebElement TryAnotherWay = driver.FindElement(By.XPath("//button[@jsaction='click:cOuCgd; mousedown:UX7yZ; mouseup:lbsD7e; mouseenter:tfO1Yc; mouseleave:JywGue; touchstart:p6p2H; touchmove:FwuNnf; touchend:yfqBxc; touchcancel:JMtRjd; focus:AHmuwe; blur:O22p3e; contextmenu:mg9Pef;mlnRJb:fLiPzd;']//span[text()='Try another way']"));
            TryAnotherWay.Click();

            //Automated Select Google Authenication
            IWebElement GoogleAuthOption = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            GoogleAuthOption.Click();

            Thread.Sleep(1000);

            //Automated Input TOTP Code
            IWebElement AuthInput = driver.FindElement(By.Id("totpPin"));
            AuthInput.SendKeys(generatedOtp);

            IWebElement AuthNext = driver.FindElement(By.Id("totpNext"));
            AuthNext.Click();

            //driver.Close();
        }

        [TestMethod]
        public void AddProject()
        {
            //Web App Setup 
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("http://localhost:3000/login");

            String winHandleBefore = driver.CurrentWindowHandle;

            var bytesecret = Base32Encoding.ToBytes("wsm7j6cilxhrchm2dadpdxtje7xei7vo");
            var totp = new Totp(bytesecret);
            var generatedOtp = totp.ComputeTotp(DateTime.UtcNow);
            Random randNum = new Random();

            DateTime dateTime = DateTime.Now;
            String ProjectName = dateTime.ToString().Replace(":", "-").Replace(" ", "_").Replace("/", "-");

            //Select Login Button
            Thread.Sleep(1000);
            driver.SwitchTo().Frame(0);
            IWebElement LoginButton = driver.FindElement(By.XPath("//body[@class='qJTHM']"));

            LoginButton.Click();

            //Automate Login
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            //Automated Email Input
            IWebElement InputEmail = driver.FindElement(By.Id("identifierId"));
            InputEmail.SendKeys("Jasper.Liwanag@softvision.com");

            IWebElement NextButton = driver.FindElement(By.XPath("//body/div[1]/div[1]/div[2]/div[1]/c-wiz[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/button[1]"));
            NextButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Automated Password Input
            IWebElement InputPass = driver.FindElement(By.Name("Passwd"));
            InputPass.SendKeys("District-Analysis2121");

            Thread.Sleep(1000);

            IWebElement NextButton2 = driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            NextButton2.Click();

            //IWebElement TryAnotherWay;
            //IWebElement tooManyFailedAttempts = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/span/section"));
            //bool isDisplayed = tooManyFailedAttempts.Displayed;

            //if (isDisplayed)
            //{
            //    TryAnotherWay = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            //}
            //else
            //{
            //    TryAnotherWay = driver.FindElement(By.XPath("//button[@jsaction='click:cOuCgd; mousedown:UX7yZ; mouseup:lbsD7e; mouseenter:tfO1Yc; mouseleave:JywGue; touchstart:p6p2H; touchmove:FwuNnf; touchend:yfqBxc; touchcancel:JMtRjd; focus:AHmuwe; blur:O22p3e; contextmenu:mg9Pef;mlnRJb:fLiPzd;']//span[text()='Try another way']"));
            //}

            //TryAnotherWay.Click();

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Automated Select Try Another Way option
            IWebElement TryAnotherWay = driver.FindElement(By.XPath("//button[@jsaction='click:cOuCgd; mousedown:UX7yZ; mouseup:lbsD7e; mouseenter:tfO1Yc; mouseleave:JywGue; touchstart:p6p2H; touchmove:FwuNnf; touchend:yfqBxc; touchcancel:JMtRjd; focus:AHmuwe; blur:O22p3e; contextmenu:mg9Pef;mlnRJb:fLiPzd;']//span[text()='Try another way']"));
            TryAnotherWay.Click();

            //Automated Select Google Authenication
            IWebElement GoogleAuthOption = driver.FindElement(By.XPath("//*[@id=\"view_container\"]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            GoogleAuthOption.Click();

            IWebElement AuthInput = driver.FindElement(By.Id("totpPin"));
            AuthInput.SendKeys(generatedOtp);

            IWebElement AuthNext = driver.FindElement(By.Id("totpNext"));
            AuthNext.Click();

            driver.SwitchTo().Window(winHandleBefore);

            //Landing Page to Add Project Form

            IWebElement HamburgerBtn = driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div[2]/button"));
            HamburgerBtn.Click();

            Thread.Sleep(3000);

            IWebElement MaintBtn = driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div[3]/div/div/div[2]/div[1]/ul/div[6]/div/div[2]/span"));
            MaintBtn.Click();

            IWebElement ProjTab = driver.FindElement(By.XPath("//button[@id=\"1\"]"));
            ProjTab.Click();

            IWebElement AddProj = driver.FindElement(By.XPath("//*[@id=\"1\"]/div/span/div[1]/button"));
            AddProj.Click();

            Thread.Sleep(3000);


            IWebElement AddProjName = driver.FindElement(By.Id("name"));
            AddProjName.SendKeys("TEST" + ProjectName);

            IWebElement AddProjCode = driver.FindElement(By.Id("code"));
            AddProjCode.SendKeys("TEST-Alpha");

            IWebElement AddProjAddBtn = driver.FindElement(By.XPath("/html/body/div[3]/div[3]/div/div[2]/button[2]"));
            AddProjAddBtn.Click();
        }

    }
}
