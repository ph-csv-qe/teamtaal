using Models.WebServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityTrackerAPI.Tests.TestData
{
    public class GenerateAssociate
    {
        public static AssociateIdPageModel getAssociate()
        {
            return new AssociateIdPageModel
            {
                AssociateId = 2019213,
                AssociateName = "Rojas,Francis Tolentino",
                AssignedProjects = new string[] { "Dell In-On-Line Build Out-2022" }
            };
        }
    }
}
