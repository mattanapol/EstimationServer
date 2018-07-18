using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Estimation.Domain;
using Estimation.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class DescriptionOfEstimationNetForm
    {
        protected virtual int TemplateRowNumber => 17;
        protected virtual string ExcelFormPath => @"Forms/DescriptionOfEstimation_NetForm.xlsx";

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
                var materialTemplateRow = summarySheet.GetRow(6);
                var blankTemplateRow2 = summarySheet.GetRow(7);
                var supportingMaterialTemplateRow = summarySheet.GetRow(8);
                var paintingTemplateRow = summarySheet.GetRow(9);
                var miscellaneousTemplateRow = summarySheet.GetRow(10);
                var subTotalTemplateRow = summarySheet.GetRow(11);
                var installationTemplateRow = summarySheet.GetRow(12);
                var transportationTemplateRow = summarySheet.GetRow(13);
                var sumMainMaterialGroupTemplateRow = summarySheet.GetRow(14);
                var blankTemplateRow = summarySheet.GetRow(15);
                var sumMaterialTypeTemplateRow = summarySheet.GetRow(16);
                

                ParseProjectSummary(projectSummary, projectNameRow);

                var materialTypeGroups = projectSummary.Child;
                int rowCount = 0;
                foreach (var materialTypeGroup in materialTypeGroups)
                {
                    var materialTypeDataDict = materialTypeGroup.GetDataDictionary();

                    var materialTypeRow = materialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                    materialTypeRow.GetCell(0).ParseData(materialTypeDataDict);
                    
                    foreach (var mainMaterial in materialTypeGroup.Child)
                    {
                        var mainMaterialDataDict = mainMaterial.GetDataDictionary();
                        var contentMainMaterialRow = mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                        contentMainMaterialRow.GetCell(0).ParseData(mainMaterialDataDict);
                        contentMainMaterialRow.GetCell(6).ParseData(mainMaterialDataDict);

                        if (mainMaterial.Child?.FirstOrDefault()?.TargetClass == "sub-group-summary")
                        {
                            foreach (var subMaterial in mainMaterial.Child)
                            {
                                var subMaterialDataDict = subMaterial.GetDataDictionary();

                                var contentSubMaterialRow =
                                    mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                                contentSubMaterialRow.GetCell(0).ParseData(subMaterialDataDict);
                                contentSubMaterialRow.GetCell(6).ParseData(subMaterialDataDict);

                                if (subMaterial.Child?.FirstOrDefault()?.TargetClass == "material")
                                {
                                    foreach (var material in subMaterial.Child)
                                    {
                                        var materialDataDict = material.GetDataDictionary();
                                        var materialRow = materialTemplateRow.CopyRow(originalWorkbook, summarySheet,
                                            TemplateRowNumber + rowCount++);
                                        materialRow.Cells.ForEach(c => c.ParseData(materialDataDict));
                                    }
                                }

                                rowCount = ParseDetailOfMaterialGroup(supportingMaterialTemplateRow,
                                    originalWorkbook,
                                    summarySheet,
                                    rowCount,
                                    subMaterialDataDict,
                                    paintingTemplateRow,
                                    miscellaneousTemplateRow,
                                    subTotalTemplateRow,
                                    installationTemplateRow,
                                    transportationTemplateRow,
                                    sumMainMaterialGroupTemplateRow,
                                    blankTemplateRow);
                            }
                        }
                        else if (mainMaterial.Child?.FirstOrDefault()?.TargetClass == "material")
                        {
                            foreach (var material in mainMaterial.Child)
                            {
                                var materialDataDict = material.GetDataDictionary();
                                var materialRow = materialTemplateRow.CopyRow(originalWorkbook, summarySheet,
                                    TemplateRowNumber + rowCount++);
                                materialRow.Cells.ForEach(c => c.ParseData(materialDataDict));
                            }

                            rowCount = ParseDetailOfMaterialGroup(supportingMaterialTemplateRow,
                                originalWorkbook,
                                summarySheet,
                                rowCount,
                                mainMaterialDataDict,
                                paintingTemplateRow,
                                miscellaneousTemplateRow,
                                subTotalTemplateRow,
                                installationTemplateRow,
                                transportationTemplateRow,
                                sumMainMaterialGroupTemplateRow,
                                blankTemplateRow);
                        }
                        //rowCount += ParseSubMaterial(originalWorkbook, summarySheet, mainMaterial, subMaterialRow);
                    }

                    var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                    sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                    sumMaterialTypeRow.GetCell(5).ParseData(materialTypeDataDict);
                    summarySheet.Autobreaks = false;
                    summarySheet.SetRowBreak(sumMaterialTypeRow.RowNum);
                }

                summarySheet.RemoveAndShiftUp(materialTypeTemplateRow);
                summarySheet.RemoveAndShiftUp(mainMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(materialTemplateRow);
                summarySheet.RemoveAndShiftUp(supportingMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(paintingTemplateRow);
                summarySheet.RemoveAndShiftUp(miscellaneousTemplateRow);
                summarySheet.RemoveAndShiftUp(subTotalTemplateRow);
                summarySheet.RemoveAndShiftUp(installationTemplateRow);
                summarySheet.RemoveAndShiftUp(transportationTemplateRow);
                summarySheet.RemoveAndShiftUp(sumMainMaterialGroupTemplateRow);
                summarySheet.RemoveAndShiftUp(blankTemplateRow);
                summarySheet.RemoveAndShiftUp(blankTemplateRow2);
                summarySheet.RemoveAndShiftUp(sumMaterialTypeTemplateRow);

                using (var newFileStream = new MemoryStream())
                {
                    originalWorkbook.Write(newFileStream);
                    excelBytes = newFileStream.ToArray();
                }
            }

            return excelBytes;
        }

        private int ParseDetailOfMaterialGroup(IRow supportingMaterialTemplateRow, IWorkbook originalWorkbook,
            ISheet summarySheet, int rowCount, Dictionary<string, string> subMaterialDataDict, IRow paintingTemplateRow,
            IRow miscellaneousTemplateRow, IRow subTotalTemplateRow, IRow installationTemplateRow,
            IRow transportationTemplateRow, IRow sumMainMaterialGroupTemplateRow, IRow blankTemplateRow)
        {
            var supportingMaterialRow =
                supportingMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet,
                    TemplateRowNumber + rowCount++);
            supportingMaterialRow.GetCell(4).ParseData(subMaterialDataDict);
            supportingMaterialRow.GetCell(5).ParseData(subMaterialDataDict);

            var paintingRow =
                paintingTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            paintingRow.GetCell(4).ParseData(subMaterialDataDict);
            paintingRow.GetCell(5).ParseData(subMaterialDataDict);

            var miscellaneousRow =
                miscellaneousTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            miscellaneousRow.GetCell(4).ParseData(subMaterialDataDict);
            miscellaneousRow.GetCell(5).ParseData(subMaterialDataDict);

            var subTotalRow =
                subTotalTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            subTotalRow.GetCell(5).ParseData(subMaterialDataDict);

            var installationRow =
                installationTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            installationRow.GetCell(4).ParseData(subMaterialDataDict);
            installationRow.GetCell(5).ParseData(subMaterialDataDict);

            var transportationRow =
                transportationTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            transportationRow.GetCell(4).ParseData(subMaterialDataDict);
            transportationRow.GetCell(5).ParseData(subMaterialDataDict);

            var sumMainMaterialGroupRow =
                sumMainMaterialGroupTemplateRow.CopyRow(originalWorkbook, summarySheet,
                    TemplateRowNumber + rowCount++);
            sumMainMaterialGroupRow.GetCell(2).ParseData(subMaterialDataDict);
            sumMainMaterialGroupRow.GetCell(5).ParseData(subMaterialDataDict);

            blankTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            return rowCount;
        }

        private static void ParseProjectSummary(ProjectSummary projectSummary, IRow projectNameRow)
        {
            var projectDataDictionary = projectSummary.GetDataDictionary();
            // Project Name Row
            projectNameRow.GetCell(0).ParseData(projectDataDictionary);
            projectNameRow.GetCell(6).ParseData(projectDataDictionary);
        }
    }
}
