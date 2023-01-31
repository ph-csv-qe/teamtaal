using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models.Abstract
{
    public class ExcelFileReader
    {
        public int SheetIndex { get; set; }

        private readonly string filePath;

        private IWorkbook workbook;

        private DataTable data;

        public ExcelFileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public ExcelFileReader(string filePath, int index)
        {
            this.filePath = filePath;
            SheetIndex = index;
        }

        public DataTable ReadExcelFile()
        {
            try
            {
                /*
                 *  Open and read the excel file
                 */

                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }

                // Get the specified sheet
                var sheet = workbook.GetSheetAt(SheetIndex);

                // Instantiate the datatable and set the table name using the sheetname from Excel
                data = new DataTable(sheet.SheetName);

                for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);

                    if (row == null) continue;

                    /*
                     * Get the value from each column on current row
                     * Ignoring the Excel CellType Formula
                     */

                    var columns = row.Select(c =>
                    {
                        if (c.CellType != CellType.Formula) return c.ToString();

                        return c.StringCellValue;
                    }).Where(c => !string.IsNullOrEmpty(c)).ToList();

                    /*
                     * Set the column names on DataTable
                     */

                    if (rowIndex == 0)
                    {
                        var newColumns = columns.Select(d => d).Where(d => !string.IsNullOrWhiteSpace(d)).ToList();
                        newColumns.ForEach(d => data.Columns.Add(d));
                        continue;
                    }

                    /*
                     * First row is treated as column name
                     * Add the data starting from 2nd row to datatable
                     */

                    DataRow tableRow = data.NewRow();
                    tableRow.ItemArray = columns.ToArray();
                    data.Rows.Add(tableRow);
                }
            }
            catch (FileNotFoundException fileEx) { throw fileEx; }
            catch (IOException ioEx) { throw ioEx; }
            catch (Exception ex) { throw ex; }

            return data;
        }

        public List<T> MapTo<T>(DataTable data) where T : new()
        {
            var props = typeof(T).GetProperties().ToList();
            var objects = new List<T>();

            // Iterating on each row to get the values

            foreach (DataRow row in data.Rows)
            {
                // Instantiate the generic class
                T cls = new T();

                foreach (DataColumn col in data.Columns)
                {
                    // Get the value from each column on current row
                    string value = row[col.ColumnName].ToString();

                    /*
                     * Set the value of the property by comparing the column name and property name
                     * Equivalent to employee.Name = "example";
                     */
                    SetPropertiesValue(props, cls, value, col.ColumnName);
                }

                objects.Add(cls);
            }

            return objects;
        }

        private void SetPropertiesValue(List<PropertyInfo> props, object cls, string value, string columnName)
        {
            // Iterating to each property of the class/object to find the property to set the value
            foreach (PropertyInfo prop in props)
            {
                if (String.IsNullOrWhiteSpace(value)) continue;

                if (String.Compare(columnName, prop.Name, true) != 0) continue;

                // Setting the value of property
                prop.SetValue(cls, Convert.ChangeType(value, prop.PropertyType));
            }
        }
    }
}
