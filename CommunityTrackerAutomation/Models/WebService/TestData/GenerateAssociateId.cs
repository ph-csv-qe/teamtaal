using Models.WebService.WebServiceModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebService.TestData
{
    public class GenerateAssociateId
    {
        public static AssociateIdPageModel GetAssociateId()
        {
            return new AssociateIdPageModel()
            {
                AssociateId = 2019213,
                AssociateName = "Rojas,Francis Tolentino",
                AssignedProjects = new string[] { "Dell In-On-Line Build Out-2022" }
            };
        }
    }
}
