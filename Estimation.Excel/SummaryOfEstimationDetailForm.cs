using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Estimation.Domain;
using Estimation.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class SummaryOfEstimationDetailForm
    {
        protected virtual string ExcelFormPath => @"Forms/SummaryOfEstimation_DetailForm.xlsx";
        protected virtual int TemplateRowNumber => 10;

        public byte[] ExportToExcel(ProjectSummary projectSummary)
        {
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(ExcelFormPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);
                var summarySheet = originalWorkbook.GetSheetAt(0);
                var projectNameRow = summarySheet.GetRow(1);
                var materialTypeTemplateRow = summarySheet.GetRow(4);
                var mainMaterialTemplateRow = summarySheet.GetRow(5);
                var subMaterialTemplateRow = summarySheet.GetRow(6);
                var subTotalTemplateRow = summarySheet.GetRow(7);
                var sumMaterialTypeTemplateRow = summarySheet.GetRow(8);
                var blankTemplateRow = summarySheet.GetRow(9);
                var grandTotalRow = summarySheet.GetRow(10);
                
                ParseProjectSummary(projectSummary, projectNameRow, grandTotalRow);

                ParseContentsBody(projectSummary, materialTypeTemplateRow, originalWorkbook, summarySheet, mainMaterialTemplateRow, subMaterialTemplateRow, subTotalTemplateRow, sumMaterialTypeTemplateRow, blankTemplateRow);

                summarySheet.RemoveAndShiftUp(materialTypeTemplateRow);
                summarySheet.RemoveAndShiftUp(mainMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(subMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(subTotalTemplateRow);
                summarySheet.RemoveAndShiftUp(sumMaterialTypeTemplateRow);
                summarySheet.RemoveAndShiftUp(blankTemplateRow);

                using (var newFileStream = new MemoryStream())
                {
                    originalWorkbook.Write(newFileStream);
                    excelBytes = newFileStream.ToArray();
                }
            }

            return excelBytes;
        }

        private void ParseProjectSummary(ProjectSummary projectSummary, IRow projectNameRow,
            IRow grandTotalRow)
        {
            var projectDataDictionary = projectSummary.GetDataDictionary();
            // Project Name Row
            projectNameRow.GetCell(0).ParseData(projectDataDictionary);
            projectNameRow.GetCell(7).ParseData(projectDataDictionary);
            // Grand Total Row
            grandTotalRow.GetCell(4).ParseData(projectDataDictionary);
            grandTotalRow.GetCell(5).ParseData(projectDataDictionary);
            grandTotalRow.GetCell(6).ParseData(projectDataDictionary);
        }

        private void ParseContentsBody(ProjectSummary projectSummary, IRow materialTypeTemplateRow,
            IWorkbook originalWorkbook, ISheet summarySheet, IRow mainMaterialTemplateRow, IRow subMaterialTemplateRow,
            IRow subTotalTemplateRow, IRow sumMaterialTypeTemplateRow, IRow blankTemplateRow)
        {
            var materialTypeGroups = projectSummary.Child;
            int rowCount = 0;
            foreach (var materialTypeGroup in materialTypeGroups)
            {
                var materialTypeDataDict = materialTypeGroup.GetDataDictionary();
                var materialTypeRow = materialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                materialTypeRow.GetCell(1).ParseData(materialTypeDataDict);

                rowCount = ParseMainMaterial(originalWorkbook, summarySheet, mainMaterialTemplateRow, subMaterialTemplateRow, subTotalTemplateRow, materialTypeGroup, rowCount);

                var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                var blankRow = blankTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                
                sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(4).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(5).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(6).ParseData(materialTypeDataDict);

                
            }
        }

        private int ParseMainMaterial(IWorkbook originalWorkbook, ISheet summarySheet, IRow mainMaterialTemplateRow,
            IRow subMaterialTemplateRow, IRow subTotalTemplateRow, IPrintable materialTypeGroup, int rowCount)
        {
            var mainMaterials = materialTypeGroup.Child;
            foreach (var mainMaterial in mainMaterials)
            {
                var mainMaterialDataDict = mainMaterial.GetDataDictionary();
                var contentMainMaterialRow = mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                contentMainMaterialRow.GetCell(0).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(1).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(4).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(5).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(6).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(7).ParseData(mainMaterialDataDict);

                if (mainMaterial.Child?.FirstOrDefault()?.TargetClass != "sub-group-summary")
                    continue;

                rowCount = ParseSubMaterial(originalWorkbook, summarySheet, subMaterialTemplateRow, rowCount, mainMaterial);

                var contentSubTotalRow = subTotalTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);

                contentSubTotalRow.GetCell(4).ParseData(mainMaterialDataDict);
                contentSubTotalRow.GetCell(5).ParseData(mainMaterialDataDict);
                contentSubTotalRow.GetCell(6).ParseData(mainMaterialDataDict);

                //rowCount += ParseSubMaterial(originalWorkbook, summarySheet, mainMaterial, subMaterialRow);
            }

            return rowCount;
        }

        private int ParseSubMaterial(IWorkbook originalWorkbook, ISheet summarySheet, IRow subMaterialTemplateRow,
            int rowCount, IPrintable mainMaterial)
        {
            foreach (var subMaterial in mainMaterial.Child)
            {
                var subMaterialDataDict = subMaterial.GetDataDictionary();
                var contentSubMaterialRow = subMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);

                contentSubMaterialRow.GetCell(1).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(4).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(5).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(6).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(7).ParseData(subMaterialDataDict);
            }

            return rowCount;
        }
    }
}
