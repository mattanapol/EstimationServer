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
    public class SummaryOfEstimationNetForm
    {
        protected virtual int TemplateRowNumber => 9;

        public void ExportToExcel(ProjectSummary projectSummary, IWorkbook templateWorkbook, ISheet templateSheet)
        {
            var projectNameRow = templateSheet.GetRow(1);
            var materialTypeTemplateRow = templateSheet.GetRow(4);
            var mainMaterialTemplateRow = templateSheet.GetRow(5);
            var subMaterialTemplateRow = templateSheet.GetRow(6);
            var sumMaterialTypeTemplateRow = templateSheet.GetRow(7);
            var blankTemplateRow = templateSheet.GetRow(8);
            var grandTotalRow = templateSheet.GetRow(9);

            ParseProjectSummary(projectSummary, projectNameRow, grandTotalRow);

            ParseContentsBody(projectSummary, materialTypeTemplateRow, templateWorkbook, templateSheet, mainMaterialTemplateRow, subMaterialTemplateRow, sumMaterialTypeTemplateRow, blankTemplateRow);

            templateSheet.RemoveAndShiftUp(materialTypeTemplateRow);
            templateSheet.RemoveAndShiftUp(mainMaterialTemplateRow);
            templateSheet.RemoveAndShiftUp(subMaterialTemplateRow);
            templateSheet.RemoveAndShiftUp(sumMaterialTypeTemplateRow);
            templateSheet.RemoveAndShiftUp(blankTemplateRow);
        }

        private void ParseProjectSummary(ProjectSummary projectSummary, IRow projectNameRow,
            IRow grandTotalRow)
        {
            var projectDataDictionary = projectSummary.GetDataDictionary();
            // Project Name Row
            projectNameRow.GetCell(0).ParseData(projectDataDictionary);
            projectNameRow.GetCell(6).ParseData(projectDataDictionary);
            // Grand Total Row
            grandTotalRow.GetCell(5).ParseData(projectDataDictionary);
        }

        private void ParseContentsBody(ProjectSummary projectSummary, IRow materialTypeTemplateRow,
            IWorkbook originalWorkbook, ISheet summarySheet, IRow mainMaterialTemplateRow, IRow subMaterialTemplateRow,
            IRow sumMaterialTypeTemplateRow, IRow blankTemplateRow)
        {
            var materialTypeGroups = projectSummary.Child;
            int rowCount = 0;
            foreach (var materialTypeGroup in materialTypeGroups)
            {
                var materialTypeDataDict = materialTypeGroup.GetDataDictionary();
                var materialTypeRow = materialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                materialTypeRow.GetCell(1).ParseData(materialTypeDataDict);

                rowCount = ParseMainMaterial(originalWorkbook, summarySheet, mainMaterialTemplateRow, subMaterialTemplateRow, materialTypeGroup, rowCount);

                var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                var blankRow = blankTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);

                sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(5).ParseData(materialTypeDataDict);
            }
        }

        private int ParseMainMaterial(IWorkbook originalWorkbook, ISheet summarySheet,
            IRow mainMaterialTemplateRow,
            IRow subMaterialTemplateRow, IPrintable materialTypeGroup, int rowCount)
        {
            var mainMaterials = materialTypeGroup.Child;
            foreach (var mainMaterial in mainMaterials)
            {
                var mainMaterialDataDict = mainMaterial.GetDataDictionary();
                var contentMainMaterialRow = mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, TemplateRowNumber + rowCount++);
                contentMainMaterialRow.GetCell(0).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(1).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(5).ParseData(mainMaterialDataDict);
                contentMainMaterialRow.GetCell(6).ParseData(mainMaterialDataDict);

                if (mainMaterial.Child?.FirstOrDefault()?.TargetClass != "sub-group-summary")
                    continue;

                rowCount = ParseSubMaterial(originalWorkbook, summarySheet, subMaterialTemplateRow, rowCount, mainMaterial);
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
                contentSubMaterialRow.GetCell(6).ParseData(subMaterialDataDict);
            }

            return rowCount;
        }
    }
}
