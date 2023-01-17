using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using OpenQA.Selenium;

namespace Models.WebPage.Selenium
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class EmployeeListPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageModel" /> class.
        /// </summary>
        /// <param name="testObject">The selenium test object</param>
        public EmployeeListPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets Employee list table container
        /// </summary>
        private LazyElement EmployessListContainer
        {
            get { return this.GetLazyElement(By.Id("members-table-container"), "Employee list table container"); }
        }

        /// <summary>
        /// Gets Employee table row by eployee ID
        /// </summary>
        private LazyElement EmployeeListTableRowById(string employeeId)
        {
            return this.GetLazyElement(By.XPath($"//div[@id='members-table-container']//td[text()='{employeeId}']"), "Search Employee Input");
        }

        /// <summary>
        /// Click employee record on the table by employeeId
        /// </summary>
        /// <param name="employeeId"></param>
        public void ClickEmployeeRecordByEmployeeId(string employeeId)
        {
            this.EmployeeListTableRowById(employeeId).Click();
        }

        /// <summary>
        /// Check if the employee list table has been shown
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.EmployessListContainer.Exists;
        }
    }
}

