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
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);
            HomePageModel homepage = page.ByPass2FactorAuthentication();


            AddProjectPageModel page2 = new AddProjectPageModel(this.TestObject);
            page2.ReachAddProject(password);

        }
        }
    }
