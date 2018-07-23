using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Estimation.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class EstimationNetForm
    {
        protected virtual string ExcelFormPath => @"Forms/Estimation_NetForm.xlsx";
        private int DetailSheetIndex { get; set; } = 1;

        public byte[] ExportToExcel(ProjectSummary projectSummary, ProjectExportRequest printRequest)
        {
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(ExcelFormPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);

                if (printRequest.SummaryReport)
                {
                    SummaryOfEstimationNetForm summaryOfEstimationNetForm = new SummaryOfEstimationNetForm();
                    summaryOfEstimationNetForm.ExportToExcel(projectSummary, originalWorkbook, originalWorkbook.GetSheetAt(0));
                }
                else
                {
                    originalWorkbook.RemoveSheetAt(0);
                    DetailSheetIndex--;
                }

                if (printRequest.DescriptionReport)
                {
                    DescriptionOfEstimationNetForm descriptionOfEstimationNetForm = new DescriptionOfEstimationNetForm();
                    descriptionOfEstimationNetForm.ExportToExcel(projectSummary, originalWorkbook, originalWorkbook.GetSheetAt(DetailSheetIndex));
                }
                else
                {
                    originalWorkbook.RemoveSheetAt(DetailSheetIndex);
                }



                using (var newFileStream = new MemoryStream())
                {
                    originalWorkbook.Write(newFileStream);
                    excelBytes = newFileStream.ToArray();
                }
            }

            return excelBytes;
        }
    }
}
