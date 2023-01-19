using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OtpNet;
using System;
using System.Diagnostics;
using System.Linq;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class LoginScreenModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "login";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginScreenModel" /> class.
        /// </summary>
        /// <param name="testObject">The test object</param>
        public LoginScreenModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }


        private LazyElement SignInButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[role='button']"), "Sign in with Google Button"); }
        }

        private LazyElement EmailTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input[type='email']"), "Email input"); }
        }

        private LazyElement EmailNextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#identifierNext > div > button"), "Next button"); }
        }

        private LazyElement PasswordTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input[type='password']"), "Password input"); }
        }

        private LazyElement PasswordNextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#passwordNext > div > button"), "Next button"); }
        }

        private LazyElement LoginButton
        {
            get { return this.GetLazyElement(By.CssSelector("#Login"), "Login button"); }
        }

        private LazyElement TryAnotherWayLink
        {
            get { return this.GetLazyElement(By.CssSelector("#view_container > div > div > div.pwWryf.bxPAYd > div > div.zQJV3 > div.dG5hZc > div.daaWTb > div"), "Try another way"); }
        }

        private LazyElement GoogleAuthOption
        {
            get { return this.GetLazyElement(By.CssSelector("#view_container > div > div > div.pwWryf.bxPAYd > div > div.WEQkZc > div > form > span > section > div > div > div.pQ0lne > ul > li:nth-child(3) > div > div.vxx8jf"), "Google Authenticator"); }
        }
        private LazyElement OtpTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("#totpPin"), "Otp textbox"); }
        }

        private LazyElement OtpNextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#totpNext"), "Otp Next button"); }
        }



        public void OpenLoginPage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
        }

        public void ClickSignInButton()
        {
            int iframeCount = WebDriver.FindElements(By.TagName("iframe")).Count;
            WebDriver.SwitchTo().Frame(0);

            SignInButton.Click();
        }

        public void EnterValidCredentials(string email, string password)
        {
            SwitchToCurrentWindow();

            EmailTextbox.SendKeys(email);
            EmailNextButton.Click();

            PasswordTextbox.SendKeys(password);
            PasswordNextButton.Click();
        }

        public HomePageModel ByPassAuthentication()
        {
            var bytesecret = Base32Encoding.ToBytes(Config.GetGeneralValue("Secret"));
            var totp = new Totp(bytesecret);
            var generateOtp = totp.ComputeTotp(DateTime.UtcNow);

            TryAnotherWayLink.Click();
            GoogleAuthOption.Click();

            OtpTextbox.SendKeys(generateOtp);
            OtpNextButton.Click();

            

            return new HomePageModel(this.TestObject);
        }


        public void AssertPageLoaded()
        {
            Assert.IsTrue(
                this.IsPageLoaded(),
                "The web page '{0}' is not loaded",
                PageUrl);
        }

        public override bool IsPageLoaded()
        {
            return this.EmailTextbox.Displayed && this.PasswordTextbox.Displayed && this.LoginButton.Displayed;
        }

        public void SwitchToCurrentWindow()
        {
            var newWindow = this.TestObject.WebDriver.WindowHandles.Last();
            this.TestObject.WebDriver.SwitchTo().Window(newWindow);
        }

    }
}

