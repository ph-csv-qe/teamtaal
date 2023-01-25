using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;
using OtpNet;
using CognizantSoftvision.Maqs.Utilities.Helper;
using System;
using System.Threading;

namespace Models.WebPage.Selenium
{
    public class AddProjectPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageModel" /> class.
        /// </summary>
        /// <param name="testObject">The test object</param>
        public AddProjectPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets landing page hamburger button
        /// </summary>
        private LazyElement LandingPageHamburgerButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[type='button'] svg[data-testid='ArrowForwardIosIcon']"), "Menu Button"); }
        }
        private LazyElement MaintenanceButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[role='button'] svg[data-testid='ConstructionIcon']"), "Maintenance Button"); }
        }
        private LazyElement ProjectTab
        {
            get { return this.GetLazyElement(By.CssSelector("button[id='1']"), "Project Tab"); }
        }
        private LazyElement AddProjectButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[type='button'] svg[data-testid='AddCircleOutlineIcon']"), "Add Project Button"); }
        }
        private LazyElement AddProjectName
        {
            get { return this.GetLazyElement(By.Id("name"), "Add Project Form - Project Name"); }
        }
        private LazyElement AddProjectCode
        {
            get { return this.GetLazyElement(By.Id("code"), "Add Project Form - Project Code"); }
        }
        private LazyElement AddProjectDetailsButton
        {
            get { return this.GetLazyElement(By.CssSelector("div.MuiDialogActions-root>button:nth-child(2)"), "Add Project Form - Add Button"); }
        }

        public void SwitchToMainWindow()
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
        }
        public void ReachAddProject()
        {
            SwitchToMainWindow();
            LandingPageHamburgerButton.Click();
            MaintenanceButton.Click();
            ProjectTab.Click();
            AddProjectButton.Click();
            DateTime dateTime = DateTime.Now;
            String ProjectName = dateTime.ToString().Replace(":", "-").Replace(" ", "_").Replace("/", "-");
            AddProjectName.SendKeys("Test" + ProjectName);
            AddProjectCode.SendKeys("Test-Beta");
            AddProjectDetailsButton.Click();
        }
        public override bool IsPageLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
