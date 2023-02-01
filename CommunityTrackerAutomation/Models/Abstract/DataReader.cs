using CognizantSoftvision.Maqs.BaseSeleniumTest;
using ExcelMapper;
using Models.Sheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Abstract
{
    public class DataReader
    {
        /// <summary>
        /// Allocation Mock Data File Reader
        /// </summary>
        private static ExcelSheet sheet;
        public static List<EmployeeModel> ReadExcelFile()
        {

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;

            var stream = File.OpenRead(Path.Combine(projectDirectory + @"\Models\AllocationFile\", "Allocation Mock Data.xlsx"));
            var importer = new ExcelImporter(stream);

            sheet = importer.ReadSheet();
            return sheet.ReadRows<EmployeeModel>().ToList();
        }
        /*/// <summary>
        /// This is for DynamicData attribute
        /// </summary>
        /// <returns>IEnumberable array of object</returns>
        public static IEnumerable<object[]> LoadExcelViaDataRow
        {
            get
            {
                return new[]
                {
                    ReadExcelFile().ToArray<object>()
                };
            }
        }*/
    }
}
