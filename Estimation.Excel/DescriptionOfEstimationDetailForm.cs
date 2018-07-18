using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Estimation.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class DescriptionOfEstimationDetailForm
    {
        protected virtual int TemplateRowNumber => 16;
        protected virtual string ExcelFormPath => @"Forms/DescriptionOfEstimation_DetailForm.xlsx";

        public byte[] ExportToExcel(ProjectSummary projectSummary)
        {
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(ExcelFormPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);
                var summarySheet = originalWorkbook.GetSheetAt(0);
                var projectNameRow = summarySheet.GetRow(1);
                var materialTypeTemplateRow = summarySheet.GetRow(5);
                var mainMaterialTemplateRow = summarySheet.GetRow(6);
                var materialTemplateRow = summarySheet.GetRow(7);
                var blankTemplateRow2 = summarySheet.GetRow(8);
                var supportingMaterialTemplateRow = summarySheet.GetRow(9);
                var paintingTemplateRow = summarySheet.GetRow(10);
                var miscellaneousTemplateRow = summarySheet.GetRow(11);
                var transportationTemplateRow = summarySheet.GetRow(12);
                var sumMainMaterialGroupTemplateRow = summarySheet.GetRow(13);
                var blankTemplateRow = summarySheet.GetRow(14);
                var sumMaterialTypeTemplateRow = summarySheet.GetRow(15);


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
                        contentMainMaterialRow.GetCell(9).ParseData(mainMaterialDataDict);

                        if (mainMaterial.Child?.FirstOrDefault()?.TargetClass == "sub-group-summary")
                        {
                            foreach (var subMaterial in mainMaterial.Child)
                            {
                                var subMaterialDataDict = subMaterial.GetDataDictionary();

                                var contentSubMaterialRow =
                                    mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                                contentSubMaterialRow.GetCell(0).ParseData(subMaterialDataDict);
                                contentSubMaterialRow.GetCell(9).ParseData(subMaterialDataDict);

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
                                transportationTemplateRow,
                                sumMainMaterialGroupTemplateRow,
                                blankTemplateRow);
                        }
                        //rowCount += ParseSubMaterial(originalWorkbook, summarySheet, mainMaterial, subMaterialRow);
                    }

                    var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                    sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                    sumMaterialTypeRow.GetCell(7).ParseData(materialTypeDataDict);
                    sumMaterialTypeRow.GetCell(8).ParseData(materialTypeDataDict);
                    summarySheet.Autobreaks = false;
                    summarySheet.SetRowBreak(sumMaterialTypeRow.RowNum);
                }

                summarySheet.RemoveAndShiftUp(materialTypeTemplateRow);
                summarySheet.RemoveAndShiftUp(mainMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(materialTemplateRow);
                summarySheet.RemoveAndShiftUp(supportingMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(paintingTemplateRow);
                summarySheet.RemoveAndShiftUp(miscellaneousTemplateRow);
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
            IRow miscellaneousTemplateRow,
            IRow transportationTemplateRow, IRow sumMainMaterialGroupTemplateRow, IRow blankTemplateRow)
        {
            var supportingMaterialRow =
                supportingMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet,
                    TemplateRowNumber + rowCount++);
            supportingMaterialRow.GetCell(5).ParseData(subMaterialDataDict);
            supportingMaterialRow.GetCell(8).ParseData(subMaterialDataDict);

            var paintingRow =
                paintingTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            paintingRow.GetCell(5).ParseData(subMaterialDataDict);
            paintingRow.GetCell(8).ParseData(subMaterialDataDict);

            var miscellaneousRow =
                miscellaneousTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            miscellaneousRow.GetCell(5).ParseData(subMaterialDataDict);
            miscellaneousRow.GetCell(8).ParseData(subMaterialDataDict);

            var transportationRow =
                transportationTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            transportationRow.GetCell(5).ParseData(subMaterialDataDict);
            transportationRow.GetCell(8).ParseData(subMaterialDataDict);

            var sumMainMaterialGroupRow =
                sumMainMaterialGroupTemplateRow.CopyRow(originalWorkbook, summarySheet,
                    TemplateRowNumber + rowCount++);
            sumMainMaterialGroupRow.GetCell(2).ParseData(subMaterialDataDict);
            sumMainMaterialGroupRow.GetCell(5).ParseData(subMaterialDataDict);
            sumMainMaterialGroupRow.GetCell(7).ParseData(subMaterialDataDict);
            sumMainMaterialGroupRow.GetCell(8).ParseData(subMaterialDataDict);

            blankTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
            return rowCount;
        }

        private static void ParseProjectSummary(ProjectSummary projectSummary, IRow projectNameRow)
        {
            var projectDataDictionary = projectSummary.GetDataDictionary();
            // Project Name Row
            projectNameRow.GetCell(0).ParseData(projectDataDictionary);
            projectNameRow.GetCell(9).ParseData(projectDataDictionary);
        }
    }
}
