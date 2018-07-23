using System.IO;
using Estimation.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class EstimationSubmitForm
    {
        protected virtual string ExcelFormPath => @"Forms/Estimation_SubmitForm.xlsx";
        private int DetailSheetIndex { get; set; } = 1;

        public byte[] ExportToExcel(ProjectSummary projectSummary, ProjectExportRequest printRequest)
        {
            byte[] excelBytes;

            using (var originalFileStream = new FileStream(ExcelFormPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook originalWorkbook = new XSSFWorkbook(originalFileStream);

                if (printRequest.SummaryReport)
                {
                    SummaryOfEstimationSubmitForm summaryOfEstimationSubmitForm = new SummaryOfEstimationSubmitForm();
                    summaryOfEstimationSubmitForm.ExportToExcel(projectSummary, originalWorkbook, originalWorkbook.GetSheetAt(0));
                }
                else
                {
                    originalWorkbook.RemoveSheetAt(0);
                    DetailSheetIndex--;
                }

                if (printRequest.DescriptionReport)
                {
                    DescriptionOfEstimationSubmitForm descriptionOfEstimationSubmitForm = new DescriptionOfEstimationSubmitForm();
                    descriptionOfEstimationSubmitForm.ExportToExcel(projectSummary, originalWorkbook, originalWorkbook.GetSheetAt(DetailSheetIndex));
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
