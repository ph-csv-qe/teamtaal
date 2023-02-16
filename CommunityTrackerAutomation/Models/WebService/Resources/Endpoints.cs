using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Models.WebService.Resources
{
    /// <summary>
    /// Class containing all endpoints used in API tests
    /// </summary>
    public class Endpoints
    {
        //Base URL
        public const string baseURL = "http://localhost:5041/";
        public static string GetURL(string enpoint) => $"{baseURL}{enpoint}";
        public static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));
        public static string GetEmployeeByAssociateId(string id) => $"{baseURL}/api/{id}";

        public static string GetProject() => $"{baseURL}/api/Project";

        public static string PostUploadFile() => $"{baseURL}/api/FileUpload";
    }
}
