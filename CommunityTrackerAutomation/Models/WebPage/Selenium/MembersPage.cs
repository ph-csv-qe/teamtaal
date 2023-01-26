using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
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
            get { return this.GetLazyElement(By.CssSelector("input[placeholder='Search']"), "Search Textbox"); }
        }
        /// <summary>
        /// Search Button
        /// </summary>
        private LazyElement SearchButton
        {
            get { return this.GetLazyElement(By.XPath("//main[@class='css-yfo96e']//button[contains(text(),'Search')]"), "Search Button"); }
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
            get { return this.GetLazyElement(By.CssSelector("svg[data-testid='CheckBoxOutlineBlankIcon']"), "Include probationary fields in export"); }
        }
        /// <summary>
        /// Export
        /// </summary>
        private LazyElement ExportButton
        {
            get { return this.GetLazyElement(By.CssSelector("button svg[data-testid='FileDownloadRoundedIcon']"), "Export"); }
        }
        /// <summary>
        /// Export
        /// </summary>
        private LazyElement EmployeeListGrid
        {
            get { return this.GetLazyElement(By.Id("members-table-container"), "Employee List"); }
        }

        /// <summary>
        /// Click the go to input page button
        /// </summary>
        /// <param name=""></param>
        public void ClickGoToInputPageButton()
        {
            GoToInputPageButton.Click();
        }
        public override bool IsPageLoaded()
        {
            throw new NotImplementedException();
        }
    }

}
