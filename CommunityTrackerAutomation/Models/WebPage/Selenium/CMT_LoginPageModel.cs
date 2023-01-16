using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OtpNet;
using System;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class CMT_LoginPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "/login";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageModel" /> 
        /// </summary>
        /// <param name="testObject">The test object</param>
        public CMT_LoginPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets user name box
        /// </summary>
        private LazyElement EmailInput
        {
            get { return this.GetLazyElement(By.Id("identifierId"), "User name input"); }
        }

        /// <summary>
        /// Gets password box
        /// </summary>
        private LazyElement PasswordInput
        {
            get { return this.GetLazyElement(By.CssSelector("input[type='password']"), "Password input"); }
        }

        /// <summary>
        /// Gets login button
        /// </summary>
        private LazyElement NextButton
        {
            get { return this.GetLazyElement(By.XPath("//span[text()='Next']"), "Next button"); }
        }

        /// <summary>
        /// Gets login button
        /// </summary>
        private LazyElement TryAnotherWay
        {
            get { return this.GetLazyElement(By.XPath("//span[text()='Try another way']"), "Try another way link"); }
        }


        /// <summary>
        /// Gets login button
        /// </summary>
        private LazyElement SignInButton
        {
            get { return this.GetLazyElement(By.CssSelector("div#container>div[role='button']"), "Login button"); }
        }

        /// <summary>
        /// Gets login button
        /// </summary>
        private LazyElement SecretInput
        {
            get { return this.GetLazyElement(By.Id("totpPin"), "Login button"); }
        }

        /// <summary>
        /// Open the login page
        /// </summary>
        public void OpenLoginPage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
            //  this.AssertPageLoaded();
        }

        /// <summary>
        /// Enter the use credentials
        /// </summary>
        /// <param name="email">The user name</param>
        /// <param name="password">The user password</param>
        public void EnterCredentials(string email, string password, int index)
        {
            SwitchToCurrentWindow(index);
            this.EmailInput.SendKeys(email);
            this.NextButton.Click();
            this.PasswordInput.SendKeys(password);
            this.NextButton.Click();
        }

        /// <summary>
        /// Click sign in button
        /// </summary>
        public void CLickSignInButton(int index)
        {
            SwitchToCurrentWindow(index);
            SignInButton.Click();
        }

        public bool SignInButtonDisplayed()
        {
            return SignInButton.Displayed;
        }

        /// <summary>
        /// Switch to current window
        /// </summary>
        public void SwitchToCurrentWindow(int index)
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[index]);
        }

        /// <summary>
        /// Bypass google 2FA
        /// </summary>
        public HomePageModel BypassGoogle2FA()
        {
            var byteSecret = Base32Encoding.ToBytes(Config.GetGeneralValue("Secret"));
            var otp = new Totp(byteSecret);
            var generatedOtp = otp.ComputeTotp(DateTime.UtcNow);

            SecretInput.SendKeys(generatedOtp);
            NextButton.Click();

            return new HomePageModel(this.TestObject);
        }

        /// <summary>
        /// Assert the login page loaded
        /// </summary>
        public void AssertPageLoaded()
        {
            Assert.IsTrue(
                this.IsPageLoaded(),
                "The web page '{0}' is not loaded",
                PageUrl);
        }

        /// <summary>
        /// Check if the home page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.SignInButton.Displayed;
        }
    }
}

