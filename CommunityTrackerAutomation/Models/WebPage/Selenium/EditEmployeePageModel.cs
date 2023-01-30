using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class EditEmployeePageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditEmployeePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public EditEmployeePageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        ///<summary>
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
        private LazyElement CognizantIdTextbox
        {
            get { return this.GetLazyElement(By.CssSelector("input[id='cognizantId']"), "Cognizant ID"); }
        }
        /// <summary>
        /// Gets Work State
        /// </summary>
        private LazyElement WorkStateCombobox
        {
            get { return this.GetLazyElement(By.Id("mui-component-select-state"), "WorkState Combobox"); }
        }
        /// <summary>
        /// Gets Work State Data Value
        /// </summary> 
        private LazyElement WorkStateListDataValue(string workStateDataValue)
        {
            return this.GetLazyElement(By.CssSelector($"li[data-value='{workStateDataValue}']"), "WorkState List");
        }
        /// <summary>
        /// Gets Job Level
        /// </summary> 
        private LazyElement JobLevelCombobox
        {
            get { return this.GetLazyElement(By.Id("mui-component-select-jobLevel"), "Job Level Combobox"); }
        }
        /// <summary>
        /// Gets Job Level Data Value
        /// </summary> 
        private LazyElement JobLevelListDataValue(string jobLevelDataValue)
        {
            return this.GetLazyElement(By.CssSelector($"li[data-value = '{jobLevelDataValue}']"), "JobLevel List");
        }
        /// <summary>
        /// Gets Hired Date
        /// </summary>
        private LazyElement HiredDateCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("input[name='hiredDate']"), "Hired Date"); }
        }
        /// <summary>
        /// Gets Project
        /// </summary>
        private LazyElement ProjectCombobox
        {
            get { return this.GetLazyElement(By.Id("mui-component-select-project"), "Project"); }
        }
        /// <summary>
        /// Gets Skills
        /// </summary>
        private LazyElement SkillsCombobox
        {
            get { return this.GetLazyElement(By.Id("auto-complete-chip"), "Skills"); }
        }
        /// <summary>
        /// Gets Probationary status
        /// </summary>
        private LazyElement ProbationaryToggleSwitch
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Probationary']/preceding-sibling::span"), "Probationary"); }
        }
        /// <summary>
        /// Gets Active status 
        /// </summary>
        private LazyElement ActiveToggleSwitch
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Active']/preceding-sibling::span//input[@type='checkbox']/parent::span"), "Active"); }
        }
        /// <summary>
        /// Gets Save button
        /// </summary>
        private LazyElement SaveButton
        {
            get { return this.GetLazyElement(By.CssSelector("svg[data-testid='SaveIcon']"), "Save"); }
        }
        /// <summary>
        /// Gets Bench Tags
        /// </summary>
        private LazyElement BenchTagsCheckbox
        {
            get { return this.GetLazyElement(By.XPath("//*[text() = 'Bench Tags']/preceding-sibling::span"), "Bench Tags"); }
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
        /// Function to update employee name
        /// </summary>
        public void EditName(string empNewName)
        {
            this.NameTextbox.Clear();
            NameTextbox.SendKeys(empNewName);
        }
        /// <summary>
        /// Function to randomise and update Work State
        /// </summary>
        public void SelectWorkState()
        {
            Random randomise = new Random();
            int workStateDataValue = randomise.Next(1, 7);
            string selectedWorkState = workStateDataValue.ToString();

            this.WorkStateCombobox.Click();
            this.WorkStateListDataValue(selectedWorkState).Click();
        }
        /// <summary>
        /// Function to randomise and update Job Level
        /// </summary>
        public void SelectJobLevel()
        {
            Random randomise = new Random();
            int jobLevelDataValue = randomise.Next(2, 11);
            string selectedJobLevel = jobLevelDataValue.ToString();

            this.JobLevelCombobox.Click();
            this.JobLevelListDataValue(selectedJobLevel).Click();
        }
        /// <summary>
        /// Function to randomise Probationary status switch
        /// </summary>
        public void ToggleProbationaryStatus()
        {
            Random randomiseValue = new Random();
            bool selectedValue = randomiseValue.Next(2) == 1;

            if (selectedValue == false)
            {
                this.ProbationaryToggleSwitch.Click();
            }
            
        }
        /// <summary>
        /// Function for clicking the Save button
        /// </summary>
        public void ClickSaveButton()
        {
            this.SaveButton.Click();
        }
        /// <summary>
        /// Gets total count of skills after editing
        /// </summary>
        public int GetTotalCountOfSkillsAfterEdit()
        {
            WebDriver.Navigate().Refresh();
            //Thread.Sleep is temporarily used due to element cannot be found without it, will do code refactoring for this page action
            System.Threading.Thread.Sleep(3000);
            int numberOfElements = WebDriver.FindElements(By.CssSelector("svg[data-testid='CancelIcon']")).Count;
            return numberOfElements;
        }
        /// <summary>
        /// Function to deactivate created employee as clean up
        /// </summary>
        public void ToggleActiveStatus(bool option)
        {
            if (option == false)
            {
                this.ActiveToggleSwitch.Click();
            }
        }
        /// <summary>
        /// Function to identify that Edit employee page has been loaded
        /// </summary>
        public override bool IsPageLoaded()
        {
            return this.ActiveToggleSwitch.Exists;
        }
    }
}
