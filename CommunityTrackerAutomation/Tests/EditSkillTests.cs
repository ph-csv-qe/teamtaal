using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System.Data;
using System.Linq;
using System.Threading;

namespace Tests
{
    /// <summary>
    /// Composite Selenium test class
    /// </summary>
    [TestClass]
    public class EditSkillTests : BaseSeleniumTest
    {
        /// <summary>
        /// Do database setup for test run
        /// </summary>
        // [ClassInitialize] - Disabled because this step will fail as the template does not include access to a test database
        public static void TestSetup(TestContext context)
        {
            // Do database setup
            using (DatabaseDriver wrapper = new DatabaseDriver(DatabaseConfig.GetProviderTypeString(), DatabaseConfig.GetConnectionString()))
            {
                var result = wrapper.Query("getStateAbbrevMatch", new { StateAbbreviation = "MN" }, commandType: CommandType.StoredProcedure);
                Assert.AreEqual(1, result.Count(), "Expected 1 state abbreviation to be returned.");
            }
        }

        /// <summary>
        /// Add/Edit employee Skill
        /// </summary>
        [TestMethod]
        public void Validate_User_Can_Add_Edit_Skills()
        {
            string username = Config.GetGeneralValue("Email");
            string password = Config.GetGeneralValue("Password");
            string empID = "933608";

            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(this.TestObject);

            //Access login page and enter credentials
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);

            //Bypass 2FA and assert homepage is loaded
            HomePageModel homepage = page.ByPass2FactorAuthentication();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsPageLoaded(), "Homepage is not loaded"));

            //Search employee and assert employee list container is visible
            homepage.EnterEmployeeID(empID);
            homepage.ClickSearchButton();
            SoftAssert.Assert(() => Assert.IsTrue(employeeList.IsPageLoaded(), "Employee list is not loaded"));

            //Clicks employee record and assert redirectino to employee records page
            employeeList.ClickEmployeeRecordByEmployeeId(empID);
            SoftAssert.Assert(() => Assert.IsTrue(employeeRecord.IsPageLoaded(), "Employee record page is not loaded"));

            //Selecting multiple skills and save changes
            employeeRecord.ClickSkillsInputField();
            employeeRecord.ClickRandomSkillsOption("MULTIPLE");
            employeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Update success notification not shown"));

            //Validates skills input is added
            homepage.EnterEmployeeID(empID);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empID);
            SoftAssert.Assert(() => Assert.AreEqual(3, employeeRecord.GetTotalCountOfSkills(), "Employee skill record is not equal"));
        }

        /// <summary>
        /// Remove employee Skill
        /// </summary>
        [TestMethod]
        public void Validate_User_Can_Remove_Skills()
        {
            string username = Config.GetGeneralValue("Email");
            string password = Config.GetGeneralValue("Password");
            string empID = "933608";

            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(this.TestObject);

            //Access login page and enter credentials
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);

            //Bypass 2FA and assert homepage is loaded
            HomePageModel homepage = page.ByPass2FactorAuthentication();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsPageLoaded(), "Homepage is not loaded"));

            //Search employee and assert employee list container is visible
            homepage.EnterEmployeeID(empID);
            homepage.ClickSearchButton();

            //Clicks employee record and assert redirectino to employee records page
            employeeList.ClickEmployeeRecordByEmployeeId(empID);

            //Remove a single skill chip and save
            employeeRecord.RemoveSkillsByOption("SINGLE");
            employeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Update success notification not shown"));

            //Validates a single skill chip is removed
            homepage.EnterEmployeeID(empID);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empID);
            SoftAssert.Assert(() => Assert.AreEqual(2, employeeRecord.GetTotalCountOfSkills(), "Employee skill record is not equal"));

            //Remove all remaining skill chips and save
            employeeRecord.RemoveSkillsByOption("REMOVEALL");
            employeeRecord.ClickSaveButton();

            //Validates a single skill chip is removed
            homepage.EnterEmployeeID(empID);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empID);
            SoftAssert.Assert(() => Assert.AreEqual(0, employeeRecord.GetTotalCountOfSkills(), "Employee skill record is not equal"));
        }
    }
}
