using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Kaewsai.Excel
{
    public class ExcelTest
    {
        public static byte[] TestCreateExcel()
        {
            // Temp
            Dictionary<string, string> DataDict = new Dictionary<string, string>()
            {
                { "##ProjectName##", "Example Project" }
            };

            var newFile = @"Forms/SummaryOfEstimation_DetailForm.xlsx";
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(newFile, FileMode.Open, FileAccess.Read))
            {

                using (var newFileStream = new MemoryStream())
                {
                    IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);
                    var summarySheet = originalWorkbook.GetSheetAt(0);
                    var projectNameRow = summarySheet.GetRow(1);
                    var materialTypeRow = summarySheet.GetRow(4);
                    var mainMaterialRow = summarySheet.GetRow(5);
                    var subMaterialRow = summarySheet.GetRow(6);
                    var subTotalRow = summarySheet.GetRow(7);
                    var sumMaterialTypeRow = summarySheet.GetRow(8);
                    var grandTotalRow = summarySheet.GetRow(10);
                    // Project Name Row
                    ParseCell(projectNameRow.GetCell(0), DataDict);
                    ParseCell(projectNameRow.GetCell(7), DataDict);

                    //ISheet sheet1 = originalWorkbook.CreateSheet("Sheet1");

                    //sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                    //var rowIndex = 0;
                    //IRow row = sheet1.CreateRow(rowIndex);
                    //row.Height = 30 * 80;
                    //row.CreateCell(0).SetCellValue("this is content");
                    //sheet1.AutoSizeColumn(0);
                    //rowIndex++;

                    //var sheet2 = originalWorkbook.CreateSheet("Sheet2");
                    //var style1 = originalWorkbook.CreateCellStyle();
                    //style1.FillForegroundColor = HSSFColor.Blue.Index2;
                    //style1.FillPattern = FillPattern.SolidForeground;

                    //var style2 = originalWorkbook.CreateCellStyle();
                    //style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                    //style2.FillPattern = FillPattern.SolidForeground;

                    //var cell2 = sheet2.CreateRow(0).CreateCell(0);
                    //cell2.CellStyle = style1;
                    //cell2.SetCellValue(0);

                    //cell2 = sheet2.CreateRow(1).CreateCell(0);
                    //cell2.CellStyle = style2;
                    //cell2.SetCellValue(1);

                    originalWorkbook.Write(newFileStream);
                    excelBytes = newFileStream.ToArray();
                }
            }

            return excelBytes;
        }

        private static void ParseCell(ICell cell, Dictionary<string, string> dataList)
        {
            foreach (var data in dataList)
            {
                cell.SetCellValue(cell.StringCellValue
                    .Replace(data.Key, data.Value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
