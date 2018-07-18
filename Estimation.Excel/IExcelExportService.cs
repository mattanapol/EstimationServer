using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Excel
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel();
    }
}
