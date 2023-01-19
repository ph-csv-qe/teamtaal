using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebPage.Selenium
{
    public class MembersPage : BaseSeleniumPageModel
    {
        
        /// <summary>
        /// QE Page
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "members/3";

        /// <summary>
        /// Initializes a new instance of the <see cref="MembersPage" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public MembersPage(ISeleniumTestObject testObject) : base(testObject)
        {
        }
        /// <summary>
        /// Search Textbox
        /// </summary>
        private LazyElement SearchTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class='css-1a4tdcb-MuiStack-root'] input"), "Search Textbox"); }
        }
        /// <summary>
        /// Search Button
        /// </summary>
        private LazyElement SearchButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiPaper-root MuiPaper-elevation'] button[class*='MuiButton-root MuiButton-contained']"), "Search Button"); }
        }
        /// <summary>
        /// Go to input page
        /// </summary>
        private LazyElement GoToInputPageButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[title='Go to Input Page']"), "Go to Input Page"); }
        }
        /// <summary>
        /// Include probationary fields in export
        /// </summary>
        private LazyElement IncludeProbationaryFieldsInExportCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("label[class*='MuiFormControlLabel-root'] input"), "Include probationary fields in export"); }
        }
        /// <summary>
        /// Export
        /// </summary>
        private LazyElement ExportButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[class*='MuiButton-root'] svg[data-testid='FileDownloadRoundedIcon']"), "Export"); }
        }
        /// <summary>
        /// Export
        /// </summary>
        private LazyElement EmployeeListGrid
        {
            get { return this.GetLazyElement(By.CssSelector("div#members-table-container"), "Employee List"); }
        }

        /// <summary>
        /// Click the go to input page button
        /// </summary>
        /// <param name=""></param>
        public void ClickGoToInputPageButton()
        {
            GoToInputPageButton.Click();
        }
        /// <summary>
        /// Generate Random Employee Number
        /// </summary>
        /// <param name=""></param>
        /// <returns>Returns a random employee number</returns>
        public int GenerateRandomEmployeeNumber()
        {
            Random random = new Random();
            int randomEmployeeNumber = random.Next(100000, 1000000);
            
            return randomEmployeeNumber;
        }
        /// <summary>
        /// Generate Random Name
        /// </summary>
        /// <param name=""></param>
        /// <returns>Returns a random name</returns>
        public string GenerateRandomName()
        {
            Random random = new Random();
            int randomNum = random.Next(0, 26);
            string randomName = "Test Employee" + randomNum;
            return randomName;
        }
        /// <summary>
        /// Creating a new employee
        /// </summary>
        /// <param name=""></param>
        public void CreateNewEmployee(bool probationary)
        {
            int employeeNumber = GenerateRandomEmployeeNumber();
            string employeeName = GenerateRandomName();
            this.ClickGoToInputPageButton();

        }
        public override bool IsPageLoaded()
        {
            throw new NotImplementedException();
        }
    }

}
