using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;

namespace Estimation.Excel
{
    public class CellParser
    {
        private const string EnclosingString = "##";

        public static void ParseCell(ICell cell, Dictionary<string, string> dataList)
        {
            foreach (var data in dataList)
            {
                cell.SetCellValue(cell.StringCellValue
                    .Replace($"{EnclosingString}{data.Key}{EnclosingString}", data.Value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
