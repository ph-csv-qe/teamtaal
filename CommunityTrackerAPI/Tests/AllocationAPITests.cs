using CommunityTrackerAPI.DataModels;
using CommunityTrackerAPI.Helpers;
using CommunityTrackerAPI.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebServiceModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace CommunityTrackerAPI.Tests
{
    [TestClass]
    public class AllocationAPITests
    {
        private static HttpClient httpClient;

        private static readonly string BaseURL = "http://localhost:5041/";

        private static readonly string Endpoint = "api";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<AssociateIdPageModel> cleanUpList = new List<AssociateIdPageModel>();

        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {

        }

        [TestMethod]
        public async Task GetAssociateIdTest()
        {
            #region Get Associate data using id 2019213
            try
            {
                // Create Json Object
                var associateTestData = GenerateAssociate.getAssociate();

                // Send Get Request
                var getResponse = await HelperClass.GetAssociateByID();
                
                // Get Content
                var httpResponseMessage = getResponse.Content;

                // Deserialize Content
                var listGetData = JsonConvert.DeserializeObject<AssociateIdPageModel>(httpResponseMessage.ReadAsStringAsync().Result);

                // Assertion
                Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode, "Status code is not equal to 200");
                Assert.IsTrue(listGetData.AssociateId == associateTestData.AssociateId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

        }
        [TestMethod]
        public async Task GetProjectAllProjectTest()
        {
            #region get all project data
            try
            {
                // Send Get Request
                var getResponse = await HelperClass.GetProjects();

                // Assertion
                Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode, "Status code is not equal to 200");
            }
            catch (Exception ex) {
                throw ex;
            }
            #endregion
        }
        [TestMethod]
        public async Task PostUploadFileTest()
        {
            const string filePath = @"C:\QEAutomation\Community Tracker Auto Team Taal\teamtaal\CommunityTrackerAPI\Tests\TestData\Magenic Allocation Dump.xlsx";

            #region create form data and send post request
            try
            {
                // Send Posr Request
                var postResponse = await HelperClass.UploadFile(filePath);

                // Assertion
                Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode, "Status code is not equal to 200");
            }
            catch (Exception ex) {
                throw ex;
            }
            #endregion
        }
    }
}