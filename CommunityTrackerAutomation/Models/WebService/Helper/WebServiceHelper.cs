using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebService.Resources;
using Models.WebService.TestData;
using Models.WebService.WebServiceModel;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebService.Helper
{
    /// <summary>
    /// Demo class containing all methods for community tracker api
    /// </summary>
    public class WebServiceHelper
    {
        private static HttpClient httpClient;

        /// <summary>
        /// Send GET request to get an specific employee by id
        /// </summary>
        ///
        public static async Task<AssociateIdPageModel> GetEmployeeByAssociateId(HttpClient client)
        {
            // Serialize Content
            var employeeData = GenerateAssociateId.GetAssociateId();
            var request = JsonConvert.SerializeObject(employeeData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            //await httpClient.PostAsync(Endpoints.GetURL(Endpoints.baseURL), postRequest);

            //#endregion

            #region get data

            // Send Request
            var httpResponse = await httpClient.GetAsync(Endpoints.GetURI($"{Endpoints.baseURL}/{employeeData.AssociateId}"));

            // Get Content
            var httpResponseMessage = httpResponse.Content;

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            // Deserialize Content
            var listEmployeeData = JsonConvert.DeserializeObject<AssociateIdPageModel>(httpResponseMessage.ReadAsStringAsync().Result);

            #region assertion

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200");
            Assert.IsTrue(listEmployeeData.AssociateId == employeeData.AssociateId);

            return listEmployeeData;
            #endregion
        }

        /// <summary>
        /// Send GET request to get project details
        /// </summary>
        ///
        public static async Task<ProjectPageModel> GetProject(HttpClient client)
        {
            // Serialize Content
            var projectData = GenerateProjectDetails.GetProjectDetails();
            var request = JsonConvert.SerializeObject(projectData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            //await httpClient.PostAsync(Endpoints.GetURL(Endpoints.baseURL), postRequest);

            #endregion

            #region get data

            // Send Request
            var httpResponse = await httpClient.GetAsync(Endpoints.GetURI($"{Endpoints.baseURL}"));

            // Get Content
            var httpResponseMessage = httpResponse.Content;

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            // Deserialize Content
            var listProjectData = JsonConvert.DeserializeObject<ProjectPageModel>(httpResponseMessage.ReadAsStringAsync().Result);

            #endregion

            #region cleanupdata

            // Add data to cleanup list
            //cleanUpList.Add(listUserData);

            #endregion

            #region assertion

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200");
            Assert.IsTrue(listProjectData.ProjectId == projectData.ProjectId);

            return listProjectData;
            #endregion
        }
    }
}
