using System;
using System.Collections.Generic;
using System.Text;
using Estimation.Domain.Models;

namespace Estimation.Excel
{
    public class SummaryOfEstimationForm
    {
        private readonly SummaryOfEstimationNetForm _summaryOfEstimationNetForm;
        private readonly SummaryOfEstimationSubmitForm _summaryOfEstimationSubmitForm;
        private readonly SummaryOfEstimationDetailForm _summaryOfEstimationDetailForm;

        public SummaryOfEstimationForm()
        {
            _summaryOfEstimationNetForm = new SummaryOfEstimationNetForm();
            _summaryOfEstimationSubmitForm = new SummaryOfEstimationSubmitForm();
            _summaryOfEstimationDetailForm = new SummaryOfEstimationDetailForm();
        }

        public byte[] ExportToExcel(ProjectSummary projectSummary, ProjectExportRequest printOrder)
        {
            switch (printOrder.SubmitForm)
            {
                case SubmitForm.SubmitForm:
                    return _summaryOfEstimationSubmitForm.ExportToExcel(projectSummary);
                case SubmitForm.MaterialAndLabourCostForm:
                    return _summaryOfEstimationDetailForm.ExportToExcel(projectSummary);
                case SubmitForm.NetForm:
                    return _summaryOfEstimationNetForm.ExportToExcel(projectSummary);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
