using CommunityTrackerAPI.Resources;
using CommunityTrackerAPI.Tests.TestData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommunityTrackerAPI.Helpers
{
    public class HelperClass
    {
        public static async Task<HttpResponseMessage> GetAssociateByID()
        {
            var httpClient = new HttpClient();

            // Serialize Content
            var requestBody = JsonConvert.SerializeObject(GenerateAssociate.getAssociate());
            var postRequest = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Send Get Request
            var httpResponse = await httpClient.GetAsync(Endpoints.GetURL($"{Endpoints.Endpoint}/Associate/{GenerateAssociate.getAssociate().AssociateId}"));

            return httpResponse;
        }
        public static async Task<HttpResponseMessage> GetProjects()
        {
            var httpClient = new HttpClient();

            // Send Request
            var httpResponse = await httpClient.GetAsync(Endpoints.GetURI($"{Endpoints.Endpoint}/Project"));

            // Get Content
            var httpResponseMessage = httpResponse.Content;

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            return httpResponse;
        }
        public static async Task<HttpResponseMessage> UploadFile(string filePath)
        {
            var httpClient = new HttpClient();
            using (var content = new MultipartFormDataContent())
            {
                var stream = new StreamContent(File.OpenRead(filePath));
                content.Add(stream, "file", "Magenic Allocation Dump.xslx");
                var httpResponse = httpClient.PostAsync(Endpoints.GetURL($"{Endpoints.Endpoint}/UploadFile"), content).Result;

                return httpResponse;
            }
        }
    }
}

