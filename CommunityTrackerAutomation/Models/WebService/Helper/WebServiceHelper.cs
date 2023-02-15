using Models.WebService.Resources;
using Models.WebService.TestData;
using Models.WebService.WebServiceModel;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Send GET request to get an specific employee by id
        /// </summary>
        ///
        public static async Task<AssociateIdPageModel> GetEmployeeByAssociateId(HttpClient client)
        {
            // Serialize Content
            /*var request = JsonConvert.SerializeObject(userData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            //Send Post Request to add new user
            postRequest.AddJsonBody(newUserData);
            var postResponse = await client.ExecutePostAsync<AssociateIdPageModel>(postRequest);


            var createdUserData = newUserData;
            return createdUserData;*/
            /*
            // Serialize Content
            var associateId = GenerateAssociateId.GetAssociateId();
            var request = JsonConvert.SerializeObject(associateId);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(baseURL), postRequest);

            #endregion

            #region get data

            // Send Request
            var httpResponse = await httpClient.GetAsync(GetURI($"{UsersEndpoint}/{userData.Username}"));

            // Get Content
            var httpResponseMessage = httpResponse.Content;
            
            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            // Deserialize Content
            var listUserData = JsonConvert.DeserializeObject<AssociateIdPageModel>(httpResponseMessage.ReadAsStringAsync().Result);*/
        }
    }
}
