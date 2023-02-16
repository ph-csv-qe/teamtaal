using Models.WebService.WebServiceModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebService.TestData
{
    public class GenerateProjectDetails
    {
        public static ProjectPageModel GetProjectDetails()
        {
            return new ProjectPageModel()
            {
                ProjectId = 1,
                ProjectDescription = "Desc",
                ProjectBillability = "Desc",
                ProjectType = "Desc",
                ProjectManagerId = 2019213,
                AccountId = 1,
                ProjectOwningPractice = "Desc",
                AssociateProjectDetails = "",
                ProjectManager = "",
                Account = ""
            };
        }
    }
}
