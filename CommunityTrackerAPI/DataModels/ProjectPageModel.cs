using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityTrackerAPI.DataModels
{
    public class ProjectPageModel
    {
        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

        [JsonProperty("projectDescription")]
        public string ProjectDescription { get; set; }

        [JsonProperty("projectBillability")]
        public string ProjectBillability { get; set; }

        [JsonProperty("projectType")]
        public string ProjectType { get; set; }

        [JsonProperty("projectManagerId")]
        public long ProjectManagerId { get; set; }

        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("projectOwningPractice")]
        public string ProjectOwningPractice { get; set; }

        [JsonProperty("associateProjectDetails")]
        public object AssociateProjectDetails { get; set; }

        [JsonProperty("projectManager")]
        public object ProjectManager { get; set; }

        [JsonProperty("account")]
        public object Account { get; set; }
    }
}
