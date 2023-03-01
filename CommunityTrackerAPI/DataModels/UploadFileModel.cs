using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityTrackerAPI.DataModels
{
    public class UploadFileModel
    {
        [JsonProperty("associateId")]
        public long AssociateId { get; set; }

        [JsonProperty("associateName")]
        public string AssociateName { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("doj")]
        public DateTimeOffset Doj { get; set; }

        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }

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

        [JsonProperty("projectManagerName")]
        public string ProjectManagerName { get; set; }

        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("homeManagerId")]
        public long HomeManagerId { get; set; }

        [JsonProperty("projectOwningPractice")]
        public string ProjectOwningPractice { get; set; }

        [JsonProperty("billabilityStatus")]
        public string BillabilityStatus { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("assignmentStartDate")]
        public DateTimeOffset AssignmentStartDate { get; set; }

        [JsonProperty("assignmentEndDate")]
        public DateTimeOffset AssignmentEndDate { get; set; }

        [JsonProperty("percentAllocation")]
        public long PercentAllocation { get; set; }

        [JsonProperty("projectRole")]
        public string ProjectRole { get; set; }

        [JsonProperty("projectStartDate")]
        public DateTimeOffset ProjectStartDate { get; set; }

        [JsonProperty("projectEndDate")]
        public DateTimeOffset ProjectEndDate { get; set; }
    }
}
