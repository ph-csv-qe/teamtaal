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
        /// Gets Go to Input Page
        /// </summary>
        private LazyElement GoToInputPageButton
        {
            get { return this.GetLazyElement(By.CssSelector("button[title='Go to Input Page']"), "Go to Input Page"); }
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
        private LazyElement JobLevelCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div#mui-component-select-jobLevel"), "Job Level"); }
        }
        private LazyElement HiredDateCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiOutlinedInput-root'] input[name='hiredDate']"), "Hired Date"); }
        }
        private LazyElement ProjectCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] div#mui-component-select-project"), "Project"); }
        }
        private LazyElement SkillsCombobox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiFormControl-root'] input[placeholder='Skills']"), "Skills"); }
        }
        private LazyElement ProbationaryButton
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }
        // 
        private LazyElement BenchTagsCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }
        private LazyElement ExpectationSettingMeetingCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }
        private LazyElement MonthlyTouchpointIntroductionEmailCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }
        private LazyElement IncludedInCommunityCommunicationsCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }
        private LazyElement SecondMonthTouchpointMeetingCheckbox
        {
            get { return this.GetLazyElement(By.CssSelector("div[class*='MuiGrid-root'] input[class*='MuiSwitch-input']"), "Probationary"); }
        }

        public override bool IsPageLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
