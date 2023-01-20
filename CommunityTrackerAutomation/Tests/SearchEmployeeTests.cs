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
    public class SearchEmployeeTests : BaseSeleniumTest
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
        public void SearchCommunityMemberTest()
        {
            //Instance of pages used
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");
            string employeeName = "Aaron Macandili";
            LoginPageModel loginPage = new LoginPageModel(this.TestObject);
            HomePageModel homepage = new HomePageModel(this.TestObject);

            // Access login and enter credentials
            loginPage.OpenLoginPage();
            loginPage.LoginWithValidCredentials(username, password);
            loginPage.ByPass2FactorAuthentication();


            // Assert if Page is successfully loaded
            //Assert.IsTrue(homepage.IsPageLoaded());

            homepage.EnterEmployeeName(employeeName);
            Assert.AreEqual(employeeName, homepage.SearchResultWindow());
        }
    }
}


