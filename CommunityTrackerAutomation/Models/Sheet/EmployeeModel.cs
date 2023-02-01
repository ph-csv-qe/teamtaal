using ExcelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Sheet
{
    public class EmployeeModel
    {
        [ExcelColumnName("AssociateID")]
        public int? AssociateID { get; set; }

        [ExcelColumnName("Associate Name")]
        public string Name { get; set; }

        [ExcelColumnName("Project Description")]
        public string Project { get; set; }

        [ExcelColumnName("Designation")]
        public string Designation { get; set; }

        [ExcelColumnName("DOJ")]
        public string HireDate { get; set; }

    }
}
