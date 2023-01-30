using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System.Data;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Composite Search Employee test class
    /// Author: Shiena
    /// </summary>
    [TestClass]
    public class UpdateEmployeeProjectTest : BaseSeleniumTest
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
        /// Do post test run web service cleanup
        /// </summary>
        // [ClassCleanup] - Disabled because this step will fail against the current base service
        public static void TestCleanup()
        {
            //// Do web service post run cleanup
            //WebServiceDriver client = new WebServiceDriver(new Uri(WebServiceConfig.GetWebServiceUri()));
            //string result = client.Delete("/api/String/Delete/1", "text/plain", true);
            //Assert.AreEqual(string.Empty, result);
        }


        /// <summary>
        /// Search Community Member test
        /// </summary>
        [TestMethod]
        public void Update_project_of_multiple_employees()
        {
            string PageUrl = SeleniumConfig.GetWebSiteBase();

            //Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            string[] employees = { "Aaron Macapagal", "An Konim Valle", "John Rafael Ang"};
            string[] employeeIDs = { "2107746", "933909" , "933549" };

            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);
            EmployeeListPageModel employeeList = new EmployeeListPageModel(this.TestObject);
            EmployeeRecordPageModel employeeRecord = new EmployeeRecordPageModel(this.TestObject);

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();

            // Assert if Page is successfully loaded
            Assert.IsTrue(homepage.IsPageLoaded());

            for (int i = 0; i < 3; i++)
            {
                //Search Employee and Open Details Page
                homepage.EnterEmployeeName(employees[i]);
                Assert.AreEqual(employees[i], homepage.SearchResultNameWindow());
                employeeList.ClickEmployeeRecordByEmployeeId(employeeIDs[i]);
                SoftAssert.Assert(() => Assert.IsTrue(employeeRecord.IsPageLoaded(), "Employee record page is not loaded"));

                //Checks Current Project, then Updates Project
                string currentProject = employeeRecord.GetCurrentProject();
                string updatedProject = employeeRecord.SelectDifferentProject(currentProject);
                Assert.AreNotEqual(currentProject, updatedProject);

                //Go back to homepage
                this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
            }

        }
    }
}


