using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// Composite Add New Employee test class
    /// Author: Emmanuel Ramiro E. Gaspar II
    /// </summary>
    [TestClass]
    public class AddNewEmployeeTests : BaseSeleniumTest
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
            CreateEmployeePageModel createEmployeePage = new CreateEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(3);

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();
            createEmployeePage.CreateNewEmployee(true, false);

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
            CreateEmployeePageModel createEmployeePage = new CreateEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(3);

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();
            createEmployeePage.CreateNewEmployee(true, true);

        }
        /// <summary>
        /// Adding a new employee with existing employee number
        /// </summary>
        [TestMethod]
        public void Create_NewEmployee_On_QualityEngineering_With_Existing_Employee_Number()
        {
            // Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            MembersPage membersPage = new MembersPage(this.TestObject);
            CreateEmployeePageModel createEmployeePage = new CreateEmployeePageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            // Navigate to Quality Engineering All Members Page
            homepage.navigateToCommunity(3);

            // Adding a new employee while probationary is off
            membersPage.ClickGoToInputPageButton();
            createEmployeePage.CreateNewEmployee(false, false); 
        }
    }
}
