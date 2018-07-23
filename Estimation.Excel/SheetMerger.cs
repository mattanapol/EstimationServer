using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Estimation.Excel
{
    public class SheetMerger
    {
        public static byte[] Merge(List<byte[]> workbooksAsByte)
        {
            IWorkbook mergedWorkbook = new XSSFWorkbook();
            foreach (var workbookAsByteArray in workbooksAsByte)
            {
                using (Stream stream = new MemoryStream(workbookAsByteArray))
                {
                    IWorkbook mergingWorkbook = new XSSFWorkbook(stream);
                    for (int i = 0; i < mergingWorkbook.NumberOfSheets; i++)
                    {
                        var originalSheet = mergingWorkbook.GetSheetAt(i);
                        var newSheet = mergedWorkbook.CreateSheet(originalSheet.SheetName);
                        foreach (IRow row in originalSheet)
                        {
                            row.CopyRow(mergedWorkbook, newSheet, row.RowNum);
                        }
                    }
                }
            }

            byte[] excelBytes;
            using (var newFileStream = new MemoryStream())
            {
                mergedWorkbook.Write(newFileStream);
                excelBytes = newFileStream.ToArray();
            }

            return excelBytes;
        }
    }
}
