﻿using System;
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
        private static readonly string _excelFormPath = @"Forms/SummaryOfEstimation_NetForm.xlsx";

        public static byte[] ExportToExcel(ProjectSummary projectSummary)
        {
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(_excelFormPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);
                var summarySheet = originalWorkbook.GetSheetAt(0);
                var projectNameRow = summarySheet.GetRow(1);
                var materialTypeTemplateRow = summarySheet.GetRow(4);
                var mainMaterialTemplateRow = summarySheet.GetRow(5);
                var subMaterialTemplateRow = summarySheet.GetRow(6);
                var sumMaterialTypeTemplateRow = summarySheet.GetRow(7);
                var blankTemplateRow = summarySheet.GetRow(8);
                var grandTotalRow = summarySheet.GetRow(9);

                ParseProjectSummary(projectSummary, projectNameRow, grandTotalRow);

                ParseContentsBody(projectSummary, materialTypeTemplateRow, originalWorkbook, summarySheet, mainMaterialTemplateRow, subMaterialTemplateRow, sumMaterialTypeTemplateRow, blankTemplateRow);

                summarySheet.RemoveAndShiftUp(materialTypeTemplateRow);
                summarySheet.RemoveAndShiftUp(mainMaterialTemplateRow);
                summarySheet.RemoveAndShiftUp(subMaterialTemplateRow);
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

        private static void ParseProjectSummary(ProjectSummary projectSummary, IRow projectNameRow,
            IRow grandTotalRow)
        {
            var projectDataDictionary = projectSummary.GetDataDictionary();
            // Project Name Row
            projectNameRow.GetCell(0).ParseData(projectDataDictionary);
            projectNameRow.GetCell(6).ParseData(projectDataDictionary);
            // Grand Total Row
            grandTotalRow.GetCell(5).ParseData(projectDataDictionary);
        }

        private static void ParseContentsBody(ProjectSummary projectSummary, IRow materialTypeTemplateRow,
            IWorkbook originalWorkbook, ISheet summarySheet, IRow mainMaterialTemplateRow, IRow subMaterialTemplateRow,
            IRow sumMaterialTypeTemplateRow, IRow blankTemplateRow)
        {
            var materialTypeGroups = projectSummary.Child;
            int rowCount = 0;
            foreach (var materialTypeGroup in materialTypeGroups)
            {
                var materialTypeDataDict = materialTypeGroup.GetDataDictionary();
                var materialTypeRow = materialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, 9 + rowCount++);
                materialTypeRow.GetCell(1).ParseData(materialTypeDataDict);

                rowCount = ParseMainMaterial(originalWorkbook, summarySheet, mainMaterialTemplateRow, subMaterialTemplateRow, materialTypeGroup, rowCount);

                var sumMaterialTypeRow = sumMaterialTypeTemplateRow.CopyRow(originalWorkbook, summarySheet, 9 + rowCount++);
                var blankRow = blankTemplateRow.CopyRow(originalWorkbook, summarySheet, 9 + rowCount++);

                sumMaterialTypeRow.GetCell(2).ParseData(materialTypeDataDict);
                sumMaterialTypeRow.GetCell(5).ParseData(materialTypeDataDict);
            }
        }

        private static int ParseMainMaterial(IWorkbook originalWorkbook, ISheet summarySheet,
            IRow mainMaterialTemplateRow,
            IRow subMaterialTemplateRow, IPrintable materialTypeGroup, int rowCount)
        {
            var mainMaterials = materialTypeGroup.Child;
            foreach (var mainMaterial in mainMaterials)
            {
                var mainMaterialDataDict = mainMaterial.GetDataDictionary();
                var contentMainMaterialRow = mainMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, 9 + rowCount++);
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

        private static int ParseSubMaterial(IWorkbook originalWorkbook, ISheet summarySheet, IRow subMaterialTemplateRow,
            int rowCount, IPrintable mainMaterial)
        {
            foreach (var subMaterial in mainMaterial.Child)
            {
                var subMaterialDataDict = subMaterial.GetDataDictionary();
                var contentSubMaterialRow = subMaterialTemplateRow.CopyRow(originalWorkbook, summarySheet, 9 + rowCount++);

                contentSubMaterialRow.GetCell(1).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(4).ParseData(subMaterialDataDict);
                contentSubMaterialRow.GetCell(6).ParseData(subMaterialDataDict);
            }

            return rowCount;
        }
    }
}