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
        [Ignore]
        public void Validate_User_Can_Add_Edit_Skills()
        {
            string username = Config.GetGeneralValue("Email");
            string password = Config.GetGeneralValue("Password");

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
            homepage.EnterEmployeeID("933608");
            homepage.ClickSearchButton();
            SoftAssert.Assert(() => Assert.IsTrue(employeeList.IsPageLoaded(), "Employee list is not loaded"));

            //Clicks employee record and assert redirectino to employee records page
            employeeList.ClickEmployeeRecordByEmployeeId("933608");
            SoftAssert.Assert(() => Assert.IsTrue(employeeRecord.IsPageLoaded(), "Employee record page is not loaded"));

            employeeRecord.ClickSkillsInputField();
            employeeRecord.ClickRandomSkillsOption();
        }

        /// <summary>
        /// Remove employee Skill
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Validate_User_Can_Remove_Skills()
        {
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            page.LoginWithValidCredentials(username, password);
            HomePageModel homepage = page.ByPass2FactorAuthentication();
            Assert.IsTrue(homepage.IsPageLoaded());
        }
    }
}
