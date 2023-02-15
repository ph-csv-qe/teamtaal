using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System.Data;
using System.Linq;
using System.Threading;

namespace Tests.UITests
{
    /// <summary>
    /// Composite Add New Employee test class
    /// Author: John Ace Donato
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
        /// Currently ignored since this test method is for Edit Skill via employee reocrds page
        [Ignore]
        public void Validate_User_Can_Add_Edit_Skills()
        {
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            string empID = Config.GetGeneralValue("SampleEmployeeID");

            EmployeeListPageModel employeeList = new EmployeeListPageModel(TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(TestObject);

            //Access login page and enter credentials
            LoginPageModel page = new LoginPageModel(TestObject);
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
        /// Currently ignored since this test method is for Edit Skill via employee reocrds page
        [Ignore]
        public void Validate_User_Can_Remove_Skills()
        {
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            string empID = Config.GetGeneralValue("SampleEmployeeID");

            EmployeeListPageModel employeeList = new EmployeeListPageModel(TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(TestObject);

            //Access login page and enter credentials
            LoginPageModel page = new LoginPageModel(TestObject);
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

        /// <summary>
        /// Test Case Name: US-129 - AUTOMATION - Verify that admin can edit a skill via maintenance page
        /// </summary>
        [TestMethod]
        public void Validate_Successful_Editing_Of_Skill()
        {
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");

            EmployeeListPageModel employeeList = new EmployeeListPageModel(TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(TestObject);
            MaintenanceSkillsPageModel maintenancePage = new MaintenanceSkillsPageModel(TestObject);

            //Access login page and enter credentials
            LoginPageModel page = new LoginPageModel(TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);

            //Bypass 2FA and assert homepage is loaded
            HomePageModel homepage = page.ByPass2FactorAuthentication();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsPageLoaded(), "Homepage is not loaded"));

            //Navigate to Maintenance page and validate page is loaded
            maintenancePage.NavigateToMaintenancePage();
            SoftAssert.Assert(() => Assert.IsTrue(maintenancePage.IsPageLoaded(), "Maintenance page is not loaded"));

            //Search keyword and click edit icon
            maintenancePage.EnterDesiredSkill(".NET");
            maintenancePage.ClickSearchButton();
            maintenancePage.ClickEditSkillIconRandomly();

            //Gets initial value of skill description value
            string skillValue = maintenancePage.GetSkillDescriptionValue();
            //Enter value on description placeholder then click update button
            maintenancePage.EnterSkillDescriptionValue(" EDITED");
            maintenancePage.ClickStatusToggleSwitch();
            maintenancePage.ClickUpdateSkillButton();
            //Assert success notification
            SoftAssert.Assert(() => Assert.IsTrue(maintenancePage.IsUpdateSuccessNotificationVisible(), "Success notification is not visible."));

            //Refresh website, then search the edited skill and assert values
            WebDriver.Navigate().Refresh();
            maintenancePage.EnterDesiredSkill($"{skillValue} EDITED");
            maintenancePage.ClickSearchButton();
            SoftAssert.Assert(() => Assert.AreEqual($"{skillValue} EDITED", maintenancePage.GetSkillValueByRow($"{skillValue} EDITED"), "Skill description not matched."));
            SoftAssert.Assert(() => Assert.AreEqual("Inactive", maintenancePage.GetSkillValueByRow($"Inactive"), "Skill status not matched"));
            //Revert changes of an edited skill
            maintenancePage.RevertSkillChanges(skillValue);
        }
    }
}
