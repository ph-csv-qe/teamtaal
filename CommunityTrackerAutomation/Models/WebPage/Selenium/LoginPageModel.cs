using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;
using OtpNet;
using CognizantSoftvision.Maqs.Utilities.Helper;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class LoginPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "login";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageModel" /> class.
        /// </summary>
        /// <param name="testObject">The test object</param>
        public LoginPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets user name box
        /// </summary>
        private LazyElement UserNameInput
        {
            get { return this.GetLazyElement(By.CssSelector("input[type='email']"), "User name input"); }
        }

        /// <summary>
        /// Gets password box
        /// </summary>
        private LazyElement PasswordInput
        {
            get { return this.GetLazyElement(By.CssSelector("input[type='password']"), "Password input"); }
        }

        /// <summary>
        /// Gets user name box
        /// </summary>
        private LazyElement GoogleSignInButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[role='button']"), "Sign in with Google"); }
        }

        /// <summary>
        /// Gets next button
        /// </summary>
        private LazyElement NextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#identifierNext > div > button"), "Next button"); }
        }

        /// <summary>
        /// Gets password next button
        /// </summary>
        private LazyElement PasswordNextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#passwordNext > div > button"), "Password Next button"); }
        }

        /// <summary>
        /// Gets password next button
        /// </summary>
        private LazyElement TotpPinInput
        {
            get { return this.GetLazyElement(By.CssSelector("#totpPin"), "Totp Pin Input"); }
        }

        /// <summary>
        /// Gets password next button
        /// </summary>
        private LazyElement TotpNextButton
        {
            get { return this.GetLazyElement(By.CssSelector("#totpNext > div > button"), "Totp Pin Input"); }
        }

        /// <summary>
        /// Gets try another way link
        /// </summary>
        private LazyElement TryAnotherWayLink
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Try another way']"), "Try Another Way Link"); }
        }

        /// <summary>
        /// Gets Get a verification code from Google Authenticator app button
        /// </summary>
        private LazyElement GetAVerificationCodeFromGoogleAuthenticatorAppButton
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Get a verification code from the ']"), "Get a verification code from the Google Authenticator button"); }
        }

        /// <summary>
        /// Open the login page
        /// </summary>
        public void OpenLoginPage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
        }

        public void SwitchWindow()
        {
            var newWindow = this.TestObject.WebDriver.WindowHandles.Last();
            this.TestObject.WebDriver.SwitchTo().Window(newWindow);
        }

        /// <summary>
        /// Enter the use credentials
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <param name="password">The user password</param>
        public void EnterCredentials(string userName, string password)
        {
            SwitchWindow();
            UserNameInput.SendKeys(userName);
            NextButton.Click();
            PasswordInput.SendKeys(password);
            PasswordNextButton.Click();
            WebDriver.Wait().ForPageLoad();
        }
        public void CheckIfPhonePromptAppears()
        {
            if(this.TryAnotherWayLink.Displayed)
            {
                TryAnotherWayLink.Click();
                WebDriver.Wait().ForPageLoad();
                GetAVerificationCodeFromGoogleAuthenticatorAppButton.Click();
                WebDriver.Wait().ForPageLoad();
            }
        }

        public void LoginWithValidCredentials(string userName, string password)
        {
            int iframeCount = WebDriver.FindElements(By.TagName("iframe")).Count;
            this.TestObject.WebDriver.SwitchTo().Frame(0);
            this.GoogleSignInButton.Click();
            this.EnterCredentials(userName, password);
        }


        public HomePageModel ByPass2FactorAuthentication()
        {
            var bytesecret = Base32Encoding.ToBytes(Config.GetGeneralValue("Secret"));
            var totp = new Totp(bytesecret);
            var generatedOtp = totp.ComputeTotp(DateTime.UtcNow);

            // Checking if phone prompt appears
            this.CheckIfPhonePromptAppears();

            TotpPinInput.SendKeys(generatedOtp);
            TotpNextButton.Click();

            return new HomePageModel(this.TestObject);
        }

        /// <summary>
        /// Check if the home page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.UserNameInput.Displayed && this.PasswordInput.Displayed && this.GoogleSignInButton.Displayed;
        }
    }
}

