using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models.WebPage.Selenium;
using System.Data;
using System.Linq;
using System.Threading;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;

namespace Tests.UITests
{
    /// <summary>
    /// Edit Employee Record
    /// Author: Cindy Jubilee Daquil
    /// </summary>
    [TestClass]
    public class EditEmployeeTests : BaseSeleniumTest
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
        /// Add/Edit employee Skill for non probationary employee
        /// </summary>
        [TestMethod]
        public void VerifyAdminRoleIsAbleToEditNonProbationaryEmployee()
        {
            LoginPageModel login = new LoginPageModel(TestObject);
            MembersPage membersPage = new MembersPage(TestObject);
            AddEmployeePageModel newEmployeeRecord = new AddEmployeePageModel(TestObject);
            HomePageModel homepage = new HomePageModel(TestObject);

            EmployeeListPageModel employeeList = new EmployeeListPageModel(TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(TestObject);
            EditEmployeePageModel editEmployeeRecord = new EditEmployeePageModel(TestObject);

            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");

            // Access login and enter credentials
            login.OpenLoginPage();
            login.LoginWithValidCredentials(username, password);
            login.ByPass2FactorAuthentication();

            // Bypass 2FA and assert homepage is loaded          
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsPageLoaded(), "Homepage is not loaded"));

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(3);

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();

            string empName = newEmployeeRecord.GenerateRandomName();
            string empNameToLower = empName.ToLower();
            string empNameToEmailFormat = empNameToLower.Replace(" ", ".");
            string empCognizantId = newEmployeeRecord.GenerateRandomEmployeeNumber();
            string empEmail = empNameToEmailFormat + "@softvision.com";
            string empWorkState = "1";
            string empJobLevel = "9";
            string empHiredDate = "13/05/2020";
            string empProjectAssign = "1";

            List<string> newEmployeeDetails = new List<string>()
            {
                empCognizantId,
                empName,
                empEmail,
                empWorkState,
                empJobLevel,
                empHiredDate,
                empProjectAssign
            };

            newEmployeeRecord.CreateNewEmployee(newEmployeeDetails, false);

            //Search employee by employee ID
            homepage.EnterEmployeeID(empCognizantId);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empCognizantId);


            //Edit employee name 
            string empNewName = " Bernadotte";
            editEmployeeRecord.EditName(empNewName);

            //Edit Work State field
            editEmployeeRecord.SelectWorkState();

            //Edit Job Level
            editEmployeeRecord.SelectJobLevel();

            //Update Skills field
            employeeRecord.ClickSkillsInputField();
            employeeRecord.ClickRandomSkillsOption("SINGLE");

            //Toggle Probationary and Active Switches
            editEmployeeRecord.ToggleProbationaryStatus();

            //Save the changes made
            editEmployeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Successfully updated"));

            //Launches employee record, post edit
            homepage.EnterEmployeeID(empCognizantId);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empCognizantId);

            //Validate employee details
            SoftAssert.Assert(() => Assert.AreEqual(4, employeeRecord.GetTotalCountOfSkills(), "Employee skill record is not equal"));

            //data clean up and save
            editEmployeeRecord.ToggleActiveStatus(false);
            editEmployeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Successfully updated"));
        }

        /// <summary>
        /// Add/Edit employee Skill for Probationary employee
        /// </summary>
        [TestMethod]
        public void VerifyAdminRoleIsAbleToEditProbationaryEmployee()
        {
            LoginPageModel login = new LoginPageModel(TestObject);
            MembersPage membersPage = new MembersPage(TestObject);
            AddEmployeePageModel newEmployeeRecord = new AddEmployeePageModel(TestObject);
            HomePageModel homepage = new HomePageModel(TestObject);

            EmployeeListPageModel employeeList = new EmployeeListPageModel(TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(TestObject);
            EditEmployeePageModel editEmployeeRecord = new EditEmployeePageModel(TestObject);

            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");

            // Access login and enter credentials
            login.OpenLoginPage();
            login.LoginWithValidCredentials(username, password);
            login.ByPass2FactorAuthentication();

            // Bypass 2FA and assert homepage is loaded          
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsPageLoaded(), "Homepage is not loaded"));

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(3);

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();

            string empName = newEmployeeRecord.GenerateRandomName();
            string empNameToLower = empName.ToLower();
            string empNameToEmailFormat = empNameToLower.Replace(" ", ".");
            string empCognizantId = newEmployeeRecord.GenerateRandomEmployeeNumber();
            string empEmail = empNameToEmailFormat + "@softvision.com";
            string empWorkState = "1";
            string empJobLevel = "9";
            string empHiredDate = "13/05/2021";
            string empProjectAssign = "1";

            List<string> newEmployeeDetails = new List<string>()
            {
                empCognizantId,
                empName,
                empEmail,
                empWorkState,
                empJobLevel,
                empHiredDate,
                empProjectAssign
            };

            newEmployeeRecord.CreateNewEmployee(newEmployeeDetails, true);

            //Search employee by employee ID
            homepage.EnterEmployeeID(empCognizantId);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empCognizantId);


            //Edit employee name 
            string empNewName = " Bernadotte";
            editEmployeeRecord.EditName(empNewName);

            //Edit Work State field
            editEmployeeRecord.SelectWorkState();

            //Edit Job Level
            editEmployeeRecord.SelectJobLevel();

            //Update Skills field
            employeeRecord.ClickSkillsInputField();
            employeeRecord.ClickRandomSkillsOption("SINGLE");

            //Toggle Probationary and Active Switches
            editEmployeeRecord.ToggleProbationaryStatus();

            //Save the changes made
            editEmployeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Successfully updated"));

            //Launches employee record, post edit
            homepage.EnterEmployeeID(empCognizantId);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(empCognizantId);

            //Validate employee details
            SoftAssert.Assert(() => Assert.AreEqual(4, employeeRecord.GetTotalCountOfSkills(), "Employee skill record is not equal"));

            //data clean up and save
            editEmployeeRecord.ToggleActiveStatus(false);
            editEmployeeRecord.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsUpdateSuccessNotificationVisible(), "Successfully updated"));
        }
    }
}
