using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace StudentManagementSystem.Utilities
{
    public static class FilesIOHelper
    {
        public static byte[] ExporttoExcel<T>(IList<T> table, string fileName)
        {
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add(fileName);
            ws.Cells["A1"].LoadFromCollection(table , true, TableStyles.Light1);
            return package.GetAsByteArray();
        }
    }
}
