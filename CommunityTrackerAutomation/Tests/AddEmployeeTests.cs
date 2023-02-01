using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using ExcelMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abstract;
using Models.Sheet;
using Models.WebPage.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.Abstract.Enum;

namespace Tests
{
    /// <summary>
    /// Composite Add New Employee test class
    /// Author: Emmanuel Ramiro E. Gaspar II
    /// </summary>
    [TestClass]
    public class AddEmployeeTests : BaseSeleniumTest
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
        /// Adding New Employee While Probationary is Off
        /// </summary>
        [TestMethod]
        public void Create_NewEmployee_On_QualityEngineering_While_Probationary_Is_Off()
        {
            // Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            MembersPage membersPage = new MembersPage(this.TestObject);
            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            AddEmployeePageModel createEmployeePage = new AddEmployeePageModel(this.TestObject);
            EditEmployeePageModel editEmployeePageModel = new EditEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Variables used in the script
            int selectedCommunity = (int)CommunityCards.QualityEngineering;
            string employeeNumber = createEmployeePage.GenerateRandomEmployeeNumber();
            List<string> employeeDetailsList = new List<string>()
            {
                employeeNumber, // employee id
                "Employee Probationary Off", // employee name
                "probationary.off@softvision.com", // email
                "1", // Work state in index form
                "9", // Position in index form
                "22/08/2022", // Hired Date
                "1" // Project in index form
            };

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(selectedCommunity);
            WebDriver.Wait().ForPageLoad();

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();
            WebDriver.Wait().ForPageLoad();
            createEmployeePage.CreateNewEmployee(employeeDetailsList, true);
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsNotificationMessageVisible("Member has been created.")), "Member has been created.");

            // Clean up process using Inactive Button
            homepage.EnterEmployeeID(employeeNumber);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(employeeNumber);
            editEmployeePageModel.ToggleActiveStatus(false);
            editEmployeePageModel.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsNotificationMessageVisible("Member has been updated.")), "Member has been updated.");
        }
        /// <summary>
        /// Adding New Employee While Probationary is On
        /// </summary>
        [TestMethod]
        public void Create_NewEmployee_On_QualityEngineering_While_Probationary_Is_On()
        {
            // Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            MembersPage membersPage = new MembersPage(this.TestObject);
            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            AddEmployeePageModel createEmployeePage = new AddEmployeePageModel(this.TestObject);
            EditEmployeePageModel editEmployeePageModel = new EditEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Variables used in the script
            int selectedCommunity = (int)CommunityCards.QualityEngineering;
            string employeeNumber = createEmployeePage.GenerateRandomEmployeeNumber();
            List<string> employeeDetailsList = new List<string>()
            {
                employeeNumber, // employee id
                "Employee Probationary On", // employee name
                "probationary.on@softvision.com", // email
                "1", // Work state in index form
                "9", // Position in index form
                "22/08/2022", // Hired Date
                "1" // Project in index form
            };

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(selectedCommunity);
            WebDriver.Wait().ForPageLoad();

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();
            WebDriver.Wait().ForPageLoad();
            createEmployeePage.CreateNewEmployee(employeeDetailsList, true);
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsNotificationMessageVisible("Member has been created.")), "Member has been created.");

            // Clean up process using Inactive Button
            homepage.EnterEmployeeID(employeeNumber);
            homepage.ClickSearchButton();
            employeeList.ClickEmployeeRecordByEmployeeId(employeeNumber);
            editEmployeePageModel.ToggleActiveStatus(false);
            editEmployeePageModel.ClickSaveButton();
            SoftAssert.Assert(() => Assert.IsTrue(homepage.IsNotificationMessageVisible("Member has been updated.")), "Member has been updated.");
        }
        /// <summary>
        /// Adding a new employee with existing employee number
        /// </summary>
        [TestMethod]
        public void Create_NewEmployee_On_QualityEngineering_With_Existing_Employee_Number()
        {
            List<string> employeeDetailsList = new List<string>()
            {
                "2219891", // employee id
                "Emmanuel Ramiro E. Gaspar II", // employee name
                "emmanuel.gaspar@softvision.com", // email
                "1", // Work state in index form
                "9", // Position in index form
                "22/08/2022", // Hired Date
                "1" // Project in index form
            };

            // Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            MembersPage membersPage = new MembersPage(this.TestObject);
            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            AddEmployeePageModel createEmployeePage = new AddEmployeePageModel(this.TestObject);
            EditEmployeePageModel editEmployeePageModel = new EditEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Verifying if employee is existing in the excel file
            var employeeExcelList = DataReader.ReadExcelFile();
            if (employeeExcelList[2].AssociateID == Convert.ToInt32(employeeDetailsList[0]))
            {
                Assert.AreEqual(employeeExcelList[2].AssociateID, Convert.ToInt32(employeeDetailsList[0]), $"Employee ID {employeeDetailsList[0]} is existing in the Allocation Mock Data");
                Assert.AreEqual(employeeExcelList[2].Name, employeeDetailsList[1], $"Employee Name {employeeDetailsList[1]} is existing in the Allocation Mock Data");
                Assert.AreEqual(employeeExcelList[2].Project, "MagenicPDPBench");
                Assert.AreEqual(employeeExcelList[2].HireDate, $"{employeeDetailsList[5]} 12:00:00 am");
            }
            else
            {
                // Variables used in the script
                int selectedCommunity = (int)CommunityCards.QualityEngineering;
                string employeeNumber = createEmployeePage.GenerateRandomEmployeeNumber();

                // Access login and enter credentials
                loginPage.OpenLoginPage();
                loginPage.LoginWithValidCredentials(username, password);
                loginPage.ByPass2FactorAuthentication();

                // Assert if Page is successfully loaded
                Assert.IsTrue(homepage.IsPageLoaded());

                // Navigate to Quality Engineering All Members Page
                homepage.navigateToCommunity(selectedCommunity);
                WebDriver.Wait().ForPageLoad();

                // Adding a new employee while probationary is off
                membersPage.ClickGoToInputPageButton();
                WebDriver.Wait().ForPageLoad();
                createEmployeePage.CreateNewEmployee(employeeDetailsList, false);
                SoftAssert.Assert(() => Assert.IsTrue(homepage.IsNotificationMessageVisible("Member has been created.")), "Member has been created.");
            }
        }
    }
}
