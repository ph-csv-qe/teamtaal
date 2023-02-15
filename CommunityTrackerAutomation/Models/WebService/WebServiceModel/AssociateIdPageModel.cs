using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebService.WebServiceModel
{
    public class AssociateIdPageModel
    {
        [JsonProperty("associateId")]
        public long AssociateId { get; set; }

        [JsonProperty("associateName")]
        public string AssociateName { get; set; }

        [JsonProperty("assignedProjects")]
        public string[] AssignedProjects { get; set; }
    }

}
