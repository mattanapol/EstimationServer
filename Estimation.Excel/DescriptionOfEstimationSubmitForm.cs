using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Excel
{
    public class DescriptionOfEstimationSubmitForm: DescriptionOfEstimationNetForm
    {
        protected override int TemplateRowNumber => 17;
        protected override string ExcelFormPath => @"Forms/DescriptionOfEstimation_SubmitForm.xlsx";
    }
}
