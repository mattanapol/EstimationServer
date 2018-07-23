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

        public void ExportToExcel(ProjectSummary projectSummary, IWorkbook templateWorkbook, ISheet templateSheet)
        {
            var projectNameRow = templateSheet.GetRow(1);
            var materialTypeTemplateRow = templateSheet.GetRow(5);
            var mainMaterialTemplateRow = templateSheet.GetRow(6);
            var materialTemplateRow = templateSheet.GetRow(7);
            var blankTemplateRow2 = templateSheet.GetRow(8);
            var supportingMaterialTemplateRow = templateSheet.GetRow(9);
            var paintingTemplateRow = templateSheet.GetRow(10);
            var miscellaneousTemplateRow = templateSheet.GetRow(11);
            var transportationTemplateRow = templateSheet.GetRow(12);
            var sumMainMaterialGroupTemplateRow = templateSheet.GetRow(13);
            var blankTemplateRow = templateSheet.GetRow(14);
            var sumMaterialTypeTemplateRow = templateSheet.GetRow(15);


            ParseProjectSummary(projectSummary, projectNameRow);

            var materialTypeGroups = projectSummary.Child;
            int rowCount = 0;
            foreach (var materialTypeGroup in materialTypeGroups)
            {
                var materialTypeDataDict = materialTypeGroup.GetDataDictionary();

                var materialTypeRow = materialTypeTemplateRow.CopyRow(templateWorkbook, templateSheet, TemplateRowNumber + rowCount++);
                materialTypeRow.GetCell(0).ParseData(materialTypeDataDict);

                foreach (var mainMaterial in materialTypeGroup.Child)
                {
                    var mainMaterialDataDict = mainMaterial.GetDataDictionary();
                    var contentMainMaterialRow = mainMaterialTemplateRow.CopyRow(templateWorkbook, templateSheet, TemplateRowNumber + rowCount++);
                    contentMainMaterialRow.GetCell(0).ParseData(mainMaterialDataDict);
                    contentMainMaterialRow.GetCell(9).ParseData(mainMaterialDataDict);

                    if (mainMaterial.Child?.FirstOrDefault()?.TargetClass == "sub-group-summary")
                    {
                        foreach (var subMaterial in mainMaterial.Child)
                        {
                            var subMaterialDataDict = subMaterial.GetDataDictionary();

                            var contentSubMaterialRow =
                                mainMaterialTemplateRow.CopyRow(templateWorkbook, templateSheet, TemplateRowNumber + rowCount++);
                            contentSubMaterialRow.GetCell(0).ParseData(subMaterialDataDict);
                            contentSubMaterialRow.GetCell(9).ParseData(subMaterialDataDict);

                            if (subMaterial.Child?.FirstOrDefault()?.TargetClass == "material")
                            {
                                foreach (var material in subMaterial.Child)
                                {
                                    var materialDataDict = material.GetDataDictionary();
                                    var materialRow = materialTemplateRow.CopyRow(templateWorkbook, templateSheet,
                                        TemplateRowNumber + rowCount++);
                                    materialRow.Cells.ForEach(c => c.ParseData(materialDataDict));
                                }
                            }

                            rowCount = ParseDetailOfMaterialGroup(supportingMaterialTemplateRow,
                                templateWorkbook,
                                templateSheet,
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
                            var materialRow = materialTemplateRow.CopyRow(templateWorkbook, templateSheet,
                                TemplateRowNumber + rowCount++);
                            materialRow.Cells.ForEach(c => c.ParseData(materialDataDict));
                        }

                        rowCount = ParseDetailOfMaterialGroup(supportingMaterialTemplateRow,
                            templateWorkbook,
                            templateSheet,
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

                var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(templateWorkbook, templateSheet, TemplateRowNumber + rowCount++);
                sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(7).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(8).ParseData(materialTypeDataDict);
                templateSheet.Autobreaks = false;
                templateSheet.SetRowBreak(sumMaterialTypeRow.RowNum);
            }

            templateSheet.RemoveAndShiftUp(materialTypeTemplateRow);
            templateSheet.RemoveAndShiftUp(mainMaterialTemplateRow);
            templateSheet.RemoveAndShiftUp(materialTemplateRow);
            templateSheet.RemoveAndShiftUp(supportingMaterialTemplateRow);
            templateSheet.RemoveAndShiftUp(paintingTemplateRow);
            templateSheet.RemoveAndShiftUp(miscellaneousTemplateRow);
            templateSheet.RemoveAndShiftUp(transportationTemplateRow);
            templateSheet.RemoveAndShiftUp(sumMainMaterialGroupTemplateRow);
            templateSheet.RemoveAndShiftUp(blankTemplateRow);
            templateSheet.RemoveAndShiftUp(blankTemplateRow2);
            templateSheet.RemoveAndShiftUp(sumMaterialTypeTemplateRow);
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
