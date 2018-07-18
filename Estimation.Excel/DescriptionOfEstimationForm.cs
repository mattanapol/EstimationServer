using System;
using System.Collections.Generic;
using System.Text;
using Estimation.Domain.Models;

namespace Estimation.Excel
{
    public class DescriptionOfEstimationForm
    {
        private readonly DescriptionOfEstimationNetForm _descriptionOfEstimationNetForm;
        private readonly DescriptionOfEstimationSubmitForm _descriptionOfEstimationSubmitForm;
        private readonly DescriptionOfEstimationDetailForm _descriptionOfEstimationDetailForm;

        public DescriptionOfEstimationForm()
        {
            _descriptionOfEstimationNetForm = new DescriptionOfEstimationNetForm();
            _descriptionOfEstimationSubmitForm = new DescriptionOfEstimationSubmitForm();
            _descriptionOfEstimationDetailForm = new DescriptionOfEstimationDetailForm();
        }

        public byte[] ExportToExcel(ProjectSummary projectSummary, ProjectExportRequest printOrder)
        {
            switch (printOrder.SubmitForm)
            {
                case SubmitForm.SubmitForm:
                    return _descriptionOfEstimationSubmitForm.ExportToExcel(projectSummary);
                case SubmitForm.MaterialAndLabourCostForm:
                    return _descriptionOfEstimationDetailForm.ExportToExcel(projectSummary);
                case SubmitForm.NetForm:
                    return _descriptionOfEstimationNetForm.ExportToExcel(projectSummary);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
