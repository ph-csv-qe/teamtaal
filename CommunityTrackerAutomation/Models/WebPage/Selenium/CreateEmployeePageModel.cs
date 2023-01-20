﻿using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Drawing;
using CognizantSoftvision.Maqs.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class CreateEmployeePageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public CreateEmployeePageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets Name
        /// </summary>
        private LazyElement NameTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input[name='name']"), "Name"); }
        }
        /// <summary>
        /// Gets CSV Mail
        /// </summary>
        private LazyElement CSVMailTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input[id='email']"), "CSV Mail"); }
        }
        /// <summary>
        /// Gets Cognizant ID
        /// </summary>
        private LazyElement CognizantIDTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input#cognizantId"), "Cognizant ID"); }
        }
        /// <summary>
        /// Gets Work State
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private LazyElement WorkStateCombobox//(string index)
        {
            get { return this.GetLazyElement(By.CssSelector("div#mui-component-select-state"), "Work State"); }
            //return this.GetLazyElement(By.Id($"li[role='option'][data-value={index}]"), "Work State"); 
        }
        /// <summary>
        /// Gets Job Level
        /// </summary>
        private LazyElement JobLevelCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div#mui-component-select-jobLevel"), "Job Level"); }
        }
        /// <summary>
        /// Gets Hired Date
        /// </summary>
        private LazyElement HiredDateCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiOutlinedInput-root'] input[name='hiredDate']"), "Hired Date"); }
        }
        /// <summary>
        /// Gets Project
        /// </summary>
        private LazyElement ProjectCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] div#mui-component-select-project"), "Project"); }
        }
        /// <summary>
        /// Gets Skills
        /// </summary>
        private LazyElement SkillsCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiFormControl-root'] input[placeholder='Skills']"), "Skills"); }
        }
        /// <summary>
        /// Gets Probationary
        /// </summary>
        private LazyElement ProbationaryButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] span[class*='MuiSwitch-thumb']"), "Probationary"); }
        }
        /// <summary>
        /// Gets Bench Tags
        /// </summary>
        private LazyElement BenchTagsCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div.MuiContainer-root.MuiContainer-maxWidthXl.css-1spqbit-MuiContainer-root:nth-child(2) div.MuiBox-root.css-k008qs:nth-child(3) main.css-yfo96e div.MuiPaper-root.MuiPaper-elevation.MuiPaper-rounded.MuiPaper-elevation1.MuiCard-root.css-93edo7-MuiPaper-root-MuiCard-root div.MuiBox-root.css-0 form.MuiGrid-root.MuiGrid-container.MuiGrid-direction-xs-column.css-l6hioc-MuiGrid-root div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-sm-12.MuiGrid-grid-md-12.MuiGrid-grid-lg-12.css-1blmxdp-MuiGrid-root div.MuiGrid-root.MuiGrid-container.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-sm-12.MuiGrid-grid-md-12.MuiGrid-grid-lg-12.css-1gd44eh-MuiGrid-root div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-sm-12.MuiGrid-grid-md-12.MuiGrid-grid-lg-11.css-ro7t2c-MuiGrid-root:nth-child(11) div.MuiGrid-root.MuiGrid-container.css-11lq3yg-MuiGrid-root div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-6.css-1fyyp8j-MuiGrid-root:nth-child(1) label.MuiFormControlLabel-root.MuiFormControlLabel-labelPlacementEnd.css-1t4oj1i-MuiFormControlLabel-root > span.MuiCheckbox-root.MuiCheckbox-colorPrimary.MuiButtonBase-root.MuiCheckbox-root.MuiCheckbox-colorPrimary.PrivateSwitchBase-root.css-qv24h5-MuiButtonBase-root-MuiCheckbox-root"), "Bench Tags"); }
        }
        /// <summary>
        /// GoP Account
        /// </summary>
        private LazyElement GoPAccountCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'GoP Account']/preceding-sibling::span"), "GoP Account"); }
        }
        /// <summary>
        /// Expectation Setting Meeting
        /// </summary>
        private LazyElement ExpectationSettingMeetingCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Expectation Setting Meeting']/preceding-sibling::span"), "Expectation Setting Meeting"); }
        }
        /// <summary>
        /// Signed Expectation Setting Document
        /// </summary>
        private LazyElement SignedExpectationSettingDocumentCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Signed Expectation Setting Document']/preceding-sibling::span"), "Signed Expectation Setting Document"); }
        }
        /// <summary>
        /// Monthly Touchpoint Introduction Email
        /// </summary>
        private LazyElement MonthlyTouchpointIntroductionEmailCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Monthly Touchpoint Introduction Email']/preceding-sibling::span"), "Monthly Touchpoint Introduction Email"); }
        }
        /// <summary>
        /// Performance Evaluation recurring Meeting
        /// </summary>
        private LazyElement PerformanceEvaluationRecurringMeetingCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Performance Evaluation recurring meeting']/preceding-sibling::span"), "Performance Evaluation recurring meeting"); }
        }
        /// <summary>
        /// Included In Community Communications
        /// </summary>
        private LazyElement IncludedInCommunityCommunicationsCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Included in Community Communications']/preceding-sibling::span"), "Included In Community Communications"); }
        }
        /// <summary>
        /// GoP Instruction Email
        /// </summary>
        private LazyElement GoPInstructionEmailCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'GoP Instruction Email']/preceding-sibling::span"), "GoP Instruction Email"); }
        }
        /// <summary>
        /// Second Month Touchpoint Meeting
        /// </summary>
        private LazyElement SecondMonthTouchpointMeetingCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '2nd Month Touchpoint Meeting']/preceding-sibling::span"), "Second Month Touchpoint Meeting"); }
        }
        /// <summary>
        /// Third Month Performance Evaluation received from Project Lead
        /// </summary>
        private LazyElement ThirdMonthPerformanceEvaluationReceivedFromProjectLeadCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '3rd Month Performance Evaluation received from Project Lead']/preceding-sibling::span"), "Third Month Performance Evaluation received from Project Lead"); }
        }
        /// <summary>
        /// Third Month Touchpoint Performance Evaluation Request to Project Lead
        /// </summary>
        private LazyElement ThirdMonthTouchpointPerformanceEvaluationRequestToProjectLeadCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '3rd Month Touchpoint Performance Evaluation Request to Project Lead']/preceding-sibling::span"), "Third Month Touchpoint Performance Evaluation Request to Project Lead"); }
        }
        /// <summary>
        /// Third Month Touchpoint Meeting
        /// </summary>
        private LazyElement ThirdMonthTouchpointMeetingCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '3rd Month Touchpoint Meeting']/preceding-sibling::span"), "Third Month Touchpoint Meeting"); }
        }
        /// <summary>
        /// Fourth Month Touchpoint Mtg
        /// </summary>
        private LazyElement FourthMonthTouchpointMtgCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '4th Month Touchpoint Mtg']/preceding-sibling::span"), "Fourth Month Touchpoint Mtg"); }
        }
        /// <summary>
        /// Fifth Month Touchpoint Performance Evaluation Request to Project Lead
        /// </summary>
        private LazyElement FifthMonthTouchpointPerformanceEvaluationRequestToProjectLeadCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '5th Month Touchpoint Performance Evaluation Request to Project Lead']/preceding-sibling::span"), "Fifth Month Touchpoint Performance Evaluation Request to Project Lead"); }
        }
        /// <summary>
        /// Fifth Month Performance Evaluation received from Project Lead
        /// </summary>
        private LazyElement FifthMonthPerformanceEvaluationReceivedFromProjectLeadCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '5th Month Performance Evaluation received from Project Lead']/preceding-sibling::span"), "Fifth Month Performance Evaluation received from Project Lead"); }
        }
        /// <summary>
        /// Fifth Month Touchpoint Meeting
        /// </summary>
        private LazyElement FifthMonthTouchpointMeetingCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = '5th Month Touchpoint Meeting']/preceding-sibling::span"), "Fifth Month Touchpoint Meeting"); }
        }
        /// <summary>
        /// Save Button
        /// </summary>
        private LazyElement SaveButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root MuiGrid-container'] button[type='submit']:nth-child(1)"), "SaveButton"); }
        }
        /// <summary>
        /// Generate Random Employee Number
        /// </summary>
        /// <param name=""></param>
        /// <returns>Returns a random employee number</returns>
        public string GenerateRandomEmployeeNumber()
        {
            Random random = new Random();
            int randomEmployeeNumber = random.Next(100000, 1000000);

            return randomEmployeeNumber.ToString();
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
        /// Click Checkbox
        /// </summary>
        /// <param name="checkbox"></param>
        public void clickCheckbox(LazyElement checkbox)
        {
            checkbox.Click();
        }
        /// <summary>
        /// Click Combobox
        /// </summary>
        /// <param name="combobox"></param>
        public void clickCombobox(LazyElement combobox)
        {
            combobox.Click();
        }
        /// <summary>
        /// Click Combobox
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="index"></param>
        public void selectItemCombobox(LazyElement combobox, int index)
        {
            this.clickCombobox(combobox);
            this.GetLazyElement(By.CssSelector($"li[role='option'][data-value='{index.ToString()}']")).Click();

        }
        /// <summary>
        /// Select Random Probationary
        /// </summary>
        /// <param name=""></param>
        public void selectRandomProbationaryOption()
        {

        }
        /// <summary>
        /// Creating a new employee
        /// </summary>
        /// <param name="probationary"></param>
        public void CreateNewEmployee(bool probationary)
        {
            EmployeeRecordPageModel employeeRecordPageModel = new EmployeeRecordPageModel(this.TestObject);

            string employeeNumber = GenerateRandomEmployeeNumber();
            string employeeName = GenerateRandomName();
            string email = "Test03@gmail.com";
            int workState = 1;
            int jobLevel = 9;
            string hiredDate = "02/01/2023";
            string project = "MagenicPDPBench";

            // Entering input for employee name
            NameTextbox.SendKeys(employeeName);

            // Entering input for email
            CSVMailTextbox.SendKeys(email);

            // Entering input for employee number
            CognizantIDTextbox.SendKeys(employeeNumber);

            // Selecting an option based on index for Job level combobox
            this.selectItemCombobox(JobLevelCombobox, jobLevel);

            // Selecting an option based on index for Workstate combobox
            this.selectItemCombobox(WorkStateCombobox, workState);

            // Selecting an option based on index for Hired Date combobox
            HiredDateCombobox.SendKeys(hiredDate);
            ProjectCombobox.SendKeys(project);

            // Selecting a random series of skills
            employeeRecordPageModel.ClickSkillsInputField();
            employeeRecordPageModel.ClickRandomSkillsOption("MULTIPLE");

            // If probatinary is true will select a random series of checkbox for probationary employee
            if (probationary == true)
            {
                GoPAccountCheckbox.Click();
                FourthMonthTouchpointMtgCheckbox.Click();
                FifthMonthTouchpointMeetingCheckbox.Click();
            }
            else
            {
                employeeRecordPageModel.ClickSkillsInputField();
                //ProbationaryButton.Click();
            }
            SaveButton.Click();
        }
        public override bool IsPageLoaded()
        {
            throw new NotImplementedException();
        }
    }
}