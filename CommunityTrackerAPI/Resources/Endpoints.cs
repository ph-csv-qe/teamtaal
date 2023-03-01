using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityTrackerAPI.Resources
{
    public class Endpoints
    {
        public static readonly string BaseURL = "http://localhost:5041/";

        public static readonly string Endpoint = "api";
        public static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";
        public static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));
    }
}
