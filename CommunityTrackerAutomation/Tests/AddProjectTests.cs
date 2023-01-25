using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage.Selenium;
using System;
using System.Data;
using System.Linq;
using System.Threading;

namespace Tests
{
    /// <summary>
    /// Composite Add New Project Test Class
    /// Author: Jasper Liwanag
    /// </summary>
    [TestClass]
    public class AddProjectTests : BaseSeleniumTest
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
        /// Successful Add Project Test
        /// </summary>
        [TestMethod]
        public void AddProjectTest()
        {
            // Instances of the Pages Used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            AddProjectPageModel addprojectPage = new AddProjectPageModel(this.TestObject);
            HomePageModel homePage = new HomePageModel(this.TestObject);

            // Access Login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            HomePageModel homepage = loginPage.ByPass2FactorAuthentication();

            // Assert if page is successfully loaded
            Assert.IsTrue(homePage.IsPageLoaded());

            // Successfully Add a New Project
            addprojectPage.ReachAddProject();

        }
        }
    }
