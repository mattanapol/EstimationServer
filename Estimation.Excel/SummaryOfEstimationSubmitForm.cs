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
    public class SummaryOfEstimationSubmitForm: SummaryOfEstimationNetForm
    {
        protected override int TemplateRowNumber => 9;
    }
}
